using System;
using System.Runtime.Serialization;

namespace BankTask
{
    [Serializable]
    public class AccountAlreadyExcistException : ApplicationException
    {
        public AccountAlreadyExcistException()
        {
        }

        public AccountAlreadyExcistException(string message) : base(message)
        {
        }

        public AccountAlreadyExcistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AccountAlreadyExcistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}