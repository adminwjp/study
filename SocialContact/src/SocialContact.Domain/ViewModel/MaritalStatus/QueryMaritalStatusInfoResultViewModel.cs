using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryMaritalStatusInfoResultViewModel : Core.Entry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AdminEntry Admin { get; set; }
        public UserEntry User { get; set; }
    }
}
