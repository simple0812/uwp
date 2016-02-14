using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace ControlDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            btnDel.Flyout?.Hide();
        }

        private void UIElement_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                var flyout = FlyoutBase.GetAttachedFlyout(element);
                if (flyout!= null)
                {
                    flyout.ShowAt(element);
                    flyout.Opened += async (obj, args) =>
                    {
                        await Task.Delay(2000);
                        flyout.Hide();
                    };
                }
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            if (element != null)
            {
                FlyoutBase.ShowAttachedFlyout(element);
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            // VisualTreeHelper 中新增了 GetOpenPopups() 方法，可以获取可视树中的全部 Popup 对象
            var popups = VisualTreeHelper.GetOpenPopups(Window.Current);
            foreach (var popup in popups)
            {
                popup.IsOpen = false;
            }
        }
        private void pp1_Click(object sender, RoutedEventArgs e)
        {
            pp1.IsOpen = true;
        }

        private void pp2_Click(object sender, RoutedEventArgs e)
        {
            pp2.IsOpen = true;
        }
    }
}
