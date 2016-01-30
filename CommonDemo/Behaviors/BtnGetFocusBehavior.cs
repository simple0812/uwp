using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Microsoft.Xaml.Interactivity;

namespace CommonDemo.Behaviors
{
    public sealed class BtnGetFocusBehavior : DependencyObject, IBehavior
    {
        private DependencyObject _associatedObject;

        private readonly Storyboard _focuStoryboard = new Storyboard();

        private const double AnimFrom = 1;

        private const double AnimTo = 1.5;

        private readonly DoubleAnimation _scaleXAnim = new DoubleAnimation();

        private readonly DoubleAnimation _scaleYAnim = new DoubleAnimation();

        public void Attach(DependencyObject associatedObject)
        {
            //获取关联的对象
            _associatedObject = associatedObject;
            if (associatedObject == null) return;
            var obj = _associatedObject as FrameworkElement;
            if (obj == null) return;

            var ct = new CompositeTransform();
            //添加CompositeTransform缩放支持
            obj.RenderTransform = ct;

            //设置动画关联对象
            Storyboard.SetTarget(_scaleXAnim, obj.RenderTransform);
            Storyboard.SetTargetProperty(_scaleXAnim, nameof(CompositeTransform.ScaleX));

            //设置动画关联对象
            Storyboard.SetTarget(_scaleYAnim, obj.RenderTransform);
            Storyboard.SetTargetProperty(_scaleYAnim, nameof(CompositeTransform.ScaleY));

            //将动画添加到故事版
            _focuStoryboard.Children.Add(_scaleYAnim);
            _focuStoryboard.Children.Add(_scaleXAnim);

            //关联事件
            obj.PointerEntered += Obj_PointerEntered;
            obj.PointerExited += Obj_PointerExited;
        }

        private void Obj_PointerExited(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            StartScaleStoryboard(AnimTo, AnimFrom);
        }

        private void Obj_PointerEntered(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            StartScaleStoryboard(AnimFrom, AnimTo);
        }

        private void StartScaleStoryboard(double from, double to)
        {
            _scaleYAnim.From = _scaleXAnim.From = from;
            _scaleYAnim.To = _scaleXAnim.To = to;
            _scaleYAnim.EasingFunction = _scaleXAnim.EasingFunction = new ExponentialEase { Exponent = 4 };
            _scaleYAnim.Duration = _scaleXAnim.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            _focuStoryboard.Begin();
        }

        public void Detach()
        {
            var obj = _associatedObject as FrameworkElement;
            if (obj == null) return;
            //解除事件
            obj.PointerEntered -= Obj_PointerEntered;
            obj.PointerExited -= Obj_PointerExited;
        }

        public DependencyObject AssociatedObject => _associatedObject;
    }
}