using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class CategoryViewModel
    {
        public int Value { get; set; }
        public string Label { get; set; }
        public ISet<CategoryViewModel> Children { get; set; }
    }
}
