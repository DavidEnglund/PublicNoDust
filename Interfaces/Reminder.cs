using System;

namespace Dustbuster
{
    public interface IReminderervice
    {
        void AddReminder(String title, String message, DateTime remindTime);        
        // this is required for iOS to setup the calendar connection service - it's required to have a refernece in android but is can just be empty
        // then called once and do nothing 
        void CreateService();
    }
}
