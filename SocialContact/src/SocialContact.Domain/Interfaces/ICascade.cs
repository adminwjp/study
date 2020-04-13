

using System;
using System.Collections.Generic;

namespace SocialContact.Domain.Interfaces
{
    public interface ICasecade<T>:ICloneable where T:ICasecade<T>
    {
        int? Id { get; set; }
        T Parent { get; set; }
        ISet<T> Children { get; set; }
    }
}
