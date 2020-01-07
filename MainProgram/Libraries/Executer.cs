using MainProgram.Extensions;
using MainProgram.Files;
using MainProgram.Threads;
using System;
using System.Diagnostics;
using System.Windows;

namespace MainProgram.Libraries
{
    public class Executer
    {
        public float[] Result { get; set; }
        public double ExecutionDuration { get; set; }
        public DateTime StartTime { get; set; }

        private readonly Stopwatch stopwatch;
        private readonly LoadedData dataToProcess;
        private ThreadsHandler threadsHandler;

        private readonly LibraryLanguage methodLanguage;
        private readonly int threadsNumber;

        private const string decimalPrecisionOfTimeExecution = "N7";

        public Executer(LibraryLanguage language, int threads, LoadedData data)
        {
            methodLanguage = language;
            threadsNumber = threads;
            dataToProcess = data;

            stopwatch = new Stopwatch();
            threadsHandler = new ThreadsHandler(threadsNumber, dataToProcess);
        }

        public void Execute()
        {
            try
            {
                StartTimeMeasuring();
                PrepareThreads();
                RunMethod();
                StopTimeMeasuring();

                SaveResults();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Exception: " + e.HResult);
            }
        }

        public string Execute(int repeats)
        {
            double timesOfExecution = 0;
            string description = string.Empty;

            for (int i = 0; i < repeats; i++)
            {
                try
                {
                    PrepareThreads();

                    StartTimeMeasuring();
                    RunMethod();
                    StopTimeMeasuring();

                    SaveResults();

                    timesOfExecution += ExecutionDuration;
                    description += RetrieveStatisticsInfo();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Exception: " + e.HResult);
                }
            }

            description += "\n\t average: " + (timesOfExecution / repeats).ToString(decimalPrecisionOfTimeExecution) + "s\n\n";
            return description;
        }

        public string RetrieveExectionInfo()
        {
            return "Start time: " + StartTime.ToString() 
                + "\nLanguage: " + methodLanguage.GetName()
                + "\nThreads: " + threadsNumber.ToString("00")
                + "\nTime elapsed: " + ExecutionDuration.ToString(decimalPrecisionOfTimeExecution) + "s\n";
        }

        public string RetrieveStatisticsInfo()
        {
            return threadsNumber.ToString("00") 
                + " threads ran in " 
                + ExecutionDuration.ToString(decimalPrecisionOfTimeExecution) + "s\n";
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
            ExecutionDuration = stopwatch.Elapsed.TotalSeconds;
        }

        private void RunMethod()
        {
            threadsHandler.WaitForThreads();
        }

        private void PrepareThreads()
        {
            threadsHandler.CreateThreads(methodLanguage);
            threadsHandler.StartThreads();
        }
    }
}