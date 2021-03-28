using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ChemicalApp.Model
{
    public class Coefficient : Element
    {
        public Dictionary<string, decimal> CoefficientElements;

        public void CalculateCoefficient(ObservableCollection<Element> elements)
        {
            decimal tmpvalue = 0;
            foreach (var element in elements)
            {
                var coef = CoefficientElements.Where(x => x.Key == element.Name).Select(x => x.Value).FirstOrDefault();
                if (coef != null)
                    tmpvalue += element.Predicted * coef;
            }
            Predicted = tmpvalue;
        }
        public bool LoadCoefficient(XDocument xdoc)
        {
            try
            {
                CoefficientElements = new Dictionary<string, decimal>();
                foreach (var item in xdoc.Element("root").Element("Coefficients").Elements("Coefficient"))
                {
                    CoefficientElements.Add(item.Element("Element").Value, decimal.Parse(item.Element("K").Value.Replace('.', ',')));

                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
