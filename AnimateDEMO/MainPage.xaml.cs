using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace AnimateDEMO
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.Tapped += (obj, e) =>
            {
                ShowAnimate();
            };
            
            this.DoubleTapped += (o, e) =>
            {
                var spChildren = sp.Children;
                var len = spChildren.Count;
                for (int i = 0; i < len; i++)
                {
                    var tb = (TextBlock) spChildren[i];
                    tb.Opacity = 0;
                }
            };
        }


        private async void ShowAnimate()
        {
            var spChildren = sp.Children;
            var len = spChildren.Count;

            for (int i = 0; i < len; i++)
            {
                await Task.Delay(200);

                var tb = (TextBlock) spChildren[i];
                var sb = new Storyboard();
                DoubleAnimation da = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = new TimeSpan(0, 0, 0, 1),
                    FillBehavior = FillBehavior.HoldEnd
                };
                Storyboard.SetTarget(da, tb);
                Storyboard.SetTargetProperty(da, "Opacity");
                sb.Children.Add(da);
                sb.Begin();
            }
        }
    }
}
