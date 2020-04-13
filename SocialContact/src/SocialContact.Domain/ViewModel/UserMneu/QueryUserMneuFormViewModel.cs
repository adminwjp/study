using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryUserMneuFormViewModel : QueryEntry
    {
        [BindProperty(Name ="role_id")]
        public int? RoleId { get; set; }
        [BindProperty(Name = "menu_id")]
        public int? MentId { get; set; }
        [BindProperty(Name = "level_id")]
        public int? LevelId { get; set; }
    }
}
