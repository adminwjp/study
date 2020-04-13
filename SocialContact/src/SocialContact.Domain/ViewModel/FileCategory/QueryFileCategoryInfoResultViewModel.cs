using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryFileCategoryInfoResultViewModel:Core.DefaultEntry
    {
        public AdminEntry Admin { get; set; }
        public QueryUserFileInfoResultViewModel File { get; set; }
        public  string Accept { get; set; }
    }
}
