using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class UserEntry:Core.Entry
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string RealName { get; set; }
        public string HeadPic { get; set; }
        public string Phone { get; set; }
        public string Sex { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
    }
}
