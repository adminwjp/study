using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryUserFileInfoResultViewModel:Core.Entry
    {
        public  string FileId { get; set; }
        public string Src { get; set; }
        public QueryFileCategoryInfoResultViewModel Category { get; set; }
        public AdminEntry Admin { get; set; }
        public UserEntry User { get; set; }
    }
}
