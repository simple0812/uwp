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
    class IncrementalLoadingList : ObservableCollection<Article>, ISupportIncrementalLoading
    {
        internal delegate void DataLoadingEventHandler();

        internal delegate void DataLoadedEventHandler();

        private bool _busy = false;
        private bool _has_more_items = false;
        private int _current_page = 1;
        public event DataLoadingEventHandler DataLoading;
        public event DataLoadedEventHandler DataLoaded;

        //private Func<uint, Task<IList<T>>> func;

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

        //public IncrementalLoadingList(Func<uint, Task<IList<T>>> _func)
        //{
        //    func = _func;
        //    HasMoreItems = true;
        //}

        public IncrementalLoadingList()
        {
            Debug.WriteLine(_current_page + "|" + TotalCount);
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
            return InnerLoadMoreItemsAsync(count).AsAsyncOperation();
        }
         
        private async Task<LoadMoreItemsResult> InnerLoadMoreItemsAsync(uint expectedCount)
        {
            await Task.Delay(1000);
            Debug.WriteLine(_current_page + "|" + TotalCount);

            _busy = true;
            var actualCount = 0;
            IList<Article> list = null;
            try
            {
                OnDataLoading();
                list = await Task.Run(() => new List<Article>()
                {
                    new Article() { Title ="aaaaaa", Content="aaaaaaaaaaaa1" },
                    new Article() { Title ="aaaaaa", Content="aaaaaaaaaaaa2" },
                    new Article() { Title ="aaaaaa", Content="aaaaaaaaaaaa3" },
                    new Article() { Title ="aaaaaa", Content="aaaaaaaaaaaa5" },
                    new Article() { Title ="aaaaaa", Content="aaaaaaaaaaaa6" },
                    new Article() { Title ="aaaaaa", Content="aaaaaaaaaaaa7" },
                    new Article() { Title ="aaaaaa", Content="aaaaaaaaaaaa8" },
                    new Article() { Title ="aaaaaa", Content="aaaaaaaaaaaa9" },
                    new Article() { Title ="aaaaaa", Content="aaaaaaaaaaaa10" },
                    new Article() { Title ="aaaaaa", Content="aaaaaaaaaaaa11" },
                    new Article() { Title ="aaaaaa", Content="aaaaaaaaaaaa12" },
                    new Article() { Title ="aaaaaa", Content="aaaaaaaaaaaa13" },
                    new Article() { Title ="aaaaaa", Content="aaaaaaaaaaaa14" },
                    new Article() { Title ="aaaaaa", Content="aaaaaaaaaaaa15" },
                    new Article() { Title ="aaaaaa", Content="aaaaaaaaaaaa16" }
                });
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
