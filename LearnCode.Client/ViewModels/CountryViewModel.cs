using LearnCode.Client.Util;
using LearnCode.Client.Util.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace LearnCode.Client.ViewModels
{
    public class CountryViewModel : ViewModelBase, IHavePagination<CountryViewItem>
    {
        private readonly CountryService countryService;

        public CountryViewModel()
        {
            countryService = new CountryService();
            CountriesViewItem = new ObservableCollection<CountryViewItem>();
            pagination = new Pagination<CountryViewItem>();
        }

        #region Property Set

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

        #endregion

        #region Load Command

        private RelayCommand loadCommand;
        public RelayCommand LoadCommand { get { return GetOrSetProperty(ref loadCommand, () => this.CreateCommand(LoadCommandImplementation)); } }

        private void LoadCommandImplementation()
        {
            int totalItems = countryService.GetCountries().Count();
            ConfigurePagination(totalItems, 10, countryService.GetCountries);

            IEnumerable<CountryViewItem> response = pagination.FirstPageItems();
            Refresh(response, pagination.FirstPage);
        }

        #endregion

        #region Pagination

        private RelayCommand nextPageCommand;
        public RelayCommand NextPageCommand { get { return GetOrSetProperty(ref nextPageCommand, () => this.CreateCommand(NextPageCommandImplementation)); } }

        private void NextPageCommandImplementation()
        {
            IEnumerable<CountryViewItem> response = pagination.NextPageItems();
            Refresh(response, pagination.NextPage);
        }

        private RelayCommand previousPageCommand;
        public RelayCommand PreviousPageCommand { get { return GetOrSetProperty(ref previousPageCommand, () => this.CreateCommand(PreviousPageCommandImplementation)); } }

        private void PreviousPageCommandImplementation()
        {
            IEnumerable<CountryViewItem> response = pagination.PreviousPageItems();
            Refresh(response, pagination.PreviousPage);
        }

        private RelayCommand firstPageCommand;
        public RelayCommand FirstPageCommand { get { return GetOrSetProperty(ref firstPageCommand, () => this.CreateCommand(FirstPageCommandImplementation)); } }

        private void FirstPageCommandImplementation()
        {
            IEnumerable<CountryViewItem> response = pagination.FirstPageItems();
            Refresh(response, pagination.FirstPage);
        }

        private RelayCommand lastPageCommand;
        public RelayCommand LastPageCommand { get { return GetOrSetProperty(ref lastPageCommand, () => this.CreateCommand(LastPageCommandImplementation)); } }

        private void LastPageCommandImplementation()
        {
            IEnumerable<CountryViewItem> response = pagination.LastPageItems();
            Refresh(response, pagination.LastPage);
        }

        #endregion

        #region Private Methods

        private void Refresh(IEnumerable<CountryViewItem> response, int page)
        {
            if (response == null)
                return;         
                                                                     
            CountriesViewItem.Clear();

            foreach (CountryViewItem country in response)
            {
                CountriesViewItem.Add(country);
            }
        }

        private void ConfigurePagination(int totalItems, int pageSize, Func<int, int, IEnumerable<CountryViewItem>> searchCommand)
        {
            pagination.ConfigurePaginationSettings(totalItems, pageSize, searchCommand);
        }

        #endregion
    }
}
