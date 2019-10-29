using DotNetDll;
using MainProgram.Extensions;
using MainProgram.Files;
using MainProgram.Libraries.AssemblyWrapper;
using System;
using System.Diagnostics;
using System.Windows;

namespace MainProgram.Libraries
{
    public class Executer
    {
        public int Result { get; set; }
        public long ExecutionDuration { get; set; }
        public DateTime StartTime { get; set; }

        private readonly Stopwatch stopwatch;
        private readonly LoadedData dataToProcess;

        private readonly LibraryLanguage methodLanguage;
        private readonly int threadsNumber;

        private MatrixByScalarMultiplication methodToExecute;

        public Executer(LibraryLanguage language, int threads, LoadedData data)
        {
            stopwatch = new Stopwatch();

            methodLanguage = language;
            threadsNumber = threads;
            dataToProcess = data;
        }

        private delegate int MatrixByScalarMultiplication();

        public void Execute()
        {
            try
            {
                ChooseMethodToUse();
                
                StartTimeMeasuring();
                Result = methodToExecute();
                StopTimeMeasuring();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Exception: " + e.HResult);
            }
        }

        public string RetrieveExectionInfo()
        {
            return "Start time: " + StartTime.ToString() 
                + "\nLanguage: " + methodLanguage.GetName()
                + "\nThreads: " + threadsNumber.ToString()
                + "\nTime elapsed: " + ExecutionDuration + "ms\n";
        }

        private void StartTimeMeasuring()
        {
            StartTime = DateTime.Now;
            stopwatch.Start();
        }

        private void StopTimeMeasuring()
        {
            stopwatch.Stop();
            ExecutionDuration = stopwatch.ElapsedMilliseconds;
        }

        private void ChooseMethodToUse()
        {
            switch (methodLanguage)
            {
                case LibraryLanguage.Assembly:
                    methodToExecute = AssemblyMethodWrapper.AsmVal;
                    break;

                case LibraryLanguage.CSharp:
                    methodToExecute = MatrixMultiplication.ImplementationTest;
                    break;
            }
        }
    }
}