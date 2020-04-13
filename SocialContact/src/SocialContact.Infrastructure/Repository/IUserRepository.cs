using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Domain.Core;

namespace Infrastructure.Repository
{
    public interface IUserRepository
    {
        IDbConnection Connection { get; }
         bool ExistsPhone(string phone)
        {
            string sql = "select count(1) from user_info where phone=@phone";
            int result = this.Connection.ExecuteScalar<int>(sql, new { phone }, this.Connection.BeginTransaction());
            return result > 0;
        }

        bool Register(string phone, string password)
        {
            string sql = "insert into user_info(phone,password) values(@phone,@password);";
            int result = this.Connection.Execute(sql, new { phone,password }, this.Connection.BeginTransaction());
            return result > 0;
        }
        UserInfo Login(string phone, string password)
        {
            string sql ="select top 1 * from user_info phone=@phone and password=@password";
            if(DbConfig.Dialect== Utility.Dialect.MySql)
            {
                sql = "select  * from user_info phone=@phone and password=@password limit 0,1";
            }
            IDataReader reader=this.Connection.ExecuteReader(sql, new { phone, password });
            return null;
        }
        bool ExistsBankId(string bankId) {
            string sql = "select count(1) from user_info where bank_id=@bankId";
            int result = this.Connection.ExecuteScalar<int>(sql, new { bankId }, this.Connection.BeginTransaction());
            return result > 0;
        }
        bool ExistsCardId(string cardId)
        {
            string sql = "select count(1) from user_info where card_id=@cardId";
            int result = this.Connection.ExecuteScalar<int>(sql, new { cardId }, this.Connection.BeginTransaction());
            return result > 0;
        }
        bool ExistsEmail(string email)
        {
            string sql = "select count(1) from user_info where email=@email";
            int result = this.Connection.ExecuteScalar<int>(sql, new { email }, this.Connection.BeginTransaction());
            return result > 0;
        }
    }
}
