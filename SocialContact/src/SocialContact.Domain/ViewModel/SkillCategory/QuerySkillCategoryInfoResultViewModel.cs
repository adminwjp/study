using SocialContact.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QuerySkillCategoryInfoResultViewModel : Core.DefaultEntry,ICasecade<QuerySkillCategoryInfoResultViewModel>
    {
        public virtual AdminEntry Admin { get; set; }
        public virtual QuerySkillCategoryInfoResultViewModel Parent { get; set; }
        public virtual ISet<QuerySkillCategoryInfoResultViewModel> Children { get; set; }

        public object Clone()
        {
            return null;
        }
    }
}
