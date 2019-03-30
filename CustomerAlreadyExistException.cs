using System;
using System.Runtime.Serialization;

namespace BankTask
{
    [Serializable]
    public class CustomerAlreadyExistException : ApplicationException
    {
        public CustomerAlreadyExistException()
        {
        }

        public CustomerAlreadyExistException(string message) : base(message)
        {
        }

        public CustomerAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CustomerAlreadyExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}