using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    public abstract class BaseUserInfo : BaseInfo
    {
        [System.ComponentModel.DataAnnotations.Schema.Column("account")]
        [System.ComponentModel.DataAnnotations.StringLength(10)]
        public string Account { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("phone")]
        [System.ComponentModel.DataAnnotations.MaxLength(11)]
        public string Phone { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("email")]
        [System.ComponentModel.DataAnnotations.StringLength(20)]
        public string Email { get; set; }
  
        [System.ComponentModel.DataAnnotations.Schema.Column("sex")]
        public bool Sex { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("remark")]
        [System.ComponentModel.DataAnnotations.StringLength(500)]
        public string Remark { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("english_remark")]
        [System.ComponentModel.DataAnnotations.StringLength(500)]
        [BindProperty(Name = "english_remark")]
        public string EnglishRemark { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("password")]
        [System.ComponentModel.DataAnnotations.StringLength(50)]
        public string Password { get; set; }

    }
}
