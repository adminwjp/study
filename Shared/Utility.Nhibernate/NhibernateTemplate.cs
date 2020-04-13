#if NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NET481 || NET482 || NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETCOREAPP3_2 || NETSTANDARD2_0 || NETSTANDARD2_1
using NHibernate;
using System.Collections.Generic;
using NHibernate.Cfg;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System;
using NHibernate.Linq;

namespace Utility.Nhibernate
{
    /// <summary> Nihernate 模板 </summary>
    public class NhibernateTemplate
    {
        /// <summary>
        ///构造函数 
        /// </summary>
        public NhibernateTemplate()
        {

        }
        protected Microsoft.Extensions.Logging.ILoggerFactory _loggerFactory;
        protected readonly EmptyInterceptor Interceptor;
        /// <summary>
        ///构造函数 
        /// </summary>
        public NhibernateTemplate(Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            Interceptor = new SQLWatcher(loggerFactory);
        }
        protected IStatelessSession StatelessSession { get;  set; }
        protected ISession Session { get; set; }
        /// <summary>
        ///构造函数 
        /// </summary>
        public NhibernateTemplate(IStatelessSession session)
        {
            StatelessSession = session;
        }   /// <summary>
            ///构造函数 
            /// </summary>
        public NhibernateTemplate(ISession session)
        {
            Session = session;
        }
        /// <summary>
        /// 构造函数 注入
        /// </summary>
        /// <param name="action"></param>
        public NhibernateTemplate(Action<Configuration> action) {
            NhibernateHelper.Init(action);
        }

