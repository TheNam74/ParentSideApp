using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace ParentSideApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static int UserAccount = -1;
        public static List<CFolder> HistoryFolder = new List<CFolder>();
        public static List<CTime> TimeLine = new List<CTime>();
        public static CFolder SyncronizeFolder = new CFolder(@"C:\Users\Tran Toan\OneDrive - VNU-HCMUS\he dieu hanh\thuc muc sync");
        public MainWindow()
        {
            string[] folderStrings = Directory.GetDirectories(SyncronizeFolder.Path, "*", SearchOption.AllDirectories);
            foreach (var folder in folderStrings)
            {
                var temp = new CFolder(folder);
                HistoryFolder.Add(temp);
            }
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginWindow loginView = new LoginWindow(this);
            loginView.ShowDialog();
        }

        private void History_OnClick(object sender, RoutedEventArgs e)
        {
            HistoryView historyWindow = new HistoryView();
            historyWindow.ShowDialog();
        }

        private void EditTimeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            TimeLineListView editTimeLineWindow = new TimeLineListView();
            editTimeLineWindow.ShowDialog();
        }
    }
}
