using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Posts.Domain.Exceptions
{
    public class PostDomainException : Exception
    {
        public PostDomainException() { }

        public PostDomainException(string message) : base(message)
        {
        }
        public PostDomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
