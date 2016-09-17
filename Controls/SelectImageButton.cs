using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Dustbuster
{
    public class SelectImageButton : AbsoluteLayout
    {
		
		public static readonly BindableProperty SelectedBackgroundColorProperty = BindableProperty.Create("SelectedBackgroundColor", typeof(Color), typeof(SelectImageButton), Color.White);
		public static readonly BindableProperty UnselectedBackgroundColorProperty = BindableProperty.Create("UnselectedBackgroundColor", typeof(Color), typeof(SelectImageButton), Color.Silver);
		public static readonly BindableProperty HoldBackgroundColorProperty = BindableProperty.Create("HoldBackgroundColor", typeof(Color), typeof(SelectImageButton), Color.Olive);


		public static readonly BindableProperty SelectedBorderWidthProperty = BindableProperty.Create("SelectedBorderWidth", typeof(int), typeof(SelectImageButton), 5);
		public static readonly BindableProperty UnselectedBorderWidthProperty = BindableProperty.Create("SelectedBorderWidth", typeof(int), typeof(SelectImageButton), 5);

		public static readonly BindableProperty SelectedBorderColorProperty = BindableProperty.Create("SelectedBorderColor", typeof(Color), typeof(SelectImageButton), Color.Gray);
		public static readonly BindableProperty UnselectedBorderColorProperty = BindableProperty.Create("UnselectedBorderColor", typeof(Color), typeof(SelectImageButton), Color.Blue);

		public static readonly BindableProperty HoldBorderColorProperty = BindableProperty.Create("HoldBorderColor", typeof(Color), typeof(SelectImageButton), Color.Navy);
		public static readonly BindableProperty HoldBorderWidthProperty = BindableProperty.Create("HoldBorderWidth", typeof(int), typeof(SelectImageButton), 5);

        // the public get and set for the selected/unselected colors and widths
        public int SelectedBorderWidth
        {
			get { return (int)GetValue(SelectedBorderWidthProperty); }
			set { SetValue(SelectedBorderWidthProperty, value); }
        }
        public Color SelectedBorderColor
        {
			get { return (Color)GetValue(SelectedBorderColorProperty); }
            set { SetValue(SelectedBorderColorProperty, value); }
        }

        public Color SelectedBackgroundColor
        {
			get { return (Color)GetValue(SelectedBackgroundColorProperty); }
			set { SetValue(SelectedBackgroundColorProperty, value); }
        }
        
        public int UnselectedBorderWidth
        {
			get { return (int)GetValue(UnselectedBorderWidthProperty); }
			set { SetValue(UnselectedBorderWidthProperty, value); }
        }
        public Color UnselectedBorderColor
        {
			get { return (Color)GetValue(UnselectedBorderColorProperty); }
			set { SetValue(UnselectedBorderColorProperty, value); }
        }
        public Color UnselectedBackgroundColor
        {
			get { return (Color)GetValue(UnselectedBackgroundColorProperty); }
			set { SetValue(UnselectedBackgroundColorProperty, value); }
        }

        public Color HoldBackgroundColor
        {
			get { return (Color)GetValue(HoldBackgroundColorProperty); }
			set { SetValue(HoldBackgroundColorProperty, value); }
        }
        public Color HoldBorderColor
        {
			get { return (Color)GetValue(HoldBorderColorProperty); }
			set { SetValue(HoldBorderColorProperty, value); }
        }
        public int HoldBorderWidth
        {
			get { return (int)GetValue(HoldBorderWidthProperty); }
			set { SetValue(HoldBorderWidthProperty, value); }
        }

        // a private image and a public image source that maps to the images source
        private Image selectedImage = new Image();
        private Image unselectedImage = new Image();

        public ImageSource SelectedImage
        {
            get
            {
                return selectedImage.Source;
            }
            set
            {
                selectedImage.Source = value;
            }
        }
        public ImageSource UnselectedImage
        {
            get
            {
                return unselectedImage.Source;
            }
            set
            {
                unselectedImage.Source = value;
            }
        }
        // the  public get functions for the current modes looks
        public int BorderWidth
        {
            get
            {
                if (selected)
                {
                    return SelectedBorderWidth;
                }
                else
                {
                    return UnselectedBorderWidth;
                }
            }
        }
        public Color BorderColor
        {
            get
            {
                if (selected)
                {
                    return SelectedBorderColor;
                }
                else
                {
                    return UnselectedBorderColor;
                }
            }
        }
       new public Color BackgroundColor
        {
            get
            {
                if (selected)
                {
					return SelectedBackgroundColor;
                }
                else
                {
                    return UnselectedBackgroundColor;
                }
            }
        }
      
        // gets the currently displayed image
        public Image CurrentImage
        {
            get
            {
                if (selected)
                {
                    return selectedImage;
                }
                else
                {
                    return unselectedImage;
                }
            }
        }
      
        // a selected bool
        private bool selected = false;
        // public interface for getting the status of the selected bool
        public bool Selected
        {
            get { return selected; }
            set
            {
                if(buttonGroup != null)
                {
                    if(!value)
                    {
                        if (buttonGroup.Selected != this)
                        {
                            setAsUnselected();
                        }// no else - if the group wants me selected I stay selected
                    }
                    else
                    {
                        if(buttonGroup.Selected == this)
                        {
                            setAsSelected();
                        }else
                        {
                            // dont need to run set as selected - the group will set itself to me and then tell me to be selected running the true value of this if
                            buttonGroup.Selected = this;
                        }
                    }
                }
                else
                {
                    // I belong to nobody and do as I please
                    if (value)
                    {
                        setAsSelected();
                    }
                    else
                    {
                        setAsUnselected();
                    }
                }
            }     
        }

        // A function to set this button as selected
        private void setAsSelected()
        {
            selected = true;

            selectedImage.IsVisible = true;
            unselectedImage.IsVisible = false;
          
            // force a redraw to make it change the colors and stuff - could proably also use forceLayout but I want to change the background anyway.
            base.BackgroundColor = SelectedBackgroundColor;            
        }
        //A function to set this button as unselected
        private void setAsUnselected()
        {
            selected = false;

            selectedImage.IsVisible = false;
            unselectedImage.IsVisible = true;

            // force a redraw to make it change colors,images and border widths
            base.BackgroundColor = UnselectedBackgroundColor;
        }
        // a select button group for this control to belong to
        private SelectButtonGroup buttonGroup;
        // public interface for the button group this control belongs to
        public SelectButtonGroup ButtonGroup
        {
            get
            {
                return buttonGroup;
            }
            set
            {
                if (value != buttonGroup)
                {
                    if (buttonGroup != null)
                    {
                        buttonGroup.RemoveButton(this);
                    }
                    buttonGroup = value;
                    value.AddButton(this);
                }              
            }
        }
        // a click function
        private TapGestureRecognizer tapRecognizer = new TapGestureRecognizer();
        public event EventHandler Clicked
        {
            remove { tapRecognizer.Tapped -= value; }
            add { tapRecognizer.Tapped += value; }
        }

        // the init class
        public SelectImageButton()
        {
            // init a tap(click) object to react to presses
            tapRecognizer.Tapped += Click_Tapped;
            GestureRecognizers.Add(tapRecognizer);

            // images - set the selected to be invisible  and the add and layout both to the absoluteLayout
             selectedImage.IsVisible = false;

            Children.Add(selectedImage);
            Children.Add(unselectedImage);

            AbsoluteLayout.SetLayoutBounds(selectedImage, new Rectangle(.5, .5, 1, 1));
            AbsoluteLayout.SetLayoutFlags(selectedImage, AbsoluteLayoutFlags.All);

            AbsoluteLayout.SetLayoutBounds(unselectedImage, new Rectangle(.5, .5, 1, 1));
            AbsoluteLayout.SetLayoutFlags(unselectedImage, AbsoluteLayoutFlags.All);
       
           
            // also need a default to set the layout to not fill
            VerticalOptions = LayoutOptions.Center;
			HorizontalOptions = LayoutOptions.Center;
            
            base.BackgroundColor = UnselectedBackgroundColor;

             // forcing a layout to cause it to properly calculate the layers coords 1st time
            ForceLayout();         
        }

        private void Click_Tapped(object sender, EventArgs e)
        {
            // this click overrides the base android one so it cant record an up event to change the colors
            // but having a property change will run the property changed event so I will set the background 
            // to hold which will then be set right back again but will then change
            base.BackgroundColor = HoldBackgroundColor;
            // if the button is not part of a group we need to set it to toggle on/off
            if (buttonGroup != null)
            {
                Selected = true;
            }
            else
            {
                if (selected)
                {
                    Selected = false;
                }
                else
                {
                    Selected = true;
                }
            }
        }
        // an override of the padding - if its set  in xaml it sets te base so ill just check its more than 0 and set it when I size the thing
        // otherwise we will set the base padding to the largest border plus _Padding so the image is inside the largest border
        private Double padding = -1;
        new public Thickness Padding 
        {
            get { return new Thickness(padding); }
            set
            {
                padding = value.Bottom;
            }
        }
        // an override of the size request to get the width and height square
        protected override SizeRequest OnSizeRequest(double widthConstraint, double heightConstraint)
        {
            // adding border to the padding to get the image inside the border
            if(padding < 0)
            {
                padding = base.Padding.Bottom;
            }

            if(SelectedBorderWidth < UnselectedBorderWidth)
            {
                base.Padding = (padding * 1)  + (UnselectedBorderWidth*1);
            }
            else
            {
                base.Padding = (padding * 1) + SelectedBorderWidth;
            }
            // now to actualy call the size of the control - first call the base size request then work out which dimension is higher(or exisits) then send that for both
            double fullsize = 0;
            SizeRequest mysize =  base.OnSizeRequest(widthConstraint, heightConstraint);
            if(mysize.Request.Height > mysize.Request.Width || Double.IsInfinity(mysize.Request.Width))
            {
                fullsize += mysize.Request.Height;
            }
            else
            {
                fullsize += mysize.Request.Width;
            }
            // and now for a default size if there is no images
            if (SelectedImage == null && UnselectedImage == null)
            {
                fullsize += 20;
            }
            // everythings been added up now to apply it
            return new SizeRequest(new Size(fullsize,fullsize));
        }
    }
}
