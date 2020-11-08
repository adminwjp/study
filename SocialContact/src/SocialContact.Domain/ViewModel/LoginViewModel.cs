using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialContact.Domain.ViewModel
{
    public class LoginViewModel//: IValidatableObject
    {
        //[Required(ErrorMessage = "账户不能为空!")]
        //[MinLength(6,ErrorMessage = "账户长度过短，最短5位数")]
        //[MaxLength(20, ErrorMessage = "账户长度过长，最长20位数")]
        [Utility.Attributes.Required(Message = "账户不能为空!")]
        [Utility.Attributes.Range(Min =5,Max =20,Message = "账户为5-20位!")]
        public  string Account { get; set; }
       // [Required(ErrorMessage = "密码不能为空!")]
       // [MinLength(6, ErrorMessage = "账户长度过短，最短6位数")]
       // [MaxLength(20, ErrorMessage = "账户长度过长，最长20位数")]
        [Utility.Attributes.Required(Message = "密码不能为空!")]
        [Utility.Attributes.Range(Min = 6, Max = 20, Message = "密码为6-20位!")]
        public  string Password { get; set; }
        public bool Remember { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
