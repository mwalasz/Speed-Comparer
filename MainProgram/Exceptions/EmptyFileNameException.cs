using System;

namespace MainProgram.Exceptions
{
    public class EmptyFileNameException : Exception
    {
        public EmptyFileNameException() : base("Filename cannot be empty.")
        {
        }

        public EmptyFileNameException(string message) : base(message)
        {
        }

        public EmptyFileNameException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
