using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram.Exceptions
{
    public class WrongInputDimensionsException : Exception
    {
        public WrongInputDimensionsException() : base("Dimensions of destination matrix are not equal to content of source array.")
        {
        }

        public WrongInputDimensionsException(string message) : base(message)
        {
        }

        public WrongInputDimensionsException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
