using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryJobCategoryFormViewModel : DefaultQueryEntry
    {
        public int? AdminId { get; set; }
        public int? ParentId { get; set; }
    }
}
