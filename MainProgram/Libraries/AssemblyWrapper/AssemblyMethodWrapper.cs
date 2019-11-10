using System.Runtime.InteropServices;

namespace MainProgram.Libraries.AssemblyWrapper
{
    public class AssemblyMethodWrapper
    {
        private const string PathToDll = @"D:\Studia\programowanie\Speed-Comparer\Libs\AssemblyDll\Debug\AssemblyDll.dll";

        [DllImport(PathToDll)]
        public static extern int AsmVal();
    }
}
