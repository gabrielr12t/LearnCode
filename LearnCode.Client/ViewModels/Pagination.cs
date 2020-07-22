using LearnCode.Client.FderivControl.Util;
using LearnCode.Client.Util;
using System;
using System.Collections.Generic;

namespace LearnCode.Client.ViewModels
{
    public partial class Pagination<T> : ViewModelBase
    {
        private Func<int, int, IEnumerable<T>> _searchCommand;

        #region Paging
        public bool Teste => true;

        public int FirstPage => 0;
        public int LatestPage => totalPages * ItemsPerPage;
        public int NextPage => CurrentPage + 1;
        public int PreviousPage => CurrentPage - 1;
        public bool HasPreviousPage => CurrentPage > 0;
        public bool HasNextPage => NextPage < LatestPage;

        #endregion

        #region Properties

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

        private int itemsPerPage;
        public int ItemsPerPage
        {
            get { return itemsPerPage == 0 ? int.MaxValue : itemsPerPage; }
            private set { SetProperty(ref itemsPerPage, value, true); }
        }

        public void SetTotalPages(int totalItems)
        {
            double pagesQuantity = totalItems / ItemsPerPage;
            double pagesQuantityRoundedToUp = Math.Ceiling(pagesQuantity);
            this.TotalPages = (pagesQuantityRoundedToUp <= 1) ? 1 : (int)pagesQuantityRoundedToUp - 1;
            this.TotalItems = totalItems;
        }

        public void SetCurrentPage(int skipQuantity)
        {
            CurrentPage = skipQuantity;
        }

        public void SetItemsPerPage(int itemsPerPage = int.MaxValue)
        {
            ItemsPerPage = itemsPerPage;
        }

        public void SetSearchCommand(Func<int, int, IEnumerable<T>> searchCommand)
        {
            _searchCommand = searchCommand;
        }

        #endregion

        #region Navigate Commands

        public IEnumerable<T> FirstPageItems()
        {
            return Navigate(PageChanges.First);
        }

        public IEnumerable<T> NextPageItems()
        {
            return Navigate(PageChanges.Next);
        }

        public IEnumerable<T> PreviousPageItems()
        {
            return Navigate(PageChanges.Previous);
        }

        public IEnumerable<T> LastPageItems()
        {
            return Navigate(PageChanges.Last);
        }

        private IEnumerable<T> Navigate(PageChanges change)
        {
            switch (change)
            {
                case PageChanges.First:
                    return _searchCommand?.Invoke(FirstPage, ItemsPerPage);

                case PageChanges.Previous:
                    if (!HasPreviousPage)
                        return null;
                    return _searchCommand?.Invoke(PreviousPage, ItemsPerPage);

                case PageChanges.Current:
                    return _searchCommand?.Invoke(CurrentPage, ItemsPerPage);

                case PageChanges.Next:
                    if (!HasNextPage)
                        return null;

                    return _searchCommand?.Invoke(NextPage, ItemsPerPage);

                case PageChanges.Last:
                    return _searchCommand?.Invoke(LatestPage, ItemsPerPage);

                default:
                    return null;
            }
        }

        #endregion
    }
}
