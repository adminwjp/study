using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryRoleInfoResultViewModel:Core.DefaultEntry
    {
        public AdminEntry Admin { get; set; }
        public ISet<AdminEntry> Admins { get; set; }
        public QueryRoleInfoResultViewModel Parent { get; set; }
        public ISet<QueryRoleInfoResultViewModel> Children { get; set; }
    }
}
