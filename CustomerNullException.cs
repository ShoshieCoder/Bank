using System;
using System.Runtime.Serialization;

namespace BankTask
{
    [Serializable]
    public class CustomerNullException : ApplicationException
    {
        public CustomerNullException()
        {
        }

        public CustomerNullException(string message) : base(message)
        {
        }

        public CustomerNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}