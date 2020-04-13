using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialContact.Api.Exceptions
{
    public class CascadeException : Exception
    {
        public CascadeException(string message)
        {
        }

        public CascadeException(string message, Exception innerException)
        {
        }

    }
}
