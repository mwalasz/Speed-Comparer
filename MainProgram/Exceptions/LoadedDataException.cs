using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram.Exceptions
{
    public class LoadedDataException : Exception
    {
        public LoadedDataException() : base("Error occurred while creating instances of scalar and matrix.")
        {
        }

        public LoadedDataException(string message) : base(message)
        {
        }

        public LoadedDataException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
