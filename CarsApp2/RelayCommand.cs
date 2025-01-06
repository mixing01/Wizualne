using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GamesDesktopApp
{
    // Komendy pozwalają nam na rozdzielenie warstwy wizualnej z wykonawczą
    // Np. zamiast bezpośrednio obsługiwać naciśnięcie przycisku w MainWindow,
    // robimy żeby naciśnięcie przycisku wywołało komendę, która wykona się w CarListViewModel
    public class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Action<object> _execute):this( _execute, null)
        {

        }
    }
}
