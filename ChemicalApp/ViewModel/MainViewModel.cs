using ChemicalApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace ChemicalApp.ViewModel
{
    public class MainViewModel : BaseViewModel
    {

        public MainViewModel()
        {
            Load();
        }

        private async void Load()
        {
            IsBysy = true;
            bool isLoad = await LoadElementsAndCoefficient();
            if (isLoad)
            {
                IsBysy = false;
            }
            else
                Message = "Ошибка загрузки";
        }
        private async Task<bool> LoadElementsAndCoefficient()
        {
            try
            {
                ObservableCollection<Element> elementList = new ObservableCollection<Element>();
                XDocument xdoc = XDocument.Load("ChemicalData.xml");
                foreach (var item in xdoc.Element("root").Element("Elements").Elements("Element"))
                {
                    Element element = new Element();
                    element.Max = decimal.Parse(item.Element("Max").Value);
                    element.Min = decimal.Parse(item.Element("Min").Value);
                    element.Value = decimal.Parse(item.Element("Value").Value);
                    element.Name = item.Element("Name").Value;
                    element.Predicted = item.Element("Predicted")?.Value != null ? decimal.Parse(item.Element("Predicted").Value) : 1m;
                    elementList.Add(element);
                }
                await Task.Delay(2000);                
                Coefficient coefficient = new Coefficient();
                foreach (var item in xdoc.Element("root").Element("Coefficients").Elements("Element"))
                {   
                    coefficient.Max = decimal.Parse(item.Element("Max").Value);
                    coefficient.Min = decimal.Parse(item.Element("Min").Value);
                    coefficient.Value = decimal.Parse(item.Element("Value").Value);
                    coefficient.Name = item.Element("Name").Value;
                    coefficient.Predicted = item.Element("Predicted")?.Value != null ? decimal.Parse(item.Element("Predicted").Value) : 1m;
                }
                
                await Task.Delay(2000);
                coefficient.LoadCoefficient(xdoc);
                coefficient.CalculateCoefficient(elementList);
                Elements = elementList;
                Elements.Add(coefficient);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                return false;
            }
        }

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
        private ObservableCollection<Element> elements; 

        /// <summary>
        /// Индикация загрузки
        /// </summary>
        public bool IsBysy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                if (isBusy)
                {
                    Message = "Загрузка";
                    MessageVisibility = Visibility.Visible;
                }
                else
                    MessageVisibility = Visibility.Collapsed;
                OnPropertyChanged(() => IsBysy);
            }
        }
        public bool isBusy = false;

        /// <summary>
        /// Сообщение для отображения
        /// </summary>
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                OnPropertyChanged(()=>Message);
            }
        }
        private string message;

        /// <summary>
        /// Изменение отображения окна сообщения
        /// </summary>
        public Visibility MessageVisibility
        {
            get
            {
                return messageVisibility;
            }
            set
            {
                messageVisibility = value;
                OnPropertyChanged(() => MessageVisibility);
            }
        }
        private Visibility messageVisibility;

        public Command ChangeCommand => changeCommand ?? (changeCommand = new Command(() => 
        {
            IsBysy = true;
            Message = "Обновление";
            List<Element> tmpElements = Elements.Where(x=>x.Name != "CEq").ToList();
            Coefficient tmp_coefficient = (Coefficient)Elements.FirstOrDefault(x=>x.Name == "CEq");

            Elements = null;
            Views.ChangeElements change = new Views.ChangeElements(tmpElements);
            change.ShowDialog();

            Elements = new ObservableCollection<Element>();
            foreach (var item in tmpElements)
            {
                Elements.Add(item);
            }
            tmp_coefficient.CalculateCoefficient(Elements);
            Elements.Add(tmp_coefficient);
            IsBysy = false;
        }));
        private Command changeCommand;
    }
}
