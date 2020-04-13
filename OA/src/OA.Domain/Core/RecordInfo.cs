using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Core
{
    /// <summary>
    /// 档案信息
    /// </summary>
    [Class(Table = "record_info", NameType = typeof(RecordInfo), Lazy = false)]
    public class RecordInfo : BaseEntity
    {
        /// <summary>
        /// 档案编号
        /// </summary>
        [Property(Column = "record_number", NotNull = true, Length = 5)]
        public string RecordNumber { get; set; }
        [Property(Column = "name", NotNull = true, Length = 10)]
        public string Name { get; set; }
        [Property(Column = "sex", NotNull = true, Length = 2)]
        public string Sex { get; set; }
        [Property(Column = "birthday", NotNull = true, TypeType = typeof(DateTime))]
        public DateTime Birthday { get; set; }
        [Property(Column = "card_no", NotNull = true, Length = 18)]
        public string CardNo { get; set; }
        [Property(Column = "photo", NotNull = true, Length = 50)]
        public string Photo { get; set; }
        /// <summary>
        /// 婚姻状况
        /// </summary>
        [Property(Column = "marital_status", NotNull = true, Length = 4)]
        public string MaritalStatus { get; set; }
        [Property(Column = "address", NotNull = true, Length = 100)]
        public string Addrees { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        [Property(Column = "postlacode", NotNull = true, Length = 5)]
        public string Postlacode { get; set; }
        /// <summary>
        /// 是否是党员
        /// </summary>
        [Property(Column = "party_member",Length =2)]
        public string PartyMember { get; set; }
        [Property(Column = "school_age")]
        public int SchoolAge { get; set; }
       /// <summary>
       /// 特长
       /// </summary>
        [Property(Column = "speciaity", NotNull = true, Length = 10)]
        public string Speciaity { get; set; }
        /// <summary>
        /// 外语
        /// </summary>
        [Property(Column = "foreign_language", NotNull = true, Length = 10)]
        public string ForeignLanguage { get; set; }
        /// <summary>
        /// 外语等级
        /// </summary>
        [Property(Column = "grate", NotNull = true, Length = 10)]
        public string Grate { get; set; }
        /// <summary>
        /// 名族
        /// </summary>
        [Property(Column = "famous_race", NotNull = true,  Length = 10)]
        public string FamousRace { get; set; }

        /// <summary>
        /// 籍贯
        /// </summary>
        [Property(Column = "native_place", NotNull = true, Length = 10)]
        public string NativePlace { get; set; }
     
        /// <summary>
        /// 政治面貌
        /// </summary>
        [Property(Column = "political_outlook", NotNull = true,  Length = 10)]
        public string PoliticalOutlook { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        [Property(Column = "education", NotNull = true, Length = 50)]
        public string Education { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        [Property(Column = "major", NotNull = true,  Length = 100)]
        public string Major { get; set; }
        /// <summary>
        /// 用工形式
        /// </summary>
        [Property(Column = "employment_form",  Length = 100)]
        public string EmploymentForm { get; set; }
        public int UserId { get; set; }
        [ManyToOne(Column = "user_id", ClassType = typeof(UserInfo))]
        public UserInfo User { get; set; }
    }
}
