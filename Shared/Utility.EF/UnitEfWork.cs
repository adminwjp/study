#if true//NETCOREAPP3_0 || NETSTANDARD2_1
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using Utility;
using Z.EntityFramework.Plus;

namespace Utility.EF
{
    /// <summary>
    /// 工作单元接口
    /// <para> 适合在一下情况使用:</para>
    /// <para>1 在同一事务中进行多表操作</para>
    /// <para>2 需要多表联合查询</para>
    /// <para>因为架构采用的是EF访问数据库，暂时可以不用考虑采用传统Unit Work的注册机制</para>
    /// </summary>
    public class UnitEfWork: IUnitWork
    {
        /// <summary>
        ///  构造函数
        /// </summary>
        //public UnitEfWork()
        //{
        //    throw new NotSupportedException();
        //}
        private DbContext _context;//数据库上下文
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="context">数据库上下文</param>
       public UnitEfWork(DbContext context)
       {
           _context = context;
       }
        /// <summary>
        /// 数据库上下文
        /// </summary>
       public DbContext DbContext { get { return _context; } }
        /// <summary>
        /// 根据过滤条件，获取记录
        /// </summary>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        public IQueryable<T> Find<T>(Expression<Func<T, bool>> exp = null) where T : class 
        {
            return Filter(exp);
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        public bool IsExist<T>(Expression<Func<T, bool>> exp) where T : class
        {  
            return exp==null? _context.Set<T>().Any() : _context.Set<T>().Any(exp);
        }
        /// <summary>
        /// 查找单个，且不被上下文所跟踪
        /// </summary>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        public T FindSingle<T>(Expression<Func<T, bool>> exp) where T:class
        {
            return exp == null ? _context.Set<T>().AsNoTracking().FirstOrDefault() : _context.Set<T>().AsNoTracking().FirstOrDefault(exp);
        }

        /// <summary>
        /// 得到分页记录
        /// </summary>
        /// <param name="pageindex">页数</param>
        /// <param name="pagesize">记录</param>
        /// <param name="orderby">排序</param>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        public IQueryable<T> Find<T>(int pageindex, int pagesize, string orderby = "", Expression<Func<T, bool>> exp = null) where T : class
        {
            if (pageindex < 1) pageindex = 1;
            if (string.IsNullOrEmpty(orderby))
                orderby = "Id descending";

            return Filter(exp)/*.OrderBy(orderby)*/.Skip(pagesize * (pageindex - 1)).Take(pagesize);
        }

        /// <summary>
        /// 根据过滤条件获取记录数
        /// </summary>
        public int GetCount<T>(Expression<Func<T, bool>> exp = null) where T : class
        {
            return Filter(exp).Count();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        public object Add<T>(T entity) where T : class
        { 
            _context.Set<T>().Add(entity);
            return null;
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void BatchAdd<T>(T[] entities) where T : class
        {
            _context.Set<T>().AddRange(entities);
        }
        /// <summary>
        /// 更新一个实体的所有属性
        /// </summary>
        /// <param name="entity">实体</param>
        public void Update<T>(T entity) where T : class
        {
            var entry = this._context.Entry(entity);
            entry.State = EntityState.Modified;

            //如果数据没有发生变化
            if (!this._context.ChangeTracker.HasChanges())
            {
                entry.State = EntityState.Unchanged;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        public void Delete<T>(T entity) where T:class
        { 
            _context.Set<T>().Remove(entity);
        }
        /// <summary>
        /// 实现按需要只更新部分更新
        /// <para>
        ///support netcoreapp 2.0 - 3.0 or netstandard 2.0
        /// </para>
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="entity">The entity.</param>
        public void Update<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity) where T:class
        {
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_0
             _context.Set<T>().Where(where).Update(entity);
#endif
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="exp">条件</param>
        public virtual void Delete<T>(Expression<Func<T, bool>> exp) where T : class
        { 
            _context.Set<T>().RemoveRange(Filter(exp));
        }
        /// <summary>
        /// 操作成功 保存到库里
        /// </summary>
        public void Save()
        {
            //try
            //{
            _context.SaveChanges();
            //}
            //catch (DbEntityValidationException e)
            //{
            //    throw new Exception(e.EntityValidationErrors.First().ValidationErrors.First().ErrorMessage);
            //}
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        private IQueryable<T> Filter<T>(Expression<Func<T, bool>> exp) where T : class
        {
            var dbSet = _context.Set<T>().AsNoTracking().AsQueryable();
            if (exp != null)
                dbSet = dbSet.Where(exp);
            return dbSet;
        }

        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        [Obsolete]
        public int ExecuteSql(string sql)
        {
            return _context.Database.ExecuteSqlCommand(sql);
        }
        void IDisposable.Dispose(){
            _context.Dispose();
        }

        public void Delete<T>(object id) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
#endif