        private readonly static Lazy<NhibernateTemplate> _helper = new Lazy<NhibernateTemplate>();//初始化
          /// <summary>
        ///NhibernateTemplate 
        /// </summary>
        public static NhibernateTemplate Default => _helper.Value;

#region ISession
        /// <summary> Nihernate 添加操作 </summary>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public object Add<T>(ISession session, T obj) where T : class
        {
            if (obj == null) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    object result = session.Save(obj);
                    transaction.Commit();
                    return result;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw e;
            }
        }
        /// <summary> Nihernate 添加操作 </summary>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public int BatchAdd<T>(ISession session, T[] obj) where T : class
        {
            if (obj == null||obj.Length==0) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    int res = 0;
                    foreach (var item in obj)
                    {
                        session.Save(item);
                        res++;
                    }
                    transaction.Commit();
                    return res;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw e;
            }
        }

        /// <summary> Nihernate 添加异步操作 </summary>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public Task<object> AddAsync<T>(ISession session, T obj) where T : class
        {
            if (obj == null) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    Task<object> result = session.SaveAsync(obj) as Task<object>;
                    transaction.CommitAsync();
                    return result;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.RollbackAsync();
                }
                throw e;
            }
        }

        /// <summary> Nihernate 修改操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public void Edit<T>(ISession session, T obj) where T : class
        {
            if (obj == null) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    session.Clear();
                    //session.Refresh();
                    //session.Merge(obj);
                    session.Update(obj);
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw e;
            }
        }
        /// <summary> Nihernate 修改操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public void BatchEdit<T>(ISession session, T[] obj) where T : class
        {
            if (obj == null || obj.Length == 0) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    foreach (var item in obj)
                    {
                        session.Update(item);
                    }
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw e;
            }
        }
        /// <summary> Nihernate 修改异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Task EditAsync<T>(ISession session, T obj) where T : class
        {
            if (obj == null ) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    Task task = session.UpdateAsync(obj);
                    transaction.CommitAsync();
                    return task;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.RollbackAsync();
                }
                throw e;
            }
        }

        /// <summary> Nihernate 删除操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="obj"></param>
        public void Del<T>(ISession session, T obj) where T : class
        {
            if (obj == null ) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    session.Delete(obj);
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw e;
            }
        }

        /// <summary> Nihernate 删除操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="id"></param>
        public void Del<T>(ISession session, object id) where T : class
        {
            if (string.IsNullOrEmpty(id?.ToString())) throw new ArgumentNullException("id");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    var obj = session.Get<T>(id);
                    session.Delete(obj);
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw e;
            }
        }

        /// <summary> Nihernate 删除异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Task DelAsync<T>(ISession session, T obj) where T : class
        {
            if (obj == null ) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    Task task = session.DeleteAsync(obj);
                    transaction.CommitAsync();
                    return task;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.RollbackAsync();
                }
                throw e;
            }
        }

        /// <summary> Nihernate 删除异步操作 </summary>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DelAsync<T>(ISession session, object id) where T : class
        {
            if (string.IsNullOrEmpty(id?.ToString())) throw new ArgumentNullException("id");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    Task task = session.DeleteAsync(session.GetAsync<T>(id));
                    transaction.CommitAsync();
                    return task;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.RollbackAsync();
                }
                throw e;
            }
        }

        /// <summary> Nihernate 查询操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get<T>(ISession session, object id) where T : class
        {
            if (string.IsNullOrEmpty(id?.ToString())) throw new ArgumentNullException("id");
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    return session.Get<T>(id);
                }
                finally
                {
                    transaction.Commit();
                }
            }
        }


        /// <summary> Nihernate 查询异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<T> GetAsync<T>(ISession session, object id) where T : class
        {
            if (string.IsNullOrEmpty(id?.ToString())) throw new ArgumentNullException("id");
            return session.GetAsync<T>(id);
        }
        /// <summary>根据实体名称查询，没有则根据泛型查询  异常</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public IQueryable<T> Query<T>(ISession session, string entityName = null) where T : class
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    return entityName == null ? session.Query<T>() : session.Query<T>(entityName);
                }
                finally
                {
                    transaction.Commit();
                }
            }
        }

        /// <summary>根据实体类型查询，没有则根据泛型查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="ISession"/></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public IList<T> QueryOver<T>(ISession session, Expression<Func<T>> alias = null) where T : class
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    return alias == null ? session.QueryOver<T>().List() : session.QueryOver<T>(alias).List();
                }
                finally
                {
                    transaction.Commit();
                }

            }
        }



        /// <summary> Nihernate 添加操作 </summary>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public object AddBySession(object obj)
        {
            using (ISession session = NhibernateHelper.OpenSession(Interceptor))
            {
                return Add(session, obj);
            }
        }
        /// <summary> Nihernate 添加操作 </summary>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public int BatchAddBySession<T>(T[] obj) where T : class
        {
            using (ISession session = NhibernateHelper.OpenSession(Interceptor))
            {
                return BatchAdd(session, obj);
            }
        }

        /// <summary> Nihernate 添加异步操作 </summary>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public Task<object> AddAsyncBySession(object obj)
        {
            using (ISession session = NhibernateHelper.OpenSession(Interceptor))
            {
                return AddAsync(session, obj);
            }
        }

        /// <summary> Nihernate 修改操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public void EditBySession<T>(T obj) where T : class
        {
            using (ISession session = NhibernateHelper.OpenSession(Interceptor))
            {
                Edit(session, obj);
            }
        }
        /// <summary> Nihernate 修改操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public void BatchEditBySession<T>(T[] obj) where T : class
        {
            using (ISession session = NhibernateHelper.OpenSession(Interceptor))
            {
                BatchEdit(session, obj);
            }
        }
        /// <summary> Nihernate 修改异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Task EditAsyncBySession<T>(T obj) where T : class
        {
            using (ISession session = NhibernateHelper.OpenSession(Interceptor))
            {
                return EditAsync(session, obj);
            }
        }

        /// <summary> Nihernate 删除操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void DelBySession<T>(T obj) where T : class
        {
            using (ISession session = NhibernateHelper.OpenSession(Interceptor))
            {
                Del(session, obj);
            }
        }

        /// <summary> Nihernate 删除操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        public void DelBySession<T>(object id) where T : class
        {
            using (ISession session = NhibernateHelper.OpenSession(Interceptor))
            {
                Del<T>(session, id);
            }
        }

        /// <summary> Nihernate 删除异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Task DelAsyncBySession<T>(T obj) where T : class
        {
            using (ISession session = NhibernateHelper.OpenSession(Interceptor))
            {
                return DelAsync(session, obj);
            }
        }

        /// <summary> Nihernate 删除异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DelAsyncBySession<T>(object id) where T : class
        {
            using (ISession session = NhibernateHelper.OpenSession(Interceptor))
            {
                return DelAsync<T>(session, id);
            }
        }

        /// <summary> Nihernate 查询操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetBySession<T>(object id) where T : class
        {
            using (ISession session = NhibernateHelper.OpenSession(Interceptor))
            {
                return Get<T>(session, id);
            }
        }


        /// <summary> Nihernate 查询异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<T> GetAsyncBySession<T>(object id) where T : class
        {
            using (ISession session = NhibernateHelper.OpenSession(Interceptor))
            {
                return GetAsync<T>(session, id);
            }
        }

        /// <summary>根据实体名称查询，没有则根据泛型查询  异常</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public IQueryable<T> QueryBySession<T>(string entityName = null) where T : class
        {
            using (ISession session = NhibernateHelper.OpenSession(Interceptor))
            {
                return Query<T>(session, entityName);
            }
        }

        /// <summary>根据实体类型查询，没有则根据泛型查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="alias"></param>
        /// <returns></returns>
        public IList<T> QueryOverBySession<T>(Expression<Func<T>> alias = null) where T : class
        {
            using (ISession session = NhibernateHelper.OpenSession(Interceptor))
            {
                return QueryOver<T>(session, alias);
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteQueryBySession(string sql)
        {
            if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException("sql");
            NHibernate.ITransaction transaction = null;
            try
            {
                using (ISession session = NhibernateHelper.OpenSession(Interceptor))
                {
                    using (transaction = session.BeginTransaction())
                    {
                        int res = session.CreateQuery(sql).ExecuteUpdate();
                        transaction.Commit();
                        return res;
                    }
                }

            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.RollbackAsync();
                }
                throw e;
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="size">每页记录</param>
        /// <returns></returns>
        public IEnumerable<T> FindBySession<T>(string sql, int size) where T : class
        {
            if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException("sql");
            size = size > 1 ? 10 : size > 100 ? 100 : size;
            using (ISession session = NhibernateHelper.OpenSession(Interceptor))
            {
                return session.CreateQuery(sql).SetMaxResults(size).List<T>();
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public T FindBySession<T>(string sql, string[] param) where T : class
        {
            if (string.IsNullOrEmpty(sql)) throw new ArgumentNullException("sql");
            using (ISession session = NhibernateHelper.OpenSession(Interceptor))
            {
                IQuery query = session.CreateQuery(sql);
                if(param!=null)
                {
                    for (int i = 0; i < param.Length; i++)
                    {
                        query = query.SetString(i, param[i]);
                    }
                }
                return query.List<T>()[0];
            }
        }
#region linq https://nhibernate.info/previous-doc/v5.1/ref/querylinq.html
        public int Count<T>(ISession session, Expression<Func<T, bool>> where) where T : class
        {
            NHibernate.IFutureValue<int> value = Filter(session.Query<T>(), where).ToFutureValue(it => it.Count());
            return value.Value;
        }
        public T Max<T>(ISession session, Expression<Func<T, bool>> where) where T : class
        {
            return Filter(session.Query<T>(), where).Max();
        }
        public IList<T> List<T>(ISession session, Expression<Func<T, bool>> where, string entityName = "") where T : class
        {
            return string.IsNullOrEmpty(entityName) ? Filter(session.Query<T>(),where).ToList() : Filter(session.Query<T>(entityName), where).ToList();
        }
        IQueryable<T> Filter<T>(IQueryable<T> source, Expression<Func<T, bool>> where) where T: class
        {
            try
            {

              return   (where == null ? source : source.Where(where));
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public IQueryable<T> Query<T>(ISession session, Expression<Func<T, bool>> where, string entityName = "") where T : class
        {
            return string.IsNullOrEmpty(entityName) ? Filter(session.Query<T>(), where) : Filter(session.Query<T>(entityName), where);
        }
        public IList<T> List<T>(IEnumerable<T> datas, Expression<Func<T, bool>> where) where T : class
        {
            return Filter(datas.AsQueryable(), where).ToList();
        }
        public int Delete<T>(ISession session, Expression<Func<T, bool>> where) where T : class
        {
            return Filter(session.Query<T>(), where).Delete();
        }
        public async Task<int> DeleteAsync<T>(ISession session, Expression<Func<T, bool>> where, System.Threading.CancellationToken cancellation = default(System.Threading.CancellationToken)) where T : class
        {
            return await Filter(session.Query<T>(), where).DeleteAsync(cancellation);
        }
        public async Task<int> UpdateAsync<T>(ISession session, Expression<Func<T, bool>> where, Expression<Func<T, T>> update, System.Threading.CancellationToken cancellation = default(System.Threading.CancellationToken)) where T : class
        {
            return await Filter(session.Query<T>(), where).UpdateAsync(update, cancellation);
        }
        public int Update<T>(ISession session, Expression<Func<T, bool>> where, Expression<Func<T, T>> update) where T : class
        {
            return Filter(session.Query<T>(), where).Update(update);
        }
        //public int Update<T, TProp>(ISession session, Expression<Func<T, bool>> where, Expression<Func<T, TProp>>[] update) where T : class
        //{
        //    UpdateBuilder<T> updateBuilder = Filter(session.Query<T>(), where).UpdateBuilder<T>();
        //    return Filter(session.Query<T>(), where).UpdateBuilder<T>().Set(it=>update[0](it),it=>update[1]);
        //}
        public int Update<T>(ISession session, Expression<Func<T, bool>> where, Expression<Func<T, dynamic>> update) where T : class
        {
            return Filter(session.Query<T>(), where).Update(update);
        }
#endregion linq https://nhibernate.info/previous-doc/v5.1/ref/querylinq.html

#endregion ISession

#region IStatelessSession
        /// <summary> Nihernate 添加操作 </summary>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public object Add<T>(IStatelessSession session,T obj) where T : class
        {
            if (obj == null) throw new ArgumentNullException("obj");
            ITransaction transaction=null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    object result = session.Insert(obj);
                    transaction.Commit();
                    return result;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw e;
            }
        }
        /// <summary> Nihernate 添加操作 </summary>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public int BatchAdd<T>(IStatelessSession session, T[] obj) where T : class
        {
            if (obj == null|| obj.Length==0) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    int res = 0;
                    foreach (var item in obj)
                    {
                        session.Insert(item);
                        res++;
                    }
                    transaction.Commit();
                    return res;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw e;
            }
        }
        
        /// <summary> Nihernate 添加异步操作 </summary>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public Task<object> AddAsync<T>(IStatelessSession session, T obj) where T : class
        {
            if (obj == null) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    Task<object> result = session.InsertAsync(obj) as Task<object>;
                    transaction.CommitAsync();
                    return result;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.RollbackAsync();
                }
                throw e;
            }
        }
      
        /// <summary> Nihernate 修改操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public void Edit<T>(IStatelessSession session, T obj) where T : class
        {
            if (obj == null) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    session.Update(obj);
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw e;
            }
        }
        /// <summary> Nihernate 修改操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public void BatchEdit<T>(IStatelessSession session, T[] obj) where T : class
        {
            if (obj == null || obj.Length == 0) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    foreach (var item in obj)
                    {
                        session.Update(item);
                    }
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw e;
            }
        }
        /// <summary> Nihernate 修改异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Task EditAsync<T>(IStatelessSession session, T obj) where T : class
        {
            if (obj == null) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    Task task = session.UpdateAsync(obj);
                    transaction.CommitAsync();
                    return task;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.RollbackAsync();
                }
                throw e;
            }
        }

        /// <summary> Nihernate 删除操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="obj"></param>
        public void Del<T>(IStatelessSession session, T obj) where T : class
        {
            if (obj == null) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    session.Delete(obj);
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw e;
            }
        }

        /// <summary> Nihernate 删除操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="id"></param>
        public void Del<T>(IStatelessSession session, object id) where T:class
        {
            if (string.IsNullOrEmpty(id?.ToString())) throw new ArgumentNullException("id");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    session.Delete(session.Get<T>(id));
                    transaction.Commit();
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                throw e;
            }
        }

        /// <summary> Nihernate 删除异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Task DelAsync<T>(IStatelessSession session, T obj) where T : class
        {
            if (obj == null) throw new ArgumentNullException("obj");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    Task task = session.DeleteAsync(obj);
                    transaction.CommitAsync();
                    return task;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.RollbackAsync();
                }
                throw e;
            }
        }

        /// <summary> Nihernate 删除异步操作 </summary>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DelAsync<T>(IStatelessSession session, object id) where T : class
        {
            if (string.IsNullOrEmpty(id?.ToString())) throw new ArgumentNullException("id");
            ITransaction transaction = null;
            try
            {
                using (transaction = session.BeginTransaction())
                {
                    Task task = session.DeleteAsync(session.GetAsync<T>(id));
                    transaction.CommitAsync();
                    return task;
                }
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.RollbackAsync();
                }
                throw e;
            }
        }

        /// <summary> Nihernate 查询操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get<T>(IStatelessSession session, object id) where T : class
        {
            if (string.IsNullOrEmpty(id?.ToString())) throw new ArgumentNullException("id");
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    return session.Get<T>(id);
                }
                finally
                {
                    transaction.Commit();
                }
            }
        }


        /// <summary> Nihernate 查询异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<T> GetAsync<T>(IStatelessSession session, object id) where T : class
        {
            if (string.IsNullOrEmpty(id?.ToString())) throw new ArgumentNullException("id");
            return session.GetAsync<T>(id);
        }
        /// <summary>根据实体名称查询，没有则根据泛型查询  异常</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public IQueryable<T> Query<T>(IStatelessSession session, string entityName=null) where T : class
        {
            using (ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    return string.IsNullOrEmpty(entityName) ? session.Query<T>() : session.Query<T>(entityName);
                }
                finally
                {
                    transaction.Commit();
                }
            }
        }

        /// <summary>根据实体类型查询，没有则根据泛型查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"><see cref="IStatelessSession"/></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public IList<T> QueryOver<T>(IStatelessSession session, Expression<Func<T>> alias=null) where T : class
        {
            using(ITransaction transaction = session.BeginTransaction())
            {
                try
                {
                    return alias == null ? session.QueryOver<T>().List() : session.QueryOver<T>(alias).List();
                }
                finally
                {
                    transaction.Commit();
                }

            }
        }



        /// <summary> Nihernate 添加操作 </summary>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public object Add(object obj)
        {
            using (IStatelessSession session = NhibernateHelper.Session)
            {
                return Add(session,obj);
            }
        }
        /// <summary> Nihernate 添加操作 </summary>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public int BatchAdd<T>(T[] obj) where T : class
        {
            using (IStatelessSession session = NhibernateHelper.Session)
            {
                return BatchAdd(session, obj);
            }
        }

        /// <summary> Nihernate 添加异步操作 </summary>
        /// <param name="obj"></param>
        /// <returns>获取主键编号</returns>
        public Task<object> AddAsync( object obj)
        {
            using (IStatelessSession session = NhibernateHelper.Session)
            {
                return AddAsync(session, obj);
            }
        }

        /// <summary> Nihernate 修改操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public void Edit<T>( T obj) where T : class
        {
            using (IStatelessSession session = NhibernateHelper.Session)
            {
                Edit(session, obj);
            }
        }
        /// <summary> Nihernate 修改操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public void BatchEdit<T>( T[] obj) where T : class
        {
            using (IStatelessSession session = NhibernateHelper.Session)
            {
                BatchEdit(session, obj);
            }
        }
        /// <summary> Nihernate 修改异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Task EditAsync<T>( T obj) where T : class
        {
            using (IStatelessSession session = NhibernateHelper.Session)
            {
              return  EditAsync(session, obj);
            }
        }

        /// <summary> Nihernate 删除操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Del<T>(T obj) where T : class
        {
            using (IStatelessSession session = NhibernateHelper.Session)
            {
                 Del(session, obj);
            }
        }

        /// <summary> Nihernate 删除操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        public void Del<T>(object id) where T : class
        {
            using (IStatelessSession session = NhibernateHelper.Session)
            {
                Del<T>(session,id);
            }
        }

        /// <summary> Nihernate 删除异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public Task DelAsync<T>( T obj) where T : class
        {
            using (IStatelessSession session = NhibernateHelper.Session)
            {
               return  DelAsync(session, obj);
            }
        }

        /// <summary> Nihernate 删除异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DelAsync<T>( object id) where T : class
        {
            using (IStatelessSession session = NhibernateHelper.Session)
            {
                return DelAsync<T>(session, id);
            }
        }

        /// <summary> Nihernate 查询操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get<T>(object id) where T : class
        {
            using (IStatelessSession session = NhibernateHelper.Session)
            {
                return Get<T>(session, id);
            }
        }


        /// <summary> Nihernate 查询异步操作 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<T> GetAsync<T>( object id) where T : class
        {
            using (IStatelessSession session = NhibernateHelper.Session)
            {
                return GetAsync<T>(session, id);
            }
        }
        
        /// <summary>根据实体名称查询，没有则根据泛型查询  异常</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public IQueryable<T> Query<T>( string entityName = null) where T : class
        {
            using (IStatelessSession session = NhibernateHelper.Session)
            {
                return Query<T>(session, entityName);
            }
        }

        /// <summary>根据实体类型查询，没有则根据泛型查询 </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="alias"></param>
        /// <returns></returns>
        public IList<T> QueryOver<T>( Expression<Func<T>> alias = null) where T : class
        {
            using (IStatelessSession session = NhibernateHelper.Session)
            {
                return QueryOver<T>(session, alias);
            }
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int  ExecuteQuery(string sql)
        {
            NHibernate.ITransaction transaction = null;
            try
            { 
                using (IStatelessSession session = NhibernateHelper.Session)
                {
                    using (transaction = session.BeginTransaction())
                    {
                        int res = session.CreateQuery(sql).ExecuteUpdate();
                        transaction.Commit();
                        return res;
                    }
                }
               
            }
            catch (Exception e)
            {
                if (transaction != null)
                {
                    transaction.RollbackAsync();
                }
                throw e;
            }
        }
      /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="size">每页记录</param>
        /// <returns></returns>
        public IEnumerable<T> Find<T>(string sql, int size) where T:class
        {  
            using (IStatelessSession session = NhibernateHelper.Session)
            {
               return session.CreateQuery(sql).SetMaxResults(size).List<T>();
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public T Find<T>(string sql,string[] param) where T:class
        {
            using (IStatelessSession session = NhibernateHelper.Session)
            {
                IQuery query=session.CreateQuery(sql);
                for (int i = 0; i < param.Length; i++)
	            {
                    query=query.SetString(i, param[i]);
	            }
                return query.List<T>()[0];
            }
        }
#region linq https://nhibernate.info/previous-doc/v5.1/ref/querylinq.html
        public int Count<T>(IStatelessSession session, Expression<Func<T, bool>> where) where T : class
        {
            NHibernate.IFutureValue<int> value = Filter(session.Query<T>(),where).ToFutureValue(it => it.Count());
            return value.Value;
        }
        public IList<T> List<T>(IStatelessSession session, Expression<Func<T, bool>> where,string entityName="") where T : class
        {
            return string.IsNullOrEmpty(entityName) ? Filter(session.Query<T>(), where).ToList(): Filter(session.Query<T>(entityName), where).ToList();
        }
        public IQueryable<T> Query<T>(IStatelessSession session, Expression<Func<T, bool>> where, string entityName = "") where T : class
        {
            return string.IsNullOrEmpty(entityName) ? Filter(session.Query<T>(), where) : Filter(session.Query<T>(entityName), where);
        }
        public int Delete<T>(IStatelessSession session, Expression<Func<T, bool>> where) where T : class
        {
            return Filter(session.Query<T>(), where).Delete();
        }
        public async Task<int> DeleteAsync<T>(IStatelessSession session, Expression<Func<T, bool>> where, System.Threading.CancellationToken cancellation= default(System.Threading.CancellationToken)) where T : class
        {
             return await Filter(session.Query<T>(), where).DeleteAsync(cancellation);
        }
        public async Task<int> UpdateAsync<T>(IStatelessSession session, Expression<Func<T, bool>> where, Expression<Func<T, T>> update, System.Threading.CancellationToken cancellation = default(System.Threading.CancellationToken)) where T : class
        {
            return await session.Query<T>().Where(where).UpdateAsync(update,cancellation);
        }
        public int Update<T>(IStatelessSession session, Expression<Func<T, bool>> where,Expression<Func<T,T>> update) where T : class
        {
            return Filter(session.Query<T>(), where).Update(update);
        }

#endregion linq https://nhibernate.info/previous-doc/v5.1/ref/querylinq.html

#endregion IStatelessSession
    }
}
#endif