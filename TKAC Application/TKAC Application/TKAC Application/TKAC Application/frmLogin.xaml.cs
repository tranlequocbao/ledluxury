using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TKAC_Application
{
    /// <summary>
    /// Interaction logic for frmLogin.xaml
    /// </summary>
    public partial class frmLogin : UserControl
    {
        public frmLogin()
        {
            InitializeComponent();
            if (System.Configuration.ConfigurationManager.AppSettings["User"].ToString() != "")
            {
                chkRemember.IsChecked = true;
                txtUsername.Text = System.Configuration.ConfigurationManager.AppSettings["User"].ToString();
                txtPassword.Password = System.Configuration.ConfigurationManager.AppSettings["Pass"].ToString();
                txtPassword.Focus();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            CheckLogin();
        }

        private void CheckLogin()
        {
            try
            {
                var context = new DirectoryContext(DirectoryContextType.Domain, System.Configuration.ConfigurationManager.AppSettings["Domain"], txtUsername.Text, txtPassword.Password.ToString());
                System.DirectoryServices.ActiveDirectory.Domain domain = System.DirectoryServices.ActiveDirectory.Domain.GetDomain(context);
                if (domain != null)
                {
                    if (chkRemember.IsChecked == true)
                    {
                        System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                        config.AppSettings.Settings["User"].Value = txtUsername.Text;
                        config.AppSettings.Settings["Pass"].Value = txtPassword.Password.ToString();
                        config.Save(ConfigurationSaveMode.Modified);
                        ConfigurationManager.RefreshSection("appSettings");
                        MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        
                    }
                }
            }
            catch
            {
                MessageBox.Show("Đăng nhập thất bại.Vui lòng kiểm tra lại thông tin!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                txtPassword.Clear();
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && txtPassword.Password.ToString().Length>0)
                CheckLogin();
        }
    }
}
