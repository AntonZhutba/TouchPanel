using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using TouchPanel.Models.Interfaces;
using Xamarin.Forms;

namespace TouchPanel.ViewModels
{
    public class CurrentDateViewModel : BaseViewModel
    {
        private string _currentTime;
        private string _currentDate;

        public CurrentDateViewModel()
        {
            UpdateCurrentDateTime();
            SetUpdateDateTimer();
        }


        public string CurrentTime
        {
            get { return _currentTime; }
            set { SetProperty(ref _currentTime, value); }
        }

        public string CurrentDate
        {
            get { return _currentDate; }
            set { SetProperty(ref _currentDate, value); }
        }

        private void SetUpdateDateTimer()
        {
            var timer = new Timer(1000);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            UpdateCurrentDateTime();
        }

        private void UpdateCurrentDateTime()
        {
            var date = DateTime.Now;
            CurrentTime = date.ToString("H:mm");
            CurrentDate = $"{date.DayOfWeek}, {date.ToString("MMMM")} {date.Day}";
        }
    }
}
