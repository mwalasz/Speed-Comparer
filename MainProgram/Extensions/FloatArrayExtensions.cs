using MainProgram.Exceptions;
using System.Linq;

namespace MainProgram.Extensions
{
    public static class FloatArrayExtensions
    {
        public static string ToString(this float[] src, int rows, int cols)
        {
            string destination = string.Empty;
            string line = string.Empty;

            if (rows * cols != src.Length)
                throw new WrongInputDimensionsException();

            int counter = cols;

            foreach (var number in src)
            {
                line += (number.ToString() + " ");
                counter--;

                if (counter.Equals(0))
                {
                    destination += (line + "\n");
                    counter = cols;

                    line = string.Empty;
                }

                if (src.Last() == number)
                    break;
            }

            return destination;
        }
    }
}
