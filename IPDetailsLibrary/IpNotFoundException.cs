using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IPDetailsLibrary
{
    public class IpNotFoundException : Exception
    {
        public IpNotFoundException()
        {
        }

        public IpNotFoundException(string message) : base(message)
        {
        }

        public IpNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IpNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
