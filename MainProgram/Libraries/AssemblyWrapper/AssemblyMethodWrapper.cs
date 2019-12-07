using System.Runtime.InteropServices;

namespace MainProgram.Libraries.AssemblyWrapper
{
    public class AssemblyMethodWrapper
    {
        private const string PathToDll = @"D:\Studia\programowanie\Speed-Comparer\build\x64\Debug\LibToDll\LibToDll.dll";

        [DllImport(PathToDll)]
        public unsafe static extern void asmMatrixScalarMultiplication(float * matrix, float scalar, int matrixLength);
    }
}
