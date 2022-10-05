using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primark.MpeTestingSuite.Service.Service
{
    public static class CommonServiceFunctions
    {
        public static long GetMSValue(long ms, long sec, long min, long hh)
        {
            if (hh > 0) { return hh * 60 * 60 * 1000; }
            if (min > 0) { return min * 60 * 1000; }
            if (sec > 0) { return sec * 1000; }
            if (ms > 0) { return ms; }
            return 0;
        }
    }
}
