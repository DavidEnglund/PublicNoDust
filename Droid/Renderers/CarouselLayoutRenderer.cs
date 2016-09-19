using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.Reflection;
using Java.Lang;
using System.Timers;
using Android.Widget;
using Android.Views;
using System.ComponentModel;
using Android.Graphics;
using Dustbuster;
using Dustbuster.Droid.Renderers;
using Android.Util;

[assembly: ExportRenderer(typeof(CarouselLayout), typeof(CarouselLayoutRenderer))]

namespace Dustbuster.Droid.Renderers
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
        HorizontalScrollView _scrollView;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            Log.Debug(tag, "~ Renderer - OnElementChanged started");
            

            base.OnElementChanged(e);
            if (e.NewElement == null) return;

            _deltaXResetTimer = new Timer(100) { AutoReset = false };
            _deltaXResetTimer.Elapsed += (object sender, ElapsedEventArgs args) => _deltaX = 0;

            _scrollStopTimer = new Timer(200) { AutoReset = false };
            _scrollStopTimer.Elapsed += (object sender, ElapsedEventArgs args2) => UpdateSelectedIndex();

            e.NewElement.PropertyChanged += ElementPropertyChanged;

            Log.Debug(tag, " ~  Renderer - OnElementChanged ended");
        }

        void ElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Log.Debug(tag, "~ Renderer - ElementPropertyChanged started");

            if (e.PropertyName == "Renderer")
            {
                // post Xamarin.Forms 2.2 implementation; gives Null Exception ~ Mike
                /*_scrollView = (HorizontalScrollView)typeof(ScrollViewRenderer)
                    .GetField("_hScrollView", BindingFlags.NonPublic | BindingFlags.Instance)
                    .GetValue(this);
                    */
                // pre Xam.Forms 2.2 implementation
                _scrollView = (HorizontalScrollView)typeof(ScrollViewRenderer)
                .GetField("hScrollView", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(this);

                _scrollView.HorizontalScrollBarEnabled = false;
                _scrollView.Touch += HScrollViewTouch;
            }
            if (e.PropertyName == CarouselLayout.SelectedIndexProperty.PropertyName && !_motionDown)
            {
                ScrollToIndex(((CarouselLayout)this.Element).SelectedIndex);
            }

            Log.Debug(tag, "~ Renderer - ElementPropertyChanged ended");
        }

        void HScrollViewTouch(object sender, TouchEventArgs e)
        {
            Log.Debug(tag, "~ Renderer - HScrollViewTouch started");

            e.Handled = false;

            switch (e.Event.Action)
            {
                case MotionEventActions.Move:
                    _deltaXResetTimer.Stop();
                    _deltaX = _scrollView.ScrollX - _prevScrollX;
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
        }

        void UpdateSelectedIndex()
        {
            Log.Debug(tag, "~ Renderer - UpdateSelectedIndex started");

            var center = _scrollView.ScrollX + (_scrollView.Width / 2);
            var carouselLayout = (CarouselLayout)this.Element;
            carouselLayout.SelectedIndex = (center / _scrollView.Width);

            Log.Debug(tag, "~ Renderer - UpdateSelectedIndex ended");
        }

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

    }
}