using Foundation;
using UIKit;

namespace Dustbuster.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            // this is to register for access to the calendar services 
            var settings = UIUserNotificationSettings.GetSettingsForTypes(
              UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.None
              , null);
            
            UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);

            #region raising with keyboard listeners
            //  initilizing the listeners to get any textfields that are being edited
            NSNotificationCenter.DefaultCenter.AddObserver(UITextField.TextFieldTextDidChangeNotification, txtBeginOrChange);
            NSNotificationCenter.DefaultCenter.AddObserver(UITextField.TextDidBeginEditingNotification, txtBeginOrChange);

            // initilizing the listeners required to handle the keyboard showing and hiding
            UIKeyboard.Notifications.ObserveWillShow(keyboardRaised);
            UIKeyboard.Notifications.ObserveWillHide(keyboardLowered);
            // I was going to use:
            // NSNotificationCenter.DefaultCenter.AddObserver(UIKeyboard.DidShowNotification, hideKB);
            // but the observe will show and hide animates the screen raising and lowering alongside the keyboard doing so.
            #endregion


            return base.FinishedLaunching(app, options);
        }
        #region raising with keyboard methods
        // ############################################### rasing text fields above keyboard methods ###############################################
        // a global variable to hold the text field to measure and decide if the app needs to raise the keyboard and how much to raise it by
        private UITextField currentlyEditingTextBox = new UITextField();

        // function to run when an entry starts or continues editing, this will handle saving the control for the keyboard show to use to raise the screen if needed
        private void txtBeginOrChange(NSNotification obj)
        {
            currentlyEditingTextBox = (UITextField)obj.Object;
        }

        // keyboard show method
        private void keyboardRaised(object sender, UIKeyboardEventArgs args)
        {
            // need to get the height of the keybard as different keyboard have different heights (numpad is higher than text for instance)
            //and different devices will have different heights as well.
            var KBHeight = (float)args.FrameEnd.Height;

            // this is the master window of the app and its this I'll use to get screen heights and this I'll move with the keyboard
            var mainWindow = UIApplication.SharedApplication.KeyWindow;
            var screenHeight = mainWindow.Frame.Height;
            var screenWidth = mainWindow.Frame.Width;
            // this first gets the screen Y of the entry then adds it's height plus a small padding then takes that from the height of the screen to get the difference
            var textBottomFromScreenBottom = screenHeight - (currentlyEditingTextBox.Superview.ConvertRectToView(currentlyEditingTextBox.Frame, mainWindow).Y + currentlyEditingTextBox.Frame.Height + 5);
            // checking that the entry being edited is lower than the keyboard and raising the screen by the difference if it is.
            if (textBottomFromScreenBottom < KBHeight)
            {
                mainWindow.Frame = new CoreGraphics.CGRect(0, -(KBHeight - textBottomFromScreenBottom), screenWidth, screenHeight);
            }
        }

        private void keyboardLowered(object sender, UIKeyboardEventArgs e)
        {
            // setting the phones screen back down now the keyboard has gone
            var mainWindow = UIApplication.SharedApplication.KeyWindow;
            mainWindow.Frame = new CoreGraphics.CGRect(0, 0, mainWindow.Frame.Width, mainWindow.Frame.Height);
        }
        #endregion
    }
}


