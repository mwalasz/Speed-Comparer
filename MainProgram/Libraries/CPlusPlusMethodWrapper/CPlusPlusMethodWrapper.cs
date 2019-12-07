using System.Runtime.InteropServices;

namespace MainProgram.Libraries.CPlusPlusMethodWrapper
{
    public class CPlusPlusMethodWrapper
    {
        private const string PathToDll = @"D:\Studia\programowanie\Speed-Comparer\build\x64\Debug\Dll_C++\Dll_C++.dll";

        [DllImport(PathToDll, CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern void MatrixScalarMultiplication(float* matrix, float scalar, int matrixLength);
    }
}
