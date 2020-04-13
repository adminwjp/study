using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryUserFileFormViewModel:QueryEntry
    {
        public  string Src { get; set; }

        public int? CategoryId { get; set; }
    }
}
