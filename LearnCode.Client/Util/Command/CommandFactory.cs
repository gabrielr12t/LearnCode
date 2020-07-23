using System;

namespace LearnCode.Client.Util.Command
{
    public static class CommandFactory
    {
        public static RelayCommand CreateCommand(this ViewModelBase viewModelBase, Action<object> execute)
        {
            return new RelayCommand(execute);
        }

        public static RelayCommand CreateCommand(this ViewModelBase viewModelBase, Action<object> execute, Func<bool> canExecute)
        {
            return new RelayCommand(execute, canExecute);
        }
    }
}
