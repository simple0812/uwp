using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace ListViewDemo.Models
{
    public abstract class IncrementalLoadingList<T> : ObservableCollection<T>, ISupportIncrementalLoading
    {
        public delegate void DataLoadingEventHandler();
        public delegate void DataLoadedEventHandler();

        private bool _busy = false;
        private bool _has_more_items = false;
        private int _current_page = 1;
        private const int PAGE_SIZE = 20;
        public event DataLoadingEventHandler DataLoading;
        public event DataLoadedEventHandler DataLoaded;

        protected abstract Task<IList<T>> LoadMore(uint pageIndex, uint pageSize);

        private int TotalCount
        {
            get; set;
        }



        public bool HasMoreItems
        {
            get
            {
                if (_busy)
                    return false;
                else
                    return _has_more_items;
            }
            private set
            {
                _has_more_items = value;
            }
        }

        protected IncrementalLoadingList()
        {
            HasMoreItems = true;
        }

        public void DoRefresh()
        {
            _current_page = 1;
            TotalCount = 0;
            Clear();
            HasMoreItems = true;
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return InnerLoadMoreItemsAsync().AsAsyncOperation();
        }
         
        private async Task<LoadMoreItemsResult> InnerLoadMoreItemsAsync()
        {
            await Task.Delay(1000);
            Debug.WriteLine(_current_page + "|" + TotalCount);

            _busy = true;
            var actualCount = 0;
            IList<T> list = null;
            try
            {
                OnDataLoading();
                list = await LoadMore((uint)_current_page, PAGE_SIZE);
            }
            catch (Exception)
            {
                HasMoreItems = false;
            }

            if (list != null && list.Any())
            {
                actualCount = list.Count;
                TotalCount += actualCount;
                _current_page++;
                HasMoreItems = true;

                foreach (var item in list)
                {
                    this.Add(item);
                }
            }
            else
            {
                HasMoreItems = false;
            }

            OnDataLoaded();
            _busy = false;
            return new LoadMoreItemsResult
            {
                Count = (uint)actualCount
            };
        }

        protected virtual void OnDataLoading()
        {
            DataLoading?.Invoke();
        }

        protected virtual void OnDataLoaded()
        {
            DataLoaded?.Invoke();
        }
    }
}
