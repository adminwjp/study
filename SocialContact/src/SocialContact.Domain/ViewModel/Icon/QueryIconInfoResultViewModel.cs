using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryIconInfoResultViewModel : Core.Entry
    {
        public  string Name { get; set; }
        public string Style { get; set; }
        public string ShowStyle => !string.IsNullOrEmpty(Style) ? $" <i class=\"{Style}\"></i>" : string.Empty;
        public  string Description { get; set; }
        public AdminEntry Admin { get; set; }
    }
}
