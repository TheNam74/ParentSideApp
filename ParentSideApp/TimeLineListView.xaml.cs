using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;

namespace ParentSideApp
{
    /// <summary>
    /// Interaction logic for TimeLineListView.xaml
    /// </summary>
    public partial class TimeLineListView : Window, INotifyPropertyChanged
    {
        public string PetersonPath = MainWindow.SyncronizeFolder.Path + @"\" + "peterson.txt";
        public string SchedulePath = MainWindow.SyncronizeFolder.Path + @"\" + "schedule.txt";
        public static BindingList<CTime> BindingCTimeList = new BindingList<CTime>();
        //khoi tao man hinh TimeLineListView
        public TimeLineListView()
        {
            InitializeComponent();
            BindingCTimeList.Clear();
        }

        private void TimeLineListView_OnLoaded(object sender, RoutedEventArgs e)
        {
            ResetBindingList();
        }

        //them mot timeline
        private void AddTimeLine_OnClick(object sender, RoutedEventArgs e)
        {
            //xin quyen truy cap mien gang bang phuong phap peterson
            enter_region();
            CTime newTime = new CTime();
            bool flag = true;
            //kiem tra du lieu nhap vao
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
                    InputFromMinutes.Text = "";
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
            //kiem tra dung do ve thoi gian
            if (CTime.InTime(BindingCTimeList.ToList(), newTime))
            {
                flag = false;
            }

            if (flag)
            {
                int i = 0;
                ResetBindingList();
                //sau khi xac nhan duoc, kiem tra dung do ve thoi gian them mot lan nua
                //tranh truong hop ca hai phu huynh cung edit mot luc
                if (CTime.InTime(BindingCTimeList.ToList(), newTime))
                {
                    MessageBox.Show("There are another timeline in time with new timeline");
                    leave_region();
                    return;
                }
                foreach (var item in BindingCTimeList)
                {
                    if (item.From.IsGreaterOrEqual(newTime.From))
                    {
                        break;
                    }
                    i++;
                }
                BindingCTimeList.Insert(i, newTime);
                //ghi du lieu moi xuong file schedule.txt
                CTime.WriteDownTheSchedule(SchedulePath, BindingCTimeList.ToList());
            }

            //roi khoi mien gang va cap nhat lai file peterson
            leave_region();
        }

        //Tai lai list de phong truong hop nhieu parent thuc hien chinh sua chung luc nhung khong cap nhat kip
        private void ResetBindingList()
        {
            BindingCTimeList.Clear();
            string[] lines = System.IO.File.ReadAllLines(SchedulePath);
            foreach (var line in lines)
            {
                BindingCTimeList.Add(new CTime(line));
            }

            TimeLineList.ItemsSource = BindingCTimeList;
        }

        private void editBtnOnclick(object sender, RoutedEventArgs e)
        {
            //xin vao mien gang
            enter_region();
            int index = TimeLineList.SelectedIndex;
            ResetBindingList();

            //kiem tra truong hop tai khoan 1 edit nhung tai khoan 2 cung luc do dax xoa timeline ma tai khoan 1 muon edit
            if (index >= BindingCTimeList.Count)
            {
                MessageBox.Show("This item don't exist");
                ResetBindingList();
                BindingCTimeList.ResetBindings();
                leave_region();
                return;
            }
            ResetBindingList();
            EditTimeLineView editView = new EditTimeLineView(index);
            if (editView.ShowDialog() == true)
            {
                //neu edit duoc thi cap nhat lai du lieu trong file schedule.txt
                BindingCTimeList[index] = EditTimeLineView.EditedCTime;
                BindingCTimeList.ResetBindings();
                CTime.WriteDownTheSchedule(SchedulePath, BindingCTimeList.ToList());
                leave_region();
            }
        }
        //xin quyen truy cap mien gang bang phuong phap peterson
        private void enter_region()
        {
            int other = 1 - MainWindow.UserAccount;

            //doc file
            string text = File.ReadAllText(PetersonPath);
            string[] interesting = text.Split(' ');

            //interesting[process]=true
            interesting[MainWindow.UserAccount] = "1";

            //turn=other
            string otherTurn = (MainWindow.UserAccount == 0) ? "parent2" : "parent1";
            interesting[2].Remove(0);
            interesting[2] = otherTurn;

            //write down the update
            string writeDown = String.Join(" ", interesting);
            File.WriteAllText(PetersonPath, writeDown);


            //while(turn==other && interested[other]==true)
            while (true)
            {
                Thread.Sleep(100);
                //read the file
                string secondRead = File.ReadAllText(PetersonPath);
                string[] interesting2 = secondRead.Split(' ');
                if (interesting2[2] != otherTurn || interesting2[other] != "1") break;
            }
        }
        //thoat mien gang, cap nhat lai cac thong so theo quy tac peterson
        private void leave_region()
        {
            string text = File.ReadAllText(PetersonPath);
            string[] interesting = text.Split(' ');
            interesting[MainWindow.UserAccount] = "0";

            string writeDown = String.Join(" ", interesting);
            File.WriteAllText(PetersonPath, writeDown);
        }


        private void RefreshTimeLine_OnClick(object sender, RoutedEventArgs e)
        {
            ResetBindingList();
        }

        private void deleteBtnOnclick(object sender, RoutedEventArgs e)
        {
            enter_region();
            int index = TimeLineList.SelectedIndex;
            ResetBindingList();
            BindingCTimeList.RemoveAt(index);
            CTime.WriteDownTheSchedule(SchedulePath, BindingCTimeList.ToList());
            leave_region();
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
