using System;
using System.Diagnostics;
using System.IO;

namespace MainProgram.Files
{
    public class FileSaver
    {
        public static void Save(string data, string fileName, bool toOpenNotePad = false)
        {
            data += "\n";

            var currentDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            var filePath = Path.Combine(currentDirectory, fileName);

            using StreamWriter outputFile = File.AppendText(filePath);
            
            outputFile.WriteLine(data);

            if (toOpenNotePad)
                Process.Start("notepad.exe", filePath);
        }

        public static void SaveAndOpen(string data)
        {
            var fileName = "stats_" + DateTime.Now.ToString("s").Replace(":", ".") + ".txt";
            FileSaver.Save(data, fileName, true);
        }
    }
}
