using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryLikeFormViewModel:QueryEntry,Interfaces.ICategory
    {
       public string Category { get; set; }
    }
}
