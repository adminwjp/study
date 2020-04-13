using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryUserMneuInfoResultViewModel : Core.DefaultEntry
    {
        public AdminEntry Admin { get; set; }
        public RoleCategoryViewModel Role { get; set; }
        public QueryUserLevelInfoResultViewModel Level {get;set;}
        public  bool Add { get; set; } = true;
        public  bool Modify { get; set; } = true;
        public  bool Delete { get; set; } = true;
        public  bool Query { get; set; } = true;
        public  bool Enable { get; set; } = true;
        public QueryMenuInfoResultViewModel Menu { get; set; }
    }
}
