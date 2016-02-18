using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace GestureDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private GestureRecognizer _gestureRecognizer;

        public MainPage()
        {
            this.InitializeComponent();
            _gestureRecognizer = new GestureService().GtGestureListener(this);

            // 监测手势
            _gestureRecognizer.GestureSettings = GestureSettings.Hold | GestureSettings.CrossSlide;
            _gestureRecognizer.CrossSliding += (sender, args) =>
            {
                {
                    lblMsg.Text += Environment.NewLine;
                    lblMsg.Text += " CrossSliding: " + args.CrossSlidingState + "; " + args.Position.ToString();
                }
            };

            _gestureRecognizer.Holding += (sender, args) =>
            {
                lblMsg.Text += Environment.NewLine;
                lblMsg.Text += " Holding: " + args.HoldingState + "; " + args.Position.ToString();
            };
        }

        private void radVertical_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // 监测垂直 Cross Slide 手势
            _gestureRecognizer.CrossSlideHorizontally = false;
        }

        private void radHorizontal_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            // 监测水平 Cross Slide 手势
            _gestureRecognizer.CrossSlideHorizontally = true;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            lblMsg.Text = "";
        }
    }
}
