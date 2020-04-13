using SocialContact.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialContact.Domain.Interfaces
{
    public interface IAdmin
    {
        AdminInfo Admin { get; set; }
    }
}
