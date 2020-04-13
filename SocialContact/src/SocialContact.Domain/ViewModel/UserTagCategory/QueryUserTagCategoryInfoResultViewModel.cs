using SocialContact.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryUserTagCategoryInfoResultViewModel : Core.DefaultEntry,ICasecade<QueryUserTagCategoryInfoResultViewModel>
    {
        public QueryUserTagCategoryInfoResultViewModel Parent { get; set; }
        public  AdminEntry Admin { get; set; }
        public  ISet<QueryUserTagCategoryInfoResultViewModel> Children { get; set; }

        public object Clone()
        {
            return null;
        }
    }
}
