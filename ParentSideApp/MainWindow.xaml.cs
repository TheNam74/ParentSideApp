using System;
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

        //khoi tao duong dan den thu muc OneDrive-Personal co san tren may tinh
        public static string ROOTPATH = Environment.GetEnvironmentVariable("OneDriveConsumer") + @"\" + "syncFolder";
        public static CFolder SyncronizeFolder = new CFolder(ROOTPATH);
        public MainWindow()
        {
            InitializeComponent();
            //  neu chua thay syncFolder se goi ham Init() de tao folder moi
            if (!Directory.Exists(ROOTPATH)) Init();
            string[] folderStrings = Directory.GetDirectories(SyncronizeFolder.Path, "*", SearchOption.AllDirectories);
            foreach (var folder in folderStrings)
            {
                var temp = new CFolder(folder);
                HistoryFolder.Add(temp);
            }
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginWindow loginView = new LoginWindow(this);
            loginView.ShowDialog();
        }

        private void History_OnClick(object sender, RoutedEventArgs e)
        {
            //Mo cua so historyView neu nguoi dung chon nut "see history"
            HistoryView historyWindow = new HistoryView();
            historyWindow.ShowDialog();
        }

        private void EditTimeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            //Mo cua so TimeLineListView neu nguoi dung chon nut "Edit timeline"
            TimeLineListView editTimeLineWindow = new TimeLineListView();
            editTimeLineWindow.ShowDialog();
        }
        void Init()
        {
            //Tạo folder
            System.IO.Directory.CreateDirectory(ROOTPATH);

            //Tạo file thời gian biểu và viết xuống luôn
            List<CTime> initScheduleList = new List<CTime>()
            {
                new CTime(6, 30, 8, 30, 30, 10, 100),
                new CTime(9, 30, 12, 30, 30, 10, 100),
                new CTime(16, 30, 21, 30, 30, 10, 100),

            };
            CTime.WriteDownTheSchedule($"{ROOTPATH}\\schedule.txt", initScheduleList);

            string text = "0 0 parent1";
            File.WriteAllText($"{ROOTPATH}\\peterson.txt", text);
        }

    }
}
