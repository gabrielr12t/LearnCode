using System;

namespace LearnCode.Client.Util.Command
{
    public class RelayCommandExecute<T> : RelayCommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public RelayCommandExecute(Action<T> execute) : this(execute, null) { }

        public RelayCommandExecute(Action<T> execute, Func<T, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter) => _canExecute?.Invoke((T)parameter) ?? false;

        public override void Execute(object parameter) => _execute((T)parameter);
    }
}
