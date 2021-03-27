using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemicalApp.Model
{
    /// <summary>
    /// Класс Елемент для отображения 
    /// </summary>
    public class Element
    {
        /// <summary>
        /// Текущее значение
        /// </summary>
        public decimal Value { get; set; }
        /// <summary>
        /// Максимальное допустимое значение 
        /// </summary>
        public decimal Max { get; set; }
        /// <summary>
        /// Минимальное допустимое значение
        /// </summary>
        public decimal Min { get; set; }
        /// <summary>
        /// Найменование 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// прогнозное значение
        /// </summary>
        public decimal Predicted { get; set; }

    }
}
