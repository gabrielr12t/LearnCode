using LearnCode.Client.Util;
using LearnCode.Client.ViewModels.Interfaces;

namespace LearnCode.Client.ViewModels
{
    public class CountryViewItem : ViewModelBase, IMarkable
    {
        private bool marked;
        public bool Marked
        {
            get { return marked; }
            set { SetProperty(ref marked, value, true); }
        }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}
