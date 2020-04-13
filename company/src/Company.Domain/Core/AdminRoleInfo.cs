using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.Domain.Core
{
    [System.ComponentModel.DataAnnotations.Schema.Table("admin_role_info")]
    public  class AdminRoleInfo:BaseDesc
    {
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        public new bool? Enable { get; set; }
        //取消不然出错 api json 请求时 
        //[System.ComponentModel.DataAnnotations.Schema.ForeignKey("admin_id")]
        //public AdminInfo Admin { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string User { 
            get {
                if (Admins != null && Admins.Any())
                {
                    return string.Join(",", Admins.Select(it => it.Account));
                }
                return string.Empty;

            } 
        }
        //注意 无法创建数据表信息  会出现问题
        //[System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [Newtonsoft.Json.JsonIgnore]
        //[System.ComponentModel.DataAnnotations.Association("role_id", "Role.Id", "Role.Id", IsForeignKey = false)]
        public ICollection<AdminInfo> Admins { get; set; }
    }
}
