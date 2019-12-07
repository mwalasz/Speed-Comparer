using System.Runtime.InteropServices;

namespace MainProgram.Libraries.CPlusPlusMethodWrapper
{
    public class CPlusPlusMethodWrapper
    {
        private const string PathToDll = @"D:\Studia\programowanie\Speed-Comparer\Libs\Dll_C++\Debug\Dll_C++.dll";

        [DllImport(PathToDll, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void MatrixScalarMultiplication(float* matrix, float scalar, int matrixLength);
        //public static extern void MatrixScalarMultiplication(float [] matrix, byte[] scalar, int matrixLength);
    }
}
