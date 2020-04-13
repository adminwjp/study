
using SocialContact.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SocialContact.Domain.Core
{
    public class AdminRoleInfo : DefaultEntry, ICasecade<AdminRoleInfo>,IAdmin
    {
        [BindProperty(Name ="role")]
        public virtual AdminRoleInfo Parent { get; set; }
        public virtual AdminInfo  Admin { get; set; }
        public virtual ISet<AdminInfo> Admins { get; set; }
        public virtual ISet<AdminRoleInfo>  Children { get; set; }

        public object Clone()
        {
            return new AdminRoleInfo()
            {
                Id = this.Id,
                CreateDate=this.CreateDate,
                Category=this.Category,
                Description=this.Description
            };
        }
    }
}
