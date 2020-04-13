using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Repository;
using Dapper;
using System.Data;

namespace Infrastructure.Service
{
    public class UserDapperRepsitory : IUserRepository
    {
        public UserDapperRepsitory(IDbConnection connection) => this.Connection = connection;
        public IDbConnection Connection { get; set; }
    }
}
