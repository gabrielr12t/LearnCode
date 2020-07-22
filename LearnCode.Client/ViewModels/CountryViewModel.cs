using LearnCode.Client.Util;
using LearnCode.Client.Util.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LearnCode.Client.ViewModels
{
    public class CountryViewModel : ViewModelBase
    {
        private readonly CountryService countryService;

        public CountryViewModel()
        {
            countryService = new CountryService();
            CountriesViewItem = new ObservableCollection<CountryViewItem>();
        }

        private ObservableCollection<CountryViewItem> countriesViewItem;
        public ObservableCollection<CountryViewItem> CountriesViewItem
        {
            get { return countriesViewItem; }
            private set { SetProperty(ref countriesViewItem, value, true); }
        }

        private RelayCommand loadCommand;
        public RelayCommand LoadCommand
        {
            get { return GetOrSetProperty(ref loadCommand, () => this.CreateCommand(LoadCommandImplementation)); }
        }
        private void LoadCommandImplementation()
        {
            IEnumerable<CountryViewItem> response = countryService.GetCountries();
            RefreshList(response);
        }

        private void RefreshList(IEnumerable<CountryViewItem> response)
        {
            CountriesViewItem.Clear();

            foreach (CountryViewItem country in response)
            {
                CountriesViewItem.Add(country);
            }
        }
    }
}
