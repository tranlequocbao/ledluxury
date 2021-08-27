using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TKAC_Application
{
    /// <summary>
    /// Interaction logic for frmScanBarcode.xaml
    /// </summary>
    public partial class frmScanBarcode : UserControl
    {
        List<_ItemTodoList> itemsTodolist = new List<_ItemTodoList>();
        public frmScanBarcode()
        {
            InitializeComponent();
            txtVincode.Focus();
            AddDept();
            ShowData();
        }

        private void rbNG_Checked(object sender, RoutedEventArgs e)
        {
            grMota.Height = new GridLength(120);
            grTtr.Height = new GridLength(120);
            btnOK.Content = "Không đạt";
            bdOK.Background = Brushes.Red;
            bdError.Visibility = Visibility.Visible;
            txtError.Focus();
        }

        private void rbNG_Unchecked(object sender, RoutedEventArgs e)
        {
            grMota.Height = new GridLength(0);
            grTtr.Height = new GridLength(35);
            btnOK.Content = "Đạt";
            bdOK.Background = Brushes.Green;
            bdError.Visibility = Visibility.Hidden;
        }

        private void AddDept()
        {
            cbbDept.Items.Add("Xưởng Lắp ráp");
            cbbDept.SelectedIndex = 0;
        }

        public class _Item
        {
            public string vincode { get; set; }
            public string model { get; set; }
            public string engine { get; set; }
            public string tt { get; set; }
            public string timestamp { get; set; }
        }

        private void ShowData()
        {
            List<_Item> items = new List<_Item>();
            items.Add(new _Item() { vincode = "RNYBC41A6KC169833", model = "CERATO 1.6 AT LUXURY", engine = "G4LCK1021682", tt = "OK", timestamp = " 13:15:34 09/03/2021" });
            items.Add(new _Item() { vincode = "RNYBC41A6KC169834", model = "CERATO 1.6 AT LUXURY", engine = "G4LCK1021683", tt = "OK", timestamp = " 13:15:34 09/03/2021" });
            items.Add(new _Item() { vincode = "RNYBC41A6KC169835", model = "CERATO 1.6 AT LUXURY", engine = "G4LCK1021684", tt = "NG", timestamp = " 13:15:34 09/03/2021" });
            items.Add(new _Item() { vincode = "RNYBC41A6KC169836", model = "CERATO 1.6 AT LUXURY", engine = "G4LCK1021685", tt = "OK", timestamp = " 13:15:34 09/03/2021" });
            items.Add(new _Item() { vincode = "RNYBC41A6KC169837", model = "CERATO 1.6 AT LUXURY", engine = "G4LCK1021686", tt = "OK", timestamp = " 13:15:34 09/03/2021" });
            items.Add(new _Item() { vincode = "RNYBC41A6KC169838", model = "CERATO 1.6 AT LUXURY", engine = "G4LCK1021687", tt = "OK", timestamp = " 13:15:34 09/03/2021" });
            items.Add(new _Item() { vincode = "RNYBC41A6KC169839", model = "CERATO 1.6 AT LUXURY", engine = "G4LCK1021688", tt = "NG", timestamp = " 13:15:34 09/03/2021" });
            items.Add(new _Item() { vincode = "RNYBC41A6KC169840", model = "CERATO 1.6 AT LUXURY", engine = "G4LCK1021689", tt = "OK", timestamp = " 13:15:34 09/03/2021" });
            items.Add(new _Item() { vincode = "RNYBC41A6KC169841", model = "CERATO 1.6 AT LUXURY", engine = "G4LCK1021690", tt = "OK", timestamp = " 13:15:34 09/03/2021" });
            lstData.ItemsSource = items;
        }

        private class TodoList
        {
            static public List<_ItemTodoList> GetData()
            {
                List<_ItemTodoList> data = new List<_ItemTodoList>();
                string sql = string.Format("Select * from THACOKIA.dbo.MaterialError");
                using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    try
                    {
                        mycon.Open();
                        SqlCommand cmd = new SqlCommand(sql, mycon);
                        SqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            data.Add(new _ItemTodoList() { IDError = read["IDError"].ToString(),NameErrorVN=read["NameErrorVN"].ToString(),NameErrorEn=read["NameErrorEn"].ToString()});
                        }
                    }
                    finally
                    {
                        mycon.Close();
                    }
                }
                return data;
            }
        }

        ListView lstTodoList = new ListView();

        private void addItem(List<_ItemTodoList> _ItemtodoLists)
        {

            resultStack.Children.Clear();
            Border border = resultStack.Parent as Border;
            lstTodoList = new ListView();
            lstTodoList.FontFamily = new FontFamily("Tahoma");
            lstTodoList.FontSize = 14;
            lstTodoList.Height = 90;
            
            GridView view = new GridView();
            GridViewColumn column = new GridViewColumn();
            column.Header = "Mã vật tư";
            column.Width = 100;
            column.DisplayMemberBinding = new Binding("IDError");
            view.Columns.Add(column);
            column = new GridViewColumn();
            column.Header = "Tên vật tư (VN)";
            column.Width = 100;
            column.DisplayMemberBinding = new Binding("NameErrorVN");
            view.Columns.Add(column);
            column = new GridViewColumn();
            column.Header = "Tên vật tư (En)";
            column.Width = 100;
            column.DisplayMemberBinding = new Binding("NameErrorEn");
            view.Columns.Add(column);
            lstTodoList.View = view;
            lstTodoList.ItemsSource = null;
            lstTodoList.ItemsSource = _ItemtodoLists;
            // Mouse events   
            lstTodoList.KeyUp += (sender, e) =>
              {
                  if (e.Key == Key.Return)
                  {
                      itemsTodolist.Add(new _ItemTodoList() { IDError = ((_ItemTodoList)lstTodoList.SelectedItem).IDError, NameErrorVN = ((_ItemTodoList)lstTodoList.SelectedItem).NameErrorVN, NameErrorEn = ((_ItemTodoList)lstTodoList.SelectedItem).NameErrorEn });
                      icTodoList.ItemsSource = null;
                      icTodoList.ItemsSource = itemsTodolist;
                      border.Visibility = Visibility.Collapsed;
                      txtError.Text = "";
                      txtError.Focus();
                  }
              };
            // Add to the panel   
            resultStack.Children.Add(lstTodoList);
        }

        public class _ItemTodoList
        {
            public string IDError { get; set; }
            public string NameErrorVN { get; set; }
            public string NameErrorEn { get; set; }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            txtVincode.Focus();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtVincode.Focus();
        }

        private void txtVincode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                using(SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    try
                    {

                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }    
                txtEngine.Focus();
            }
        }

        private void txtError_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                lstTodoList.SelectedIndex = 0;
                itemsTodolist.Add(new _ItemTodoList() { IDError = ((_ItemTodoList)lstTodoList.SelectedItem).IDError, NameErrorVN = ((_ItemTodoList)lstTodoList.SelectedItem).NameErrorVN, NameErrorEn = ((_ItemTodoList)lstTodoList.SelectedItem).NameErrorEn });
                icTodoList.ItemsSource = null;
                icTodoList.ItemsSource = itemsTodolist;
                Border border = resultStack.Parent as Border;
                border.Visibility = Visibility.Collapsed;
                txtError.Text = "";
                txtError.Focus();
            }
            else if(e.Key==Key.Down)
            {
                lstTodoList.SelectedIndex = 0;
                lstTodoList.Focus();
            }    
            else
            {
                bool found = false;
                var border = resultStack.Parent as Border;
                var data = TodoList.GetData();

                string query = (sender as TextBox).Text;

                if (query.Length == 0)
                {
                    // Clear   
                    resultStack.Children.Clear();
                    border.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    border.Visibility = System.Windows.Visibility.Visible;
                }

                // Clear the list   
                resultStack.Children.Clear();
                List<_ItemTodoList> item = new List<_ItemTodoList>();
                // Add the result   
                foreach (var obj in data)
                {
                    if (obj.IDError.ToLower().Contains(query.ToLower()) || obj.NameErrorVN.ToUpper().Contains(query.ToUpper()) || obj.NameErrorEn.ToUpper().Contains(query.ToUpper()))
                    {
                        // The word starts with this... Autocomplete must work   
                        item.Add(new _ItemTodoList() { IDError = obj.IDError, NameErrorVN = obj.NameErrorVN, NameErrorEn = obj.NameErrorEn });
                        found = true;
                    }
                    addItem(item);
                }

                if (!found)
                {
                    resultStack.Children.Clear();
                    resultStack.Children.Add(new TextBlock() { Text = "No results found.", FontFamily = new FontFamily("Tahoma"), FontSize = 12 });
                }
                if (e.Key == Key.Return)
                {
                    if (!found)
                    {
                        resultStack.Children.Clear();
                        border.Visibility = System.Windows.Visibility.Collapsed;
                    }
                }
            } 
        }

        private void btnDeleteItem_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(((_ItemTodoList)icTodoList.SelectedItem).IDError);
            itemsTodolist.Remove((_ItemTodoList)icTodoList.SelectedItem);
            icTodoList.ItemsSource = null;
            icTodoList.ItemsSource = itemsTodolist;
        }

        private void ListViewItem_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ListViewItem item = (ListViewItem)sender;
            //item.IsSelected = true;
            icTodoList.SelectedItem = item;
        }

        private void txtQA_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}
