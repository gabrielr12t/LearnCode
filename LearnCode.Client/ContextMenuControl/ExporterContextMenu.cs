using LearnCode.Client.Data;
using LearnCode.Client.Util;
using LearnCode.Client.Util.Command;
using System.Collections.Generic;

namespace LearnCode.Client.ContextMenuControl
{
    public class ExporterContextMenu<T> : ViewModelBase
    {
        private readonly ExporterData<T> exporterData;

        public ExporterContextMenu()
        {
            exporterData = new ExporterData<T>();
        }

        private RelayCommand exportCommand;
        public RelayCommand ExportCommand { get { return GetOrSetProperty(ref exportCommand, () => this.CreateCommand(ExportCommandImplementation)); } }
        private void ExportCommandImplementation(object input)
        {
            if (input == null)
                return;

            exporterData.ExporterToDocuments(input as IEnumerable<T>);
        }
    }
}
