using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryLikeInfoResultViewModel:Core.Entry
    {
        public  QueryLikeCategoryInfoResultViewModel Category { get; set; }
        public UserEntry User { get; set; }
    }
}
