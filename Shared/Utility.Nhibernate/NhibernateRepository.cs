#if NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NET481 || NET482 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETCOREAPP3_2 || NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Utility.Nhibernate
{
    public class NhibernateRepository<T> : NhibernateTemplate,IDisposable,IRepository<T> where T:class
    {
        readonly NHibernate.IStatelessSession _session;
        public NhibernateRepository(NHibernate.IStatelessSession session)
        {
            this._session = session;
            this.Connection = session.Connection;
        }

        public object DbContext => throw new NotImplementedException();

        public IDbConnection Connection { get; }

        public void Add(T entity)
        {
            base.Add<T>(this._session, entity);
        }

        public void BatchAdd(T[] entities)
        {
            base.BatchAdd<T>(this._session, entities);
        }

        public void Delete(T entity)
        {
            base.Del<T>(this._session, entity);
        }

        public void Delete(Expression<Func<T, bool>> exp)
        {
            base.Delete<T>(this._session, exp);
        }

        public int ExecuteSql(string sql)
        {
            return base.ExecuteQuery(sql);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> exp = null)
        {
            return Filter(exp);
        }
        IQueryable<T> Filter(Expression<Func<T, bool>> exp = null)
        {
            return exp==null?  base.Query<T>(this._session):base.Query<T>(this._session).Where(exp);
        }
        public IQueryable<T> Find(int pageindex = 1, int pagesize = 10, string orderby = "", Expression<Func<T, bool>> exp = null)
        {
            throw new NotImplementedException();
        }

        public T FindSingle(Expression<Func<T, bool>> exp = null)
        {
            return Filter(exp).FirstOrDefault();
        }

        public int GetCount(Expression<Func<T, bool>> exp = null)
        {
            return base.Count<T>(this._session, exp);
        }

        public bool IsExist(Expression<Func<T, bool>> exp)
        {
            return base.Count<T>(this._session, exp) >= 1;
        }

        public void Save()
        {
        }

        public void Update(T entity)
        {
            base.Edit<T>(this._session, entity);
        }

        public void Update(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity)
        {
            base.Update<T>(this._session, where, entity);
        }
        void IDisposable.Dispose(){
            this._session.Dispose();
        }
    }
}
#endif