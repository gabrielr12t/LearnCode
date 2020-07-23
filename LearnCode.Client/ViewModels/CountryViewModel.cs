using LearnCode.Client.Paging;
using LearnCode.Client.Util;
using LearnCode.Client.Util.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

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

        #region Property Set

        private ObservableCollection<CountryViewItem> countriesViewItem;
        public ObservableCollection<CountryViewItem> CountriesViewItem
        {
            get { return countriesViewItem; }
            private set { SetProperty(ref countriesViewItem, value, true); }
        }

        private int currentPage;
        public int CurrentPage
        {
            get { return currentPage; }
            private set
            {
                if (value <= totalPages)
                    SetProperty(ref currentPage, value, true);
            }
        }

        private int totalPages;
        public int TotalPages
        {
            get { return totalPages; }
            private set { SetProperty(ref totalPages, value, true); }
        }

        private int totalItems;
        public int TotalItems
        {
            get { return totalItems; }
            private set { SetProperty(ref totalItems, value, true); }
        }

        #endregion

        #region Paging

        public int FirstPage => (PagingConstants.DefaultFirstPage - 1) * PagingConstants.DefaultItemsPerPage;
        public int LastPage => TotalPages * PagingConstants.DefaultItemsPerPage;

        public int NextPage => (CurrentPage + 1) * PagingConstants.DefaultItemsPerPage;
        public int PreviousPage => (CurrentPage - 1) * PagingConstants.DefaultItemsPerPage;

        public bool HasNextPage => NextPage <= LastPage;
        public bool HasPreviousPage => PreviousPage >= FirstPage;
       
        #endregion

        #region Load Command

        private RelayCommand loadCommand;
        public RelayCommand LoadCommand { get { return GetOrSetProperty(ref loadCommand, () => this.CreateCommand(LoadCommandImplementation)); } }

        private void LoadCommandImplementation(object input)
        {
            int totalItems = countryService.GetCountries().Count();
            IEnumerable<CountryViewItem> response = countryService.GetCountries(FirstPage, PagingConstants.DefaultItemsPerPage);

            if (response != null)
            {
                TotalItems = totalItems;
                Refresh(response, FirstPage);
            }
        }

        #endregion

        #region Navigate Command

        private RelayCommand nextPageCommand;
        public RelayCommand NextPageCommand { get { return GetOrSetProperty(ref nextPageCommand, () => this.CreateCommand(NextPageCommandImplementation)); } }

        private void NextPageCommandImplementation(object input)
        {
            if (!HasNextPage)
                return;

            IEnumerable<CountryViewItem> response = countryService.GetCountries(NextPage, PagingConstants.DefaultItemsPerPage);
            Refresh(response, NextPage);
        }

        private RelayCommand previousPageCommand;
        public RelayCommand PreviousPageCommand { get { return GetOrSetProperty(ref previousPageCommand, () => this.CreateCommand(PreviousPageCommandImplementation)); } }

        private void PreviousPageCommandImplementation(object input)
        {
            if (!HasPreviousPage)
                return;

            IEnumerable<CountryViewItem> response = countryService.GetCountries(PreviousPage, PagingConstants.DefaultItemsPerPage);
            Refresh(response, PreviousPage);
        }

        private RelayCommand firstPageCommand;
        public RelayCommand FirstPageCommand { get { return GetOrSetProperty(ref firstPageCommand, () => this.CreateCommand(FirstPageCommandImplementation)); } }

        private void FirstPageCommandImplementation(object input)
        {
            IEnumerable<CountryViewItem> response = countryService.GetCountries(FirstPage, PagingConstants.DefaultItemsPerPage);
            Refresh(response, FirstPage);
        }

        private RelayCommand lastPageCommand;
        public RelayCommand LastPageCommand { get { return GetOrSetProperty(ref lastPageCommand, () => this.CreateCommand(LastPageCommandImplementation)); } }

        private void LastPageCommandImplementation(object input)
        {
            IEnumerable<CountryViewItem> response = countryService.GetCountries(LastPage, PagingConstants.DefaultItemsPerPage);
            Refresh(response, LastPage);
        }

        private RelayCommand toPageCommand;
        public RelayCommand ToPageCommand { get { return GetOrSetProperty(ref toPageCommand, () => this.CreateCommand(PageIndexCommandImplementation)); } }

        private void PageIndexCommandImplementation(object input)
        {
            int page;
            int.TryParse(input as string, out page);

            if (page < 0 || page > TotalPages)
                return;

            int nextItems = page * PagingConstants.DefaultItemsPerPage;
            IEnumerable<CountryViewItem> response = countryService.GetCountries(nextItems, PagingConstants.DefaultItemsPerPage);
            Refresh(response, nextItems);
        }

        #endregion

        #region Private Methods

        private void Refresh(IEnumerable<CountryViewItem> response, int currentSkipItem)
        {
            if (response == null)
                return;

            SetTotalPages(TotalItems);
            SetCurrentPage(currentSkipItem);

            CountriesViewItem.Clear();

            foreach (CountryViewItem country in response)
            {
                CountriesViewItem.Add(country);
            }
        }

        private void SetTotalPages(int totalItems)
        {
            double pagesQuantity = (Convert.ToDouble(totalItems) / Convert.ToDouble(PagingConstants.DefaultItemsPerPage));
            double pagesQuantityRoundedToUp = Math.Ceiling(pagesQuantity);
            TotalPages = (pagesQuantityRoundedToUp <= 1) ? 1 : Convert.ToInt32(pagesQuantityRoundedToUp) - 1;
        }

        private void SetCurrentPage(int skipQuantity)
        {
            int page = Convert.ToInt32((skipQuantity / PagingConstants.DefaultItemsPerPage));
            CurrentPage = page;
        }

        #endregion
    }
}
