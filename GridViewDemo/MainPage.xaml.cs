using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace GridViewDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            var hours = new List<string>();
            for (int i = 0; i < 24; i++)
            {
                hours.Add(i.ToString());
            }
            this.InitializeComponent();
            gvHour.ItemsSource = hours;
        }

        private void GvHour_OnItemClick(object sender, ItemClickEventArgs e)
        {
//            gvHour.Visibility = Visibility.Collapsed;
            new TopPopup().Show("hello world");
        }

        private void BtnHour_OnClick(object sender, RoutedEventArgs e)
        {
//            gvHour.Visibility = Visibility.Collapsed;
        }
    }


}
