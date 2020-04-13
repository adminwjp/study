
using NHibernate.Mapping.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OA.Domain.Core
{
    [Class(Table = "user_info", NameType = typeof(UserInfo), Lazy = false)]
    public class UserInfo:BaseEntity, IValidatableObject
    {
        [Property(Column = "account", NotNull = true, TypeType = typeof(string), Length = 20)]
        public virtual string Account { get; set; }
        [Property(Column = "password", NotNull = true, TypeType = typeof(string), Length = 50)]
        public virtual string Password { get; set; }
        public virtual int? RoleId { get; set; }
        [ManyToOne(Column = "role_id",ClassType =typeof(RoleInfo))]
        public virtual RoleInfo Role { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(this.Account))
                yield return new ValidationResult("账户不能为空") ;
            else if(this.Account.Length > 20&&this.Account.Length<5) yield return new ValidationResult("账户长度只能为5-20位");
            if (string.IsNullOrEmpty(this.Password))
                yield return new ValidationResult("密码不能为空");
            else if (this.Password.Length > 50 && this.Password.Length < 5) yield return new ValidationResult("密码长度只能为5-50位");
        }
    }
}
