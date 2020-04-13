using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryMenuInfoResultViewModel:Core.Entry,System.ICloneable
    {
        public  string MenuName { get; set; }
        public IconViewModel Icon { get; set; }
        public  string MenuGroup { get; set; }
        public  string Href { get; set; }
        public  bool Collpse { get; set; }
        public AdminEntry Admin { get; set; }
        public  string Description { get; set; }
        public  ISet<QueryMenuInfoResultViewModel> Children { get; set; }
        public QueryMenuInfoResultViewModel Parent { get; set; }
        public object Clone()
        {
            return new QueryMenuInfoResultViewModel()
            {
                Id=this.Id,
                MenuName = this.MenuName,
                MenuGroup=this.MenuGroup,
                Href=this.Href,
                Collpse=this.Collpse,
                Description=this.Description
            };
        }
    }
}
