using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChemicalApp.Helpers
{
    class ValidationValue : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string str_value = value.ToString().Replace('.', ',');
            if(decimal.TryParse(str_value,out decimal result))
            {
                return ValidationResult.ValidResult;
            }
            else
                return new ValidationResult(false, "Не верный формат данных (Формат: 0,00)");
        }
    }
}
