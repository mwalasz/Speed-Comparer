using System;
using System.Threading;
using System.Windows;

namespace AssemblyProject
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //Calculation calc;
        public App()
        {
            ThreadPool.GetAvailableThreads(out int w, out int c);
            ThreadPool.GetMinThreads(out int workers, out int completion);
            System.Console.WriteLine();
            Console.WriteLine("Number Of Logical Processors: {0}", Environment.ProcessorCount);
        }
    }
}
