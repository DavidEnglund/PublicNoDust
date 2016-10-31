using System;
using System.Linq;
using Xamarin.Forms;
using System.Collections;

namespace Dustbuster
{
    public interface ITabProvider
    {
        string ImageSource { get; set; }
    }

    public class PagerIndicatorDots : StackLayout
    {
        public PagerIndicatorDots()
        {
            HorizontalOptions = LayoutOptions.CenterAndExpand;
            VerticalOptions = LayoutOptions.Center;
            Orientation = StackOrientation.Horizontal;
            DotColor = Color.FromHex("#18b750");
        }
        

        private void CreateDot()
        {
            //Make one button and add it to the dotLayout

            Children.Add(new SelectImageButton
			{
				HeightRequest = DotSize,
				WidthRequest = DotSize,
				SelectedBackgroundColor = DotColor,
				UnselectedBackgroundColor = DotColor,
				SelectedBorderWidth = 0,
				UnselectedBorderWidth = 0,
				HoldBackgroundColor = DotColor,
				HoldBorderWidth = 0,

				Opacity = 0.4
			});
        }

        private void CreateTabs()
        {
            foreach (var item in ItemsSource)
            {
                var tab = item as ITabProvider;
                var image = new Image
                {
                    HeightRequest = 42,
                    WidthRequest = 42,
                    BackgroundColor = DotColor,
                    Source = tab.ImageSource,
                };

                Children.Add(image);
            }
        }

        private void ItemsSourceChanged()
        {
            if (ItemsSource == null) return;

            // Dots 
            var countDelta = ItemsSource.Count - Children.Count;

            if (countDelta > 0)
            {
                for (var i = 0; i < countDelta; i++)
                {
                    CreateDot();
                }                
            }
            else if (countDelta < 0)
            {
                for (var i = 0; i < -countDelta; i++)
                {
                    Children.RemoveAt(0);
                }
            }
        }

        private void SelectedItemChanged()
        {

            var selectedIndex = ItemsSource.IndexOf(SelectedItem);
            var pagerIndicators = Children.Cast<SelectImageButton>().ToList();

            foreach (var pi in pagerIndicators)
            {
                UnselectDot(pi);
            }

            if (selectedIndex > -1)
            {
                SelectDot(pagerIndicators[selectedIndex]);
            }
        }

        private static void UnselectDot(SelectImageButton dot)
        {
            dot.Opacity = 0.1;
        }

        private static void SelectDot(SelectImageButton dot)
        {
            dot.Opacity = 0.8;
        }

        #region Bindable Properties
        public static BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                nameof(ItemsSource),
                typeof(IList),
                typeof(PagerIndicatorDots),
                null,
                BindingMode.OneWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((PagerIndicatorDots)bindable).ItemsSourceChanged();
                }
        );

        public static BindableProperty SelectedItemProperty =
            BindableProperty.Create(
                nameof(SelectedItem),
                typeof(object),
                typeof(PagerIndicatorDots),
                null,
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((PagerIndicatorDots)bindable).SelectedItemChanged();
                }
        );
        #endregion


        //Properties
        public Color DotColor { get; set; }
        public double DotSize { get; set; }
        public IList ItemsSource
        {
            get
            {
                return (IList)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }
        public object SelectedItem
        {
            get
            {
                return GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }
    }
}