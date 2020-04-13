using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryUserInfoResultViewModel : Core.Entry
    {
        public  string Account { get; set; }
        public  string Password { get; set; }
        [FromForm(Name = "nick_name")]
        public  string NickName { get; set; }
        [FromForm(Name = "real_name")]
        public virtual string RealName { get; set; }
        [FromForm(Name = "head_pic")]
        public string HeadPic { get; set; }
        public  DateTime? Birthday { get; set; }
        public  string Phone { get; set; }
        public  string Sex { get; set; }
        public string Edution { get; set; }
        public string Marital { get; set; }
        public  string Description { get; set; }
        public  string Email { get; set; }
        [FromForm(Name = "card_id")]
        public  string CardId { get; set; }
        [FromForm(Name = "card_photo1")]
        public string CardPhoto1 { get; set; }
        [FromForm(Name = "card_photo2")]
        public string CardPhoto2 { get; set; }
        [FromForm(Name = "hand_card_photo1")]
        public string HandCardPhoto1 { get; set; }
        [FromForm(Name = "hand_card_photo2")]
        public string HandCardPhoto2 { get; set; }
        [FromForm(Name = "card_photo_status")]
        public  bool CardPhotoStatus { get; set; }

        [FromForm(Name = "bank_id")]
        public  string BankId { get; set; }
        public  string Status { get; set; }
        public  int? FailCount { get; set; }
        [FromForm(Name = "login_ip")]
        public  string LoginIp { get; set; }
        public string Level { get; set; }

    }
}
