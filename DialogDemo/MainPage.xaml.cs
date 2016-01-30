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

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace DialogDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            PrintVisualTree(0, sp);
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("hello world", "message");

            dialog.Commands.Add(new UICommand("yes", cmd => { btnTest.Content = "haha"; }, commandId:0));
            dialog.Commands.Add(new UICommand("no", cmd => { btnTest.Content = "hehe"; }, commandId:1));

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;
            await dialog.ShowAsync();
        }

        private void PrintVisualTree(int depth, DependencyObject obj)
        {
            Debug.WriteLine(depth + " " + obj);
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                PrintVisualTree(depth + 1, VisualTreeHelper.GetChild(obj, i));
        }
    }
}
