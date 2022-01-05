﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace ParentSideApp
{
    /// <summary>
    /// Interaction logic for TimeLineListView.xaml
    /// </summary>
    public partial class TimeLineListView : Window, INotifyPropertyChanged
    {
        public string SchedulePath = MainWindow.SyncronizeFolder.Path + @"\" + "schedule.txt";
        public static BindingList<CTime> BindingCTimeList = new BindingList<CTime>();
        public TimeLineListView()
        {
            InitializeComponent();
            BindingCTimeList.Clear();
        }

        private void TimeLineListView_OnLoaded(object sender, RoutedEventArgs e)
        {
            string[] lines = System.IO.File.ReadAllLines(SchedulePath);
            foreach (var line in lines)
            {
                BindingCTimeList.Add(new CTime(line));
            }

            TimeLineList.ItemsSource = BindingCTimeList;
        }

        private void AddTimeLine_OnClick(object sender, RoutedEventArgs e)
        {

            CTime newTime = new CTime();
            bool flag = true;
            if (InputFromHours.Text == "" || InputFromMinutes.Text == "" || InputToHours.Text == "" ||
                InputToMinutes.Text == "") return;
            if (InputFromHours.Text != "")
            {
                int newValue = Int32.Parse(InputFromHours.Text);
                if (0 <= newValue && newValue <= 24)
                {
                    newTime.From.Hour = newValue;
                    InputFromHours.Text = "";
                }
                else
                {
                    flag = false;
                    MessageBox.Show("Time in From set is not available");
                }
            }
            if (InputFromMinutes.Text != "")
            {
                int newValue = Int32.Parse(InputFromMinutes.Text);
                if (0 <= newValue && newValue <= 60)
                {
                    newTime.From.Minute = newValue;
                    InputFromHours.Text = "";
                }
                else
                {
                    flag = false;
                    MessageBox.Show("Time in From set is not available");
                }
            }
            if (InputToHours.Text != "")
            {
                int newValue = Int32.Parse(InputToHours.Text);
                if (0 <= newValue && newValue <= 24)
                {
                    newTime.To.Hour = newValue;
                    InputToHours.Text = "";
                }
                else
                {
                    flag = false;
                    MessageBox.Show("Time in To set is not available");
                }
            }
            if (InputToMinutes.Text != "")
            {
                int newValue = Int32.Parse(InputToMinutes.Text);
                if (0 <= newValue && newValue <= 60)
                {
                    newTime.To.Minute = newValue;
                    InputToMinutes.Text = "";
                }
                else
                {
                    flag = false;
                    MessageBox.Show("Time in To set is not available");
                }
            }
            if (InputDuration.Text != "")
            {
                int newValue = Int32.Parse(InputDuration.Text);
                if (newValue >= 0)
                {
                    newTime.Duration = Int32.Parse(InputDuration.Text);
                    InputDuration.Text = "";
                }
                else
                {
                    flag = false;
                    MessageBox.Show("Duration is not available");
                }
            }
            if (InputInterrupt.Text != "")
            {
                int newValue = Int32.Parse(InputInterrupt.Text);
                if (newValue >= 0)
                {
                    newTime.Interupt = newValue;
                    InputInterrupt.Text = "";
                }
                else
                {
                    flag = false;
                    MessageBox.Show("Interrupt is not available");
                }
            }
            if (InputSum.Text != "")
            {
                int newValue = Int32.Parse(InputSum.Text);
                if (newValue >= 0)
                {
                    newTime.Sum = newValue;
                    InputSum.Text = "";
                }
                else
                {
                    flag = false;
                    MessageBox.Show("Sum is not available");
                }
            }

            if (CTime.InTime(BindingCTimeList.ToList(), newTime))
            {
                flag = false;
            }

            if (flag)
            {
                int i = 0;
                foreach (var item in BindingCTimeList)
                {
                    if (item.From.IsGreaterOrEqual(newTime.From))
                    {
                        break;
                    }
                    i++;
                }
                BindingCTimeList.Insert(i, newTime);
                CTime.WriteDownTheSchedule(SchedulePath, BindingCTimeList.ToList());
            }
        }

        private void editBtnOnclick(object sender, RoutedEventArgs e)
        {
            int index = TimeLineList.SelectedIndex;
            EditTimeLineView editView = new EditTimeLineView(index);
            if (editView.ShowDialog() == true)
            {
                BindingCTimeList[index] = EditTimeLineView.EditedCTime;
                BindingCTimeList.ResetBindings();
                CTime.WriteDownTheSchedule(SchedulePath, BindingCTimeList.ToList());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
