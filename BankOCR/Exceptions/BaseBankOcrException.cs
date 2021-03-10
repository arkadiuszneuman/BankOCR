#nullable enable
using System;
using System.Runtime.Serialization;

namespace BankOCR.Exceptions
{
    public class BaseBankOcrException : Exception
    {
        public BaseBankOcrException()
        {
        }

        protected BaseBankOcrException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public BaseBankOcrException(string? message) : base(message)
        {
        }

        public BaseBankOcrException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}