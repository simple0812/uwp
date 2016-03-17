using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using NCrontab;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace SomeTestDemo
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Foo();
        }

        private void Foo()
        {
            var task1 = new Task(async () =>
            {
                Debug.WriteLine("Begin");
                await Task.Delay(10000000);
                Debug.WriteLine("Finish");
            });
            Debug.WriteLine("Before start:" + task1.Status);
            task1.Start();
            Debug.WriteLine("After start:" + task1.Status);
            task1.Wait();
            Debug.WriteLine("After Finish:" + task1.Status);

            var c =  CrontabSchedule.Parse("* * * * *");
            var start = DateTime.Now.AddMinutes(1);
            var end = DateTime.Now.AddMinutes(5);
            var p = c.GetNextOccurrences(start, end);
            var t = c.GetNextOccurrence(start);

            foreach (var each in p)
            {
                Debug.WriteLine(each.ToString("yyyy MMMM dd HH:mm:ss"));
            }
        }

        private void TextBox_OnTextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            Debug.WriteLine("...");
        }

        private void TextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            var txt = textBox.Text;
            if (String.IsNullOrEmpty(txt))
            {
                textBox.Text = "";
            }
            else
            {
                textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
                textBox.SelectionStart = textBox.Text.Length;
            }
        }

        private void TextBox_OnTextCompositionChanged(TextBox sender, TextCompositionChangedEventArgs args)
        {
            Debug.WriteLine("x");
        }

        private void UIElement_OnKeyDown(object sender, KeyRoutedEventArgs e)
        {
            
        }

        private void TextBoxxx_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Debug.WriteLine("===============");
        }
    }
}
