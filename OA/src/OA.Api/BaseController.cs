using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using OA.Domain.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Utility;
using Microsoft.Extensions.DependencyInjection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OA.Api
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ProducesResponseType(typeof(ResponseApi), 200)]
    public class BaseController<T> : ControllerBase where T: BaseEntity,new()
    {
        protected readonly ILogger<BaseController<T>> Logger;
        protected readonly IRepository<T> Repository;
        public BaseController(ILogger<BaseController<T>> logger, IRepository<T> repository)
        {
            this.Logger = logger;
            this.Repository = repository;
        }
        [HttpPost("add")]
        public virtual ResponseApi Add([FromForm] T obj)
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
            //if (!ModelState.IsValid)
            //{
            //    return ResponseApiUtils.Fail();
            //}
            obj.CreateDate = DateTime.Now;
            this.Repository.Add(obj);
            return ResponseApiUtils.Success();
        }
        /// <summary>
        /// formform 数据转换
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="str"></param>
        protected virtual void Ref<M>(ref M obj, string str) where M : class
        {
            obj = JsonUtils.Instance.ToObject<M>(str);
        }
        [HttpPost("edit")]
        public virtual ResponseApi Edit([FromForm] T obj)
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
            //if (!ModelState.IsValid)
            //{
            //    return ResponseApiUtils.Fail();
            //}
            return Edited(obj);
        }
        protected virtual ResponseApi Edited(T obj)
        {
            var old = this.Repository.FindSingle(it => it.Id == obj.Id);
            obj.CreateDate = old.CreateDate;
            obj.UpdateDate = DateTime.Now;
            this.Repository.Update(obj);
            return ResponseApiUtils.Success();
        }
        [HttpPost("delete")]
        public virtual ResponseApi Delete([FromForm]DelEntry delEntry)
        {
            if (Request.ContentType.Contains("application/json"))
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body))
                {
                    // Ref(ref obj, reader.ReadToEnd());
                    Ref(ref delEntry, reader.ReadToEndAsync().Result);//类库影响
                }
            }
            Expression<Func<T, bool>> where = null;
            if (delEntry.Id.HasValue)
            {
                where = where.Or(it => it.Id == delEntry.Id.Value);
            }
            else if (delEntry.Ids != null && delEntry.Ids.Length > 0)
            {
                //多条件不支持 删除操作
                //NHibernate.ISession session = HttpContext.RequestServices.GetService<NHibernate.ISession>();
                //NHibernate.ICriteria criteria= session.CreateCriteria<T>();
                //NHibernate.Criterion.AbstractCriterion abstractCriterion=null;
                //bug
                foreach (var item in delEntry.Ids)
                {
                    // where = where.Or(it => it.Id == item);
                    this.Repository.Delete(it=>it.Id==item);
                    //if(abstractCriterion==null)
                    //{
                    //    abstractCriterion = NHibernate.Criterion.Expression.IdEq(item);
                    //}
                    //else
                    //{
                    //    abstractCriterion |= NHibernate.Criterion.Expression.IdEq(item);
                    //}
                }
                //criteria.Add(abstractCriterion);
                return ResponseApiUtils.Success();
            }
            else
            {
                return ResponseApiUtils.Fail();
            }
            this.Repository.Delete(where);
            return ResponseApiUtils.Success();
        }
        [HttpPost("get")]
        public virtual ResponseApi Get()
        {
            var data = this.Repository.Find(null).ToList();
            return ResponseApiUtils.Success().SetData(data);
        }
    }
    public class DelEntry
    {
        public int? Id { get; set; }
        public int[] Ids { get; set; }
    }
         
}
