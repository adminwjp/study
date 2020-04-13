using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    public abstract class BaseDesc:BaseName
    {
        [System.ComponentModel.DataAnnotations.Schema.Column("description")]
        [System.ComponentModel.DataAnnotations.StringLength(500)]
        public string Description { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("english_description")]
        [System.ComponentModel.DataAnnotations.StringLength(500)]
        [FromForm(Name = "english_description")]
        public string EnglishDescription { get; set; }
    }
}
