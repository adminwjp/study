using AutoMapper;
using SocialContact.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    [AutoMap(typeof(AdminInfo))]
    public class QueryAdminInfoResultViewModel : Entry
    {
        public  string Account { get; set; }
        public  string Password { get; set; }
        public  string NickName { get; set; }
        public  CategoryEntry Role { get; set; }
        public  DateTime? LoginDate { get; set; }
        public  string Token { get; set; }
        public  int ExpressIn { get; set; }
        public  string RealName { get; set; }
        public  string HeadPic { get; set; }
        public  DateTime Birthday { get; set; }
        public  string Phone { get; set; }
        public  string Sex { get; set; }
        public  string Description { get; set; }
        public  string Email { get; set; }
        public ISet<QueryAdminInfoResultViewModel> Children { get; set; }
    }
}
