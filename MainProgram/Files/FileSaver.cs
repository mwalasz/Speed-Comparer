using System;
using System.IO;

namespace MainProgram.Files
{
    public class FileSaver
    {
        public static void Save(string data, string fileName)
        {
            data += "\n";

            var currentDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;

            using StreamWriter outputFile = File.AppendText(Path.Combine(currentDirectory, fileName));
            
            outputFile.WriteLine(data);
        }
    }
}
