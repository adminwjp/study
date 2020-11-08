using AutoMapper;
using SocialContact.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    [AutoMap(typeof(AdminRoleInfo))]
    public class CategoryEntry
    {
        public int Id { get; set; }
        public string Category { get; set; }
    }
}
