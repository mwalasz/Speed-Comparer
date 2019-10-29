using System;
using System.Linq;

namespace MainProgram.Extensions
{
    public static class FloatArrayExtensions
    {
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
            var dst = new float[src.Length / 4];
            Buffer.BlockCopy(src, 0, dst, 0, src.Length);

            return dst;
        }
    }
}
