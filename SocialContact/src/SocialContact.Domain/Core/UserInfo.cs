namespace SocialContact.Domain.Core{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class UserInfo : Entry {
        [Utility.Attributes.Required(Message = "请输入账户")]
        [Utility.Attributes.Range(Min =5,Max =15,Message = "请输入长度在 5 到 15 个字符账户")]
        public virtual string Account { get; set; }
        [Utility.Attributes.Required(Message = "请输入密码")]
        [Utility.Attributes.Range(Min = 6, Max = 20, Message = "请输入长度在 6 到 20 个字符密码")]
        public virtual string Password { get; set; }
        [Utility.Attributes.Required(Message = "请输入昵称")]
        [Utility.Attributes.Range(Min = 2, Max = 10, Message = "请输入长度在 2 到 10 个字符昵称")]
        [FromForm(Name ="nick_name")]
        public virtual string NickName { get; set; }
        [Utility.Attributes.Required(Message = "请输入姓名")]
        [Utility.Attributes.Range(Min = 2, Max = 10, Message = "请输入长度在 2 到 10 个字符姓名")]
        [FromForm(Name = "real_name")]
        public virtual string RealName { get; set; }
        [FromForm(Name = "head_pic")]
        public virtual UserFileInfo HeadPic { get; set; }
        [Utility.Attributes.Required(Message = "请选择日期")]
        public virtual DateTime? Birthday { get; set; }
        [Utility.Attributes.Required(Message = "请输入手机号")]
        [Utility.Attributes.Range(Min = 11, Max = 11, Message = "请输入长度在 11 个数字手机号")]
        public virtual string Phone { get; set; }
        [Utility.Attributes.Required(Message = "请选择性别")]
        public virtual string Sex { get; set; }
        public virtual EdutionCategoryInfo Edution { get;set; }
        public virtual MaritalStatusInfo  Marital { get; set; }
        [Utility.Attributes.Required(Message = "请输入描述")]
        [Utility.Attributes.Range(Min = 10, Max = 500, Message = "请输入长度在 10 到 500 个字符描述")]
        public virtual string Description { get; set; }
        [Utility.Attributes.Required(Message = "请输入邮箱")]
        [Utility.Attributes.Range(Min = 10, Max = 20, Message = "请输入长度在 10 到 20 个字符邮箱")]
        public virtual string Email { get; set; }
        [Utility.Attributes.Required(Message = "请输入身份证号")]
        [Utility.Attributes.Range(Min = 17, Max = 18, Message = "请输入长度在 17 到 18 个字符身份证号")]
        [FromForm(Name = "card_id")]
        public virtual string CardId { get; set; }
        [FromForm(Name = "card_photo1")]
        public virtual UserFileInfo CardPhoto1 { get; set; }
        [FromForm(Name = "card_photo2")]
        public virtual UserFileInfo CardPhoto2 { get; set; }
        [FromForm(Name = "hand_card_photo1")]
        public virtual UserFileInfo HandCardPhoto1 { get; set; }
        [FromForm(Name = "hand_card_photo2")]
        public virtual UserFileInfo HandCardPhoto2 { get; set; }
        [FromForm(Name = "card_photo_status")]
        public virtual bool CardPhotoStatus { get; set; }
        [Utility.Attributes.Required(Message = "请输入银行卡号")]
        [Utility.Attributes.Range(Min = 20, Max = 20, Message = "请输入长度在 20 位数字银行卡号")]

        [FromForm(Name = "bank_id")]
        public virtual string BankId { get; set; }
        public virtual UserStatusInfo Status { get; set; }
        public virtual int? FailCount { get; set; }
        [FromForm(Name = "login_ip")]
        public  virtual string LoginIp { get; set; }
        [BindProperty(Name = "login_date")]
        public virtual DateTime? LoginDate { get; set; }
        public virtual UserLevelInfo Level { get; set; }
        //public virtual ISet<WorkInfo> Works { get; set; }
        //public virtual ISet<LikeInfo>  Likes { get; set; }
        //public virtual ISet<SkillInfo> Skills { get; set; }
        //public virtual ISet<UserTagInfo>  UserTags { get; set; }
    }

}