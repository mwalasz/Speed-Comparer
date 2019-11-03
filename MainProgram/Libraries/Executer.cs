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
        public float[] Result { get; set; }
        public long ExecutionDuration { get; set; }
        public DateTime StartTime { get; set; }

        private readonly Stopwatch stopwatch;
        private readonly LoadedData dataToProcess;
        private ThreadsHandler threadsHandler;

        private readonly LibraryLanguage methodLanguage;
        private readonly int threadsNumber;

        private MatrixByScalarMultiplication methodToExecute;
        private Action<object> method;

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
                ChooseLibraryAndPrepareThreads();

                StartTimeMeasuring();
                RunMethod();
                StopTimeMeasuring();

                SaveResults();
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

        private void SaveResults()
        {
            Result = threadsHandler.GetResults();
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

        private void RunMethod()
        {
            threadsHandler.StartThreads();
            threadsHandler.WaitForThreads();
        }

        private void ChooseLibraryAndPrepareThreads()
        {
            switch (methodLanguage)
            {
                case LibraryLanguage.Assembly:
                    methodToExecute = AssemblyMethodWrapper.AsmVal;
                    break;

                case LibraryLanguage.CSharp:
                    method = MatrixMultiplication.ActionTest;
                    methodToExecute = MatrixMultiplication.ImplementationTest;
                    break;
            }

            threadsHandler.CreateThreads(method);
        }
    }
}