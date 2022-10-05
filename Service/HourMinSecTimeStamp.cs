using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primark.MpeTestingSuite.Service
{
    public class HourMinSecTimeStamp
    {
        public long Milliseconds;
        public long HH;
        public long MM;
        public long Sec;

        public HourMinSecTimeStamp(long milliseconds)
        {
            Milliseconds = milliseconds;
            HH = Convert.ToInt64(Math.Truncate(Convert.ToDouble((milliseconds/1000)/3600)));
            MM = Convert.ToInt64(Math.Truncate(Convert.ToDouble((milliseconds / 1000) / 60)) - HH * 60);
            Sec= Convert.ToInt64(Math.Truncate(Convert.ToDouble((milliseconds / 1000) )) - HH * 3600 - MM * 60);
        }

        public static bool operator ==(HourMinSecTimeStamp value1, HourMinSecTimeStamp value2)
        {
            return (value1.HH == value2.HH) & (value1.MM == value2.MM) & ((value1.Sec == value2.Sec));
        }
        public static bool operator !=(HourMinSecTimeStamp value1, HourMinSecTimeStamp value2)
        {
            return (value1.HH != value2.HH) | (value1.MM != value2.MM) | ((value1.Sec != value2.Sec));
        }

        public static HourMinSecTimeStamp operator +(HourMinSecTimeStamp value1, HourMinSecTimeStamp value2)
        {
            return new HourMinSecTimeStamp(value1.Milliseconds + value2.Milliseconds);
        }
        public static HourMinSecTimeStamp operator -(HourMinSecTimeStamp value1, HourMinSecTimeStamp value2)
        {
            HourMinSecTimeStamp stamp = new HourMinSecTimeStamp(value1.Milliseconds - value2.Milliseconds);
            if ((value1!= value2)) stamp.Sec++;
            return stamp;
        }

        public string ToRegularString()
        {
            string _hhStr = HH <= 0 ? "00" : $"{HH.ToString()}";
            string _mmStr = MM <= 0 ? "00" : $"{MM.ToString()}";
            string _secStr = Sec <= 0 ? "00" : $"{Sec.ToString()}";
            return $"{_hhStr}:{_mmStr}:{_secStr}";
        }
        public string ToRegularStringWithDescription()
        {
            string _hhStr = HH <= 0 ? "" : $"{HH.ToString()} hh ";
            string _mmStr = MM <= 0 ? "" : $"{MM.ToString()} min ";
            string _secStr = Sec <= 0 ? "" : $"{Sec.ToString()} sec ";
            List<string> x = new List<string>();
            x.Add(_hhStr);
            x.Add(_mmStr);
            x.Add(_secStr);
            string s = string.Join("", x.Where(y => (!string.IsNullOrEmpty(y))).ToList());
            return s;
        }

    }
}
