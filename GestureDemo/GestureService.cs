using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;

namespace GestureDemo
{
    public class GestureService
    {
        private GestureRecognizer _gestureRecognizer;

        /*
        * Windows.UI.Input.GestureSettings 是一个 flag 枚举，其值包括：
        * None, Tap, DoubleTap, Hold, HoldWithMouse, RightTap, Drag, CrossSlide, ManipulationTranslateX, ManipulationTranslateY, ManipulationTranslateRailsX, ManipulationTranslateRailsY, ManipulationRotate, ManipulationScale, ManipulationTranslateInertia, ManipulationRotateInertia, ManipulationScaleInertia
        */
        public GestureRecognizer GtGestureListener(UIElement ele)
        {
            _gestureRecognizer = new GestureRecognizer();

            ele.PointerMoved += GestureRecognizerDemo_PointerMoved;
            ele.PointerPressed += GestureRecognizerDemo_PointerPressed;
            ele.PointerReleased += GestureRecognizerDemo_PointerReleased;

            return _gestureRecognizer;
        }

        private void GestureRecognizerDemo_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            // 告诉 GestureRecognizer 指针按下了，以便 GestureRecognizer 做手势监测
            _gestureRecognizer.ProcessDownEvent(e.GetCurrentPoint(null));
        }

        private void GestureRecognizerDemo_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            // 告诉 GestureRecognizer 指针移动中，以便 GestureRecognizer 做手势监测
            _gestureRecognizer.ProcessMoveEvents(e.GetIntermediatePoints(null));
        }

        private void GestureRecognizerDemo_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            // 告诉 GestureRecognizer 指针释放了，以便 GestureRecognizer 做手势监测
            _gestureRecognizer.ProcessUpEvent(e.GetCurrentPoint(null));
        }
    }
}
