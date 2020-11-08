using SocialContact.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.Core
{
    public abstract class DefaultEntry : Entry,ICategory
    {
        [Utility.Attributes.Required(Message = "请输入分类名称", EnglishMessage  = "plecase input category name ")]
        [Utility.Attributes.Range(Min = 2, Max = 10, EnglishMin = 5, EnglishMax = 20, Message = "长度在 2 到 10 个字符分类名称", EnglishMessage = "length 5 to 20 char category name")]
        public virtual string Category { get; set; }
        [Utility.Attributes.Required(Message = "请输入描述")]
        [Utility.Attributes.Range(Min = 10, Max = 500, EnglishMin  = 10, EnglishMax  = 500, Message = "长度在 10 到 500 个字符描述",EnglishMessage = "length 10 to 500 char category description")]
        public virtual string Description { get; set; }
    }
}
