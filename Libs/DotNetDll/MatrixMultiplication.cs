using System;

namespace DotNetDll
{
    public static class MatrixMultiplication
    {
        public static int ImplementationTest()
        {
            return 111;
        }

        public static void ActionTest(object args)
        {
            var arguments = (DllArgs)args;

            float scalar = BitConverter.ToSingle(arguments.Scalar, 0);
            int start = arguments.Start;
            int stop = arguments.Stop;

            start /= 4;
            stop /= 4;

            var byteArrayCopy = arguments.Matrix;
            var convertedArrayCopy = byteArrayCopy.ToFloatArray();

            for (int i = start; i < stop; i++)
                convertedArrayCopy[i] *= scalar;

            var resultByteArray = convertedArrayCopy.ToByteArray();
            
            arguments.Matrix = resultByteArray;
            args = arguments;
        }
    }
}
