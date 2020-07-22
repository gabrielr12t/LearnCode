using LearnCode.Client.Util;
using LearnCode.Client.Util.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LearnCode.Client.ViewModels
{
    public class CountryViewModel : ViewModelBase, IHasPagination<CountryViewItem>
    {
        private readonly CountryService countryService;

        public CountryViewModel()
        {
            countryService = new CountryService();
            CountriesViewItem = new ObservableCollection<CountryViewItem>();
            pagination = new Pagination<CountryViewItem>();
        }

        private Pagination<CountryViewItem> pagination;
        public Pagination<CountryViewItem> Pagination
        {
            get { return pagination; }
            private set { SetProperty(ref pagination, value, true); }
        }

        private ObservableCollection<CountryViewItem> countriesViewItem;
        public ObservableCollection<CountryViewItem> CountriesViewItem
        {
            get { return countriesViewItem; }
            private set { SetProperty(ref countriesViewItem, value, true); }
        }

        private RelayCommand loadCommand;
        public RelayCommand LoadCommand { get { return GetOrSetProperty(ref loadCommand, () => this.CreateCommand(LoadCommandImplementation)); } }

        private void LoadCommandImplementation()
        {
            int totalItems = countryService.GetCountries().Count();

            Pagination.SetItemsPerPage(10);
            Pagination.SetTotalPages(totalItems);
            Pagination.SetSearchCommand(countryService.GetCountries);

            var response = countryService.GetCountries(Pagination.FirstPage, Pagination.ItemsPerPage);

            Refresh(response, Pagination.FirstPage);
        }

        #region Pagination

        private RelayCommand nextPageCommand;
        public RelayCommand NextPageComman { get { return GetOrSetProperty(ref nextPageCommand, () => this.CreateCommand(NextPageCommandImplementation)); } }

        private void NextPageCommandImplementation()
        {
            var response = pagination.NextPageItems();
            Refresh(response, Pagination.NextPage);
        }

        private RelayCommand previousPageCommand;
        public RelayCommand PreviousPageCommand { get { return GetOrSetProperty(ref previousPageCommand, () => this.CreateCommand(PreviousPageCommandImplementation)); } }

        private void PreviousPageCommandImplementation()
        {
            var response = pagination.PreviousPageItems();
            Refresh(response, Pagination.PreviousPage);
        }

        #endregion

        #region Refresh List

        private void Refresh(IEnumerable<CountryViewItem> response, int page)
        {
            if (response == null)
                return;

            Pagination.SetTotalPages(Pagination.TotalItems);
            Pagination.SetCurrentPage(page);

            CountriesViewItem.Clear();

            foreach (CountryViewItem country in response)
            {
                CountriesViewItem.Add(country);
            }
        }

        #endregion
    }
}
