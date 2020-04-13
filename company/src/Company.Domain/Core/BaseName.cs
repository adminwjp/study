using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Company.Domain.Core
{
    public abstract class BaseName : BaseInfo
    {
        [System.ComponentModel.DataAnnotations.Schema.Column("name")]
        [System.ComponentModel.DataAnnotations.StringLength(10)]
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Schema.Column("english_name")]
        [System.ComponentModel.DataAnnotations.StringLength(30)]
        [FromForm(Name = "english_name")]
        public string EnglishName { get; set; }
        
    }
}
