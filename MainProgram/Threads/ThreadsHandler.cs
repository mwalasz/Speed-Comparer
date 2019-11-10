using MainProgram.Extensions;
using MainProgram.Files;
using MainProgram.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MainProgram.Threads
{
    public class ThreadsHandler
    {
        private readonly int threadsNumber;
        private readonly Task[] tasks;
        private readonly float scalar;

        private float[] floatArrayData;
        private DllArgs[] libraryArguments;

        public ThreadsHandler(int threads, LoadedData data)
        {
            scalar = data.Scalar.Value;
            
            threadsNumber = threads;
            floatArrayData = data.Matrix.Content.ToOneDimensional();
            
            tasks = new Task[threadsNumber];
        }
        
        public void CreateThreads(Action<float[], float, int> methodToExecute)
        {
            int numberOfNumbers = floatArrayData.Length;
            int numbersPerThread = numberOfNumbers / threadsNumber;
            int restOfNumbers = numberOfNumbers % threadsNumber;
            int numbersToProcess = 1;
            int threads = (threadsNumber <= numberOfNumbers) ? threadsNumber : numberOfNumbers;

            libraryArguments = new DllArgs[threads];

            for (int i = 0; i < threads; i++)
            {
                int tempId = i;

                if (threads == threadsNumber)
                    numbersToProcess = (restOfNumbers-- > 0) ? numbersPerThread + 1 : numbersPerThread;

                libraryArguments[tempId] = new DllArgs { Length = numbersToProcess, Matrix = GetSubArray(ref floatArrayData, numbersToProcess), Scalar = scalar };
                
                tasks[tempId] = new Task(() => methodToExecute(libraryArguments[tempId].Matrix, libraryArguments[tempId].Scalar, libraryArguments[tempId].Length));
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

        public float[] GetResults()
        {
            var results = new List<float>();

            foreach (var args in libraryArguments)
                results.AddRange(args.Matrix);

            floatArrayData = results.ToArray();

            return floatArrayData;
        }

        private float[] GetSubArray(ref float[] src, int numOfElements)
        {
            var subArray = src.Take(numOfElements)
                .ToArray();

            src = src.Skip(numOfElements)
                .ToArray();

            return subArray;
        }
    }
}
