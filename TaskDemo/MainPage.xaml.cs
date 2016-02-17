using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace TaskDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            Debug.WriteLine("foo start............");
            Debug.WriteLine(Foo("fox").Result);
            Debug.WriteLine("foo end............");

            Debug.WriteLine("bar start............");
            var task = Bar("x");
            var awaiter = task.GetAwaiter();
            awaiter.OnCompleted(() =>
            {
                Debug.WriteLine(awaiter.GetResult());
                Debug.WriteLine("bar end............");
            });

        }

        private Task<string> Foo(string foo)
        {
            return Task.Run(() =>
            {
                var fx = foo + "foo";
                return fx;
            });
        }

        private async Task<string> Bar(string bar)
        {
            await Task.Run(() =>
            {
                Debug.WriteLine("bar task");
                var fx = bar + "foo";
                return fx;
            });

            Debug.WriteLine("bar task end");

            return bar + "bar";
        }

    }
}
