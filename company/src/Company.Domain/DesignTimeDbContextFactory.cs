using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CompanyDbContext>
    {
        public CompanyDbContext CreateDbContext(string[] args)
        {
            var bulder = new DbContextOptionsBuilder<CompanyDbContext>();
            bulder.UseMySql("Database=company;Data Source=localhost;User Id=root;Password=wjp930514.;Old Guids=True;charset=utf8;");
            return new CompanyDbContext(bulder.Options);
        }
    }
}
