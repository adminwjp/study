using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryWorkInfoResultViewModel : Core.Entry
    {
        public string CompanyName { get; set; }
        public string Job { get; set; }
        public QueryJobCategoryInfoResultViewModel Category { get; set; }
        public  string Description { get; set; }
        public  DateTime StartDate { get; set; }
        public  DateTime EndDate { get; set; }
        public  UserEntry User { get; set; }
    }
}
