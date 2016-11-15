using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.Reflection;
using System.Timers;
using System.ComponentModel;
using Dustbuster;
using Dustbuster.iOS;
using UIKit;
using System.Diagnostics;


[assembly: ExportRenderer(typeof(CarouselLayout), typeof(CarouselLayoutRenderer))]

namespace Dustbuster.iOS
{
    /// <summary>
    ///  Much of this code needs to be added to a dependency service.
    ///  The iOS version needs to be created as well. ~ Mike
    /// </summary>

    class CarouselLayoutRenderer : ScrollViewRenderer
    {
        string tag = "LOG";

        int _prevScrollX;
        int _deltaX;
        bool _motionDown;
        Timer _deltaXResetTimer;
        Timer _scrollStopTimer;
      //  HorizontalScrollView _scrollView;
        UIScrollView _scrollView;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            //PauseAssistiveTechnologyNotification.Debug(tag, "~ Renderer - OnElementChanged started");
            

            base.OnElementChanged(e);
            if (e.NewElement == null) return;

            _deltaXResetTimer = new Timer(100) { AutoReset = false };
            _deltaXResetTimer.Elapsed += (object sender, ElapsedEventArgs args) => _deltaX = 0;

            _scrollStopTimer = new Timer(200) { AutoReset = false };
            _scrollStopTimer.Elapsed += (object sender, ElapsedEventArgs args2) => UpdateSelectedIndex();

            e.NewElement.PropertyChanged += ElementPropertyChanged;

         //   Log.Debug(tag, " ~  Renderer - OnElementChanged ended");
        }

        void ElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Log.Debug(tag, "~ Renderer - ElementPropertyChanged started");

            if (e.PropertyName == "Renderer")
            {
                // post Xamarin.Forms 2.2 implementation; gives Null Exception ~ Mike
                /*_scrollView = (HorizontalScrollView)typeof(ScrollViewRenderer)
                    .GetField("_hScrollView", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(this);
                    */
                // pre Xam.Forms 2.2 implementation
                _scrollView = this; //(UIScrollView)typeof(ScrollViewRenderer).GetField("hScrollView", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
                // set no scroll bar and no zooming
                _scrollView.ShowsHorizontalScrollIndicator = false;
                _scrollView.ShowsVerticalScrollIndicator = false;
                _scrollView.MaximumZoomScale = 1;
                _scrollView.MinimumZoomScale = 1;
                _scrollView.PagingEnabled = true;
                _scrollView.Scrolled += HScrollViewTouch;
                _scrollView.DraggingStarted += HScrollViewTouch;
                _scrollView.DraggingEnded += HScrollViewTouch;
            }
            if (e.PropertyName == CarouselLayout.SelectedIndexProperty.PropertyName && !_motionDown)
            {
                ScrollToIndex(((CarouselLayout)this.Element).SelectedIndex);
                
            }

         ///   Log.Debug(tag, "~ Renderer - ElementPropertyChanged ended");
        }


        void HScrollViewTouch(object sender, System.EventArgs e)
        {
            if (_scrollView.Dragging)
            {
                UpdateSelectedIndex();
            }
            Debug.WriteLine("---=== well does it even attempt to scroll ===---");
            /*
           // Log.Debug(tag, "~ Renderer - HScrollViewTouch started");
           e.
            e.Handled = false;

            switch (e.Event.Action)
            {
                case MotionEventActions.Move:
                    _deltaXResetTimer.Stop();
                    _deltaX = _scrollView.- _prevScrollX;
                    _prevScrollX = _scrollView.ScrollX;

                    UpdateSelectedIndex();

                    _deltaXResetTimer.Start();
                    break;
                case MotionEventActions.Down:
                    _motionDown = true;
                    _scrollStopTimer.Stop();
                    break;
                case MotionEventActions.Up:
                    _motionDown = false;
                    SnapScroll();
                    _scrollStopTimer.Start();
                    break;
            }

            Log.Debug(tag, "~ Renderer - HScrollViewTouch ended");
            /**/
        }

        void UpdateSelectedIndex()
        {
           // Log.Debug(tag, "~ Renderer - UpdateSelectedIndex started");

            //var center = _scrollView.ScrollX + (_scrollView.Width / 2);
            var carouselLayout = (CarouselLayout)this.Element;
            int page =  (int)(_scrollView.ContentOffset.X / _scrollView.Frame.Size.Width);
            carouselLayout.SelectedIndex = page; //(center / _scrollView.Width);
            
          //  Log.Debug(tag, "~ Renderer - UpdateSelectedIndex ended");
        }

        void ScrollToIndex(int targetIndex)
        {
            //Log.Debug(tag, "~ Renderer - ScrollToIndex method started");

            // set the target x to the index times the width of the scroll view // I hope this all works
            var targetX = targetIndex * _scrollView.Frame.Size.Width;
            // setup a new rectange with the scrollviews w and g with the y0 and the x set to the target x
            var scrollToRect = new CoreGraphics.CGRect();
            scrollToRect.Height = _scrollView.Frame.Size.Height;
            scrollToRect.Width = _scrollView.Frame.Size.Width;
            scrollToRect.X = targetX;
            scrollToRect.Y = 0;
            _scrollView.ScrollRectToVisible(scrollToRect,true);
            //_scrollView.Post(new Runnable(() => {
          //      _scrollView.SmoothScrollTo(targetX, 0);
          //  }));
        }
        /*
        void SnapScroll()
        {
            Log.Debug(tag, "~ Renderer - SnapScroll started");

            var roughIndex = (float)_scrollView.ScrollX / _scrollView.Width;

            var targetIndex =
                _deltaX < 0 ? Math.Floor(roughIndex)
                : _deltaX > 0 ? Math.Ceil(roughIndex)
                : Math.Round(roughIndex);

            ScrollToIndex((int)targetIndex);

            Log.Debug(tag, "~ Renderer - SnapScroll ended");
        }

        void ScrollToIndex(int targetIndex)
        {
            Log.Debug(tag, "~ Renderer - ScrollToIndex method started");
 

             var targetX = targetIndex * _scrollView.Width;
            _scrollView.Post(new Runnable(() => {
                _scrollView.SmoothScrollTo(targetX, 0);
            }));
        }

        bool _initialized = false;
        public override void Draw(Canvas canvas)
        {
            Log.Debug(tag, "~ Renderer - Draw (override method) started");

            base.Draw(canvas);
            if (_initialized) return;
            _initialized = true;
            var carouselLayout = (CarouselLayout)this.Element;
            _scrollView.ScrollTo(carouselLayout.SelectedIndex * Width, 0);
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            Log.Debug(tag, "~ Renderer - OnSizeChanged started");

            if (_initialized && (w != oldw))
            {
                _initialized = false;
            }
            base.OnSizeChanged(w, h, oldw, oldh);
        }
        /**/

    }
}