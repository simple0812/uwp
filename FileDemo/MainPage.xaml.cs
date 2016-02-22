using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace FileDemo
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

        private async void Read_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                StorageFolder sf = ApplicationData.Current.LocalFolder;
                StorageFolder cacheFolder = await sf.CreateFolderAsync("cache", CreationCollisionOption.OpenIfExists);
                var file = await cacheFolder.CreateFileAsync("txt", CreationCollisionOption.OpenIfExists);
                using (var stream = await file.OpenStreamForReadAsync())
                {
                    using (StreamReader sd = new StreamReader(stream))
                    {
                        txt.Text = sd.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine("Read_OnClick");
                Debug.WriteLine(ex);
            }
            
        }

        private void Clear_OnClick(object sender, RoutedEventArgs e)
        {
            txt.Text = "";
        }

        private void Navigate_OnClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof (StringView), null);
        }

        private async void Save_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                return;
            }

            try
            {
                StorageFolder sf = ApplicationData.Current.LocalFolder;
                StorageFolder cacheFolder = await sf.CreateFolderAsync("cache", CreationCollisionOption.OpenIfExists);
                var file = await cacheFolder.CreateFileAsync("txt", CreationCollisionOption.ReplaceExisting);
                using (var stream = await file.OpenStreamForWriteAsync())
                {
                    using (StreamWriter sw = new StreamWriter(stream))
                    {
                        await sw.WriteAsync(txt.Text);
                        throw new Exception("xxx");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Save_OnClick");
                Debug.WriteLine(ex);
            }
            
        }
    }
}
