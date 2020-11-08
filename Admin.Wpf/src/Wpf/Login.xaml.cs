using System;
using System.Windows;
using Utility;
using Wpf.ViewModels;
//using System.Windows.Forms;

namespace Wpf
{
    /// <summary>
    /// login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {
        UserLoginViewModel _userLoginViewModel=new UserLoginViewModel()
        {
            Account = "admin",
            Password = "123456",
            Rember = true,
            AutoLogin = true
        };
        #if !NETCOREAPP3_1
        NotifyIcon _notifyIcon;
        #endif
        bool _auto = true;
        public Login()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.CanMinimize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //this.ShowInTaskbar = false;
            this.DataContext = this._userLoginViewModel;
            this.StateChanged += Login_StateChanged;
#if !NETCOREAPP3_1
            _notifyIcon = NotifyIconHelper.NotifyIcon;
#endif
            if(_userLoginViewModel.AutoLogin)
            {
                LoginClick(null, null);
            }
            else
            {
                _auto = false;
            }
        }

        private void Login_StateChanged(object sender, EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                //this.Hide();
            }
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            if (!_userLoginViewModel.ValidateLogin(_auto))
            {
                _auto = false;
                return;
            }

            _auto = false;
            var mainWindow = new MainWindow1();
            this.Close();
            mainWindow.ShowDialog();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
