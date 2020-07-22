using System;

namespace LearnCode.Client.Util.Command
{
    public class RelayCommand : CommandBase, IDisposable
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand() { }

        public RelayCommand(Action execute) : this(execute, null) { }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentException(nameof(execute));
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;
       
        public override void Execute(object parameter) => _execute();

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
