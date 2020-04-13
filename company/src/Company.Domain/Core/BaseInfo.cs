using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    public /*abstract*/ class BaseInfo
    {
        [System.ComponentModel.DataAnnotations.Key]
        [System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated( System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        [System.ComponentModel.DataAnnotations.Schema.Column("id")]
        public int? Id { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [BindProperty(Name ="ids")]
        public int[] Ids { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("create_date",TypeName = "datetime")]
        [System.ComponentModel.DataAnnotations.Required]
        public DateTime? CreateDate { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("modify_date", TypeName = "datetime")]
        public DateTime? ModifyDate { get; set; }

        private bool _create;
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public bool Create
        {
            get { return this._create; }
            set
            {
                if (value)
                {
                    this.CreateDate = DateTime.Now;
                }
                this._create = value;
            }
        }
        [System.ComponentModel.DataAnnotations.Schema.Column("enable")]
        public bool? Enable { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [FromForm(Name ="create_start_date")]
        public DateTime? CreateStartDate { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [FromForm(Name = "modify_start_date")]
        public DateTime? ModifyStartDate { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [FromForm(Name = "create_end_date")]
        public DateTime? CreateEndDate { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        [FromForm(Name = "modify_end_date")]
        public DateTime? ModifyEndDate { get; set; }

    }
}
