using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    [System.ComponentModel.DataAnnotations.Schema.Table("user_info")]
    public class UserInfo:BaseUserInfo
    {
        [System.ComponentModel.DataAnnotations.Schema.Column("city")]
        [System.ComponentModel.DataAnnotations.StringLength(20)]
        public string City { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("file")]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string File { get; set; }
    }
}
