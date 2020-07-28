using System.Windows.Input;

namespace LearnCode.Client.ContextMenuControl
{
    public class ContextMenuCommand
    {
        public ContextMenuCommand(string header, ICommand command)
        {
            Header = header;
            Command = command;
        }

        public string Header { get; set; }

        public ICommand Command { get; set; }
    }
}
