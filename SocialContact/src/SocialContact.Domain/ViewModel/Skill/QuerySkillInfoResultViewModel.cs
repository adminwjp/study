using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QuerySkillInfoResultViewModel : Core.Entry
    {
        public QuerySkillCategoryInfoResultViewModel Category { get; set; }
        public UserEntry User { get; set; }
    }
}
