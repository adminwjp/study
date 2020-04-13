using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.ViewModel
{
    public class QueryUserFormViewModel : QueryEntry { 

        public  string Account { get; set; }
        public  string Password { get; set; }
        public string Token { get; set; }
        [FromForm(Name = "nick_name")]
        public  string NickName { get; set; }
        [FromForm(Name = "real_name")]
        public  string RealName { get; set; }
        [FromForm(Name = "birthday_date")]
        public  DateTime[] BirthdayDate { get; set; }
        public  string Phone { get; set; }
        public  string Sex { get; set; }
        [FromForm(Name = "edution_id")]
        public int? EdutionId { get; set; }
        [FromForm(Name = "marital_id")]
        public int? MaritalId { get; set; }
        public  string Email { get; set; }
        [FromForm(Name = "card_id")]
        public  string CardId { get; set; }
        [FromForm(Name = "card_photo_status")]
        public  bool CardPhotoStatus { get; set; }

        [FromForm(Name = "bank_id")]
        public virtual string BankId { get; set; }
        [FromForm(Name = "status_id")]
        public int? StatusId { get; set; }
        [FromForm(Name = "login_ip")]
        public  string LoginIp { get; set; }
        [FromForm(Name = "level_Id")]
        public int? LevelId { get; set; }
        [BindProperty(Name = "login_date")]
        public DateTime[] LoginDate { get; set; }
    }
}
