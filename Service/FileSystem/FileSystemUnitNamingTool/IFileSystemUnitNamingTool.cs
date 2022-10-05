using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primark.MpeTestingSuite.Service.FileSystem.FileSystemUnitNamingTool
{

    //this tool allow naming of multiple files/dirs having same base name, for ex. Applog_1, Applog_2, Applog_3 etc.

    public interface IFileSystemUnitNamingTool
    {
        string Extention { get; set; }
        //void Init(string directory, string baseName, string extention, bool isFile);
        string GetName();
        
    }
}
