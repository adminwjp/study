#if NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NET481 || NET482 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETCOREAPP3_2 || NETSTANDARD2_0 || NETSTANDARD2_1
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Utility.Nhibernate
{
    public class UnitNhibernateWork : NhibernateTemplate,IUnitWork
    {
        readonly NHibernate.IStatelessSession _statelessSession;
        readonly NHibernate.ISession _session;
        //public UnitNhibernateWork(NHibernate.IStatelessSession session)
        //{
        //    this._statelessSession = session;
        //}
        //public UnitNhibernateWork(NHibernate.ISession session)
        //{
        //    this._session = session;
        //}
        //public UnitNhibernateWork(NHibernate.IStatelessSession session, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory):base(loggerFactory)
        //{
        //    this._statelessSession = session;
        //}
        public UnitNhibernateWork(NHibernate.ISession session, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            this._session = session;
            this.Connection = session.Connection;
        }

        public IDbConnection Connection { get; }
        #region ISession
        public object Add<T>(T entity) where T : class
        {
           return base.Add<T>(this._session, entity);
        }

        public new void BatchAdd<T>(T[] entities) where T : class
        {
            base.BatchAdd<T>(this._session, entities);
        }

        public void Delete<T>(T entity) where T : class
        {
            base.Del<T>(this._session, entity);
        }

        public void Delete<T>(Expression<Func<T, bool>> exp) where T : class
        {
            base.Delete<T>(this._session, exp);
        }

        public void Delete<T>(object id)where T:class
        {
            base.Del<T>(this._session, id);
        }

        public int ExecuteSql(string sql)
        {
            return base.ExecuteQueryBySession(sql);
        }

        public IQueryable<T> Find<T>(Expression<Func<T, bool>> exp = null) where T : class
        {
            return exp == null ? base.Query<T>(this._session) : base.Query<T>(this._session).Where(exp);
        }

        public IQueryable<T> Find<T>(int pageindex = 1, int pagesize = 10, string orderby = "", Expression<Func<T, bool>> exp = null) where T : class
        {
            throw new NotImplementedException();
        }

        public T FindSingle<T>(Expression<Func<T, bool>> exp = null) where T : class
        {
            return (exp == null ? base.Query<T>(this._session) : base.Query<T>(this._session).Where(exp)).FirstOrDefault();
        }

        public int GetCount<T>(Expression<Func<T, bool>> exp = null) where T : class
        {
            return base.Count<T>(this._session, exp);
        }

        public bool IsExist<T>(Expression<Func<T, bool>> exp) where T : class
        {
            return base.Count<T>(this._session, exp) >= 1;
        }

        public void Save()
        {

        }

        public void Update<T>(T entity) where T : class
        {
            base.Edit<T>(this._session, entity);
        }

        public void Update<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity) where T : class
        {
            base.Update<T>(this._session, where, entity);
        }
        void IDisposable.Dispose()
        {
            this._session.Dispose();
        }
#endregion ISession

        //#region IStatelessSession
        //public void Add<T>(T entity) where T : class
        //{
        //    base.Add<T>(this._statelessSession,entity);
        //}

        //public new void BatchAdd<T>(T[] entities) where T : class
        //{
        //    base.BatchAdd<T>(this._statelessSession, entities);
        //}

        //public void Delete<T>(T entity) where T : class
        //{
        //    base.Del<T>(this._statelessSession, entity);
        //}

        //public void Delete<T>(Expression<Func<T, bool>> exp) where T : class
        //{
        //    base.Delete<T>(this._statelessSession, exp);
        //}

        //public  void Delete<T>(object id) where T : class
        //{
        //    base.Del<T>(this._statelessSession, id);
        //}

        //public int ExecuteSql(string sql)
        //{
        //    return base.ExecuteQuery(sql);
        //}

        //public IQueryable<T> Find<T>(Expression<Func<T, bool>> exp = null) where T : class
        //{
        //    return exp==null?base.Query<T>(this._statelessSession): base.Query<T>(this._statelessSession).Where(exp);
        //}

        //public IQueryable<T> Find<T>(int pageindex = 1, int pagesize = 10, string orderby = "", Expression<Func<T, bool>> exp = null) where T : class
        //{
        //    throw new NotImplementedException();
        //}

        //public T FindSingle<T>(Expression<Func<T, bool>> exp = null) where T : class
        //{
        //    return (exp == null ? base.Query<T>(this._statelessSession) : base.Query<T>(this._statelessSession).Where(exp)).FirstOrDefault();
        //}

        //public int GetCount<T>(Expression<Func<T, bool>> exp = null) where T : class
        //{
        //    return base.Count<T>(this._statelessSession,exp);
        //}

        //public bool IsExist<T>(Expression<Func<T, bool>> exp) where T : class
        //{
        //    return base.Count<T>(this._statelessSession, exp) >= 1;
        //}

        //public void Save()
        //{

        //}

        //public void Update<T>(T entity) where T : class
        //{
        //    base.Edit<T>(this._statelessSession, entity);
        //}

        //public void Update<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity) where T : class
        //{
        //    base.Update<T>(this._statelessSession, where,entity);
        //}
        //void IDisposable.Dispose(){
        //    this._statelessSession.Dispose();
        //}
        //#endregion IStatelessSession
    }
}
#endif