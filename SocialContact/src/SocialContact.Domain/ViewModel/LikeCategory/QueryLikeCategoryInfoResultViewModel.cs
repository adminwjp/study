using SocialContact.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryLikeCategoryInfoResultViewModel:Core.DefaultEntry,ICasecade<QueryLikeCategoryInfoResultViewModel>
    {
        public virtual AdminEntry Admin { get; set; }
        public virtual QueryLikeCategoryInfoResultViewModel Parent { get; set; }
        public virtual ISet<QueryLikeCategoryInfoResultViewModel> Children { get; set; }

        public object Clone()
        {
            return null;
        }
    }
}
