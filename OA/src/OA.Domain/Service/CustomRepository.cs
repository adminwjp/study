using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Utility.Database.Service
{
    public class CustomRepository<T> : IRepository<T> where T : class
    {
        public object DbContext => throw new NotImplementedException();

        public IDbConnection Connection => throw new NotImplementedException();

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void BatchAdd(T[] entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<T, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public int ExecuteSql(string sql)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> exp = null)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> Find(int pageindex = 1, int pagesize = 10, string orderby = "", Expression<Func<T, bool>> exp = null)
        {
            throw new NotImplementedException();
        }

        public T FindSingle(Expression<Func<T, bool>> exp = null)
        {
            throw new NotImplementedException();
        }

        public int GetCount(Expression<Func<T, bool>> exp = null)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(Expression<Func<T, bool>> exp)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Expression<Func<T, bool>> where, Expression<Func<T, T>> entity)
        {
            throw new NotImplementedException();
        }
    }
}
