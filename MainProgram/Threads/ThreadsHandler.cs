using MainProgram.Files;
using MainProgram.Extensions;
using System;
using System.Threading.Tasks;
using DotNetDll;

namespace MainProgram.Threads
{
    public class ThreadsHandler
    {
        private readonly int threadsNumber;
        private byte[] bytesData;
        
        private DllArgs[] libraryArguments;
        private Task[] tasks;

        public ThreadsHandler(int threads, LoadedData data)
        {
            threadsNumber = threads;
            bytesData = data.Matrix.Content.ToOneDimensional()
                .ToByteArray();

            libraryArguments = new DllArgs[threadsNumber];
            tasks = new Task[threadsNumber];
        }
        
        public void CreateThreads(Action<object> methodToExecute)
        {
            int numberOfNumbers = (bytesData.Length / 4);

            int interval = bytesData.Length / threadsNumber;
            int rest = numberOfNumbers % threadsNumber;

            int iterations = threadsNumber;
            bool numbersAreFewerThanThreads = false;

            if (numberOfNumbers < threadsNumber)
            {
                interval = 4;
                iterations = numberOfNumbers;
                numbersAreFewerThanThreads = true;
            }

            for (int i = 0; i < iterations; i++)
            {
                var start = (i * interval);
                var stop = (start + (interval - 1));

                if (!numbersAreFewerThanThreads && i == threadsNumber - 1)
                    stop += rest * 4;

                libraryArguments[i] = new DllArgs { Start = start, Stop = stop, Data = bytesData };
                int tempId = i;
                tasks[i] = new Task(() => methodToExecute(libraryArguments[tempId]));
            }
        }

        public void StartThreads()
        {
            foreach (var task in tasks)
                task.Start();
        }

        public void WaitForThreads()
        {
            Task.WaitAll(tasks);
        }
    }
}
