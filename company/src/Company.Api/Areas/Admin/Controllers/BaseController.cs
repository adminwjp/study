using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Company.Domain.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;

namespace Company.Api.Areas.Admin.Controllers
{
    [Area("admin")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/admin/[controller]")]
    public class BaseController<T> : ControllerBase where T:Company.Domain.Core.BaseInfo,new()
    {
        protected readonly IRepository<T> Repository;
        protected readonly ILogger<BaseController<T>> Logger;
        public BaseController(IRepository<T> repository, ILogger<BaseController<T>> logger)
        {
            this.Repository = repository;
            this.Logger = logger;
        }
        protected virtual Language GetLanguage()
        {
            return Language.Chinese;
        }
        [HttpPost("add")]
        public virtual async Task<ResponseApi> Add([FromForm] T obj)
        {
            if (Request.ContentType != null)
            {
                if (Request.ContentType.Contains("application/json"))
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                    {
                        Ref(ref obj, reader.ReadToEnd());
                    }
                }
                else if (Request.ContentType.Contains("text/xml"))
                {
                    using System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body);
                    Type t = typeof(T);
                    XmlSerializer serializer = new XmlSerializer(t);
                    obj = serializer.Deserialize(reader) as T;
                }
            }
            this.AddMiddleExecet(obj);
            obj.CreateDate = DateTime.Now;
            // this.ActionParamParse(Request, ref obj);
            this.Repository.Add(obj);
            return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.AddSuccess));
        }
        protected virtual void AddMiddleExecet(T obj)
        {
      
        }
        protected virtual void EditMiddleExecet(T obj)
        {
      
        }
        [HttpPost("edit")]
        public virtual async Task<ResponseApi> Edit([FromForm] T obj)
        {
           // obj.Create =true;
            if (Request.ContentType != null)
            {
                if (Request.ContentType.Contains("application/json"))
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                    {
                        Ref(ref obj, reader.ReadToEnd());
                    }
                }
                else if (Request.ContentType.Contains("text/xml"))
                {
                    using System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body);
                    Type t = typeof(T);
                    XmlSerializer serializer = new XmlSerializer(t);
                    obj = serializer.Deserialize(reader) as T;
                }
            }
            this.EditMiddleExecet(obj);
            // this.ActionParamParse(Request, ref obj);
            var old = this.Repository.FindSingle(it => it.Id == obj.Id);
            obj.CreateDate = old.CreateDate;
            obj.ModifyDate = DateTime.Now;
            this.Repository.Update(obj);
            return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.ModifySuccess));
        }
        [HttpGet("editstatus")]
        public virtual async Task<ResponseApi> Edit([FromQuery]BaseInfo baseInfo)
        {
             if(baseInfo.Id.HasValue)
            {
                this.Repository.Update(it => it.Id == baseInfo.Id, it => new T() { Enable = baseInfo.Enable });
                this.Repository.Save();
                return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.ModifySuccess));
            }
            return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.Fail));
        }
        [HttpPost("editstatus")]
        public virtual async Task<ResponseApi> PostEdit([FromForm]BaseInfo baseInfo)
        {
            //this.Repository.Update(it=>it.Id==baseInfo.Id,it=> new T() { Enable =!it.Enable});// Unknown column 'm.enable'
            if (baseInfo.Ids != null && baseInfo.Ids.Length > 0)
            {
                Expression<Func<T, bool>> where = null;
                foreach (var item in baseInfo.Ids)
                {
                    where = where.Or(it => it.Id == item);
                    this.Repository.Save();
                }
                this.Repository.Update(where, it => new T() { Enable = baseInfo.Enable });
                this.Repository.Save();
                return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.ModifySuccess));
            }
            return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.Fail));
        }
        [HttpGet("delete")]
        [HttpPost("delete")]
        public virtual async Task<ResponseApi> Delete([FromQuery]int? id,[FromForm,FromBody]int[] ids)
        {
            if (id.HasValue||(ids != null && ids.Any()))
            {
                if (id.HasValue)
                {
                    this.Repository.Delete(it=>it.Id==id);
                }
                else
                {
                    foreach (var item in ids)
                    {
                        this.Repository.Delete(it => it.Id == item);
                    }
                }
                return await Task.FromResult(ResponseApiUtils.GetResponse(GetLanguage(),Utility.Code.DeleteSuccess));
            }
            else
            {
                Utility.ResponseApi ResponseApi = ResponseApiUtils.GetResponse(GetLanguage(), Utility.Code.ParamNotNull,false);
                ResponseApi.Message=$"id {ResponseApi.Data}";
                return await Task.FromResult(ResponseApi);
            }
        }
        [HttpPost("query")]
        public virtual async Task<ResponseApi> Query([FromForm,FromBody]T obj, int? page, int? size)
        {
            PageUtils.Set(ref page,ref size);
            if (Request.ContentType != null)
            {
                if (Request.ContentType.Contains("application/json"))
                {
                    using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                    {
                        Ref(ref obj, reader.ReadToEnd());
                    }
                }
                else if (Request.ContentType.Contains("text/xml"))
                {
                    using System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body);
                    Type t = typeof(T);
                    XmlSerializer serializer = new XmlSerializer(t);
                    obj = serializer.Deserialize(reader) as T;
                }
            }
            
            // this.ActionParamParse(Request,ref obj);
            Utility.ResponseApi<ResultModel<T>> responseApi = ResponseApiUtils.GetResponse<ResultModel<T>>(GetLanguage(),Utility.Code.QuerySuccess);
            responseApi.Data = new ResultModel<T>();
            responseApi.Data.Data=QueryList(obj, page, size);
            responseApi.Data.Result = new PageModel() { Page = 1, Size = 10, Records = responseApi.Data.Data==null?0: responseApi.Data.Data.Count };
            responseApi.Data.Result.Total = responseApi.Data.Result.Records == 0 ? 1:responseApi.Data.Result.Records % 10==0? responseApi.Data.Result.Records / 10:(responseApi.Data.Result.Records / 10)+1;
            return await Task.FromResult(responseApi);
        }
        protected virtual IQueryable<T> Query(Expression<Func<T, bool>> expression=null)
        {
            return this.Repository.Find(expression);
        }
        protected virtual Expression<Func<T, bool>> QueryFilter(Expression<Func<T, bool>> expression, T obj)
        {
           
            if(obj.CreateEndDate.HasValue&& obj.CreateEndDate.HasValue)
            {
                expression = expression.Or(it => it.CreateDate >= obj.CreateStartDate.Value&& it.CreateDate <= obj.CreateEndDate.Value);
            }
            else
            {
                if (obj.CreateStartDate.HasValue)
                {
                    expression = expression.Or(it => it.CreateDate >= obj.CreateStartDate.Value);
                }
                if (obj.CreateEndDate.HasValue)
                {
                    expression = expression.Or(it => it.CreateDate <= obj.CreateEndDate.Value);
                }
               
            }
            if (obj.ModifyStartDate.HasValue && obj.ModifyEndDate.HasValue) {
                expression = expression.Or(it => it.ModifyDate >= obj.ModifyStartDate.Value && it.ModifyDate <= obj.ModifyEndDate.Value);
            }
            else
            {
                if (obj.ModifyStartDate.HasValue)
                {
                    expression = expression.Or(it => it.ModifyDate >= obj.ModifyStartDate.Value);
                }
                if (obj.ModifyEndDate.HasValue)
                {
                    expression = expression.Or(it => it.ModifyDate <= obj.ModifyEndDate.Value);
                }
            }
             
            if (obj.Enable.HasValue)
            {
                expression = expression.Or(it => it.Enable == obj.Enable.Value);
            }
            return expression;
        }
        protected virtual Expression<Func<F, bool>> QueryFilter<F>(Expression<Func<F, bool>> expression, F obj)where F :BaseName
        {
            if (!string.IsNullOrEmpty(obj.Name))
            {
                expression = expression.Or(it => it.Name.Contains(obj.Name));
            }
            if (!string.IsNullOrEmpty(obj.EnglishName))
            {
                expression = expression.Or(it => it.EnglishName.Contains(obj.EnglishName));
            }
            return expression;
        }
        protected virtual List<T> QueryList(T obj, int? page, int? size)
        {
            return Query().Skip((page.Value-1)*size.Value).Take(size.Value).ToList();
        }
        /// <summary>
        /// formbody 数据转换
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="str"></param>
        protected void Ref<M>(ref M obj, string str) where M : class
        {
            obj = JsonUtils.Instance.ToObject<M>(str, JsonUtils.JsonSerializerSettings);
        }
        protected virtual void ActionParamParse(HttpRequest request,ref T obj)
        {
            if (request.ContentType.Contains("application/json"))
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(request.Body))
                {
                    Ref(ref obj, reader.ReadToEnd());
                }
            }
            else if (request.ContentType.Contains("text/xml"))
            {
                using System.IO.StreamReader reader = new System.IO.StreamReader(request.Body);
                Type t = typeof(T);
                XmlSerializer serializer = new XmlSerializer(t);
                obj = serializer.Deserialize(reader) as T;
            }
        }
    }
}