using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SomeTestDemo.Behaviors
{
    public class NumbericTextbox
    {
        public static readonly DependencyProperty InputFormatProperty = DependencyProperty.RegisterAttached("InputFormat", typeof(string), typeof(NumbericTextbox),
            new PropertyMetadata(null, new PropertyChangedCallback(
                 (o, e) =>
                 {
                     TextBox tb = o as TextBox;

                     if (tb != null)
                     {
                         tb.TextChanged += Tb_TextChanged;
                     }
                 }
                )));

        private static void Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            Debug.WriteLine(GetInputFormat(textBox));


            if (null == textBox) return;

            var txt = textBox.Text;

            int d = int.TryParse(txt, out d) ? d : -1;

            if (d >= 0) return;

            if (String.IsNullOrEmpty(txt))
            {
                textBox.Text = "";
            }
            else
            {
                textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
                if (textBox.Text != null) textBox.SelectionStart = textBox.Text.Length;
            }
        }

        public static string GetInputFormat(DependencyObject obj)
        {
            return obj.GetValue(InputFormatProperty) as string;
        }

        public static void SetInputFormat(DependencyObject obj, string value)
        {
            obj.SetValue(InputFormatProperty, value);
        }


    }


}
