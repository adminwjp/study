using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.Core
{
    public class WorkInfo:Entry
    {
        [Utility.Required(Message = "请输入公司名称")]
        [Utility.Range(Min = 5, Max = 50, Message = "长度在 5 到 50 个字符公司名称")]
        [BindProperty(Name ="company_name")]
        public virtual string CompanyName { get; set; }
        [Utility.Required(Message = "请输入公司职位")]
        [Utility.Range(Min = 2, Max = 50, Message = "长度在 2 到 50 个字符公司职位")]
        public virtual string Job { get; set; }
        public virtual JobCategoryInfo Category { get; set; }
        [Utility.Required(Message = "请输入描述")]
        [Utility.Range(Min = 10, Max = 500, Message = "长度在 10 到 500 个字符描述")]
        public virtual string Description { get; set; }
        [Utility.Required(Message = "请选择工作开始时间")]
        [BindProperty(Name = "start_date")]

        public virtual DateTime? StartDate { get; set; }
        [Utility.Required(Message = "请选择工作结束时间")]
        [BindProperty(Name = "end_date")]
        public virtual DateTime? EndDate { get; set; }
        public virtual UserInfo User { get; set; }

    }
}
