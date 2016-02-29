using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using ListViewDemo.Models;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace ListViewDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int _span = 0;
        public MainPage()
        {
            this.InitializeComponent();
            Init();
        }

        private void Init()
        {
            Articles list = new Articles();

            list.DataLoading += () =>
            {
                pr.IsActive = true;
            };

            list.DataLoaded += () =>
            {
                pr.IsActive = false;
            };

            lv.ItemsSource = list;
        }

        private void Item_OnLoaded(object sender, RoutedEventArgs e)
        {
            _span += 1000;
            if (_span >= 1000*20) _span = 1000;
            
            var sp = sender as StackPanel;
            var sb = new Storyboard();
            DoubleAnimation da = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new TimeSpan(0, 0, 0, _span / 1000),
                FillBehavior = FillBehavior.HoldEnd
            };
            Storyboard.SetTarget(da, sp);
            Storyboard.SetTargetProperty(da, "Opacity");
            sb.Children.Add(da);
            sb.Begin();
        }
    }
}
