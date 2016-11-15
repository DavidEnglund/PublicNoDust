using System;
using System.Collections.Generic;
using System.Text;
using Dustbuster;
using UIKit;
using EventKitUI;
using EventKit;
using Foundation;
using Dustbuster.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(IOSReminderService))]
namespace Dustbuster.iOS
{
    public class IOSReminderService : IReminderService
    {

        public void AddReminder(String title, String message, DateTime RemindTime)
        {
            // create a new reminder and set its title and message
            EKReminder reminder = EKReminder.Create(eventStore);
            reminder.Title = title;

            reminder.Notes = message;

            // adding the remind time to the reminder as its due date and alarm time
            // an alarm time
            EKAlarm timeToRing = new EKAlarm();
            NSDate nsDate = (NSDate)DateTime.SpecifyKind(RemindTime, DateTimeKind.Utc);
            timeToRing.AbsoluteDate = nsDate;

            reminder.AddAlarm(timeToRing);

            // date components required to add to the reminder
            NSDateComponents dueDate = new NSDateComponents();
            dueDate.Calendar = new NSCalendar(NSCalendarType.Gregorian);
            // all of these value need to be applied induvidualy - although in xcode there is a way to apply them at once
            dueDate.Year = RemindTime.Year;
            dueDate.Month = RemindTime.Month;
            dueDate.Day = RemindTime.Day;
            dueDate.Hour = RemindTime.Hour;
            dueDate.Minute = RemindTime.Minute;
            dueDate.Second = RemindTime.Second;

            if (dueDate.IsValidDate)
            {
                // well that seems to be a valid date component so lets set the start and due date to it
                reminder.StartDateComponents = dueDate;
                reminder.DueDateComponents = dueDate;
            }
            // and finnaly lets save it to the phone's calendar - 1st create an error too record to then set the calendar to save to then save to it
            NSError e;
            reminder.Calendar = eventStore.DefaultCalendarForNewReminders;
            eventStore.SaveReminder(reminder, true, out e);
            // alert user if the reminder could not be added
            if (e != null)
            {

                UIAlertController.Create("Failed to add reminder", e.ToString(), UIAlertControllerStyle.Alert);
            }

        }

        // creating a service that can be used to add reminders to - copied code from the net: https://forums.xamarin.com/discussion/36062/eventkit-where-to-store-ekeventstore-when-using-forms
        public EKEventStore eventStore { get; set; }
        private bool accessGranted = false;

        public void CreateService()
        {
            if (eventStore == null)
            {
                eventStore = new EKEventStore();
                eventStore.RequestAccess(EKEntityType.Reminder,
                    (bool granted, NSError e) =>
                    {
                        if (granted)
                        {
                            accessGranted = true;
                        }
                        else
                        {
                            accessGranted = false;
                        }
                    });
            }
        }
    }
}
