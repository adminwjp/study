using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    [System.ComponentModel.DataAnnotations.Schema.Table("admin_info")]
    public  class AdminInfo:BaseUserInfo
    {
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("role_id")]
        public AdminRoleInfo Role { get; set; }

    }
}
