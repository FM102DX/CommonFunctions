using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pimark.MpeTestingSuite.Service
{
    public static class ValidationFunctions
    {
        public static ValidationResult IsStrictNamingString(string value)
        {
            Regex regex;

            if (value.Contains('\\') | value.Contains('|') | value.Contains('/') | value.Contains('^'))
            {
                return ValidationResult.sayNo();
            }
          
            regex = new Regex("[^a-zA-Z0-9]", RegexOptions.IgnoreCase);
            
            var rez = regex.Matches(value);

            if (rez.Count>0)
            {
                return ValidationResult.sayNo();
            }
            return ValidationResult.sayOk();
        }

  
    }
}