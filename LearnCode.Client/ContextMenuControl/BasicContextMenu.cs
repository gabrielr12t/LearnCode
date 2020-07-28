using LearnCode.Client.Util;
using LearnCode.Client.Util.Command;
using LearnCode.Client.ViewModels.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;

namespace LearnCode.Client.ContextMenuControl
{
    public class BasicContextMenu<T> : ViewModelBase
    {
        private readonly IEnumerable<T> _enumerable;

        public BasicContextMenu(IEnumerable<T> enumerable)
        {
            _enumerable = enumerable;
        }

        #region Mark/Unmark Command

        private RelayCommand markCommand;
        [DisplayName("Mark")]
        public RelayCommand MarkCommand { get { return GetOrSetProperty(ref markCommand, () => this.CreateCommand(MarkCommandImplementation)); } }

        private void MarkCommandImplementation(object input)
        {
            if (input is IMarkable markable)
            {
                markable.Marked = true;
            }
        }

        private RelayCommand unmarkCommand;
        [DisplayName("Unmark")]
        public RelayCommand UnmarkCommand { get { return GetOrSetProperty(ref unmarkCommand, () => this.CreateCommand(UnmarkCommandImplementation)); } }

        private void UnmarkCommandImplementation(object input)
        {
            if (input is IMarkable markable)
            {
                markable.Marked = false;
            }
        }

        private RelayCommand markAllCommand;
        [DisplayName("Mark All")]
        public RelayCommand MarkAllCommand { get { return GetOrSetProperty(ref markAllCommand, () => this.CreateCommand(MarkAllCommandImplementation)); } }

        private void MarkAllCommandImplementation(object input)
        {
            foreach (IMarkable item in _enumerable)
            {
                item.Marked = true;
            }
        }

        private RelayCommand unmarkAllCommand;
        [DisplayName("Unmark All")]
        public RelayCommand UnmarkAllCommand { get { return GetOrSetProperty(ref unmarkAllCommand, () => this.CreateCommand(UnmarkAllCommandImplementation)); } }

        private void UnmarkAllCommandImplementation(object input)
        {
            foreach (IMarkable markable in _enumerable)
            {
                markable.Marked = false;
            }
        }

        private RelayCommand invertMarkedACommand;
        [DisplayName("Invert Marked")]
        public RelayCommand InvertMarkedACommand { get { return GetOrSetProperty(ref invertMarkedACommand, () => this.CreateCommand(InvertMarkedCommandImplementation)); } }

        private void InvertMarkedCommandImplementation(object input)
        {
            foreach (IMarkable markable in _enumerable)
            {
                markable.Marked = !markable.Marked;
            }
        }

        #endregion
    }
}
