using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Pimark.MpeTestingSuite.Service
{
    public class ArgsHelper
    {
        //reads args in format argument=value

        string[] _args;
        Dictionary<string, string> _argList = new Dictionary<string, string>();

        public ArgsHelper (string[] args)
        {
            
            _args = args;
            foreach (string arg in _args)
            {
                string[] t = arg.Split('=');

                bool exists = _argList.TryGetValue(t[0].ToLower(), out string s1);

                if (t.Length==2 && !exists)
                {
                    _argList.Add(t[0].ToLower(), t[1]);
                }
            }
        }

        public string GetArgByName(string argName)
        {
            if (!_argList.TryGetValue(argName.ToLower(), out string s)) s = "";
            return s;
        }

        public bool ArgExists(string argName)
        {
            argName = argName.ToLower();
            int count = _argList.Where(x => x.Key == argName).ToList().Count();
            return count>0;
        }

    }
}
