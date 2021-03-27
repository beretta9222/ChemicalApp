using ChemicalApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChemicalApp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        /// <summary>
        /// Список елементов
        /// </summary>
        public ObservableCollection<Element> Elements
        {
            get
            {
                return elements;
            }
            set
            {
                elements = value;
                OnPropertyChanged(() => Elements);

            }
        }
        private ObservableCollection<Element> elements = new ObservableCollection<Element>() { new Element() { Max = 10.5453m, Min = 0.65m, Name = "C", Value = 4.64m, Predicted = 2.232m  } } ; 


        public bool IsBysy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged(() => IsBysy);
            }
        }
        public bool isBusy = false;
    }
}
