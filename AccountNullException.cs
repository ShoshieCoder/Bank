using System;
using System.Runtime.Serialization;

namespace BankTask
{
    [Serializable]
    public class AccountNullException : ApplicationException
    {
        public AccountNullException()
        {
        }

        public AccountNullException(string message) : base(message)
        {
        }

        public AccountNullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}