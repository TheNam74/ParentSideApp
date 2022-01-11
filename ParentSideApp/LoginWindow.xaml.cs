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
            if ((usernameTextBox.Text == "parent1" && PasswordTextBox.Password == "321") || (usernameTextBox.Text == "parent2" && PasswordTextBox.Password == "345"))
            {
                if (usernameTextBox.Text == "parent1")
                {
                    MainWindow.UserAccount = 0;
                }
                else
                {
                    MainWindow.UserAccount = 1;
                }
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
