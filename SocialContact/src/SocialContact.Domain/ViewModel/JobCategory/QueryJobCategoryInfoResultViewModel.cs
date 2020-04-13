using SocialContact.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryJobCategoryInfoResultViewModel : Core.DefaultEntry,ICasecade<QueryJobCategoryInfoResultViewModel>
    {
        public virtual AdminEntry Admin { get; set; }
        public virtual QueryJobCategoryInfoResultViewModel Parent { get; set; }
        public virtual ISet<QueryJobCategoryInfoResultViewModel> Children { get; set; }

        public object Clone()
        {
            return null;
        }
    }
}
