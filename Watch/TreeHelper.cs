using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace Watch
{
    public static class TreeHelper
    {
        public static T FindChild<T>(DependencyObject root) where T : class
        {
            Queue<DependencyObject> queue = new Queue<DependencyObject>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                DependencyObject dependencyObject = queue.Dequeue();
                int num = VisualTreeHelper.GetChildrenCount(dependencyObject) - 1;
                while (0 <= num)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(dependencyObject, num);
                    T t = child as T;
                    if (t != null)
                    {
                        return t;
                    }
                    queue.Enqueue(child);
                    num--;
                }
            }
            return default(T);
        }

        public static List<T> FindChildren<T>(object parent) where T : UIElement
        {
            List<T> visualCollection = new List<T>();
            GetVisualChildCollection(parent as DependencyObject, visualCollection);
            return visualCollection;
        }

        private static void GetVisualChildCollection<T>(DependencyObject parent, List<T> visualCollection) where T : UIElement
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    visualCollection.Add(child as T);
                }
                else if (child != null)
                {
                    GetVisualChildCollection(child, visualCollection);
                }
            }
        }

        public static VisualStateGroup FindVisualState(FrameworkElement element, string name)
        {
            if (element == null)
                return null;

            IList groups = (IList)VisualStateManager.GetVisualStateGroups(element);
            return groups.Cast<VisualStateGroup>().FirstOrDefault(group => group.Name == name);
        }
    }
}
