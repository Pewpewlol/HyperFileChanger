using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows.Input;
using System;
using Windows.UI.Xaml.Data;

namespace XamlStandardsLib
{
    public class ModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            if (!(PropertyChanged == null))
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
    public class DelegateCommand<T> : ICommand
    {
        Predicate<T> canExecuteHdl { get; set; }
        Action<T> executeHdl { get; set; }
        
        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<T> executeHdl, Predicate<T> canExecuteHdl)
        {
            this.executeHdl = executeHdl;
            if (executeHdl == null)
                throw new ArgumentException("executeHdl", "Please specify the command.");
            this.canExecuteHdl = canExecuteHdl;
        }
        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, null);
            }
        }
        public bool CanExecute(object parameter)
        {
            return canExecuteHdl == null || canExecuteHdl((T)parameter) == true;
        }

        public void Execute(object parameter)
        {
            executeHdl((T)parameter);
        }
    }
    public class Converter<T> : IValueConverter
    {
        public Action<T> Funktion { get; set; }
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
        
    }


    
}
