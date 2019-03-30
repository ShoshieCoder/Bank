using System;
using System.Runtime.Serialization;

namespace BankTask
{
    [Serializable]
    public class NotSameCustomerException : ApplicationException
    {
        public NotSameCustomerException()
        {
        }

        public NotSameCustomerException(string message) : base(message)
        {
        }

        public NotSameCustomerException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotSameCustomerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}