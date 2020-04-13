using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    [System.ComponentModel.DataAnnotations.Schema.Table("work_category_info")]
    public class WorkCategoryInfo : BaseName
    {
        [System.ComponentModel.DataAnnotations.Schema.Column("filter")]
        [System.ComponentModel.DataAnnotations.StringLength(30)]
        public string Filter { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("work_id")]
        public WorkInfo Work { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public string Src { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
       [BindProperty(Name ="work_id")]
        public int? WorkId { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("parent_id")]
        public WorkCategoryInfo Parent { get; set; }
        [BindProperty(Name = "parent_id")]
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int? ParentId { get; set; }
        public IList<WorkCategoryInfo> Children { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.ForeignKey("admin_id")]
        public AdminInfo Admin { get; set; }
    }
}
