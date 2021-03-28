using ChemicalApp.Model;
using Microsoft.Xaml.Behaviors.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChemicalApp.ViewModel
{
    public class ChangeElementViewModel : BaseViewModel
    {
        public ChangeElementViewModel(List<Element> elements)
        {
            Elements = elements;
        }

        /// <summary>
        /// Список елементов
        /// </summary>
        public List<Element> Elements
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
        private List<Element> elements;

        public Commands SaveChangeCommand
        {
            get
            {
                return saveChangeCommand ?? (saveChangeCommand = new Commands(param => ExecuteAttachmentChecked(param), CanExecuteAttachmentChecked()));
            }
        }
        private Commands saveChangeCommand;
        private void ExecuteAttachmentChecked(object param)
        {
            (param as Window).Close();
        }

        private bool CanExecuteAttachmentChecked()
        {
            return true;
        }
    }
}
