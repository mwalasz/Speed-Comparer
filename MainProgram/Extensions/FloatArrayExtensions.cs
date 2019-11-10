using MainProgram.Exceptions;
using System;
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

        public static float[] ToOneDimensional(this float[,] src)
        {
            return src.Cast<float>()
                .ToArray();
        }

        public static byte[] ToByteArray(this float[] src)
        {
            var dst = new byte[src.Length * 4];
            Buffer.BlockCopy(src, 0, dst, 0, dst.Length);

            return dst;
        }

        public static float[] ToFloatArray(this byte[] src)
        {
            var dst = new float[(src.Length + 1) / 4];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);

            return dst;
        }
    }
}
