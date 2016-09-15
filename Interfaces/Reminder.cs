﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dustbuster
{
    /*
    class Reminder
    {
        public void Create(String message, DateTime remindTime)
        {

        }
    }
    */
    public interface IReminderervice
    {
        void AddReminder(String title, String message, DateTime remindTime);
        
        // this is required for iOS to setup the calendar connection service - it's required to have a refernece in android but is can just be empty
        // then called once and do nothing 
        void CreateService();
    }

}
