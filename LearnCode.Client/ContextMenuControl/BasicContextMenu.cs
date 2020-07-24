using LearnCode.Client.Util;
using LearnCode.Client.Util.Command;
using LearnCode.Client.ViewModels.Interfaces;
using System.Collections;

namespace LearnCode.Client.ContextMenuControl
{
    public class BasicContextMenu : ViewModelBase
    {
        #region Mark/Unmark Command

        private RelayCommand markCommand;
        public RelayCommand MarkCommand { get { return GetOrSetProperty(ref markCommand, () => this.CreateCommand(MarkCommandImplementation)); } }

        private void MarkCommandImplementation(object input)
        {
            if (input is IMarkable markable)
            {
                markable.Marked = true;
            }
        }

        private RelayCommand unmarkCommand;
        public RelayCommand UnmarkCommand { get { return GetOrSetProperty(ref unmarkCommand, () => this.CreateCommand(UnmarkCommandImplementation)); } }

        private void UnmarkCommandImplementation(object input)
        {
            if (input is IMarkable markable)
            {
                markable.Marked = false;
            }
        }

        private RelayCommand markAllCommand;
        public RelayCommand MarkAllCommand { get { return GetOrSetProperty(ref markAllCommand, () => this.CreateCommand(MarkAllCommandImplementation)); } }

        private void MarkAllCommandImplementation(object input)
        {
            IEnumerable items = input as IEnumerable;
            
            if (items == null) 
                return;

            foreach (IMarkable item in items)
            {
                item.Marked = true;
            }

        }

        private RelayCommand unmarkAllCommand;
        public RelayCommand UnmarkAllCommand { get { return GetOrSetProperty(ref unmarkAllCommand, () => this.CreateCommand(UnmarkAllCommandImplementation)); } }

        private void UnmarkAllCommandImplementation(object input)
        {
            IEnumerable items = input as IEnumerable;
            
            if (items == null)
                return;

            foreach (IMarkable markable in items)
            {
                markable.Marked = false;
            }
        }

        private RelayCommand invertMarkedACommand;
        public RelayCommand InvertMarkedACommand { get { return GetOrSetProperty(ref invertMarkedACommand, () => this.CreateCommand(InvertMarkedCommandImplementation)); } }

        private void InvertMarkedCommandImplementation(object input)
        {
            IEnumerable items = input as IEnumerable;

            if (items == null)
                return;

            foreach (IMarkable markable in items)
            {
                markable.Marked = !markable.Marked;
            }
        }

        #endregion
    }
}
