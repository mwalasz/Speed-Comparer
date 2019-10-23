using System;

namespace MainProgram.Exceptions
{
    public class MatrixDataException : Exception
    {
        public MatrixDataException() : base("Wrong amount of numbers in matrix detected")
        {
        }

        public MatrixDataException(string message) : base(message)
        {
        }

        public MatrixDataException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
