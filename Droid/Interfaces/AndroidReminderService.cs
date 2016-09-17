using System;
using Android.Content;
using Dustbuster.Droid;
using Android.Provider;
using Xamarin.Forms;
using System.Diagnostics;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidReminderService))]
namespace Dustbuster.Droid
{
    class AndroidReminderService : IReminderervice
    {
        /// <summary>
        /// Handles the reminder insertion to the Android device calendar
        /// </summary>
        /// <param name="title">The title of the reminder</param>
        /// <param name="message">The reminder message to be stored</param>
        /// <param name="remindDate">The date picked from the Contact Request Page that triggers the reminder</param>        

        public void AddReminder(string title, string message, DateTime remindDate, string remindTime)
        {
            // Sets the reminder to the appropriate time. The implementation here may not be ideal, and will also need to be done on iOS.
            // it should probably go in the view model ~ MM
            switch (remindTime)
            {
                case "Morning":
                    remindDate = remindDate.AddHours(8.00);
                    break;
                case "Afternoon":
                    remindDate = remindDate.AddHours(12.00);
                    break;
                case "Evening":
                    remindDate = remindDate.AddHours(16.00);
                    break;
                default:
                    remindDate = remindDate.AddHours(8.00);
                    break;
            }

            Debug.WriteLine("~ LOG ~ DATE TIME IS: {0} , YOU SELECTED {1} ", remindDate.ToString(), remindTime );

            // well here is where the magic happens I guess
            // start with getting  content value object to add the event details to
            ContentValues eventValues = new ContentValues();
            // now we need to get a list of the calendars and check at least one exists 
            // WARNING: I am using just the fist calendar found, and this may be a problem for at least some users and I might need to go though them all and match on a priority list
            // get the string values of the columns we want to get out of the calendars list
            string[] calendarsProjection = {
                CalendarContract.Calendars.InterfaceConsts.Id,
                CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
                CalendarContract.Calendars.InterfaceConsts.AccountName
            };
            // now getting the list of calendars on the phone then chekcing that there is one and then either using it or alerting the user that there is no calendar
            var cursor = Forms.Context.ContentResolver.Query(CalendarContract.Calendars.ContentUri, calendarsProjection, null, null, null);
            if (!cursor.MoveToNext())
            {
                App.Current.MainPage.DisplayAlert("Calendar error", "You have no calendar setup", "OK");
            }
            else
            {
                // setting the calendar it will be added to then the title,description and attendee(self)
                eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, cursor.GetInt(0));
                eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, title);
                eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, message);
                eventValues.Put(CalendarContract.Events.InterfaceConsts.SelfAttendeeStatus, "1");
                // setting the start date and end date of the event. event will end one minute after start
                eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, Convert.ToInt64((remindDate.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalMilliseconds));
                eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend, Convert.ToInt64((remindDate.AddMinutes(1).ToUniversalTime() - new DateTime(1970, 1, 1)).TotalMilliseconds));

                // setting the timezone to local time
                eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, TimeZone.CurrentTimeZone.StandardName);
                eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, TimeZone.CurrentTimeZone.StandardName);
                // telling the event that it has an alarm
                eventValues.Put(CalendarContract.Events.InterfaceConsts.HasAlarm, 1);
                // adding the event to the calendar and then geting its id now it is added and has one
                var uri = Forms.Context.ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);
                int id = int.Parse(uri.LastPathSegment);

                // and now for a reminder for the event
                ContentValues reminderValue = new ContentValues();
                // this reminder will only need the event id, a time offset(from the start of the event) and a method of alerting the user(notification chosen but email is availible)
                reminderValue.Put(CalendarContract.Reminders.InterfaceConsts.EventId, id);
                reminderValue.Put(CalendarContract.Reminders.InterfaceConsts.Minutes, 0);
                reminderValue.Put(CalendarContract.Reminders.InterfaceConsts.Method, 1);
                // adding the reminder to the calendar event
                var uriR = Forms.Context.ContentResolver.Insert(CalendarContract.Reminders.ContentUri, reminderValue);

                App.Current.MainPage.DisplayAlert("All good.", "A reminder has been set in your phone's calendar", "OK");
            }
        }

        public void CreateService()
        {
          // might need to do a ignore something here as android does not need to register calendar access the same way as ios
          // if I have time I'll do find out how to check for permission and alert user if not granted but otherwise for android 
          // this will remain empty to be called once and do nothing
        }
    }
}