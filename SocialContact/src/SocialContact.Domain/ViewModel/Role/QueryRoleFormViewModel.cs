using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryRoleFormViewModel:DefaultQueryEntry
    {
        [BindProperty(Name = "role_id")]
        //[BindProperty(Name ="parend_id")]
        public int? ParentId { get; set; }
    }
}
