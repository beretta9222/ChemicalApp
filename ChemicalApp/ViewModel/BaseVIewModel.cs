using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Input;

namespace ChemicalApp.ViewModel
{
    #region BaseViewModel
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region WindowPropertys
        public void ShowMessageBox(string message)
        {
            MessageBox.Show(message, "", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public ICommand Close
        {
            get
            {
                return new Command(CloseApplication);
            }
        }

        public ICommand Maximice
        {
            get
            {
                return new Command(MaximiceApplication);
            }
        }

        public ICommand Minimice
        {
            get
            {
                return new Command(MinimiceApplication);
            }
        }

        public ICommand DragMove
        {
            get
            {
                return new Command(DragMoveCommand);
            }
        }

        public ICommand Restart
        {
            get
            {
                return new Command(RestartCommand);
            }
        }

        private static void RestartCommand()
        {
            Application.Current.Shutdown();
        }

        private static void DragMoveCommand()
        {
            Application.Current.MainWindow.DragMove();
        }

        private static void CloseApplication()
        {
            Application.Current.Shutdown();
        }

        private static void MaximiceApplication()
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            else
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
        }

        private static void MinimiceApplication()
        {
            if (Application.Current.MainWindow.WindowState == WindowState.Minimized)
            {
                Application.Current.MainWindow.Opacity = 1;
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
            else
            {
                Application.Current.MainWindow.Opacity = 0;
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            }
        }

        #endregion

        #region Propertychanged

        /// <summary>
        /// Применить изменения
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        protected void OnPropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            OnPropertyChanged(propertyName);
        }

        /// <summary>
        /// Получить название свойства
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }

        /// <summary>
        /// Применение свойства по имени
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


    }
    #endregion
    #region Command
    public class Command : ICommand
    {
        private readonly Action _command;
        private readonly Func<bool> _canExecute;

        public Command(Action command, Func<bool> canExecute = null)
        {
            if (command == null)
                throw new ArgumentNullException("command");
            _canExecute = canExecute;
            _command = command;
        }

        public void Execute(object parameter)
        {
            _command();
        }
      

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;
            return _canExecute();
        }

        public event EventHandler CanExecuteChanged;
    }
    public class Commands : ICommand
    {
        private Action<object> _action;
        private bool _canExecute;
        public Commands(Action<object> action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }        

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
    #endregion
}
