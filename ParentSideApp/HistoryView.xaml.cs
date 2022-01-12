using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ParentSideApp
{
    /// <summary>
    /// Interaction logic for HistoryView.xaml
    /// </summary>
    public partial class HistoryView : Window
    {
        public CFolder CurrentFolder { get; set; }
        public CTime CurrentTime { get; set; }
        public BindingList<CFile> CurrentFile = new BindingList<CFile>();

        public string TimeLogPath
        {
            get
            {
                string result = CurrentFolder.Path + @"\log.txt";
                return result;
            }
        }

        public HistoryView()
        {
            InitializeComponent();
        }

        private void HistoryView_OnLoaded(object sender, RoutedEventArgs e)
        {
            HistoryFolder.ItemsSource = MainWindow.HistoryFolder;
        }

        private void HistoryFolder_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Neu nguoi dung chon folder moi thi update lai cac log time trong combobox log
            CurrentFolder = ((ComboBox)sender).SelectedItem as CFolder;
            string[] lines = System.IO.File.ReadAllLines(TimeLogPath);

            //clear timelog cu roi sau do update lai timelog moi
            MainWindow.TimeLine.Clear();
            foreach (var line in lines)
            {
                CTime newCTime = new CTime(line);
                MainWindow.TimeLine.Add(newCTime);
            }

            //update lai view cho nguoi xem
            TimeLine.ItemsSource = MainWindow.TimeLine;
        }

        private void TimeLine_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Neu nguoi dung chon mot time log, hien thi cac anh len tren khung nhin.
            CurrentFile.Clear();
            CurrentTime = ((ComboBox)sender).SelectedItem as CTime;
            DirectoryInfo d = new DirectoryInfo(CurrentFolder.Path);

            //Doc tat ca file anh trong folder
            FileInfo[] files = d.GetFiles("*.png");

            foreach (FileInfo file in files)
            {
                //so sanh anh co trong khoan thoi gian minh chon hay khong, neu co thi hien thi anh len cho nguoi xem
                if (CTime.InTime(CurrentTime, file.Name))
                {
                    var newPictureFile = new CFile(file);
                    CurrentFile.Add(newPictureFile);
                }
            }

            PictureView.ItemsSource = CurrentFile;
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            //Double click chuot vao mot anh se thi hien thi anh len
            ListViewItem item = (ListViewItem)sender;

            if (item != null)
            {
                CFile selectedFile = (CFile)item.Content;

                if (selectedFile != null) Process.Start(selectedFile.FullFileNamePath);
            }

        }
    }
}
