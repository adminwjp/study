using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Core
{
    /// <summary>
    /// 个人信息
    /// </summary>
    [Class(Table = "person_info", NameType = typeof(PersonInfo), Lazy = false)]
    public class PersonInfo:BaseEntity
    {
        [Property(Column = "qq",  Length = 15)]
        public string QQ { get; set; }
        [Property(Column = "eamil", Length = 20)]
        public string Email { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        [Property(Column = "handset", Length = 15)]
        public string Handset { get; set; }
        /// <summary>
        /// 手机号
        /// </summary>
        [Property(Column = "telphone", Length = 15)]
        public string Telphone { get; set; }
        [Property(Column = "address", Length = 100)]
        public string Address { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        [Property(Column = "postlacode", Length = 5)]
        public string Postlacode { get; set; }
        /// <summary>
        /// 第一次学龄
        /// </summary>
        [Property(Column = "second_school_age", Length = 10)]
        public string SecondSchoolAge { get; set; }
        /// <summary>
        /// 二次专业
        /// </summary>
        [Property(Column = "second_speciaity", Length = 40)]
        public string SecondSpeciaity { get; set; }
        /// <summary>
        /// 大学学校
        /// </summary>
        [Property(Column = "graduate_school", Length = 40)]
        public string GraduateSchool { get; set; }
        /// <summary>
        /// 大学就读时间
        /// </summary>
        [Property(Column = "graduate_date")]
        public DateTime GraduateDate { get; set; }
        /// <summary>
        /// 入党时间
        /// </summary>
        [Property(Column = "party_member_date")]
        public DateTime PartyMemberDate { get; set; }
        /// <summary>
        /// 计算机等级
        /// </summary>
        [Property(Column = "computer_grate",Length =10)]
        public string ComputerGrate { get; set; }
        [Property(Column = "likes", Length = 50)]
        public string Likes { get; set; }
        /// <summary>
        /// 强项  特长
        /// </summary>
        [Property(Column = "ones_strong_suit", Length = 50)]
        public string OnesStrongSuit { get; set; }
        public int UserId { get; set; }
        [ManyToOne(Column = "user_id", ClassType = typeof(UserInfo))]
        public UserInfo User { get; set; }
    }
}
