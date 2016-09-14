using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections;
using System.Diagnostics;
using Dustbuster.Views;

namespace Dustbuster
{
    public class CarouselLayout : ScrollView
    {
        public StackLayout _stack;
        int _selectedIndex;
        Label[] thisClassLabels = new Label[5];
        int aSuperAwesomeCount = 0;

        public CarouselLayout(params Label[] labels)
        {
            for (int i = 0; i < thisClassLabels.Length; i++)
            {
                thisClassLabels[i] = labels[i];
            }



            Debug.WriteLine("LOG 005 ~ CarouselLayout constructor");
            Orientation = ScrollOrientation.Horizontal;
            _stack = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                //david
                Padding = 0,
                Spacing = 0
            };
            Content = _stack;
        }

        // populating the List with Stacklayout elements
        public IList<View> Children
        {
            get
            {
                return _stack.Children;
            }
        }

        private bool _layingOutChildren;
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            base.LayoutChildren(x, y, width, height);
            if (_layingOutChildren) return;

            _layingOutChildren = true;
            foreach (var child in Children)
            {
                child.WidthRequest = width;
                child.HeightRequest = height;
            }
            _layingOutChildren = false;
        }

        public static readonly BindableProperty SelectedIndexProperty =
            BindableProperty.Create(
                nameof(SelectedIndex),
                typeof(int),
                typeof(CarouselLayout),
                0,
                BindingMode.TwoWay,
                propertyChanged: async (bindable, oldValue, newValue) =>
                {
                    await ((CarouselLayout)bindable).UpdateSelectedItem();
                }
            );

        public int SelectedIndex
        {
            get
            {
                return (int)GetValue(SelectedIndexProperty);
            }
            set
            {
                SetValue(SelectedIndexProperty, value);
            }
        }

        private async Task UpdateSelectedItem()
        {
            await Task.Delay(300);
            SelectedItem = SelectedIndex > -1 ? Children[SelectedIndex].BindingContext : null;
            Debug.WriteLine("LOG ~ UpdateSelectedItem {0}", SelectedItem);
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                nameof(ItemsSource),
                typeof(IList),
                typeof(CarouselLayout),
                null,
                propertyChanging: (bindableObject, oldValue, newValue) =>
                {
                    ((CarouselLayout)bindableObject).ItemsSourceChanging();
                },
                propertyChanged: (bindableObject, oldValue, newValue) =>
                {
                    ((CarouselLayout)bindableObject).ItemsSourceChanged();
                }
            );

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

        private void ItemsSourceChanging()
        {
            if (ItemsSource == null) return;
            _selectedIndex = ItemsSource.IndexOf(SelectedItem);
            Debug.WriteLine("LOG ~ Item sourced changing to {0}", _selectedIndex);
        }

        private void ItemsSourceChanged()
        {
            _stack.Children.Clear();
            foreach (var item in ItemsSource)
            {
                var view = (View)ItemTemplate.CreateContent();
                var bindableObject = view as BindableObject;
                if (bindableObject != null)
                    bindableObject.BindingContext = item;
                _stack.Children.Add(view);
            }

            if (_selectedIndex >= 0)
                SelectedIndex = _selectedIndex;

            Debug.WriteLine("LOG ~ Item source changed to {0}", SelectedIndex);
        }

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(
                nameof(SelectedItem),
                typeof(object),
                typeof(CarouselLayout),
                null,
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((CarouselLayout)bindable).UpdateSelectedProduct();
                }
            );

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

        private void UpdateSelectedProduct()
        {
            if (SelectedItem == BindingContext) return;

            SelectedIndex = Children
                .Select(c => c.BindingContext)
                .ToList()
                .IndexOf(SelectedItem);

            //This very much almost does not work at 100% (means it's broken)
            /*thisClassLabels[0].Text = "CHANGED !! " + aSuperAwesomeCount.ToString();
            thisClassLabels[1].Text = "CHANGED !!";
            thisClassLabels[2].Text = "CHANGED !!";
            thisClassLabels[3].Text = "CHANGED !!";
            thisClassLabels[4].Text = "CHANGED !!";
            aSuperAwesomeCount++;*/

            Debug.WriteLine("LOG ~ UpdateSelectedProduct {0}", SelectedItem);
        }

        //Not original code added  to create a bindable property for title
        public static readonly BindableProperty SelectedTitleProperty =
            BindableProperty.Create(
                nameof(SelectedTitle),
                typeof(string),
                typeof(CarouselLayout),
                "0",
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((CarouselLayout)bindable).UpdateSelectedTitle();
                }
            );

        public string SelectedTitle
        {
            get
            {
                return (string)GetValue(SelectedTitleProperty);
            }
            set
            {
                SetValue(SelectedTitleProperty, value);
            }
        }

        private void UpdateSelectedTitle()
        {
            SelectedTitle = ProductPage.viewModel.Pages.ElementAt(_selectedIndex).Title;
            Debug.WriteLine("LOG Title Changed to: {0}", SelectedTitle);
        }


        public DataTemplate ItemTemplate { get; set; }


        
    }

}