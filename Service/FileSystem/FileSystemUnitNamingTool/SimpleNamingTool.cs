using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primark.MpeTestingSuite.Service.FileSystem.FileSystemUnitNamingTool
{
    public class SimpleNamingTool : IFileSystemUnitNamingTool
    {

        public string Extention { get; set; }
        private string _directory;
        private string _baseName;
        private bool _isFile;

        public SimpleNamingTool (string directory, string baseName, bool isFile)
        {
            _directory = directory;
            _baseName = baseName;
            _isFile = isFile;

        }

        public string GetName()
        {
            bool exists = false;
            string fileName="";

            if (_isFile)
            {
                int counter = -1;
                do
                {
                    counter++;
                    fileName = (counter==0) ? Path.Combine(_directory, $"{_baseName}.{Extention}") : Path.Combine(_directory, $"{_baseName}_{counter}.{Extention}");
                    exists = File.Exists(fileName);
                }
                while (exists);
                return fileName;
            }
            else
            {
                int counter = -1;
                do
                {
                    counter++;
                    fileName = (counter == 0) ? Path.Combine(_directory, $"{_baseName}") : Path.Combine(_directory, $"{_baseName}_{counter}");
                    exists = Directory.Exists(fileName);
                    counter++;
                }
                while (exists);
                return fileName;
            }
        }

    }
}
