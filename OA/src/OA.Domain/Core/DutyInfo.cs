using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace OA.Domain.Core
{
    /// <summary>
    /// 职位信息
    /// </summary>
    [Class(Table = "duty_info", Name = "DutyInfo", NameType = typeof(DutyInfo), Lazy = false)]
    public class DutyInfo:BaseEntity
    {
        [Property(Column = "accession_date")]
        /// <summary>
        /// 加入时间
        /// </summary>
        public DateTime AccessionDate { get; set; }
        /// <summary>
        /// 离开时间
        /// </summary>
        [Property(Column = "dimission_date")]
        public DateTime DimissionDate { get; set; }
        /// <summary>
        /// 离职原因
        /// </summary>
        [Property(Column = "dimission_reason")]
        public string DimissionReason { get; set; }
        /// <summary>
        /// 第一次工作(签条约)时间
        /// </summary>
        [Property(Column = "first_pact_date")]
        public DateTime FirstPactDate { get; set; }
        /// <summary>
        /// 第一次工作(签条约)开始时间
        /// </summary>
        [Property(Column = "pact_start_date")]
        public DateTime PactStartDate { get; set; }
        /// <summary>
        /// 第一次工作(签条约)结束时间
        /// </summary>
        [Property(Column = "pact_end_date")]
        public DateTime PactEndDate { get; set; }
        [Property(Column = "bank_name", Length =50)]
        public string BankName { get; set; }
        [Property(Column = "bank_id", Length = 20)]
        public string BandId { get; set; }
        /// <summary>
        /// 社会安全()编号
        /// </summary>
        [Property(Column = "society_safefy_no", Length = 20)]
        public string SocietySafefyNo { get; set; }
        /// <summary>
        /// 年保险金(养老金)编号
        /// </summary>
        [Property(Column = "annuity_safefy_no", Length = 20)]
        public string AnnuitySafefyNo { get; set; }
        /// <summary>
        /// 失业救助金编号
        /// </summary>
        [Property(Column = "dole_safefy_no", Length = 20)]
        public string DoleSafefyNo { get; set; }
        /// <summary>
        /// 医疗保险金编号
        /// </summary>
        [Property(Column = "medicare_safefy_no", Length = 20)]
        public string MedicareSafefyNo { get; set; }
        /// <summary>
        /// 工伤赔偿费编号
        /// </summary>
        [Property(Column = "compo_safefy_no", Length = 20)]
        public string CompoSafefyNo { get; set; }
        /// <summary>
        /// 公积金 编号
        /// </summary>
        [Property(Column = "acoumulation_fund_no", Length = 20)]
        public string AcoumulationFundNo { get; set; }
    }
}
