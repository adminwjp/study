using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.Core
{
    public class UserMenuInfo:Entry
    {
        public virtual MenuInfo Menu { get; set; }
        public virtual AdminInfo Admin { get; set; }
        public virtual AdminRoleInfo Role { get; set; }
        public virtual UserLevelInfo Level { get; set; }
        public virtual bool Add { get; set; } = true;
        public virtual bool Modify { get; set; } = true;
        public virtual bool Delete { get; set; } = true;
        public virtual bool Query { get; set; } = true;
        public virtual bool Enable { get; set; } = true;
        public string Type { get; set; }
        public bool Val { get; set; }
        public void Clear()
        {
            if (this.Role != null)
            {
                this.Role.Admin = null;
                this.Role.Parent = null;
                this.Role.Children = null;
                this.Role.Admins = null;
            }
            if (this.Level != null)
                this.Level.Admin = null;
            if (this.Menu != null)
            {
                this.Menu.Admin = null;
                this.Menu.Menu = null;
                this.Menu.Admin = null;
                this.Menu.Icon = null;
                this.Menu.Parent = null;
                this.Menu.Children = null;
            }
        }
    }
}
