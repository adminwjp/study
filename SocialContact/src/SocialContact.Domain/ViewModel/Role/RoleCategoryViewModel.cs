using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class RoleCategoryViewModel
    {
        public int Value { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public ISet<RoleCategoryViewModel> Children { get; set; }
    }
}
