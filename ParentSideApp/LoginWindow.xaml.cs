using System.ComponentModel;
using System.Windows;
namespace ParentSideApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public bool Flag = false;
        private readonly MainWindow _mainwindowView;
        public LoginWindow(MainWindow currentMainWindow)
        {
            _mainwindowView = currentMainWindow;
            InitializeComponent();
        }

        private void LoginBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (usernameTextBox.Text == "123")
            {
                Flag = true;
                this.Close();
                _mainwindowView.Show();

            }
            else
            {
                MessageBox.Show("Wrong username or password");
            }
        }

        private void LoginWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (Flag == false) System.Windows.Application.Current.Shutdown();
        }
    }
}
