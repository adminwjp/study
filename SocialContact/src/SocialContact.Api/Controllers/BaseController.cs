using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialContact.Domain.Core;
using SocialContact.Domain.Interfaces;
using SocialContact.Domain.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NHibernate.Linq;
using SocialContact.Api.Data;
using SocialContact.Api.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Criterion;
using SocialContact.Api.Models;
using Utility;
using System.Xml.Serialization;
using Utility.Redis;
using Utility.Domain.Uow;
using Utility.Response;
using Utility.Enums;
using Utility.Json.Extensions;
using Utility.Json;
using Utility.Base64;
using Utility.Validate;
using Utility.Page;
using Utility.Model;
using Utility.ObjectMapping;
using Utility.Randoms;
using Utility.Security.Extensions;

namespace SocialContact.Api.Controllers
{
    /// <summary>
    /// 后台基类 [FromForm,FromBody]  FromForm模型绑定优先级最高  绑定失败 ,[FromBody] 可以省略 手动绑定,FromForm 必须存在 不然 FromForm请求通过不了
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="F"></typeparam>
    /// <typeparam name="G"></typeparam>
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ProducesResponseType(typeof(ResponseApi), StatusCodes.Status200OK)]
    public  class BaseController<T,F,G> : ControllerBase where T:Entry,new() where F : QueryEntry where G : Entry,new() 
    {
        protected ILogger<BaseController<T,F, G>> Logger;
        protected IUnitWork UnitWork;
        protected IRedisCache RedisCache;
        protected IConfiguration Configuration;
        protected IMemoryCache Cache;
        protected IObjectMapper ObjectMapper;
        protected string PageName = string.Empty;
        protected bool IsCustomValidator { get; set; }
        /// <summary>
        /// 测试用的
        /// </summary>
        protected bool Test { get
            {
                var admin = GetAdminInfo();
                return admin == null;
            }
        }
        protected AuthrizeValidator AuthrizeValidator;
        public BaseController(IRedisCache redisCache, IUnitWork unitWork, IMemoryCache cache, ILogger<BaseController<T, F, G>> logger)
        {
            this.RedisCache = redisCache;
            this.UnitWork = unitWork;
            this.Cache = cache;
            this.Logger = logger;
        }
        public BaseController(IRedisCache redisCache, IUnitWork unitWork, ILogger<BaseController<T, F, G>> logger)
        {
            this.RedisCache = redisCache;
            this.UnitWork = unitWork;
            this.Logger = logger;
        }
        public BaseController(IRedisCache redisCache, IUnitWork unitWork, IMemoryCache cache, AuthrizeValidator authrize, ILogger<BaseController<T, F, G>> logger) : this(redisCache, unitWork, cache, logger)
        {
            this.AuthrizeValidator = authrize;
        }
        public BaseController(IRedisCache redisCache, IUnitWork unitWork,  AuthrizeValidator authrize, ILogger<BaseController<T, F, G>> logger) : this(redisCache, unitWork, logger)
        {
            this.AuthrizeValidator = authrize;
        }
        public BaseController(IRedisCache redisCache, IUnitWork unitWork, IConfiguration configuration, IMemoryCache cache,  ILogger<BaseController<T, F, G>> logger) : this(redisCache, unitWork, cache, logger)
        {
            this.Configuration = configuration;
        }
        public BaseController(IRedisCache redisCache, IUnitWork unitWork, IConfiguration configuration, IMemoryCache cache, AuthrizeValidator authrize, ILogger<BaseController<T, F, G>> logger):this(redisCache,unitWork,cache,authrize,logger)
        {
            this.Configuration = configuration;
        }
        protected virtual AdminInfo GetAdminInfo()
        {
            string key = HttpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(key)) return null;//throw new AuthNotFoundException(ResponseApi.Create((GetLanguage(), Code.AuthFail).Message);
            //string value = key.AesDecrypt(Core.AesKey, Core.AesIv);
            //string account = value.Split('_')[0];
            var data = this.Cache.Get<AdminInfo>(key);
            if (data == null)
            {
				var tokenStr = RedisCache.GetString(key);
                if (string.IsNullOrEmpty(tokenStr))
                {
                 throw new AuthNotFoundException(ResponseApi.Create(GetLanguage(), Code.AuthFail).Message);
                }
                data = tokenStr.ToObject<AdminInfo>();
                if (data.LoginDate.Value.AddHours(24) < DateTime.Now.AddMinutes(-5))
                {  
				    throw new AuthNotFoundException(ResponseApi.Create(GetLanguage(), Code.TokenExpires).Message);
                }
                Cache.Set<AdminInfo>(key, data, data.LoginDate.Value.AddHours(24));
            }
            return data;
        }
        protected virtual int GetUserId()
        {
            return 0;
        }
        protected  void SetAdmin<H>(H obj)where H : IAdmin
        {
            if (Test)
            {
                obj.Admin = obj.Admin == null || !obj.Admin.Id.HasValue ? null : obj.Admin;
            }
            else
            {
                obj.Admin = GetAdminInfo();
            }
        }
        protected void SetParent<H>(H obj) where H : ICasecade<H>
        {
            obj.Parent = obj.Parent == null || !obj.Parent.Id.HasValue ? obj : obj.Parent;
        }
        [HttpPost("add")]
        public virtual async Task<ResponseApi> Add([FromForm,FromBody]T obj)
        {
			if (Request.ContentType.Contains("application/json"))
            {	
				using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                {
					// Ref(ref obj, reader.ReadToEnd());
                    Ref(ref obj, reader.ReadToEndAsync().Result);//类库影响
                }
            }
            else if (Request.ContentType.Contains("text/xml"))
            {		
                using System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body);
                Type t = typeof(T);
                XmlSerializer serializer = new XmlSerializer(t);
                obj = serializer.Deserialize(reader) as T;
            }
            //this.ActionParam(HttpContext.Request,ref obj);//无效 作用域可能 绑定模型失败
            //ActionParam(HttpContext.Request,ref obj);
            var validate = await this.Validate(obj, 1);
            if (validate != null)
            {
                return validate;
            }
            else
            {
                return await AddMiddlewareExecuted(obj);
            }
        }
        protected Language GetLanguage()
        {
            return Language.Chinese;
        }
        protected virtual async Task<ResponseApi> AddMiddlewareExecuted(T obj)
        {
            obj = this.AddMiddlewareExecute(obj);
            obj.CreateDate = DateTime.Now;
            this.UnitWork.Insert(obj);
            ResponseApi response = ResponseApi.Create(GetLanguage(), Code.AddSuccess);
            return await Task.FromResult(response);
        }
        /// <summary>
        /// 添加 数据之前绑定数据到对象上
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>

        protected virtual  T AddMiddlewareExecute(T obj)
        {
            obj.CreateDate = DateTime.Now;
            return obj;
        }
        
        /// <summary>
        /// 修改 数据之前绑定数据到对象上
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual T EditMiddlewareExecute(T obj)
        {
            obj.UpdateDate = DateTime.Now;
            return obj;
        }
        protected virtual async Task<ResponseApi> EditMiddlewareExecuted(T obj)
        {
            this.EditMiddlewareExecute(obj);
            obj.UpdateDate = DateTime.Now;
            var oldObj = this.UnitWork.FindSingle<T>(it => it.Id == obj.Id);
            obj = this.UpdateOldObject(obj, ref oldObj);
            UnitWork.Update(obj);
            ResponseApi response = ResponseApi.Create(GetLanguage(), Code.ModifySuccess);
            return await Task.FromResult(response);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("edit")]
        public virtual async Task<ResponseApi> Edit([FromForm, FromBody]T obj)
        {
			if (Request.ContentType.Contains("application/json"))
            {	
				using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                {
					// Ref(ref obj, reader.ReadToEnd());
                    Ref(ref obj, reader.ReadToEndAsync().Result);//类库影响
                }
            }
            else if (Request.ContentType.Contains("text/xml"))
            {		
                using System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body);
                Type t = typeof(T);
                XmlSerializer serializer = new XmlSerializer(t);
                obj = serializer.Deserialize(reader) as T;
            }
            //this.ActionParam(HttpContext.Request,ref obj);//无效 作用域可能 绑定模型失败
           //ActionParam(HttpContext.Request,ref obj);
            var validate = await this.Validate(obj,3);
            if (validate != null)
            {
                return validate;
            }
            else
            {
                return await EditMiddlewareExecuted(obj);
            }
        }

        /// <summary>
        /// 修改数据 有些数据更新了
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="oldObj"></param>
        /// <returns></returns>
        protected virtual T UpdateOldObject(T newObj,ref T oldObj)
        {
            newObj.CreateDate = oldObj.CreateDate;
            return newObj;
        }
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("delete/{id}")]
        public virtual async Task<ResponseApi> Delete(int? id)
        {
            if (!id.HasValue)
            {
                ResponseApi response = ResponseApi.Create(GetLanguage(), Code.ParamNotNull,false);
                response.Message = $"id {response.Message}";
                return await Task.FromResult(response);
            }
            else
            {
                this.DeleteBefore(id.Value);
                this.UnitWork.Delete<T>(it => it.Id == id);
                this.DeleteAfter(id.Value);
                ResponseApi response = ResponseApi.Create(GetLanguage(), Code.DeleteSuccess);
                return await Task.FromResult(response);
            }
        }
        /// <summary>
        /// 删除之前操作  默认未实现
        /// </summary>
        /// <param name="id"></param>
        protected virtual  void DeleteBefore(int id)
        {

        }
        /// <summary>
        /// 删除之后操作  默认未实现
        /// </summary>
        /// <param name="id"></param>
        protected virtual void DeleteAfter(int id)
        {

        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="query"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpPost("query")]
        public virtual async Task<ResponseApi> Query([FromForm,FromBody]F query, int? page, int? size)
        {
			PageHelper.Set(ref page,ref size);
            //Request.EnableBuffering();
           	if (Request.ContentType.Contains("application/json"))
            {	
				using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                {
					// Ref(ref query, reader.ReadToEnd());
                    Ref(ref query, reader.ReadToEndAsync().Result);//类库影响
                }
            }
            else if (Request.ContentType.Contains("text/xml"))
            {		
                using System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body);
                Type t = typeof(F);
                XmlSerializer serializer = new XmlSerializer(t);
                query = serializer.Deserialize(reader) as F;
            }
            //this.ActionParam(HttpContext.Request,ref query);//无效 作用域可能 绑定模型失败
		   // ActionParam(HttpContext.Request,ref query);
            return await DefaultQuery(query,page,size);
        }
        protected virtual async Task<ResponseApi> DefaultQuery(F query, int? page, int? size)
        {
            IQueryable<T> queryable = this.Query();
            //树形列表查询 无条件则可以 树形列表查询 有条件取消
            List<NHibernate.Criterion.AbstractCriterion> wheres = new List<NHibernate.Criterion.AbstractCriterion>();
            List<NHibernate.Criterion.AbstractCriterion> ands = new List<NHibernate.Criterion.AbstractCriterion>();
            bool res = this.QueryFilterByOr(ref wheres, query);
            bool andRes = this.QueryFilterByAnd(ands, query);
            if (andRes) res = true;
            List<T> data = null;
            int total = 0;
            if (res)
            {
                //模糊查询
                using (NHibernate.ISession session = HttpContext.RequestServices.GetService<NHibernate.ISession>())
                {
                    NHibernate.ICriteria criteria = session.CreateCriteria<T>();
                    NHibernate.Criterion.AbstractCriterion abstractCriterion = wheres.Any() ? wheres[0] : ands[0];
                    for (int i = wheres.Any() ? 0 : 1; i < ands.Count; i++)
                    {
                        abstractCriterion &= ands[i];
                    }
                    for (int i = 1; i < wheres.Count; i++)
                    {
                        abstractCriterion |= wheres[i];
                    }

                    criteria = criteria.Add(abstractCriterion);
                    NHibernate.ICriteria pageCriteria = (NHibernate.ICriteria)criteria.Clone();
                    total = pageCriteria.SetProjection(Projections.RowCount()).UniqueResult<int>();
                    OrderBy(ref criteria);
                    data = criteria.SetFirstResult((page.Value - 1) * size.Value).SetMaxResults(size.Value).List<T>().ToList();
                    //数据不规则整理成树形列表信息 如果断层了 断层的下级的成为上级
                    data = this.DataParseIfWhileReference(data, true);
                }
            }
            else
            {
                data = this.QueryChildFilter(queryable, query).OrderBy(it => it.CreateDate).Skip((page.Value - 1) * size.Value).Take(size.Value).ToList();
                total = queryable.ToFutureValue(it => it.Count()).Value;
                //树形列表查询 去递归引用 automapper 才能正常运行 否则异常
                data = this.DataParseIfWhileReference(data);
            }
            ResultModel<G> result = new ResultModel<G>();
            result.Data = data.Any() ? ObjectMapper.Map<List<G>>(data) : null;
            result.Result = new PageModel() { Total = total, Size = size.Value, Page = total == 0 ? 0 : total % size.Value == 0 ? total / size.Value : (total / size.Value + 1) };
            ResponseApi response = ResponseApi.Create(GetLanguage(), Code.QuerySuccess);
            response.Data = result;
            return await Task.FromResult(response);
        }
        protected virtual void OrderBy(ref NHibernate.ICriteria criteria)
        {
            criteria=criteria.AddOrder(Order.Asc("CreateDate"));
        }
        protected virtual List<T> DataParseIfWhileReference(List<T> data,bool where=false)
        {
            return data;
        }
        /// <summary>
        /// 删除多条数据
        /// </summary>
        /// <param name="deleteEntry"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public virtual async Task<ResponseApi> Delete([/*Bind("ids"),*/ FromForm,FromBody] DeleteEntry deleteEntry)
        {
			if (Request.ContentType.Contains("application/json"))
            {	
				using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                {
					// Ref(ref deleteEntry, reader.ReadToEnd());
                    Ref(ref deleteEntry, reader.ReadToEndAsync().Result);//类库影响
                }
            }
            else if (Request.ContentType.Contains("text/xml"))
            {		
                using System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body);
                Type t = typeof(DeleteEntry);
                XmlSerializer serializer = new XmlSerializer(t);
                deleteEntry = serializer.Deserialize(reader) as DeleteEntry;
            }
            //this.ActionParam(HttpContext.Request,ref deleteEntry);//无效 作用域可能 绑定模型失败
            if (!deleteEntry.Ids.Any())
            {
                ResponseApi response = ResponseApi.Create(GetLanguage(), Code.ParamNotNull, false);
                response.Message = $"id {response.Message}";
                return await Task.FromResult(response);
            }
            else
            {
                this.DeleteBefore(deleteEntry.Ids);
                foreach (var item in deleteEntry.Ids)
                {
                    T obj = new T();
                    obj.Id = item;
                    this.UnitWork.Delete<T>(obj);
                    // UnitWork.Delete<T>(item);
                }
                this.DeleteAfter(deleteEntry.Ids);
                ResponseApi response = ResponseApi.Create(GetLanguage(), Code.DeleteSuccess);
                return await Task.FromResult(response);
            }
        }
        protected virtual void ActionParam<M>(HttpRequest request,ref M obj)where M:class
        {
			//启用 否则出现异常  除非卸载方法体类 作用域不同导致 Synchronous operations are disallowed. Call ReadAsync or set AllowSynchronousIO to true instead
	
            if (request.ContentType.Contains("application/json"))
            {	
				//Request.EnableBuffering();
                using (System.IO.StreamReader reader = new System.IO.StreamReader(request.Body))
                {
                    Ref(ref obj, reader.ReadToEndAsync().Result);
                }
            }
            else if (request.ContentType.Contains("text/xml"))
            {		
			    //Request.EnableBuffering();
                using System.IO.StreamReader reader = new System.IO.StreamReader(request.Body);
                Type t = typeof(M);
                XmlSerializer serializer = new XmlSerializer(t);
                obj = serializer.Deserialize(reader) as M;
            }
        }
        /// <summary>
        /// 删除之前操作
        /// </summary>
        /// <param name="id"></param>
        protected virtual void DeleteBefore(int[] id)
        {

        }
        /// <summary>
        /// 去除递归 后 的排序 默认未实现
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        protected virtual List<T> RecursionOrderBy(List<T> result)
        {
           
            return result;
        }
        /// <summary>
        /// 递归去除 无限引用 用于 automapper 
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <param name="result"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        protected virtual List<M> RecursionDataParse<M>(List<M> result, int? parentId)where M : ICasecade<M>
        {
            int j = -1;
            for (int i = 0; i < result.Count; i++)
            {
                var item = result[i]; 
                item.Parent = item.Parent == null ? (M)item.Clone() : (M)item.Parent.Clone();
                if (item.Id == parentId)
                {
                    j = i;
                    continue;
                }
                if (item.Children == null || !item.Children.Any())
                {
                    continue;
                }
                var data = RecursionDataParse(item.Children.ToList(), item.Id);
                item.Children = data.ToHashSet();
            }
            if (j != -1)
            {
                result.RemoveAt(j);
            }
            return result;
        }

        /// <summary>
        /// 递归去除 无限引用 用于 automapper 整理树形列表  如果断层了 断层的下级的成为上级 
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <param name="result"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        protected virtual List<M> RecursionDataParseByWhere<M>(List<M> result, List<int> ids =null) where M :class, ICasecade<M>
        {
            List<M> childs = new List<M>();
            bool first = true;
            if (ids != null) first = false;
            ids ??= result.Select(it => it.Id.Value).ToList();
            SortedSet<int> indexs = new SortedSet<int>();
            for (int i = 0; i < result.Count; i++)
            {
                var item = result[i];
                item.Parent = item.Parent == null ? (M)item.Clone() : (M)item.Parent.Clone();
                if (!first&&ids.FindAll(it=>it==item.Id.Value).Count==1)
                {
                    indexs.Add(i);
                    continue;
                }
                childs.Add((M)item.Clone());
                if (item.Children != null && item.Children.Any())
                {
                    List<M> tempChilds = RecursionDataParseByWhere(item.Children.ToList(),ids);
                    item.Children = tempChilds.ToHashSet();
                }
            }
            foreach (var item in indexs)
            {
                result.RemoveAt(item);
            }
            return result;
        }
        protected virtual void DeleteAfter(int[] id)
        {

        }
        /// <summary>
        /// 添加 或修改 数据之前验证
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="way"></param>
        /// <returns></returns>
        protected virtual async Task<ResponseApi> Validate(T obj,int way=0)
        {
            if (IsCustomValidator)
            {
                var error = ValidateHelper.ValidateError(typeof(T), obj,way);
                if (error==null||error.Data == null)
                {
                    Dictionary<string, List<string>> customErrors = new Dictionary<string, List<string>>();
                    customErrors = await this.CustomValidate(customErrors, obj, way);
                    if(customErrors.Count > 0)
                    {
                        ResponseApi response = ResponseApi.Create(GetLanguage(), Code.ParamError,false);
                        response.Data = new { Error = customErrors };
                        return await Task.FromResult(response);
                    }
                    return await Task.FromResult<ResponseApi>(null) ;
                }
                else
                {
                    Dictionary<string, List<string>> errors = ((Dictionary<string, Dictionary<string, List<string>>>)error.Data)["Error"] ;
                    errors = await CustomValidate(errors, obj,way);
                   // error.Data = new { Error = customErrors };
                }
                return error;
            }
            return await Task.FromResult<ResponseApi>(null);
        }
        protected virtual void GetBase64(SocialContact.Domain.Core.UserFileInfo obj)
        {
            if (Request.Form.Files.Count == 1)
            {
                string suffix = Request.Form.Files[0].FileName.Split('.').LastOrDefault();
                obj.Src = $"{RandomHelper.OrderId.Sha1()}.{suffix}";
                obj.FileId = RandomHelper.OrderId.Sha1();
                using System.IO.Stream stream = Request.Form.Files[0].OpenReadStream();
                byte[] buffer = new Byte[stream.Length];
                stream.Read(buffer);
                obj.Base64 = Base64Helper.Base64String(buffer);
            }
            else
            {
                var src = obj.FileId==null?null: this.RedisCache.GetHashValue("file", obj.FileId);
                if (string.IsNullOrEmpty(src))
                {
                    var temp = this.Cache.Get<List<UserFileEntry>>(Core.FileChannel).Find(it => it.FileName == obj.FileId);
                    src = temp?.FileSrc??string.Empty;
                }
                else
                {
                    var strs = src.Split('_');
                    obj.Src = strs[0];
                    src = obj.Src;
                    obj.Type = strs.Length == 2 ? strs[1] : null;
                }
                //byte[] buffer = this._httpClientFactory.CreateClient().GetAsync(this._fileAddress).Result.Content.ReadAsByteArrayAsync().Result;
                //obj.Base64 = Base64Utils.Base64String(System.IO.File.ReadAllBytes(buffer));
                if (System.IO.File.Exists($"{AddressConfig.UploadImgDirectory}//{ src}"))
                {
                    obj.Base64 =  Base64Helper.Base64String(System.IO.File.ReadAllBytes($"{AddressConfig.UploadImgDirectory}//{ src}"));
                }
            }
        }
        /// <summary>
        /// 自定义验证
        /// </summary>
        /// <param name="errors"></param>
        /// <param name="obj"></param>
        /// <param name="way"></param>
        /// <returns></returns>
        protected virtual async Task<Dictionary<string, List<string>>> CustomValidate(Dictionary<string, List<string>> errors, T obj,int way=0)
        {
            return await Task.FromResult(errors);
        }
        /// <summary>
        /// formform 数据转换
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="str"></param>
        protected void Ref<M>(ref M obj, string str)where M:class
        {
            obj = JsonHelper.ToObject<M>(str, JsonHelper.JsonSerializerSettings);
        }
        private IQueryable<T> Query()
        {
            IQueryable<T> queryable = UnitWork.Find<T>(null);
            return queryable;
        }
        /// <summary>
        /// 实现树形列表查询 未任何实现
        /// </summary>
        /// <param name="query"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual IQueryable<T> QueryChildFilter(IQueryable<T> query, F obj)
        {
            return query;
        }
        /// <summary>
        /// 默认实现树形列表查询
        /// </summary>
        /// <typeparam name="M"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        protected virtual IQueryable<M> QueryChildFilter<M>(IQueryable<M> query)where M:ICasecade<M>
        {
            query = query.Where(it => it.Id == it.Parent.Id || !it.Parent.Id.HasValue);
            return query;
        }

        /// <summary>
        /// 模糊查询 通用查询 默认实现
        /// </summary>
        /// <param name="criterias"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual bool QueryFilterByOr(ref List<NHibernate.Criterion.AbstractCriterion> criterias, F obj)
        {
            bool res = false;
            if (obj.Id != null && obj.Id.HasValue)
            {
                criterias.Add(Expression.IdEq(obj.Id));
                res = true;
            }
            if (obj.CreateDate != null && obj.CreateDate.Length == 2)
            {
                criterias.Add(Expression.Between("CreateDate", obj.CreateDate[0], obj.CreateDate[1]));
                res = true;
            }
            if (obj.UpdateDate != null && obj.UpdateDate.Length == 2)
            {
                criterias.Add(Expression.Between("UpdateDate", obj.UpdateDate[0], obj.UpdateDate[1]));
                res = true;
            }
            return res;
        }
        /// <summary>
        /// 模糊查询 通用查询 默认实现
        /// </summary>
        /// <param name="criterias"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual bool QueryFilterByAnd(List<NHibernate.Criterion.AbstractCriterion> criterias, F obj)
        {
            return false;
        }
        /// <summary>
        ///模糊查询 过滤分类信息 默认实现
        /// </summary>
        /// <typeparam name="J"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="query"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected virtual bool QueryFilterByCategory<J, K>(ref List<NHibernate.Criterion.AbstractCriterion> criterias, K obj) where J : ICategory where K : ICategory
        {
            if (!string.IsNullOrEmpty(obj.Category))
            {
                criterias.Add(Expression.Like("Category", $"%{obj.Category}%"));
                return true;
            }
            return false;
        }
        protected virtual bool QueryChildFilterByCategory<H,K>(ref List<NHibernate.Criterion.AbstractCriterion> criterias, K obj)  where H : Entry, ICategory where K : ICategory
        {
            bool res = false;
            if (!string.IsNullOrEmpty(obj.Category))
            {
                int[] ids = this.UnitWork.Find<H>(it => it.Category.Like($"%{obj.Category}%")).Select(it => it.Id.Value).ToArray();
                if (ids != null && ids.Any())
                {
                    criterias.Add(Expression.In("Category.Id", ids));
                    res = true;
                }
            }
            return res;
        }
        [HttpGet("category")]

        public virtual async Task<ResponseApi> Category()
        {
            List<CategoryEntry> categoryEntries = UnitWork.Find<T>(null).Select(Select()).ToList();
            ResponseApi response = ResponseApi.Create(GetLanguage(), Code.QuerySuccess);
            response.Data = categoryEntries;
            return await Task.FromResult(response);
        }
        protected virtual Func<T, CategoryEntry> Select()
        {
            return null;
        }
        protected virtual Func<H, CategoryEntry> SelectByCategory<H>() where H : Entry,ICategory, new()
        {
            return it => new CategoryEntry() { Id = it.Id.Value, Category = it.Category };
        }
        [HttpGet("operator")]

        public virtual async Task<ResponseApi> Operator()
        {
            ResponseApi response = ResponseApi.Create(GetLanguage(), Code.QuerySuccess);
            response.Data = AuthrizeValidator.GetOperatorAuthrize(GetUserId(), this.PageName);
            return await Task.FromResult(response);
        }
    }
}