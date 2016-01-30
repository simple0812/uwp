using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace ScreenDemo
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


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // 在锁屏设置界面，如果将本 app 设置为背景提供程序，则锁屏界面上会有一个名为“打开应用”的按钮，点击后会通过 /MainPage.xaml?WallpaperSettings=1 启动本 app
            // 相关的 UriMapper 参见 MyUriMapper.cs

            //IDictionary<string, string> queryStrings = this.NavigationContext.QueryString;
            
            //if (e.Parameter.("WallpaperSettings"))
            //    lblMsg.Text = "在锁屏界面启动了本 app，启动的 url 是：" + e.Uri.ToString();

            base.OnNavigatedTo(e);
        }

        // 跳转到锁屏设置界面
        private async void btnGotoLockScreen_Click(object sender, RoutedEventArgs e)
        {
            bool success = await Launcher.LaunchUriAsync(new Uri("ms-settings-lock:"));
        }

        // 发送信息到锁屏
        private void btnSendMessageToLockScreen_Click(object sender, RoutedEventArgs e)
        {

            Debug.Write(new Windows.ApplicationModel.LockScreen.LockScreenInfo());
            // 锁屏信息来自 tile（StandardTileData, FlipTile, IconicTile, CycleTile 均可）
            //ShellTile shellTile = ShellTile.ActiveTiles.First();
            //if (shellTile != null)
            //{
            //    StandardTileData tile = new StandardTileData();
            //    tile.BackContent = "发送信息到锁屏"; // 需要在锁屏上显示的文本内容（需要在锁屏界面设置本 app 为显示详细状态）
            //    tile.Count = 10; // 需要在锁屏上显示的数字内容，图标来自 <DeviceLockImageURI /> 节点的设置（需要在锁屏界面设置本 app 为显示即时状态）

            //    shellTile.Update(tile);
            //}
        }

        // 修改锁屏背景
        private  void btnLockScreenBackground_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    /*
            //     * 注：
            //     * 在项目根目录下增加 DefaultLockScreen.jpg 文件，用于当 app 成为锁屏背景的实际提供程序却没有设置锁屏背景时，作为锁屏背景的默认图片
            //     * 比如用户在锁屏设置页面选择了本 app 作为锁屏背景的实际提供程序，此时本 app 无法设置锁屏背景，那么锁屏背景图就会被设置为项目根目录下的 DefaultLockScreen.jpg 文件
            //     */

            //    // 判断本 app 是否是锁屏背景的实际提供程序
            //    bool isProvider = LockScreenManager.IsProvidedByCurrentApplication;
            //    if (!isProvider)
            //    {
            //        // 请求成为锁屏背景的实际提供程序（会弹出一个对话框）
            //        LockScreenRequestResult result = await LockScreenManager.RequestAccessAsync();

            //        // LockScreenRequestResult.Granted - 用户已允许；LockScreenRequestResult.Denied - 用户已拒绝
            //        isProvider = result == LockScreenRequestResult.Granted;
            //    }

            //    if (isProvider)
            //    {
            //        // 图片地址支持“ms-appx:///”和“ms-appdata:///Local/”，文件名必须与当前锁屏背景的文件名不同
            //        Uri uri = new Uri("ms-appx:///Assets/AppTile.png", UriKind.Absolute);

            //        // 设置当前锁屏的背景图
            //        Windows.Phone.System.UserProfile.LockScreen.SetImageUri(uri);

            //        // 获取当前锁屏的背景图的 uri
            //        Uri currentUri = Windows.ApplicationModel.LockScreen.LockScreenInfo
            //        lblMsg.Text = "当前锁屏的背景图的 url: " + currentUri.ToString();
            //    }
            //    else
            //    {
            //        lblMsg.Text = "用户不允许此 app 成为锁屏背景的实际提供程序";
            //    }
            //}
            //catch (Exception ex)
            //{
            //    lblMsg.Text = ex.ToString();
            //}
        }
    }
}
