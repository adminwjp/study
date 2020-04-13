using SocialContact.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class DefaultQueryEntry : QueryEntry, ICategory
    {
        public string Category { get; set; }
    }
}
