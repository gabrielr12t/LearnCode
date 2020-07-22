using System;

namespace LearnCode.Client.Util.Command
{
    public static class CommandExecuteFactory
    {
        public static RelayCommandExecute<T> CreateCommand<T>(this ViewModelBase viewmodel, Action<T> execute)
        {
            return new RelayCommandExecute<T>(execute);
        }

        public static RelayCommandExecute<T> CreateCommand<T>(this ViewModelBase viewmodel, Action<T> execute, Func<T, bool> canExecute)
        {
            return new RelayCommandExecute<T>(execute, canExecute);
        }
    }
}
