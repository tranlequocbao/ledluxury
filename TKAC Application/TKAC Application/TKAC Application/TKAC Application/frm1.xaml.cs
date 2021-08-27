using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Data.SqlClient;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TKAC_Application
{
    /// <summary>
    /// Interaction logic for frm.xaml
    /// </summary>
    public partial class frm1 : UserControl
    {
        public frm1 (frmFollow._ItemGlobal _Item)
        {
            InitializeComponent();
            AddMenuTheoDoiKeHoach();
        }

        private void AddMenuTheoDoiKeHoach()
        {
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    string sql = string.Format("Select * from THACOKIA.dbo.DeptKIA where Worker=1 order by IDDept asc");
                    SqlCommand cmd = new SqlCommand(sql,mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        MenuItem item = new MenuItem();
                        item.Height = 40;
                        item.Width = 200;
                        StackPanel stackPanel = new StackPanel();
                        stackPanel.Orientation = Orientation.Horizontal;
                        TextBlock text = new TextBlock();
                        text.Text = read["NameDept"].ToString();
                        text.FontSize = 14;
                        text.FontFamily = new FontFamily("Tahoma");
                        text.VerticalAlignment = VerticalAlignment.Center;
                        text.Foreground = Brushes.White;
                        stackPanel.Children.Add(text);
                        item.Header = stackPanel;
                        item.ToolTip = read["IDDept"].ToString();
                        item.Click += Item_Click;
                        MnMain.Items.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "AddMenuTheoDoiKeHoach");
                }
                finally
                {
                    mycon.Close();
                }
            }

        }

        private void Item_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = (MenuItem)sender;
            frmFollow._ItemGlobal _Item = new frmFollow._ItemGlobal();
            _Item.IDDept = item.ToolTip.ToString();
            frmFollow frm = new frmFollow(_Item);
            dpShow.Children.Clear();
            dpShow.Children.Add(frm);
        }
    }
}
