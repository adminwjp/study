using SocialContact.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public abstract class QueryEntry
    {
        public int? Id { get; set; }
        [BindProperty(Name = "create_date")]
        public DateTime[] CreateDate { get; set; }
        [BindProperty(Name = "update_date")]
        public DateTime[] UpdateDate { get; set; }
    }
}
