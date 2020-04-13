using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryWorkFormViewModel : QueryEntry
    {
        [BindProperty(Name = "company_name")]
        public  string CompanyName { get; set; }
        public  string Job { get; set; }
        [BindProperty(Name = "category_id")]
        public int? CategoryId { get; set; }
        [BindProperty(Name = "work_date")]
        public  DateTime[] WorkDate { get; set; }
        [BindProperty(Name = "user_id")]
        public int? UserId { get; set; }
    }
}
