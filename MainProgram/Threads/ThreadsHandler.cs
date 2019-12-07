﻿using MainProgram.Extensions;
using MainProgram.Files;
using MainProgram.Libraries;
using MainProgram.Libraries.AssemblyWrapper;
using MainProgram.Libraries.CPlusPlusMethodWrapper;
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
        private readonly float scalar;

        private float[] floatArrayData;
        private Task[] tasks;
        private DllArgs[] libraryArguments;

        public ThreadsHandler(int threads, LoadedData data)
        {
            scalar = data.Scalar.Value;
            
            threadsNumber = threads;
            floatArrayData = data.Matrix.Content.ToOneDimensional();
        }
        
        public unsafe void CreateThreads(LibraryLanguage libraryLanguage)
        {
            int numberOfNumbers = floatArrayData.Length;
            int numbersPerThread = numberOfNumbers / threadsNumber;
            int restOfNumbers = numberOfNumbers % threadsNumber;
            int numbersToProcess = 1;
            int threads = (threadsNumber <= numberOfNumbers) ? threadsNumber : numberOfNumbers;
            
            tasks = new Task[threads];
            libraryArguments = new DllArgs[threads];

            for (int i = 0; i < threads; i++)
            {
                int tempId = i;

                if (threads == threadsNumber)
                    numbersToProcess = (restOfNumbers-- > 0) ? numbersPerThread + 1 : numbersPerThread;

                libraryArguments[tempId] = new DllArgs { Length = numbersToProcess, Matrix = GetSubArray(ref floatArrayData, numbersToProcess), Scalar = scalar };
                
                var dupa = libraryArguments[tempId].Matrix;

                fixed (float * ptr = dupa)
                {
                    tasks[tempId] = new Task(this.ChooseMethodToExecute(ptr, tempId, libraryLanguage));
                    //tasks[tempId] = new Task(() => methodToExecute(ptr, libraryArguments[tempId].Scalar, libraryArguments[tempId].Length));
                }
            }
        }

        public unsafe void Assembly(float* array, float scalar, int length)
        {
            AssemblyMethodWrapper.MatrixScalarMultiplication(array, scalar, length);
        }

        public unsafe void CPlusPlus(float* array, float scalar, int length)
        {
            CPlusPlusMethodWrapper.MatrixScalarMultiplication(array, scalar, length);
        }

        public unsafe Action ChooseMethodToExecute(float * array, int id, LibraryLanguage libraryLanguage)
        {
            switch (libraryLanguage)
            {
                case LibraryLanguage.Assembly:
                    return () => Assembly(array, libraryArguments[id].Scalar, libraryArguments[id].Length);

                case LibraryLanguage.CPlusPlus:
                    return () => CPlusPlus(array, libraryArguments[id].Scalar, libraryArguments[id].Length);
                default:
                    throw new Exception("Unable to choose method to execute");
            }
        }

        public void StartThreads()
        {
            Parallel.ForEach(tasks, t => t.Start());
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
