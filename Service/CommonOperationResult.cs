using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pimark.MpeTestingSuite.Service
{
    public class CommonOperationResult
    {
        //common message class to report result of some operation
        public bool success;
        public string msg;
        public object returningValue;

        public static CommonOperationResult GetInstance(bool _success, string _msg, object _returningValue = null)
        {
            CommonOperationResult c = new CommonOperationResult();
            c.success = _success;
            c.msg = _msg;
            c.returningValue = _returningValue;
            return c;
        }

        public static CommonOperationResult ReturnValue(object _returningValue = null) { return GetInstance(true, "", _returningValue); }
        public static CommonOperationResult SayFail(string _msg = "") { return GetInstance(false, _msg, null); }
        public static CommonOperationResult SayOk(string _msg = "") { return GetInstance(true, _msg, null); }
        public static CommonOperationResult SayItsNull(string _msg = "") { return GetInstance(true, _msg, null); }

        public string ShrotString() => $"Success: {success} message: {msg}";
    }
}
