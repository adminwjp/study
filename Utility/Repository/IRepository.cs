#if !NET20 && !NET30 && !NET35
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace Utility
{
    /// <summary>
    /// 通用泛型接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        IDbConnection Connection { get; }
        /// <summary>
        /// 数据库上下文
        /// </summary>
        object DbContext { get; }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        T FindSingle(Expression<Func<T, bool>> exp = null);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        bool IsExist(Expression<Func<T, bool>> exp);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        IQueryable<T> Find(Expression<Func<T, bool>> exp = null);
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="pageindex">页数</param>
        /// <param name="pagesize">记录</param>
        /// <param name="orderby">排序</param>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        IQueryable<T> Find(int pageindex = 1, int pagesize = 10, string orderby = "",
            Expression<Func<T, bool>> exp = null);
        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        int GetCount(Expression<Func<T, bool>> exp = null);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        void Add(T entity);
        /// <summary>
        ///批量 添加
        /// </summary>
        /// <param name="entities">实体</param>
        void BatchAdd(T[] entities);

        /// <summary>
        /// 更新一个实体的所有属性
        /// </summary>
        /// <param name="entity">实体</param>
        void Update(T entity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        void Delete(T entity);
        /// <summary>
        /// 实现按需要只更新部分更新
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">更新条件</param>
        /// <param name="entity">更新后的实体</param>
        void Update(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity);
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="exp">条件</param>
        void Delete(Expression<Func<T, bool>> exp);
        /// <summary>
        /// 操作成功 保存到库里
        /// </summary>
        void Save();
        /// <summary>
        /// 执行sql
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        int ExecuteSql(string sql);
    }
}
#endif