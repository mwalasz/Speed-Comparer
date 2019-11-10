using MainProgram.Exceptions;
using MainProgram.Extensions;
using MainProgram.Maths;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace MainProgram.Files
{
    public class FileReader
    {
        public string FileName { get; set; }

        private readonly List<string> fileContent;
        private readonly StreamReader file;

        public FileReader(string fileName)
        {
            fileContent = new List<string>();
            FileName = fileName 
                ?? throw new EmptyFileNameException();

            try
            {
                file = new StreamReader(FileName);
                ReadFile();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Exception: " + e.HResult);
            }
        }

        ~FileReader()
        {
            file.Close();
        }

        public LoadedData GetLoadedData()
        {
            Scalar scalar = new Scalar(fileContent.First());
            Matrix matrix = new Matrix(fileContent.ConvertToMatrix());

            if (scalar == null || matrix == null)
                throw new LoadedDataException();
            else return new LoadedData(matrix, scalar);
        }

        private void ReadFile()
        {
            int linesReaded = 0;

            if (file != null)
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    fileContent.Add(line);
                    linesReaded++;
                }
            }
        }
    }
}
