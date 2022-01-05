using System;
using System.Linq;
using System.Windows;

namespace ParentSideApp
{
    /// <summary>
    /// Interaction logic for EditTimeLineView.xaml
    /// </summary>
    public partial class EditTimeLineView : Window
    {
        public static CTime EditedCTime;
        public int SelectedIndex;
        public EditTimeLineView(int selectedTime)
        {
            SelectedIndex = selectedTime;
            EditedCTime = TimeLineListView.BindingCTimeList[selectedTime].Clone();
            InitializeComponent();
            CurrentValue.DataContext = EditedCTime;
        }

        private void Confirm_OnClick(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            if (InputFromHours.Text != "")
            {
                int newValue = Int32.Parse(InputFromHours.Text);
                if (0 <= newValue && newValue <= 24)
                {
                    EditedCTime.From.Hour = newValue;
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
                    EditedCTime.From.Minute = newValue;
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
                    EditedCTime.To.Hour = newValue;
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
                    EditedCTime.To.Minute = newValue;
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
                    EditedCTime.Duration = Int32.Parse(InputDuration.Text);
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
                    EditedCTime.Interupt = newValue;
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
                    EditedCTime.Sum = newValue;
                }
                else
                {
                    flag = false;
                    MessageBox.Show("Sum is not available");
                }
            }

            if (!CTime.InTime(TimeLineListView.BindingCTimeList.ToList(), EditedCTime, SelectedIndex))
            {
                this.DialogResult = true;
            }
            if (flag) this.Close();
        }

        private void Cancel_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
