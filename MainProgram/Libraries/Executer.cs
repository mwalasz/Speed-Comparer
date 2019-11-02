using DotNetDll;
using MainProgram.Extensions;
using MainProgram.Files;
using MainProgram.Libraries.AssemblyWrapper;
using MainProgram.Threads;
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
        private ThreadsHandler threadsHandler;

        private readonly LibraryLanguage methodLanguage;
        private readonly int threadsNumber;

        private MatrixByScalarMultiplication methodToExecute;

        public Executer(LibraryLanguage language, int threads, LoadedData data)
        {
            methodLanguage = language;
            threadsNumber = threads;
            dataToProcess = data;

            stopwatch = new Stopwatch();
            threadsHandler = new ThreadsHandler(threadsNumber, dataToProcess);
        }

        private delegate int MatrixByScalarMultiplication();

        public void Execute()
        {
            try
            {
                ChooseMethodToUse();
                PrepareThreads();
                
                StartTimeMeasuring();
                threadsHandler.WaitForThreads();
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

        private void PrepareThreads()
        {
            Action dg = () => {
            };

            threadsHandler.CreateThreads(dg);
            threadsHandler.StartThreads();
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