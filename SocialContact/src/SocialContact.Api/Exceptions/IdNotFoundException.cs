using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialContact.Api.Exceptions
{
    public class IdNotFoundException:Exception
    {
        public IdNotFoundException(string message)
        {
        }

        public IdNotFoundException(string message, Exception innerException)
        {
        }

    }
}
