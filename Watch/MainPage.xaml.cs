using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Input;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using Microsoft.VisualBasic.CompilerServices;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace Watch
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            InitWatch(cvs);
            InitWatch(cvsx);

            //使用Storyboard实现动画
            sb.Begin();
            Animate();
        }

        //使用定时器实现动画
        private async void Animate()
        {
            SetTime(cvs);
            await Task.Delay(1000);
            Animate();
        }

        private void SetTime(Canvas canvas)
        {
            var time = DateTime.Now;
            double curHour = time.Hour % 12;
            double curMinute = time.Minute;
            double curSecond = time.Second;

            List<Line> lines = TreeHelper.FindChildren<Line>(canvas);

            if(lines.Count < 3) return;
            var xhour = lines[2];
            var xminute = lines[1];
            var xsecond = lines[0];

            xhour.RenderTransform = new RotateTransform
            {
                CenterX = 100,
                CenterY = 100,
                Angle = curHour * 30 + curMinute / 2
            };

            xminute.RenderTransform = new RotateTransform
            {
                CenterX = 100,
                CenterY = 100,
                Angle = curMinute * 6 + curSecond / 10
            };

            xsecond.RenderTransform = new RotateTransform
            {
                CenterX = 100,
                CenterY = 100,
                Angle = curSecond * 6
            };
        }

        private void InitWatch(Canvas canvas)
        {
            for (int i = 0; i < 60; i++)
            {
                var thickness = 1;
                var height = 5;
                if (i % 5 == 0)
                {
                    thickness = 2;
                    height = 10;
                }
                var line = new Line
                {
                    X1 = 100,
                    X2 = 100,
                    Y1 = 0,
                    Y2 = height,
                    Stroke = new SolidColorBrush(Colors.White),
                    StrokeThickness = thickness,
                    RenderTransform = new RotateTransform() { CenterX = 100, CenterY = 100, Angle = i * 6 }
                };

                canvas.Children.Add(line);
            }

            SetTime(canvas);
        }
    }
}
