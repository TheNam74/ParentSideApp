using System.ComponentModel;
using System.Windows;

namespace ParentSideApp
{
    /// <summary>
    /// Interaction logic for TimeLineListView.xaml
    /// </summary>
    public partial class TimeLineListView : Window
    {
        public string SchedulePath = MainWindow.SyncronizeFolder.Path + @"\" + "schedule.txt";
        public BindingList<CTime> BindingCTimeList = new BindingList<CTime>();
        public TimeLineListView()
        {
            InitializeComponent();
        }

        private void TimeLineListView_OnLoaded(object sender, RoutedEventArgs e)
        {
            BindingCTimeList.Clear();
            string[] lines = System.IO.File.ReadAllLines(SchedulePath);
            foreach (var line in lines)
            {
                BindingCTimeList.Add(new CTime(line));
            }

            TimeLineList.ItemsSource = BindingCTimeList;
        }

        private void AddTimeLine_OnClick(object sender, RoutedEventArgs e)
        {
        }
    }
}
