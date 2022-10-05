using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pimark.MpeTestingSuite.Service
{
    public class ValidationResult
    {
        public bool validationSuccess;
        public string validationMsg;
        public object validatedValue;

        public static ValidationResult getInstance(bool _validationSuccess, string _validationMsg = "", object _validatedValue = null)
        {
            ValidationResult vr = new ValidationResult
            {
                validationSuccess = _validationSuccess,
                validationMsg = _validationMsg,
                validatedValue = _validatedValue
            };
            return vr;
        }
        public static ValidationResult sayOk(string _validationMsg = "")
        {
            return getInstance(true, _validationMsg);
        }
        public static ValidationResult sayNo(string _validationMsg = "")
        {
            return getInstance(false, _validationMsg);
        }

    }
}
