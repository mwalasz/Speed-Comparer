using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainProgram.Files
{
    public class FileSaver
    {
        public string FileName { get; set; }

        public FileSaver()
        {

        }

        public FileSaver(string fileName)
        {
            FileName = fileName;
        }
    }
}
