using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Pimark.MpeTestingSuite.Service
{
    
    public class TreadManager
    {
        //this is class to manage pool of similar tasks in object, with ability to gather stats on it
        
        List<TreadClass> Treads=new List<TreadClass>();

        public void StartTread (string treadName, ThreadStart start, AutoResetEvent autoResetEvent, Serilog.ILogger logger)
        {
            TreadClass tread = new TreadClass(treadName, start, autoResetEvent, logger);
        }

        public class TreadClass
        {
            Thread _thread;
            Serilog.ILogger _logger;
            public TreadClass(string treadName, ThreadStart start , AutoResetEvent autoResetEvent, Serilog.ILogger logger)
            {
                _logger = logger;
                try
                {
                    _thread = new Thread(start);
                    _thread.Name = "trName=" + treadName;
                    _thread.Start();
                    _logger.Information($"Tread {treadName} successfully started");

                }
                catch
                {
                    _logger.Error("Unable to start tread");
                }
            }

            //   autoResetEvent.Set();

        }



    }
    

}
