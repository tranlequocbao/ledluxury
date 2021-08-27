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
using LiveCharts;
using LiveCharts.Wpf;
using ClosedXML.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;

namespace TKAC_Application
{
    /// <summary>
    /// Interaction logic for frmFollow.xaml
    /// </summary>
    public partial class frmFollow : UserControl
    {
        string iddept = "";bool LoadForm = false;
        public frmFollow(_ItemGlobal item)
        {
            InitializeComponent();
            iddept = item.IDDept;
            lblDept.Text = NameDept(item.IDDept);
            loadSetting();
            ShowCombobox();
            AddCombobox(); LoadRadioButton();
            //ShowChartSanLuongNgay();
            //ShowChartSanLuongThang();
            //ShowChiTiet();
            //ShowTongHop();
            //ShowChartTongHop();
            LBLNgayDen.Text = LBLNgayNhap.Text = DateTime.Now.ToString("dd/MM/yyyy");
            
            LoadForm = true;

            showchart();
            if(iddept == "D00004")
            {
                chartLapRap.Visibility = Visibility.Visible;
                pieTongHop.Visibility = Visibility.Hidden;
            }
            else
            {
                chartLapRap.Visibility = Visibility.Hidden;
                pieTongHop.Visibility = Visibility.Visible;
            }
        }

        public class _ItemGlobal
        {
            public string IDDept { get; set; }
            public string IDStation { get; set; }
        }

        private string NameDept(string iddept)
        {
            string name = "";
            using(SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    string sql = string.Format("Select NameDept from THACOKIA.dbo.DeptKIA where IDDept ='{0}'", iddept);
                    SqlCommand cmd = new SqlCommand(sql, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    read.Read();
                    name = read["NameDept"].ToString();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "NameDept");
                }
                finally
                {
                    mycon.Close();
                }
            }    
            return name;
        }

