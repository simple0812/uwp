using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace WebViewDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            //wv.Navigate(new Uri("http://www.baidu.com"));

            wv.NavigationStarting += (obj, e) =>
            {
                Debug.WriteLine("NavigationStarting");
            };
            // web 页面加载中
            wv.ContentLoading += (obj, e) =>
            {
                Debug.WriteLine("ContentLoading");
            };
            // web 页面的 DOM 加载完成
            wv.DOMContentLoaded += (obj, e) =>
            {
                Debug.WriteLine("DOMContentLoaded");
            };
            // web 页面导航完成（成功或失败）
            wv.NavigationCompleted += (obj, e) =>
            {
                Debug.WriteLine("NavigationCompleted");
            };

            wv.NavigationFailed += (obj, e) =>
            {
                Debug.WriteLine(e);
                Debug.WriteLine("NavigationFailed");
            };
        }

        private void WebView_OnScriptNotify(object sender, NotifyEventArgs e)
        {
          tb.Text= e.Value;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var url = tb.Text.Trim().ToLower();
            if (String.IsNullOrEmpty(url))
            {
                return;
            }

            if (url.IndexOf(@"http://", StringComparison.Ordinal) != 0)
            {
                url = "http://" + url;
            }

            wv.Navigate(new Uri(url));
        }

        private async void InvokeJs_OnClick(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("InvokeJs_OnClick");
            var result = await wv.InvokeScriptAsync("sayHelloToJs", new string[] { "zhanglei" });
            await new MessageDialog(result).ShowAsync();
        }


        // 通过 eval 方式访问 DOM
        private async void btnAccessDOM_Click(object sender, RoutedEventArgs e)
        {
            // 获取 document.title 的值
            List<string> arguments = new List<string> { "document.title" };
            string result = await wv.InvokeScriptAsync("eval", arguments);

            await new MessageDialog(result).ShowAsync();
        }

        // 通过 eval 向页面中注册 JavaScript 脚本
        private async void btnRegisterJavaScript_Click(object sender, RoutedEventArgs e)
        {
            // 向 html 中注册脚本
            List<string> arguments = new List<string> { "function xxx(){return '由 app 向 html 注册的脚本返回的数据';}" };
            await wv.InvokeScriptAsync("eval", arguments);

            // 调用刚刚注册的脚本
            string result = await wv.InvokeScriptAsync("xxx", null);

            await new MessageDialog(result).ShowAsync();
        }
    }

    public class JsInvokeModel
    {
        public string Type { get; set; }
    }
}
