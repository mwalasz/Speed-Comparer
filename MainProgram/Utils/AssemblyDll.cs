using System.Runtime.InteropServices;

namespace MainProgram.Utils
{ 
    public static class AssemblyDll
    {
        private const string PathToDll = @"D:\Studia\programowanie\AssemblyProject\Debug\AssemblyDll.dll";
        
        [DllImport(PathToDll)]
        public static extern int AsmVal();
    }
}
