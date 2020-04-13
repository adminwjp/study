using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialContact.Api.Exceptions
{
    public class AuthNotFoundException:Exception
    {  
        public AuthNotFoundException(string message)
        {
        }

        public AuthNotFoundException(string message, Exception innerException)
        {
        }
    }
}