        private void LoadRadioButton()
        {
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    string sql = string.Format("SELECT COUNT(Ca) as ct FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}'", iddept);
                    SqlCommand cmd = new SqlCommand(sql, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    read.Read();
                    if(int.Parse(read["ct"].ToString())==1)
                    {
                        rb1Ca.IsChecked = true;
                    }    
                    else
                    {
                        rb2Ca.IsChecked = true;
                    }    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "NameDept");
                }
                finally
                {
                    mycon.Close();
                }
            }
        }

        private string KeyDept(string iddept)
        {
            string name = "";
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    string sql = string.Format("Select NameDept from THACOKIA.dbo.DeptKIA where IDDept ='{0}'", iddept);
                    SqlCommand cmd = new SqlCommand(sql, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    read.Read();
                    name = read["NameDept"].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "NameDept");
                }
                finally
                {
                    mycon.Close();
                }
            }
            return name;
        }

        private void ShowChartSanLuongNgay()
        {
            ChtSanLuongNgay.Series.Clear();
            ChtSanLuongNgay.Series.Add(new PieSeries { Title = "Sản xuất ngày", Fill = Brushes.Green, DataLabels = true, FontSize = 15, FontFamily = new FontFamily("Tahoma"), StrokeThickness = 0, Values = new ChartValues<int> { ActualNgay() } });
            ChtSanLuongNgay.Series.Add(new PieSeries { Title = "Còn lại", Fill = Brushes.Red, DataLabels = true, FontSize = 15, FontFamily = new FontFamily("Tahoma"), StrokeThickness = 0, Values = new ChartValues<int> { PlanNgay() - ActualNgay() } });
            ChtSanLuongNgay.LegendLocation = LegendLocation.Bottom;
        }

        private int ActualNgay()
        {
            int value = 0; 
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    string TimeIn = " 00:00:00";
                    string TimeOut = " 00:00:00";
                    string sql = string.Empty;
                    if (rb1Ca.IsChecked == true)
                    {
                        sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}'", iddept);
                        SqlCommand cmdtime = new SqlCommand(sql, mycon);
                        SqlDataReader readtime = cmdtime.ExecuteReader();
                        while (readtime.Read())
                        {
                            TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                            TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                        }
                    }
                    else
                    {
                        if (chkCa1.IsChecked == true)
                        {
                            sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}' and Ca='Ca1'", iddept);
                            SqlCommand cmdtime = new SqlCommand(sql, mycon);
                            SqlDataReader readtime = cmdtime.ExecuteReader();
                            while (readtime.Read())
                            {
                                TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                                TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                            }
                        }
                        else
                        {
                            sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}' and Ca='Ca2'", iddept);
                            SqlCommand cmdtime = new SqlCommand(sql, mycon);
                            SqlDataReader readtime = cmdtime.ExecuteReader();
                            while (readtime.Read())
                            {
                                TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                                TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                            }
                        }
                    }

                    
                        sql = string.Format("Select COUNT(THACOKIA.dbo.ScanCount.VinCode) as ct from THACOKIA.dbo.ScanCount where THACOKIA.dbo.ScanCount.Station='{2}' and THACOKIA.dbo.ScanCount.TimeStamp>'{0}' and THACOKIA.dbo.ScanCount.TimeStamp<'{1}'", TimeIn, TimeOut,idstation);
                        SqlCommand cmd = new SqlCommand(sql, mycon);
                        SqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            value = Int32.Parse(read["ct"].ToString());
                            lblActualDay.Text = read["ct"].ToString();
                        }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ActualNgay");
                }
                finally
                {
                    mycon.Close();
                }
            }
            return value;
        }

        private int PlanNgay()
        {
                int value = 0;
                using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    try
                    {
                        mycon.Open();
                        string sql = string.Format("SELECT *  FROM THACOKIA.dbo.[PLAN] where THACOKIA.dbo.[PLAN].SHIFT='Ca1' and Station='{0}'",idstation);
                        SqlCommand cmd = new SqlCommand(sql, mycon);
                        SqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            value = Int32.Parse(read["DayPlan"].ToString());
                            lblPlanDay.Text = read["DayPlan"].ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message,"PlanNgay");
                    }
                    finally
                    {
                        mycon.Close();
                    }
                }
            return value;
        }

        private void ShowChartSanLuongThang()
        {
            ChtSanLuongThang.Series.Clear();
            ChtSanLuongThang.Series.Add(new PieSeries { Title = "Sản xuất tháng", Fill = Brushes.Green, DataLabels = true, FontSize = 15, FontFamily = new FontFamily("Tahoma"), StrokeThickness = 0, Values = new ChartValues<int> { ActualThang() } });
            ChtSanLuongThang.Series.Add(new PieSeries { Title = "Còn lại", Fill = Brushes.Red, DataLabels = true, FontSize = 15, FontFamily = new FontFamily("Tahoma"), StrokeThickness = 0, Values = new ChartValues<int> { PlanThang() - ActualThang() } });
            ChtSanLuongThang.LegendLocation = LegendLocation.Bottom;
        }

        private int ActualThang()
        {
            int value = 0;
            
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();

                   
                        string sql = string.Format("Select Count(VinCode) as ct from THACOKIA.dbo.ScanCount where Station = '{2}' and MONTH(TimeStamp)= '{0}' and YEAR(TimeStamp)= '{1}'", DateTime.Now.Month.ToString(), DateTime.Now.Year.ToString(),idstation);
                        SqlCommand cmd = new SqlCommand(sql, mycon);
                        SqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            value = Int32.Parse(read["ct"].ToString());
                            lblActualMonth.Text = read["ct"].ToString();
                        }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    mycon.Close();
                }
            }
            return value;
        }

        private int PlanThang()
        {
            int value = 0;
            
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    
                        string sql = string.Format("SELECT *  FROM THACOKIA.dbo.[PLAN] where THACOKIA.dbo.[PLAN].SHIFT='Ca1' and Station='{0}'",idstation);
                        SqlCommand cmd = new SqlCommand(sql, mycon);
                        SqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            value = Int32.Parse(read["MonthPlan"].ToString());
                            lblPlanMonth.Text = read["MonthPlan"].ToString();
                        }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    mycon.Close();
                }
            }
            return value;
        }

        public class _ItemChiTiet
        {
            public string tt { get; set; }
            public string vincode { get; set; }
            public string model { get; set; }
            public string timestamp { get; set; }
        }

        private void ShowChiTiet()
        {
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    string TimeIn = " 00:00:00";
                    string TimeOut = " 00:00:00";
                    string sql = string.Empty;
                    if (rb1Ca.IsChecked==true)
                    {
                        sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}'",iddept);
                        SqlCommand cmdtime = new SqlCommand(sql, mycon);
                        SqlDataReader readtime = cmdtime.ExecuteReader();
                        while (readtime.Read())
                        {
                            TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                            TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                        }
                    }    
                    else
                    {
                        if (chkCa1.IsChecked == true)
                        {
                            sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}' and Ca='Ca1'", iddept);
                            SqlCommand cmdtime = new SqlCommand(sql, mycon);
                            SqlDataReader readtime = cmdtime.ExecuteReader();
                            while (readtime.Read())
                            {
                                TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                                TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                            }
                            
                        }
                        else
                        {
                            sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}' and Ca='Ca2'", iddept);
                            SqlCommand cmdtime = new SqlCommand(sql, mycon);
                            SqlDataReader readtime = cmdtime.ExecuteReader();
                            while (readtime.Read())
                            {
                                TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                                TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                            }
                            
                        }    
                    }

                   
                        sql = string.Format("  Select  THACOKIA.dbo.ScanCount.VinCode,THACOKIA.dbo.QTSX.Model,THACOKIA.dbo.ScanCount.TimeStamp from THACOKIA.dbo.QTSX,THACOKIA.dbo.ScanCount where THACOKIA.dbo.QTSX.Vin_Code = THACOKIA.dbo.ScanCount.VinCode and THACOKIA.dbo.ScanCount.Station='{2}' and THACOKIA.dbo.ScanCount.TimeStamp>='{0}' and THACOKIA.dbo.ScanCount.TimeStamp<='{1}' order by THACOKIA.dbo.ScanCount.TimeStamp asc", TimeIn, TimeOut,idstation);
                        SqlCommand cmd = new SqlCommand(sql, mycon);
                        List<_ItemChiTiet> _Items = new List<_ItemChiTiet>();
                        SqlDataReader read = cmd.ExecuteReader();

                        int id = 0;
                        while (read.Read())
                        {
                            id++;
                            _Items.Add(new _ItemChiTiet() { tt = id.ToString(), vincode = read["VinCode"].ToString(), model = read["Model"].ToString(), timestamp = Convert.ToDateTime(read["TimeStamp"].ToString()).ToString("HH:mm:ss dd/MM/yyyy") });
                        }
                        lstThuHienNgay.ItemsSource = _Items;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    mycon.Close();
                }
            }
        }

        public class _ItemTongHop
        {
            public string tt { get; set; }
            public string model { get; set; }
            public string count { get; set; }
        }

        private void ShowTongHop()
        {
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    string TimeIn = " 00:00:00";
                    string TimeOut = " 00:00:00";
                    string sql = string.Empty;
                    if (rb1Ca.IsChecked == true)
                    {
                        sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}'", iddept);
                        SqlCommand cmdtime = new SqlCommand(sql, mycon);
                        SqlDataReader readtime = cmdtime.ExecuteReader();
                        while (readtime.Read())
                        {
                            TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                            TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                        }
                        
                    }
                    else
                    {
                        if (chkCa1.IsChecked == true)
                        {
                            sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}' and Ca='Ca1'", iddept);
                            SqlCommand cmdtime = new SqlCommand(sql, mycon);
                            SqlDataReader readtime = cmdtime.ExecuteReader();
                            while (readtime.Read())
                            {
                                TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                                TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                            }
                            
                        }
                        else
                        {
                            sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}' and Ca='Ca2'", iddept);
                            SqlCommand cmdtime = new SqlCommand(sql, mycon);
                            SqlDataReader readtime = cmdtime.ExecuteReader();
                            while (readtime.Read())
                            {
                                TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                                TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                            }
                            
                        }
                    }

                   
                        sql = string.Format("Select  DISTINCT THACOKIA.dbo.QTSX.Model, Count(THACOKIA.dbo.ScanCount.VinCode) as ct from THACOKIA.dbo.QTSX,THACOKIA.dbo.ScanCount where THACOKIA.dbo.QTSX.Vin_Code = THACOKIA.dbo.ScanCount.VinCode and THACOKIA.dbo.ScanCount.Station='{2}' and THACOKIA.dbo.ScanCount.TimeStamp>'{0}' and THACOKIA.dbo.ScanCount.TimeStamp<'{1}' group by THACOKIA.dbo.QTSX.Model order by THACOKIA.dbo.QTSX.Model asc", TimeIn, TimeOut,idstation);
                        SqlCommand cmd = new SqlCommand(sql, mycon);
                        List<_ItemTongHop> _Items = new List<_ItemTongHop>();
                        SqlDataReader read = cmd.ExecuteReader();
                        int id = 0;
                        while (read.Read())
                        {
                            id++;
                            _Items.Add(new _ItemTongHop() { tt = id.ToString(), model = read["Model"].ToString(), count = read["ct"].ToString() });
                        }
                        lstTongHop.ItemsSource = _Items;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ShowTongHop");
                }
                finally
                {
                    mycon.Close();
                }
            }
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public static string idstation;
        public Func<double, string> Formatter { get; set; }
        private void ShowCombobox()
        {
            using (SqlConnection mycon=new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    string sql = string.Format("select * from[THACOKIA].[dbo].[DeptKIA], [THACOKIA].[dbo].[StationonDept] where[THACOKIA].[dbo].[DeptKIA].IDDept = [THACOKIA].[dbo].[StationonDept].IDDept and[THACOKIA].[dbo].[StationonDept].IDDept = '{0}'", iddept);
                    SqlCommand cmd = new SqlCommand(sql,mycon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        ComboBoxItem item1 = new ComboBoxItem();
                        item1.Content = reader["KeyStation"].ToString();
                        item1.ToolTip = reader["NameStation"].ToString();
                        cbbStation.Items.Add(item1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ShowCombobox");
                }
                finally
                {
                    mycon.Close();
                    cbbStation.SelectedIndex = 0;
                }
            }
        }
        
        private void ShowChartTongHop()
        {
            SeriesCollection = new SeriesCollection();
                using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    string TimeIn = " 00:00:00";
                    string TimeOut = " 00:00:00";
                    string sql = string.Empty;
                    if (rb1Ca.IsChecked == true)
                    {
                        sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}'", iddept);
                        SqlCommand cmdtime = new SqlCommand(sql, mycon);
                        SqlDataReader readtime = cmdtime.ExecuteReader();
                        while (readtime.Read())
                        {
                            TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                            TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                        }
                        
                    }
                    else
                    {
                        if (chkCa1.IsChecked == true)
                        {
                            sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}' and Ca='Ca1'", iddept);
                            SqlCommand cmdtime = new SqlCommand(sql, mycon);
                            SqlDataReader readtime = cmdtime.ExecuteReader();
                            while (readtime.Read())
                            {
                                TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                                TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                            }
                            
                        }
                        else
                        {
                            sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}' and Ca='Ca2'", iddept);
                            SqlCommand cmdtime = new SqlCommand(sql, mycon);
                            SqlDataReader readtime = cmdtime.ExecuteReader();
                            while (readtime.Read())
                            {
                                TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                                TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                            }
                            
                        }
                    }

                        if (SeriesCollection.Count() > 0)
                        {
                            pieTongHop.Series = SeriesCollection;
                        }
                        else
                        {
                            sql = string.Format("Select  DISTINCT THACOKIA.dbo.QTSX.Model, Count(THACOKIA.dbo.ScanCount.VinCode) as ct from THACOKIA.dbo.QTSX,THACOKIA.dbo.ScanCount where THACOKIA.dbo.QTSX.Vin_Code = THACOKIA.dbo.ScanCount.VinCode and THACOKIA.dbo.ScanCount.Station='{2}' and THACOKIA.dbo.ScanCount.TimeStamp>'{0}' and THACOKIA.dbo.ScanCount.TimeStamp<'{1}' group by THACOKIA.dbo.QTSX.Model order by THACOKIA.dbo.QTSX.Model asc", TimeIn, TimeOut, idstation);
                            SqlCommand cmd = new SqlCommand(sql, mycon);
                            SqlDataReader read = cmd.ExecuteReader();
                            while (read.Read())
                            {
                                SeriesCollection.Add(new StackedRowSeries
                                {
                                    Values = new ChartValues<double> { int.Parse(read["ct"].ToString()) },
                                    StackMode = StackMode.Percentage,
                                    DataLabels = true,
                                    LabelPoint = p => p.X.ToString(),
                                    Title = read["Model"].ToString()
                                }); ;
                                pieTongHop.Series = SeriesCollection;
                            }
                        }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    mycon.Close();
                }
            }
            Formatter = val => val.ToString("Xe");
            Labels = new[] { ActualNgay().ToString() };
            DataContext = this;
        }
        private void lblPlanMonth_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
                
                try
                {
                    int sl = int.Parse(lblPlanMonth.Text);
                    using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
                    {
                        try
                        {
                            
                                string sql = string.Format("Update THACOKIA.dbo.[PLAN] set MonthPlan='{0}' where THACOKIA.dbo.[PLAN].SHIFT='Ca1' and Station='{1}'", sl, idstation);
                                mycon.Open();
                                SqlCommand cmd = new SqlCommand(sql, mycon);
                                cmd.ExecuteNonQuery();
                                ShowChartSanLuongThang();
                                lblPlanMonth.Text = sl.ToString();
                            

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            mycon.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "lblPlanMonth_KeyUp");
                }
                showchart();
            }
            
        }

        private void lblPlanDay_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    int sl = int.Parse(lblPlanDay.Text);
                    using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
                    {
                        try
                        {
                                string sql1 = string.Format("Update THACOKIA.dbo.[PLAN] set DayPlan='{0}' where THACOKIA.dbo.[PLAN].SHIFT='Ca1' and Station='{1}'", sl, idstation);
                                mycon.Open();
                                SqlCommand cmd1 = new SqlCommand(sql1, mycon);
                                cmd1.ExecuteNonQuery();
                                ShowChartSanLuongNgay();
                                lblPlanDay.Text = sl.ToString();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            mycon.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "lblPlanDay_KeyUp");
                }
                showchart();
            }
            
        }

        private void AddCombobox()
        {
            cbbInCa1.Items.Clear();
            cbbInCa2.Items.Clear();
            cbbOutCa1.Items.Clear();
            cbbOutCa2.Items.Clear();
            string sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[LibraryTime]");
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(sql, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while(read.Read())
                    {
                        if(read["Shift"].ToString()=="Ca1")
                        {
                            if(bool.Parse(read["InOut"].ToString())==true)
                            {
                                cbbInCa1.Items.Add(read["TimeStamp"].ToString());
                            }   
                            else
                            {
                                cbbOutCa1.Items.Add(read["TimeStamp"].ToString());
                            }    
                        }
                        else
                        {
                            if (bool.Parse(read["InOut"].ToString()) == true)
                            {
                                cbbInCa2.Items.Add(read["TimeStamp"].ToString());
                            }
                            else
                            {
                                cbbOutCa2.Items.Add(read["TimeStamp"].ToString());
                            }
                        }    
                    }    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    mycon.Close();
                    cbbInCa1.SelectedIndex = 0;
                    cbbOutCa1.SelectedIndex = 0;
                    cbbInCa2.SelectedIndex = 0;
                    cbbOutCa2.SelectedIndex = 0;
                }
            }
        }

        private void loadSetting()
        {
            string sql = string.Format("SELECT Count(Ca) as ct  FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}'",iddept);
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(sql, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    read.Read();
                    if(int.Parse(read["ct"].ToString())==1)
                    {
                        rb1Ca.IsChecked = true;
                    }
                    else
                    {
                        rb2Ca.IsChecked = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    mycon.Close();
                }
            }
        }

        private void rb1Ca_Checked(object sender, RoutedEventArgs e)
        {
            gr2Ca.IsEnabled = false;
            stp1Ca.IsEnabled = true;
            string sql = string.Format("SELECT *  FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}'", iddept);
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(sql, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while(read.Read())
                    {
                        if (read["TimeOut"].ToString() == " 06:30:00 PM")
                            rb1830.IsChecked = true;
                        else if (read["TimeOut"].ToString() == " 08:45:00 PM")
                            rb2045.IsChecked = true;
                        else if (read["TimeOut"].ToString() == " 10:15:00 PM")
                            rb2215.IsChecked = true;
                        else if (read["TimeOut"].ToString() == " 00:00:00 AM")
                            rb2400.IsChecked = true;
                        else
                            rboff.IsChecked = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    mycon.Close();
                }
            }
        }

        private void rb2Ca_Checked(object sender, RoutedEventArgs e)
        {
            gr2Ca.IsEnabled = true;
            stp1Ca.IsEnabled = false;
            string sql = string.Format("SELECT *  FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}'", iddept);
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(sql, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        if(read["Ca"].ToString()=="Ca1")
                        {
                            cbbInCa1.SelectedItem = read["TimeIn"].ToString();
                            cbbOutCa1.SelectedItem = read["TimeOut"].ToString();
                        }    
                        else
                        {
                            cbbInCa2.SelectedItem = read["TimeIn"].ToString();
                            cbbOutCa2.SelectedItem = read["TimeOut"].ToString();
                        }    
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    mycon.Close();
                }
            }
        }

        private void rboff_Checked(object sender, RoutedEventArgs e)
        {
            if(LoadForm==true)
            {
                string sql = string.Format("Delete FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}'", iddept);
                using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    try
                    {
                        mycon.Open();
                        SqlCommand cmd = new SqlCommand(sql, mycon);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("Insert into THACOKIA.dbo.TimeSetting (IDDept,Ca,TimeIn,TimeOut,Multi) Values ('{0}','{1}','{2}','{3}',0)", iddept, "Ca", " 07:00:00 AM"," 04:45:00 PM");
                        cmd = new SqlCommand(sql, mycon);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        mycon.Close();
                    }
                }
            }    
        }

        private void rb1830_Checked(object sender, RoutedEventArgs e)
        {
            if (LoadForm == true)
            {
                string sql = string.Format("Delete FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}'", iddept);
                using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    try
                    {
                        mycon.Open();
                        SqlCommand cmd = new SqlCommand(sql, mycon);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("Insert into THACOKIA.dbo.TimeSetting (IDDept,Ca,TimeIn,TimeOut,Multi) Values ('{0}','{1}','{2}','{3}',0)", iddept, "Ca", " 07:00:00 AM", " 06:30:00 PM");
                        cmd = new SqlCommand(sql, mycon);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        mycon.Close();
                    }
                }
            }
        }

        private void rb2045_Checked(object sender, RoutedEventArgs e)
        {
            if (LoadForm == true)
            {
                string sql = string.Format("Delete FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}'", iddept);
                using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    try
                    {
                        mycon.Open();
                        SqlCommand cmd = new SqlCommand(sql, mycon);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("Insert into THACOKIA.dbo.TimeSetting (IDDept,Ca,TimeIn,TimeOut,Multi) Values ('{0}','{1}','{2}','{3}',0)", iddept, "Ca", " 07:00:00 AM", " 08:45:00 PM");
                        cmd = new SqlCommand(sql, mycon);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        mycon.Close();
                    }
                }
            }
        }

        private void rb2215_Checked(object sender, RoutedEventArgs e)
        {
            if (LoadForm == true)
            {
                string sql = string.Format("Delete FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}'", iddept);
                using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    try
                    {
                        mycon.Open();
                        SqlCommand cmd = new SqlCommand(sql, mycon);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("Insert into THACOKIA.dbo.TimeSetting (IDDept,Ca,TimeIn,TimeOut,Multi) Values ('{0}','{1}','{2}','{3}',0)", iddept, "Ca", " 07:00:00 AM", " 10:15:00 PM");
                        cmd = new SqlCommand(sql, mycon);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        mycon.Close();
                    }
                }
            }
        }

        private void rb2400_Checked(object sender, RoutedEventArgs e)
        {
            if (LoadForm == true)
            {
                string sql = string.Format("Delete FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}'", iddept);
                using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    try
                    {
                        mycon.Open();
                        SqlCommand cmd = new SqlCommand(sql, mycon);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("Insert into THACOKIA.dbo.TimeSetting (IDDept,Ca,TimeIn,TimeOut,Multi) Values ('{0}','{1}','{2}','{3}',0)", iddept, "Ca", " 07:00:00 AM", " 11:59:59 PM");
                        cmd = new SqlCommand(sql, mycon);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        mycon.Close();
                    }
                }
            }
        }

        private void cbbInCa1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LoadForm == true)
            {
                string sql = string.Format("Delete FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}' and Multi = 0", iddept);
                using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    try
                    {
                        mycon.Open();
                        SqlCommand cmd = new SqlCommand(sql, mycon);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("Select Count(Ca) as ct from [THACOKIA].[dbo].[TimeSetting] where IDDept = '{0}' and Ca='Ca1'", iddept);
                        cmd = new SqlCommand(sql, mycon);
                        SqlDataReader read = cmd.ExecuteReader();
                        read.Read();
                        if (int.Parse(read["ct"].ToString()) == 0)
                        {
                            sql = string.Format("Insert into THACOKIA.dbo.TimeSetting (IDDept,Ca,TimeIn,TimeOut,Multi) Values ('{0}','{1}','{2}','{3}',1)", iddept, "Ca1", cbbInCa1.SelectedValue.ToString(),cbbOutCa1.SelectedItem.ToString());
                            cmd = new SqlCommand(sql, mycon);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            sql = string.Format("Update [THACOKIA].[dbo].[TimeSetting] set TimeIn = '{1}' where Ca='Ca1' and IDDept = '{0}'", iddept, cbbInCa1.SelectedValue.ToString());
                            cmd = new SqlCommand(sql, mycon);
                            cmd.ExecuteNonQuery();
                        }    
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        mycon.Close();
                    }
                }
            }
        }

        private void cbbOutCa1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LoadForm == true)
            {
                string sql = string.Format("Delete FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}' and Multi = 0", iddept);
                using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    try
                    {
                        mycon.Open();
                        SqlCommand cmd = new SqlCommand(sql, mycon);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("Select Count(Ca) as ct from [THACOKIA].[dbo].[TimeSetting] where IDDept = '{0}' and Ca='Ca1'", iddept);
                        cmd = new SqlCommand(sql, mycon);
                        SqlDataReader read = cmd.ExecuteReader();
                        read.Read();
                        if (int.Parse(read["ct"].ToString()) == 0)
                        {
                            sql = string.Format("Insert into THACOKIA.dbo.TimeSetting (IDDept,Ca,TimeIn,TimeOut,Multi) Values ('{0}','{1}','{2}','{3}',1)", iddept, "Ca1", cbbInCa1.SelectedValue.ToString(), cbbOutCa1.SelectedItem.ToString());
                            cmd = new SqlCommand(sql, mycon);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            sql = string.Format("Update [THACOKIA].[dbo].[TimeSetting] set TimeOut = '{1}' where Ca='Ca1' and IDDept = '{0}'", iddept, cbbOutCa1.SelectedValue.ToString());
                            cmd = new SqlCommand(sql, mycon);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        mycon.Close();
                    }
                }
            }
        }

        private void cbbInCa2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LoadForm == true)
            {
                string sql = string.Format("Delete FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}' and Multi = 0", iddept);
                using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    try
                    {
                        mycon.Open();
                        SqlCommand cmd = new SqlCommand(sql, mycon);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("Select Count(Ca) as ct from [THACOKIA].[dbo].[TimeSetting] where IDDept = '{0}' and Ca='Ca2'", iddept);
                        cmd = new SqlCommand(sql, mycon);
                        SqlDataReader read = cmd.ExecuteReader();
                        read.Read();
                        if (int.Parse(read["ct"].ToString()) == 0)
                        {
                            sql = string.Format("Insert into THACOKIA.dbo.TimeSetting (IDDept,Ca,TimeIn,TimeOut,Multi) Values ('{0}','{1}','{2}','{3}',1)", iddept, "Ca2", cbbInCa1.SelectedValue.ToString(), cbbOutCa1.SelectedItem.ToString());
                            cmd = new SqlCommand(sql, mycon);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            sql = string.Format("Update [THACOKIA].[dbo].[TimeSetting] set TimeIn = '{1}' where Ca='Ca2' and IDDept = '{0}'", iddept, cbbInCa2.SelectedValue.ToString());
                            cmd = new SqlCommand(sql, mycon);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        mycon.Close();
                    }
                }
            }
        }

        private void cbbOutCa2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LoadForm == true)
            {
                string sql = string.Format("Delete FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}' and Multi = 0", iddept);
                using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    try
                    {
                        mycon.Open();
                        SqlCommand cmd = new SqlCommand(sql, mycon);
                        cmd.ExecuteNonQuery();
                        sql = string.Format("Select Count(Ca) as ct from [THACOKIA].[dbo].[TimeSetting] where IDDept = '{0}' and Ca='Ca2'", iddept);
                        cmd = new SqlCommand(sql, mycon);
                        SqlDataReader read = cmd.ExecuteReader();
                        read.Read();
                        if (int.Parse(read["ct"].ToString()) == 0)
                        {
                            sql = string.Format("Insert into THACOKIA.dbo.TimeSetting (IDDept,Ca,TimeIn,TimeOut,Multi) Values ('{0}','{1}','{2}','{3}',1)", iddept, "Ca2", cbbInCa1.SelectedValue.ToString(), cbbOutCa1.SelectedItem.ToString());
                            cmd = new SqlCommand(sql, mycon);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            sql = string.Format("Update [THACOKIA].[dbo].[TimeSetting] set TimeOut = '{1}' where Ca='Ca2' and IDDept = '{0}'", iddept, cbbOutCa2.SelectedValue.ToString());
                            cmd = new SqlCommand(sql, mycon);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        mycon.Close();
                    }
                }
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog();
            saveFile.Title = "Browse Text Files";
            saveFile.DefaultExt = "xlsx";
            saveFile.Filter = "Excel files (.xlsx)|*.xlsx|All files (.*)|*.*";
            saveFile.ShowDialog();
            if (saveFile.FileName != "")
            {
                var wb = new XLWorkbook();
                var sheet = wb.Worksheets.Add(DateTime.Now.ToString("ddMMyyyy"));
                sheet.Column(1).Width = 5.86;
                sheet.Column(2).Width = 35.14;
                sheet.Column(3).Width = 26;
                sheet.Column(4).Width = 30;
                sheet.Range(sheet.Cell(1, 1), sheet.Cell(1, 4)).Merge();
                sheet.Row(1).Height = 28.5;
                sheet.Cell(1, 1).Value = "BÁO CÁO NHẬN XE NGÀY " + DateTime.Now.ToString("dd") + " THÁNG " + DateTime.Now.ToString("MM") + " NĂM " + DateTime.Now.ToString("yyyy");
                sheet.Cell(1, 1).Style.Font.Bold = true;
                sheet.Cell(1, 1).Style.Font.FontName = "Times New Roman";
                sheet.Cell(1, 1).Style.Font.FontSize = 14;
                sheet.Cell(1, 1).Style.Alignment.WrapText = true;
                sheet.Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                sheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Row(2).Height = 8.5;
                sheet.Row(3).Height = 30;
                sheet.Cell(3, 1).Value = "TT";
                sheet.Cell(3, 1).Style.Font.Bold = true;
                sheet.Cell(3, 1).Style.Font.FontName = "Times New Roman";
                sheet.Cell(3, 1).Style.Font.FontSize = 12;
                sheet.Cell(3, 1).Style.Alignment.WrapText = true;
                sheet.Cell(3, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                sheet.Cell(3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell(3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                sheet.Cell(3, 2).Value = "Loại xe";
                sheet.Cell(3, 2).Style.Font.Bold = true;
                sheet.Cell(3, 2).Style.Font.FontName = "Times New Roman";
                sheet.Cell(3, 2).Style.Font.FontSize = 12;
                sheet.Cell(3, 2).Style.Alignment.WrapText = true;
                sheet.Cell(3, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                sheet.Cell(3, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell(3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                sheet.Cell(3, 3).Value = "Số khung";
                sheet.Cell(3, 3).Style.Font.Bold = true;
                sheet.Cell(3, 3).Style.Font.FontName = "Times New Roman";
                sheet.Cell(3, 3).Style.Font.FontSize = 12;
                sheet.Cell(3, 3).Style.Alignment.WrapText = true;
                sheet.Cell(3, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                sheet.Cell(3, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell(3, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                sheet.Cell(3, 4).Value = "Thời gian nhận xe";
                sheet.Cell(3, 4).Style.Font.Bold = true;
                sheet.Cell(3, 4).Style.Font.FontName = "Times New Roman";
                sheet.Cell(3, 4).Style.Font.FontSize = 12;
                sheet.Cell(3, 4).Style.Alignment.WrapText = true;
                sheet.Cell(3, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                sheet.Cell(3, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet.Cell(3, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    try
                    {
                        mycon.Open();
                        string TimeIn = " 00:00:00";
                        string TimeOut = " 00:00:00";
                        string sql = string.Empty;
                        if (rb1Ca.IsChecked == true)
                        {
                            sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}'", iddept);
                            SqlCommand cmdtime = new SqlCommand(sql, mycon);
                            SqlDataReader readtime = cmdtime.ExecuteReader();
                            readtime.Read();
                            TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                            TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                        }
                        else
                        {
                            if (chkCa1.IsChecked == true)
                            {
                                sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}' and Ca='Ca1'", iddept);
                                SqlCommand cmdtime = new SqlCommand(sql, mycon);
                                SqlDataReader readtime = cmdtime.ExecuteReader();
                                readtime.Read();
                                TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                                TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                            }
                            else
                            {
                                sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}' and Ca='Ca2'", iddept);
                                SqlCommand cmdtime = new SqlCommand(sql, mycon);
                                SqlDataReader readtime = cmdtime.ExecuteReader();
                                readtime.Read();
                                TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                                TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                            }
                        }
                        sql = string.Format("  Select THACOKIA.dbo.ScanCount.VinCode,THACOKIA.dbo.QTSX.Model,THACOKIA.dbo.ScanCount.TimeStamp from THACOKIA.dbo.QTSX,THACOKIA.dbo.ScanCount where THACOKIA.dbo.QTSX.Vin_Code = THACOKIA.dbo.ScanCount.VinCode and THACOKIA.dbo.ScanCount.Station='{2}' and THACOKIA.dbo.ScanCount.TimeStamp>'{0}' and THACOKIA.dbo.ScanCount.TimeStamp<'{1}' order by THACOKIA.dbo.ScanCount.TimeStamp asc", TimeIn, TimeOut,idstation);
                        SqlCommand cmd = new SqlCommand(sql, mycon);
                        SqlDataReader read = cmd.ExecuteReader();
                        int id = 0;
                        while (read.Read())
                        {
                            id++;
                            sheet.Row(id + 3).Height = 30;
                            sheet.Cell(id + 3, 1).Value = id.ToString();
                            sheet.Cell(id + 3, 1).Style.Font.FontName = "Times New Roman";
                            sheet.Cell(id + 3, 1).Style.Font.FontSize = 12;
                            sheet.Cell(id + 3, 1).Style.Alignment.WrapText = true;
                            sheet.Cell(id + 3, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            sheet.Cell(id + 3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            sheet.Cell(id + 3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            sheet.Cell(id + 3, 2).Value = read["Model"].ToString();
                            sheet.Cell(id + 3, 2).Style.Font.FontName = "Times New Roman";
                            sheet.Cell(id + 3, 2).Style.Font.FontSize = 12;
                            sheet.Cell(id + 3, 2).Style.Alignment.WrapText = true;
                            sheet.Cell(id + 3, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            sheet.Cell(id + 3, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            sheet.Cell(id + 3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            sheet.Cell(id + 3, 3).Value = read["VinCode"].ToString();
                            sheet.Cell(id + 3, 3).Style.Font.FontName = "Times New Roman";
                            sheet.Cell(id + 3, 3).Style.Font.FontSize = 12;
                            sheet.Cell(id + 3, 3).Style.Alignment.WrapText = true;
                            sheet.Cell(id + 3, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            sheet.Cell(id + 3, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            sheet.Cell(id + 3, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            sheet.Cell(id + 3, 4).Value = Convert.ToDateTime(read["TimeStamp"].ToString()).ToString("HH:mm:ss dd/MM/yyyy");
                            sheet.Cell(id + 3, 4).Style.Font.FontName = "Times New Roman";
                            sheet.Cell(id + 3, 4).Style.Font.FontSize = 12;
                            sheet.Cell(id + 3, 4).Style.Alignment.WrapText = true;
                            sheet.Cell(id + 3, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            sheet.Cell(id + 3, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            sheet.Cell(id + 3, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        mycon.Close();
                    }
                }

                sheet.PageSetup.PageOrientation = XLPageOrientation.Portrait;
                sheet.PageSetup.AdjustTo(96);
                sheet.PageSetup.Margins.Top = 0.25;
                sheet.PageSetup.Margins.Bottom = 0.25;
                sheet.PageSetup.Margins.Left = 0;
                sheet.PageSetup.Margins.Right = 0;
                sheet.PageSetup.Margins.Header = 0;
                sheet.PageSetup.Margins.Footer = 0;
                sheet.PageSetup.PaperSize = XLPaperSize.A4Paper;
                sheet.PageSetup.CenterHorizontally = true;
                sheet.PageSetup.SetRowsToRepeatAtTop(3, 3);
                sheet.SheetView.FreezeRows(3);
                var sheet2 = wb.Worksheets.Add("TỔNG HỢP");
                sheet2.Column(1).Width = 5.86;
                sheet2.Column(2).Width = 47.43;
                sheet2.Column(3).Width = 17.14;
                sheet2.Range(sheet2.Cell(1, 1), sheet2.Cell(1, 3)).Merge();
                sheet2.Row(1).Height = 28.5;
                sheet2.Cell(1, 1).Value = "BÁO CÁO TỔNG HỢP NHẬN XE NGÀY " + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy");
                sheet2.Cell(1, 1).Style.Font.Bold = true;
                sheet2.Cell(1, 1).Style.Font.FontName = "Times New Roman";
                sheet2.Cell(1, 1).Style.Font.FontSize = 14;
                sheet2.Cell(1, 1).Style.Alignment.WrapText = true;
                sheet2.Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                sheet2.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet2.Row(2).Height = 8.5;
                sheet2.Row(3).Height = 30;
                sheet2.Cell(3, 1).Value = "TT";
                sheet2.Cell(3, 1).Style.Font.Bold = true;
                sheet2.Cell(3, 1).Style.Font.FontName = "Times New Roman";
                sheet2.Cell(3, 1).Style.Font.FontSize = 12;
                sheet2.Cell(3, 1).Style.Alignment.WrapText = true;
                sheet2.Cell(3, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                sheet2.Cell(3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet2.Cell(3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                sheet2.Cell(3, 2).Value = "Loại xe";
                sheet2.Cell(3, 2).Style.Font.Bold = true;
                sheet2.Cell(3, 2).Style.Font.FontName = "Times New Roman";
                sheet2.Cell(3, 2).Style.Font.FontSize = 12;
                sheet2.Cell(3, 2).Style.Alignment.WrapText = true;
                sheet2.Cell(3, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                sheet2.Cell(3, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet2.Cell(3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                sheet2.Cell(3, 3).Value = "Số lượng";
                sheet2.Cell(3, 3).Style.Font.Bold = true;
                sheet2.Cell(3, 3).Style.Font.FontName = "Times New Roman";
                sheet2.Cell(3, 3).Style.Font.FontSize = 12;
                sheet2.Cell(3, 3).Style.Alignment.WrapText = true;
                sheet2.Cell(3, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                sheet2.Cell(3, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                sheet2.Cell(3, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
                {
                    try
                    {
                        mycon.Open();
                        string TimeIn = " 00:00:00";
                        string TimeOut = " 00:00:00";
                        string sql = string.Empty;
                        if (rb1Ca.IsChecked == true)
                        {
                            sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}'", iddept);
                            SqlCommand cmdtime = new SqlCommand(sql, mycon);
                            SqlDataReader readtime = cmdtime.ExecuteReader();
                            readtime.Read();
                            TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                            TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                        }
                        else
                        {
                            if (chkCa1.IsChecked == true)
                            {
                                sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}' and Ca='Ca1'", iddept);
                                SqlCommand cmdtime = new SqlCommand(sql, mycon);
                                SqlDataReader readtime = cmdtime.ExecuteReader();
                                readtime.Read();
                                TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                                TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                            }
                            else
                            {
                                sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='{0}' and Ca='Ca2'", iddept);
                                SqlCommand cmdtime = new SqlCommand(sql, mycon);
                                SqlDataReader readtime = cmdtime.ExecuteReader();
                                readtime.Read();
                                TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                                TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                            }
                        }
                        sql = string.Format("Select  DISTINCT THACOKIA.dbo.QTSX.Model, Count(THACOKIA.dbo.ScanCount.VinCode) as ct from THACOKIA.dbo.QTSX,THACOKIA.dbo.ScanCount where THACOKIA.dbo.QTSX.Vin_Code = THACOKIA.dbo.ScanCount.VinCode and THACOKIA.dbo.ScanCount.Station='{0}' and THACOKIA.dbo.ScanCount.TimeStamp>'{0}' and THACOKIA.dbo.ScanCount.TimeStamp<'{1}' group by THACOKIA.dbo.QTSX.Model order by THACOKIA.dbo.QTSX.Model asc", TimeIn, TimeOut,idstation);
                        SqlCommand cmd = new SqlCommand(sql, mycon);
                        SqlDataReader read = cmd.ExecuteReader();
                        int id = 0, sum = 0;
                        while (read.Read())
                        {
                            id++;
                            sheet2.Row(id + 3).Height = 30;
                            sheet2.Cell(id + 3, 1).Value = id.ToString();
                            sheet2.Cell(id + 3, 1).Style.Font.FontName = "Times New Roman";
                            sheet2.Cell(id + 3, 1).Style.Font.FontSize = 12;
                            sheet2.Cell(id + 3, 1).Style.Alignment.WrapText = true;
                            sheet2.Cell(id + 3, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            sheet2.Cell(id + 3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            sheet2.Cell(id + 3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            sheet2.Cell(id + 3, 2).Value = read["Model"].ToString();
                            sheet2.Cell(id + 3, 2).Style.Font.FontName = "Times New Roman";
                            sheet2.Cell(id + 3, 2).Style.Font.FontSize = 12;
                            sheet2.Cell(id + 3, 2).Style.Alignment.WrapText = true;
                            sheet2.Cell(id + 3, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            sheet2.Cell(id + 3, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                            sheet2.Cell(id + 3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                            sheet2.Cell(id + 3, 3).Value = read["ct"].ToString();
                            sheet2.Cell(id + 3, 3).Style.Font.FontName = "Times New Roman";
                            sheet2.Cell(id + 3, 3).Style.Font.FontSize = 12;
                            sheet2.Cell(id + 3, 3).Style.Alignment.WrapText = true;
                            sheet2.Cell(id + 3, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            sheet2.Cell(id + 3, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            sheet2.Cell(id + 3, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            sum = sum + int.Parse(read["ct"].ToString());
                        }
                        id++;
                        sheet2.Range(sheet2.Cell(id + 3, 1), sheet2.Cell(id + 3, 2)).Merge();
                        sheet2.Row(id + 3).Height = 30;
                        sheet2.Cell(id + 3, 1).Value = "TỔNG:";
                        sheet2.Cell(id + 3, 1).Style.Font.FontName = "Times New Roman";
                        sheet2.Cell(id + 3, 1).Style.Font.Bold = true;
                        sheet2.Cell(id + 3, 1).Style.Font.FontSize = 12;
                        sheet2.Cell(id + 3, 1).Style.Alignment.WrapText = true;
                        sheet2.Cell(id + 3, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        sheet2.Cell(id + 3, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                        sheet2.Cell(id + 3, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        sheet2.Cell(id + 3, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                        sheet2.Cell(id + 3, 3).Value = sum;
                        sheet2.Cell(id + 3, 3).Style.Font.FontName = "Times New Roman";
                        sheet2.Cell(id + 3, 3).Style.Font.FontSize = 12;
                        sheet2.Cell(id + 3, 3).Style.Alignment.WrapText = true;
                        sheet2.Cell(id + 3, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        sheet2.Cell(id + 3, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        sheet2.Cell(id + 3, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        mycon.Close();
                    }
                }
                wb.SaveAs(saveFile.FileName);
                wb.Dispose();
                MessageBox.Show("Xuất báo cáo thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnRef_Click(object sender, RoutedEventArgs e)
        {
            ShowChartSanLuongNgay();
            ShowChartSanLuongThang();
            ShowChiTiet();
            ShowTongHop();
            ShowChartTongHop();
            showchart();
        }

        private void datapicker2_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LBLNgayDen.Text = datapicker2.SelectedDate.Value.ToString("dd/MM/yyyy");
        }

        private void datapicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            LBLNgayNhap.Text = datapicker1.SelectedDate.Value.ToString("dd/MM/yyyy");
        }

        private void cbbStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem item = (ComboBoxItem)cbbStation.SelectedItem;
            idstation = item.Content.ToString();
            ShowChartSanLuongNgay();
            ShowChartSanLuongThang();
            ShowChiTiet();
            ShowTongHop();
            ShowChartTongHop();
            showchart();
        }

        private void btnTim_Click(object sender, RoutedEventArgs e)
        {
            string sql = string.Format("select * from [THACOKIA].[dbo].[ScanCount], [THACOKIA].[dbo].[QTSX] where THACOKIA.dbo.QTSX.Vin_Code = THACOKIA.dbo.ScanCount.VinCode and Station='{0}' and DAY(THACOKIA.dbo.ScanCount.TimeStamp)>='{1}' and MONTH(THACOKIA.dbo.ScanCount.TimeStamp)>='{2}' and YEAR(THACOKIA.dbo.ScanCount.TimeStamp)>='{3}' and DAY(THACOKIA.dbo.ScanCount.TimeStamp)<='{4}' and MONTH(THACOKIA.dbo.ScanCount.TimeStamp)<='{5}' and YEAR(THACOKIA.dbo.ScanCount.TimeStamp)<='{6}' order by TimeStamp desc", idstation, LBLNgayNhap.Text.ToString().Substring(0, 2), LBLNgayNhap.Text.ToString().Substring(3, 2), LBLNgayNhap.Text.ToString().Substring(6, 4), LBLNgayDen.Text.ToString().Substring(0, 2), LBLNgayDen.Text.ToString().Substring(3, 2), LBLNgayDen.Text.ToString().Substring(6, 4));
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                        SqlCommand cmd = new SqlCommand(sql, mycon);
                        SqlDataReader reader = cmd.ExecuteReader();
                        List<_ItemChiTiet> item = new List<_ItemChiTiet>();
                        int stt = 0;
                        while (reader.Read())
                        {
                            stt++;
                            item.Add(new _ItemChiTiet() { vincode = reader["VinCode"].ToString(), model = reader["Model"].ToString(), tt = stt.ToString(), timestamp = (Convert.ToDateTime(reader["TimeStamp"].ToString()).ToString("HH:mm:ss dd/MM/yyyy"))});
                        }
                        lstThuHienNgay.ItemsSource = item;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    mycon.Close();
                }
            }
        }

        private void btnXuatExcel_Click(object sender, RoutedEventArgs e)
        {
            string duongdan = "";
            Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = "Excel Documents (*.xlsx)|*.xlsx";
            if (dialog.ShowDialog() == true)
            {
                duongdan = dialog.FileName;
            }
            if (string.IsNullOrEmpty(duongdan))
            {
                MessageBox.Show("Đường dẫn không hợp lệ. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                using (ExcelPackage p = new ExcelPackage())
                {
                    p.Workbook.Worksheets.Add(DateTime.Now.ToString("ddMMyyyy"));
                    ExcelWorksheet ws = p.Workbook.Worksheets[1];
                    ws.Name = "Sheet1";
                    ws.Cells.Style.Font.Name = "Times New Roman";
                    ws.Cells.AutoFitColumns();
                    string[] arrColumnHeader = { "STT", "Số khung", "Loại Xe", "Ngày nhập" };
                    var countColHeader = arrColumnHeader.Count();
                    ws.Cells[1, 1].Value = "Thống kê thông tin sản lượng";
                    ws.Cells[1, 1, 1, countColHeader].Merge = true;
                    ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                    ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    ws.Column(1).Width = 5;
                    ws.Column(2).Width = 25;
                    ws.Column(3).Width = 30;
                    ws.Column(4).Width = 25;

                    int col = 1;
                    int row = 2;
                    foreach (var item in arrColumnHeader)
                    {
                        var cell = ws.Cells[row, col];
                        var border = cell.Style.Border;
                        cell.Style.Font.Bold=true;
                        cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                        
                        cell.Value = item;
                        col++;
                    }
                    List<_ItemChiTiet> list = lstThuHienNgay.ItemsSource.Cast<_ItemChiTiet>().ToList();
                    foreach (var item in list)
                    {
                        col = 1;
                        row++;
                        ws.Cells[row, col].Value = item.tt;
                        col++;
                        ws.Cells[row, col].Value = item.vincode;
                        col++;
                        ws.Cells[row, col].Value = item.model;
                        col++;
                        ws.Cells[row, col].Value = item.timestamp;
                    }
                    Byte[] bin = p.GetAsByteArray();
                    File.WriteAllBytes(duongdan, bin);
                }
                MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void showchart()
        {
            ChtSanLuongNgayTrim.Series.Clear();
            ChtSanLuongNgayTrim.Series.Add(new PieSeries { Title = "Sản xuất ngày", Fill = Brushes.Green, DataLabels = true, FontSize = 10, FontFamily = new FontFamily("Tahoma"), StrokeThickness = 0, Values = new ChartValues<int> { ActualNgayTrim() } });
            ChtSanLuongNgayTrim.Series.Add(new PieSeries { Title = "Còn lại", Fill = Brushes.Red, DataLabels = true, FontSize = 10, FontFamily = new FontFamily("Tahoma"), StrokeThickness = 0, Values = new ChartValues<int> { PlanNgayTrim() - ActualNgayTrim() } });
            ChtSanLuongNgayTrim.LegendLocation = LegendLocation.Bottom;

            ChtSanLuongNgayChassis.Series.Clear();
            ChtSanLuongNgayChassis.Series.Add(new PieSeries { Title = "Sản xuất ngày", Fill = Brushes.Green, DataLabels = true, FontSize = 10, FontFamily = new FontFamily("Tahoma"), StrokeThickness = 0, Values = new ChartValues<int> { ActualNgayChassis() } });
            ChtSanLuongNgayChassis.Series.Add(new PieSeries { Title = "Còn lại", Fill = Brushes.Red, DataLabels = true, FontSize = 10, FontFamily = new FontFamily("Tahoma"), StrokeThickness = 0, Values = new ChartValues<int> { PlanNgayChassis() - ActualNgayChassis() } });
            ChtSanLuongNgayChassis.LegendLocation = LegendLocation.Bottom;

            ChtSanLuongNgayFinal.Series.Clear();
            ChtSanLuongNgayFinal.Series.Add(new PieSeries { Title = "Sản xuất ngày", Fill = Brushes.Green, DataLabels = true, FontSize = 10, FontFamily = new FontFamily("Tahoma"), StrokeThickness = 0, Values = new ChartValues<int> { ActualNgayFinal() } });
            ChtSanLuongNgayFinal.Series.Add(new PieSeries { Title = "Còn lại", Fill = Brushes.Red, DataLabels = true, FontSize = 10, FontFamily = new FontFamily("Tahoma"), StrokeThickness = 0, Values = new ChartValues<int> { PlanNgayFinal() - ActualNgayFinal() } });
            ChtSanLuongNgayFinal.LegendLocation = LegendLocation.Bottom;
        }



        private int ActualNgayTrim()
        {
            int value = 0;
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    string TimeIn = " 00:00:00";
                    string TimeOut = " 00:00:00";
                    string sql = string.Empty;

                    sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='D00004'");
                    SqlCommand cmdtime = new SqlCommand(sql, mycon);
                    SqlDataReader readtime = cmdtime.ExecuteReader();
                    while (readtime.Read())
                    {
                        TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                        TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                    }

                    string sql1 = string.Format("Select COUNT(THACOKIA.dbo.ScanCount.VinCode) as ct from THACOKIA.dbo.ScanCount where THACOKIA.dbo.ScanCount.Station='TRIM' and THACOKIA.dbo.ScanCount.TimeStamp>'{0}' and THACOKIA.dbo.ScanCount.TimeStamp<'{1}'", TimeIn, TimeOut);
                    SqlCommand cmd = new SqlCommand(sql1, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        value = Int32.Parse(read["ct"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ActualNgay");
                }
                finally
                {
                    mycon.Close();
                }
            }
            return value;
        }

        private int ActualNgayChassis()
        {
            int value = 0;
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    string TimeIn = " 00:00:00";
                    string TimeOut = " 00:00:00";
                    string sql = string.Empty;

                    sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='D00004'");
                    SqlCommand cmdtime = new SqlCommand(sql, mycon);
                    SqlDataReader readtime = cmdtime.ExecuteReader();
                    while (readtime.Read())
                    {
                        TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                        TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                    }

                    string sql1 = string.Format("Select COUNT(THACOKIA.dbo.ScanCount.VinCode) as ct from THACOKIA.dbo.ScanCount where THACOKIA.dbo.ScanCount.Station='CHASSIS' and THACOKIA.dbo.ScanCount.TimeStamp>'{0}' and THACOKIA.dbo.ScanCount.TimeStamp<'{1}'", TimeIn, TimeOut);
                    SqlCommand cmd = new SqlCommand(sql1, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        value = Int32.Parse(read["ct"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ActualNgay");
                }
                finally
                {
                    mycon.Close();
                }
            }
            return value;
        }

        private int ActualNgayFinal()
        {
            int value = 0;
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    string TimeIn = " 00:00:00";
                    string TimeOut = " 00:00:00";
                    string sql = string.Empty;

                    sql = string.Format("SELECT * FROM [THACOKIA].[dbo].[TimeSetting] where IDDept ='D00004'");
                    SqlCommand cmdtime = new SqlCommand(sql, mycon);
                    SqlDataReader readtime = cmdtime.ExecuteReader();
                    while (readtime.Read())
                    {
                        TimeIn = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeIn"].ToString();
                        TimeOut = DateTime.Now.ToString("yyyy-MM-dd") + readtime["TimeOut"].ToString();
                    }

                    string sql1 = string.Format("Select COUNT(THACOKIA.dbo.ScanCount.VinCode) as ct from THACOKIA.dbo.ScanCount where THACOKIA.dbo.ScanCount.Station='CSFINAL' and THACOKIA.dbo.ScanCount.TimeStamp>'{0}' and THACOKIA.dbo.ScanCount.TimeStamp<'{1}'", TimeIn, TimeOut);
                    SqlCommand cmd = new SqlCommand(sql1, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        value = Int32.Parse(read["ct"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ActualNgay");
                }
                finally
                {
                    mycon.Close();
                }
            }
            return value;
        }

        private int PlanNgayTrim()
        {
            int value = 0;
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();

                    string sql = string.Format("SELECT *  FROM THACOKIA.dbo.[PLAN] where THACOKIA.dbo.[PLAN].SHIFT='Ca1' and Station='TRIM'");
                    SqlCommand cmd = new SqlCommand(sql, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        value = Int32.Parse(read["DayPlan"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "PlanNgay");
                }
                finally
                {
                    mycon.Close();
                }
            }
            return value;
        }

        private int PlanNgayChassis()
        {
            int value = 0;
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    string sql = string.Format("SELECT *  FROM THACOKIA.dbo.[PLAN] where THACOKIA.dbo.[PLAN].SHIFT='Ca1' and Station='CHASSIS'");
                    SqlCommand cmd = new SqlCommand(sql, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        value = Int32.Parse(read["DayPlan"].ToString());
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "PlanNgay");
                }
                finally
                {
                    mycon.Close();
                }
            }
            return value;
        }

        private int PlanNgayFinal()
        {
            int value = 0;
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    string sql = string.Format("SELECT *  FROM THACOKIA.dbo.[PLAN] where THACOKIA.dbo.[PLAN].SHIFT='Ca1' and Station='CSFINAL'");
                    SqlCommand cmd = new SqlCommand(sql, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        value = Int32.Parse(read["DayPlan"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "PlanNgay");
                }
                finally
                {
                    mycon.Close();
                }
            }
            return value;
        }
    }
}
