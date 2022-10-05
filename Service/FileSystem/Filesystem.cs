using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pimark.MpeTestingSuite.Service
{
    public static class FileSystem
    {
        public static CommonOperationResult ClearDirectory(string directory)
        {
            try 
            {
                DirectoryInfo folder = new DirectoryInfo(directory);

                foreach (FileInfo file in folder.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo dir in folder.GetDirectories())
                {
                    dir.Delete(true);
                }
                return CommonOperationResult.SayOk();
            }
            catch(Exception ex)
            {
                return CommonOperationResult.SayFail(ex.Message);
            }
        }

    }
}
