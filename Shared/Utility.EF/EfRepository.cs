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
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public  class EfRepository<T> : IDisposable,IRepository<T> where T : class
   {
        /// <summary>
        ///  构造函数
        /// </summary>
        //public EfRepository()
        //{
        //    throw new NotSupportedException();
        //}
        private DbContext _context;//数据库上下文
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="context">数据库上下文</param>
        public EfRepository(DbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// 数据库上下文
        /// </summary>

        object IRepository<T>.DbContext { get { return _context; } }

        /// <summary>
        /// 根据过滤条件，获取记录
        /// </summary>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        public IQueryable<T> Find(Expression<Func<T, bool>> exp = null)
        {
            return Filter(exp);
        }
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        public bool IsExist(Expression<Func<T, bool>> exp)
        {
            return exp==null? _context.Set<T>().Any() : _context.Set<T>().Any(exp);
        }

        /// <summary>
        /// 查找单个，且不被上下文所跟踪
        /// </summary>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        public T FindSingle(Expression<Func<T, bool>> exp)
        {
            return exp==null? _context.Set<T>().AsNoTracking().FirstOrDefault(): _context.Set<T>().AsNoTracking().FirstOrDefault(exp);
        }

        /// <summary>
        /// 得到分页记录
        /// </summary>
        /// <param name="pageindex">页数</param>
        /// <param name="pagesize">记录</param>
        /// <param name="orderby">排序</param>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        public IQueryable<T> Find(int pageindex, int pagesize, string orderby = "", Expression<Func<T, bool>> exp = null)
        {
            if (pageindex < 1) pageindex = 1;
            if (string.IsNullOrEmpty(orderby))
                orderby = "Id descending";
            return Filter(exp)/*.OrderBy(it=>it.Id)*/.Skip(pagesize * (pageindex - 1)).Take(pagesize).AsQueryable();
        }

        /// <summary>
        /// 根据过滤条件获取记录数
        /// </summary>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        public int GetCount(Expression<Func<T, bool>> exp = null)
        {
            return Filter(exp).Count();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        public void Add(T entity)
        { 
            _context.Set<T>().Add(entity);
            Save();
            _context.Entry(entity).State = EntityState.Detached;
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entities">The entities.</param>
        public void BatchAdd(T[] entities)
        {
            _context.Set<T>().AddRange(entities);
            Save();
        }
        /// <summary>
        /// 更新一个实体的所有属性
        /// </summary>
        /// <param name="entity">实体</param>
        public void Update(T entity)
        {
            var entry = this._context.Entry(entity);
            //更新时可能出现异常 bug 
            entry.State = EntityState.Modified;
            //如果数据没有发生变化
            if (!this._context.ChangeTracker.HasChanges())
            {
                return;
            }
            //此处则没更新
           // entry.State = EntityState.Modified;
            Save();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            Save();
        }
        /// <summary>
        /// 实现按需要只更新部分更新
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// <para>
        /// support netcoreapp 2.0 - 3.0 or netstandard 2.0
        /// </para>
        /// </summary>
        /// <param name="where">The where.</param>
        /// <param name="entity">The entity.</param>
        public void Update(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity)
        {
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1  || NETSTANDARD2_0
          _context.Set<T>().Where(where).Update(entity);
#endif
        }
        /// <summary>
        /// 批量删除
        /// <para>
        /// support netcoreapp 2.0 - 3.0 or netstandard 2.0
        /// </para>
        /// </summary>
        /// <param name="exp">条件</param>
        public virtual void Delete(Expression<Func<T, bool>> exp)
        {
#if NETCOREAPP2_0 || NETCOREAPP2_1 || NETCOREAPP2_2 || NETCOREAPP3_0 || NETCOREAPP3_1 || NETSTANDARD2_0
            _context.Set<T>().Where(exp).Delete();
#endif
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
        private IQueryable<T> Filter(Expression<Func<T, bool>> exp)
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
            return  _context.Database.ExecuteSqlCommand(sql);
        } 
        void IDisposable.Dispose(){
            _context.Dispose();
        }
    }
}
#endif