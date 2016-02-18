using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Web.Http;
using Newtonsoft.Json;

namespace ListViewDemo.Models
{
    class Articles : IncrementalLoadingList<Article>
    {
        public Articles()
        {

        }

        protected override async Task<IList<Article>> LoadMore(uint pageIndex, uint pageSize)
        {
            string json = await HttpHelper.SendGetRequest($"http://192.168.22.232:3003/foo?pageIndex={pageIndex}&pageSize={pageSize}");
            return JsonConvert.DeserializeObject<List<Article>>(json);
        }
    }
}
