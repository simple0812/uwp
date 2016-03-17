using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace GridViewDemo
{
    public sealed partial class CustomDatePicker : UserControl
    {
        public static readonly DependencyProperty StartTimeProperty =
            DependencyProperty.Register("StartTime", typeof(DateTime), typeof(CustomDatePicker), new PropertyMetadata(null));

        public static readonly DependencyProperty EndTimeProperty =
            DependencyProperty.Register("EndTime", typeof(string), typeof(CustomDatePicker), new PropertyMetadata(null));

        public DateTime SelectedTime { get; set; }
        public string Test { get; set; }

        public DateTime StartTime
        {
            get
            {
                if (StartTimeProperty != null) return (DateTime)GetValue(StartTimeProperty);
                return DateTime.Now;
            }
            set { SetValue(StartTimeProperty, value); }
        }

        public string EndTime
        {
            get { return (string)GetValue(EndTimeProperty); }
            set { SetValue(EndTimeProperty, value); }
        }

        public event Action<object> Completed;
        public event Action<object> Closed;

        public CustomDatePicker()
        {
            this.InitializeComponent();
            this.Loaded += (sender, args) =>
            {
                InitControl();
            };
        }


        private void InitControl()
        {
            Debug.WriteLine(Test);
            Debug.WriteLine(EndTime);
            calendar.MinDate = new DateTimeOffset(DateTime.Now);
            calendar.MaxDate = new DateTimeOffset(DateTime.Now.AddYears(1));
            var hours = new List<string>();
            var minutes = new List<string>();
            for (int i = 0; i < 60; i++)
            {
                if (i < 24)
                {
                    hours.Add(i.ToString());
                }

                minutes.Add(i.ToString());
            }

            gvHour.ItemsSource = hours;
            gvMinute.ItemsSource = minutes;
        }

        private void Calendar_OnSelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine(calendar.SelectedDates);

        }

        private void GvHour_OnItemClick(object sender, ItemClickEventArgs e)
        {
            calendar.Visibility = Visibility.Collapsed;
            gvHour.Visibility = Visibility.Collapsed;
            gvMinute.Visibility = Visibility.Visible;
        }

        private void GvMinute_OnItemClick(object sender, ItemClickEventArgs e)
        {
            //            calendar.Visibility = Visibility.Collapsed;
            //            gvHour.Visibility = Visibility.Visible;
            //            gvMinute.Visibility = Visibility.Collapsed;

        }

        private void BtnHour_OnClick(object sender, RoutedEventArgs e)
        {
            calendar.Visibility = Visibility.Visible;
            gvHour.Visibility = Visibility.Collapsed;
            gvMinute.Visibility = Visibility.Collapsed;
        }

        private void BtnMinute_OnClick(object sender, RoutedEventArgs e)
        {
            calendar.Visibility = Visibility.Collapsed;
            gvHour.Visibility = Visibility.Visible;
            gvMinute.Visibility = Visibility.Collapsed;
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            if (calendar.SelectedDates == null || calendar.SelectedDates.Count == 0)
            {
                new TopPopup().Show("日期未设置");
            }
            else
            {
                DateTimeOffset date = calendar.SelectedDates[0];
                var hour = gvHour.SelectedIndex;
                var minute = gvMinute.SelectedIndex;

                if (hour == -1)
                {
                    new TopPopup().Show("小时未设置");
                }

                if (minute == -1)
                {
                    new TopPopup().Show("分钟未设置");
                }

                SelectedTime = new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);

                CloseControl();
                Completed?.Invoke(this);
            }
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            CloseControl();
            Closed?.Invoke(this);
        }

        private void Calendar_OnSelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            calendar.Visibility = Visibility.Collapsed;
            gvHour.Visibility = Visibility.Visible;
            gvMinute.Visibility = Visibility.Collapsed;
        }

        private void CloseControl()
        {
            var fp = this.Parent as FlyoutPresenter;
            var popup = fp?.Parent as Popup;
            if (popup != null) popup.IsOpen = false;
        }
    }
}
