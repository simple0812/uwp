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
    public class TextboxBehavior
    {
        public static readonly DependencyProperty InputStyleProperty = DependencyProperty.RegisterAttached("InputStyle", typeof(string), typeof(TextboxBehavior),
            new PropertyMetadata(null, new PropertyChangedCallback(
                 (o, e) =>
                 {
                     TextBox tb = o as TextBox;

                     if (tb != null)
                     {
//                         tb.GotFocus += tb_GotFocus;
//                         tb.LostFocus += tb_LostFocus;
                         tb.TextChanged += Tb_TextChanged;
                     }
                 }
                )));

        private static void Tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            Debug.WriteLine(GetInputStyle(textBox));


            if (null == textBox) return;

            var txt = textBox.Text;

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

        static void tb_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox control = sender as TextBox;
            string tempText = GetInputStyle(control);
            if (control != null && control.Text.Trim() == "")
                control.Text = tempText;
        }

        private static void tb_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox control = sender as TextBox;
            string tempText = GetInputStyle(control);
            if (control != null && control.Text.Trim() == tempText)
                control.Text = "";

        }

        public static string GetInputStyle(DependencyObject obj)
        {
            return obj.GetValue(InputStyleProperty) as string;
        }

        public static void SetInputStyle(DependencyObject obj, string value)
        {
            obj.SetValue(InputStyleProperty, value);
        }



    }
}
