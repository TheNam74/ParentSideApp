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
            CurrentFolder = ((ComboBox)sender).SelectedItem as CFolder;
            string[] lines = System.IO.File.ReadAllLines(TimeLogPath);
            foreach (var line in lines)
            {
                CTime newCTime = new CTime(line);
                MainWindow.TimeLine.Add(newCTime);
            }
            TimeLine.ItemsSource = MainWindow.TimeLine;
        }

        private void TimeLine_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentFile.Clear();
            CurrentTime = ((ComboBox)sender).SelectedItem as CTime;
            DirectoryInfo d = new DirectoryInfo(CurrentFolder.Path);

            FileInfo[] files = d.GetFiles("*.png");

            foreach (FileInfo file in files)
            {
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
            ListViewItem item = (ListViewItem)sender;

            if (item != null)
            {
                CFile selectedFile = (CFile)item.Content;

                if (selectedFile != null) Process.Start(selectedFile.FullFileNamePath);
            }

        }
    }
}
