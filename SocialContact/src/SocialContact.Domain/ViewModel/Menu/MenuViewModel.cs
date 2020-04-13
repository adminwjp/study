using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class MenuViewModel
    {
        public int Value { get; set; }
        public string Label { get; set; }
        public string Style { get; set; }
        public string Href { get; set; }
        public string Group { get; set; }
        public ISet<MenuViewModel> Children { get; set; }
        
    }
}
