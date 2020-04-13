using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryEdutionCategoryInfoResultViewModel: Core.DefaultEntry
    {
        public  ISet<UserEntry> Users { get; set; }
        public AdminEntry Admin { get; set; }
    }
}
