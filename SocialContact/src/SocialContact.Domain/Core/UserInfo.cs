namespace SocialContact.Domain.Core{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class UserInfo : Entry {
        [Utility.Attributes.Required(Message = "�������˻�")]
        [Utility.Attributes.Range(Min =5,Max =15,Message = "�����볤���� 5 �� 15 ���ַ��˻�")]
        public virtual string Account { get; set; }
        [Utility.Attributes.Required(Message = "����������")]
        [Utility.Attributes.Range(Min = 6, Max = 20, Message = "�����볤���� 6 �� 20 ���ַ�����")]
        public virtual string Password { get; set; }
        [Utility.Attributes.Required(Message = "�������ǳ�")]
        [Utility.Attributes.Range(Min = 2, Max = 10, Message = "�����볤���� 2 �� 10 ���ַ��ǳ�")]
        [FromForm(Name ="nick_name")]
        public virtual string NickName { get; set; }
        [Utility.Attributes.Required(Message = "����������")]
        [Utility.Attributes.Range(Min = 2, Max = 10, Message = "�����볤���� 2 �� 10 ���ַ�����")]
        [FromForm(Name = "real_name")]
        public virtual string RealName { get; set; }
        [FromForm(Name = "head_pic")]
        public virtual UserFileInfo HeadPic { get; set; }
        [Utility.Attributes.Required(Message = "��ѡ������")]
        public virtual DateTime? Birthday { get; set; }
        [Utility.Attributes.Required(Message = "�������ֻ���")]
        [Utility.Attributes.Range(Min = 11, Max = 11, Message = "�����볤���� 11 �������ֻ���")]
        public virtual string Phone { get; set; }
        [Utility.Attributes.Required(Message = "��ѡ���Ա�")]
        public virtual string Sex { get; set; }
        public virtual EdutionCategoryInfo Edution { get;set; }
        public virtual MaritalStatusInfo  Marital { get; set; }
        [Utility.Attributes.Required(Message = "����������")]
        [Utility.Attributes.Range(Min = 10, Max = 500, Message = "�����볤���� 10 �� 500 ���ַ�����")]
        public virtual string Description { get; set; }
        [Utility.Attributes.Required(Message = "����������")]
        [Utility.Attributes.Range(Min = 10, Max = 20, Message = "�����볤���� 10 �� 20 ���ַ�����")]
        public virtual string Email { get; set; }
        [Utility.Attributes.Required(Message = "���������֤��")]
        [Utility.Attributes.Range(Min = 17, Max = 18, Message = "�����볤���� 17 �� 18 ���ַ����֤��")]
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
        [Utility.Attributes.Required(Message = "���������п���")]
        [Utility.Attributes.Range(Min = 20, Max = 20, Message = "�����볤���� 20 λ�������п���")]

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