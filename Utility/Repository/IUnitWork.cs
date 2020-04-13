#if !NET20 && !NET30 && !NET35
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Utility
{
    /// <summary>
    /// 工作单元接口
    /// <para> 适合在一下情况使用:</para>
    /// <para>1 在同一事务中进行多表操作</para>
    /// <para>2 需要多表联合查询</para>
    /// <para>因为架构采用的是EF,Nhibernate访问数据库，暂时可以不用考虑采用传统Unit Work的注册机制</para>
    /// </summary>
    public interface IUnitWork:IDisposable
    {
        IDbConnection Connection { get;  }
        /// <summary>
        /// 数据库上下文
        /// </summary>
        //DbContext DbContext { get; }
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        T FindSingle<T>(Expression<Func<T, bool>> exp = null) where T:class;
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        bool IsExist<T>(Expression<Func<T, bool>> exp) where T : class;
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        IQueryable<T> Find<T>(Expression<Func<T, bool>> exp = null) where T : class;
        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="pageindex">页数</param>
        /// <param name="pagesize">记录</param>
        /// <param name="orderby">排序</param>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        IQueryable<T> Find<T>(int pageindex = 1, int pagesize = 10, string orderby = "",
            Expression<Func<T, bool>> exp = null) where T : class;
        /// <summary>
        /// 查询数量
        /// </summary>
        /// <param name="exp">条件</param>
        /// <returns></returns>
        int GetCount<T>(Expression<Func<T, bool>> exp = null) where T : class;
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        object Add<T>(T entity) where T : class;
        /// <summary>
        ///批量 添加
        /// </summary>
        /// <param name="entities">实体</param>
        void BatchAdd<T>(T[] entities) where T : class;

        /// <summary>
        /// 更新一个实体的所有属性
        /// </summary>
        /// <param name="entity">实体</param>
        void Update<T>(T entity) where T : class;
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        void Delete<T>(T entity) where T : class;
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">id</param>
        void Delete<T>(object id) where T : class;

        /// <summary>
        /// 实现按需要只更新部分更新
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="where">更新条件</param>
        /// <param name="entity">更新后的实体</param>
        void Update<T>(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity) where T : class;
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="exp">条件</param>
        void Delete<T>(Expression<Func<T, bool>> exp) where T : class;
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
