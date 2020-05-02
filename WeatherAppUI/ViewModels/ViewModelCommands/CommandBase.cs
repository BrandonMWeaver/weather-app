using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeatherAppUI.ViewModels.ViewModelCommands
{
    class CommandBase<T> : ICommand where T : class
    {
        private readonly Action<T> _function;

        public event EventHandler CanExecuteChanged;

        public CommandBase(Action<T> function)
        {
            this._function = function;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this._function.Invoke(parameter as T);
        }
    }
}
