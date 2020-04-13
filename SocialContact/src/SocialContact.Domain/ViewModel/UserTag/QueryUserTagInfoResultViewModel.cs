using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryUserTagInfoResultViewModel : Core.Entry
    {
        public UserEntry User { get; set; }
        public QueryUserTagCategoryInfoResultViewModel Category { get; set; }
    }
}
