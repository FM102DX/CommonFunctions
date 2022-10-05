using Pimark.MpeTestingSuite.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primark.MpeTestingSuite.Service.Service.DataTransfer
{
   public class UDBStorage
   {
        public List<UniversalDataBox> DataBoxses = new List<UniversalDataBox>();

        public UDBStorage AddDataBox (UniversalDataBox box)
        {
            DataBoxses.Add(box);
            return this;
        }
   }
}
