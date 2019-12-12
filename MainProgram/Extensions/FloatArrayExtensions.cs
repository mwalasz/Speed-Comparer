using MainProgram.Exceptions;
using System;
using System.Linq;

namespace MainProgram.Extensions
{
    public static class FloatArrayExtensions
    {
        public static string ToString(this float[] src, int rows, int cols)
        {
            float[] line;

            string[] arrayOfLines = new string[cols];

            for (int i = 0; i < cols; i++)
            {
                line = src.Take(cols).ToArray();

                src = src.Skip(cols).ToArray();
                arrayOfLines[i] = string.Join(" ", line);
            }

            return string.Join("\n", arrayOfLines);
        }

        public static float[] ToOneDimensional(this float[,] src)
        {
            return src.Cast<float>()
                .ToArray();
        }
    }
}
