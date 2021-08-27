using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Data.SqlClient;
using System.Configuration;

namespace ManHinhHienThi
{
    public partial class SanLuongFinal : Window
    {
        public SanLuongFinal()
        {
            InitializeComponent();
            ShowData(DateTime.Now);
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            txtNgayThang.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public class Xe
        {
            public int thutu { get; set; }
            public string sokhung { get; set; }
            public string thoigiannhap { get; set; }
            public string loaixe { get; set; }
            public string mauson { get; set; }
        }

        private void btnShutdown_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Bạn có muốn thoát ứng dụng hay không?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                App.Current.Shutdown();
            }
        }

        private void ShowData(DateTime date)
        {
            string sql = string.Format("Select * from [THACOKIA].[dbo].[ScanCount],[THACOKIA].[dbo].[QTSX]  where THACOKIA.dbo.QTSX.Vin_Code = THACOKIA.dbo.ScanCount.VinCode and  DAY(THACOKIA.dbo.ScanCount.TimeStamp)='{0}' and MONTH(THACOKIA.dbo.ScanCount.TimeStamp)='{1}' and YEAR(THACOKIA.dbo.ScanCount.TimeStamp)='{2}' and Station='{3}' order by TimeStamp desc ", date.Day, date.Month, date.Year, lblTramNhap.Text);
            using (SqlConnection mycon = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(sql, mycon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Xe> item = new List<Xe>();
                    int stt = 0;
                    while (reader.Read())
                    {
                        stt++;
                        item.Add(new Xe() { thutu = stt, loaixe = reader["Model"].ToString(), mauson = reader["Color"].ToString(), sokhung = reader["VinCode"].ToString(), thoigiannhap = (Convert.ToDateTime(reader["TimeStamp"].ToString()).ToString("dd/MM/yyyy HH:mm:ss")) });
                    }
                    lvXe.ItemsSource = null;
                    lvXe.ItemsSource = item;
                    lblThucTeNgay.Text = stt.ToString();

                    string sql1 = string.Format("select top 1 * from [THACOKIA].[dbo].[PLAN] where Station like '%{0}%' and SHIFT ='Ca1' ", lblTramNhap.Text);
                    SqlCommand cm1 = new SqlCommand(sql1, mycon);
                    SqlDataReader reader1 = cm1.ExecuteReader();
                    while (reader1.Read())
                    {
                        lblKeHoachNgay.Text = reader1["DayPlan"].ToString();
                        lblKeHoachThang.Text = reader1["MonthPlan"].ToString();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Lỗi hệ thống: " + e.ToString(), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    mycon.Close();
                }
            }

            string sql2 = string.Format("select count (VinCode) as ThucHienTrongThang  from [THACOKIA].[dbo].[ScanCount] where MONTH(THACOKIA.dbo.ScanCount.TimeStamp)='{0}' and YEAR(THACOKIA.dbo.ScanCount.TimeStamp)='{1}' and Station='{2}'", date.Month, date.Year, lblTramNhap.Text);
            using (SqlConnection mycon2 = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon2.Open();
                    SqlCommand cm2 = new SqlCommand(sql2, mycon2);
                    SqlDataReader reader2 = cm2.ExecuteReader();
                    while (reader2.Read())
                    {
                        lblThucTeThang.Text = reader2["ThucHienTrongThang"].ToString();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Lỗi hệ thống: " + e.ToString(), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    mycon2.Close();
                }
            }
        }

        private void txtSoKhung_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (txtSoKhung.Text.Trim().Length == 17)
                {

                    string sql = string.Format("select * from [THACOKIA].[dbo].[QTSX] where Vin_Code='{0}'", txtSoKhung.Text);
                    string ktsk = string.Format("Select count(TimeStamp) as sk from [THACOKIA].[dbo].[ScanCount] where VinCode='{0}' and Station='{1}'", txtSoKhung.Text, lblTramNhap.Text);
                    using (SqlConnection mycon = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
                        try
                        {
                            mycon.Open();
                            SqlCommand cmd = new SqlCommand(ktsk, mycon);
                            SqlDataReader rd = cmd.ExecuteReader();
                            while (rd.Read())
                            {
                                if (Convert.ToInt16(rd["sk"].ToString()) == 0)
                                {
                                    SqlCommand cm = new SqlCommand(sql, mycon);
                                    SqlDataReader read = cm.ExecuteReader();
                                    while (read.Read())
                                    {
                                        if (read["Vin_Code"].ToString() != "")
                                        {
                                            lblSoKhung.Text = read["Vin_Code"].ToString();
                                            lblLoaiXe.Text = read["Model"].ToString();
                                            lblMauSon.Text = read["Color"].ToString();
                                            lblSoKhung.Visibility = Visibility.Visible;
                                            lblLoaiXe.Visibility = Visibility.Visible;
                                            lblMauSon.Visibility = Visibility.Visible;

                                            string sql1 = string.Format("select * from [THACOKIA].[dbo].[Barcode] where Model='{0}'", lblLoaiXe.Text);
                                            SqlCommand cm1 = new SqlCommand(sql1, mycon);
                                            SqlDataReader reader1 = cm1.ExecuteReader();
                                            while (reader1.Read())
                                            {
                                                lblSoLot.Text = reader1["ModelCode"].ToString();
                                                lblSoLot.Visibility = Visibility.Visible;
                                            }

                                            string sql2 = string.Format("Insert into THACOKIA.dbo.ScanCount (VinCode, Station, TimeStamp) Values ('{0}','{1}','{2}')", txtSoKhung.Text, lblTramNhap.Text, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                                            SqlCommand cm2 = new SqlCommand(sql2, mycon);
                                            cm2.ExecuteNonQuery();
                                            lblOK.Text = "OK";
                                            lblOK.Visibility = Visibility.Visible;
                                            lblOK.Foreground = Brushes.Green;
                                            ShowData(DateTime.Now);
                                            txtSoKhung.Clear();
                                        }
                                        else
                                        {
                                            lblOK.Text = "NG!";
                                            lblOK.Visibility = Visibility.Visible;
                                            lblOK.Foreground = Brushes.Red;
                                        }
                                    }
                                }
                                else
                                {

                                    SqlCommand cm = new SqlCommand(sql, mycon);
                                    SqlDataReader read = cm.ExecuteReader();
                                    while (read.Read())
                                    {
                                        if (read["Vin_Code"].ToString() != "")
                                        {
                                            lblSoKhung.Text = read["Vin_Code"].ToString();
                                            lblLoaiXe.Text = read["Model"].ToString();
                                            lblMauSon.Text = read["Color"].ToString();
                                            lblSoKhung.Visibility = Visibility.Visible;
                                            lblLoaiXe.Visibility = Visibility.Visible;
                                            lblMauSon.Visibility = Visibility.Visible;
                                            lblOK.Text = "OK";
                                            lblOK.Visibility = Visibility.Visible;
                                            lblOK.Foreground = Brushes.Green;

                                            string sql1 = string.Format("select * from [THACOKIA].[dbo].[Barcode] where Model='{0}'", lblLoaiXe.Text);
                                            SqlCommand cm1 = new SqlCommand(sql1, mycon);
                                            SqlDataReader reader1 = cm1.ExecuteReader();
                                            while (reader1.Read())
                                            {
                                                lblSoLot.Text = reader1["ModelCode"].ToString();
                                                lblSoLot.Visibility = Visibility.Visible;
                                            }
                                        }
                                        else
                                        {
                                            lblOK.Text = "NG!";
                                            lblOK.Visibility = Visibility.Visible;
                                            lblOK.Foreground = Brushes.Red;
                                        }
                                    }
                                    string sql3 = string.Format("Update THACOKIA.dbo.ScanCount set TimeStamp='{2}' where VinCode='{0}' and Station='{1}' ", txtSoKhung.Text, lblTramNhap.Text, DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                                    SqlCommand cm3 = new SqlCommand(sql3, mycon);
                                    cm3.ExecuteNonQuery();
                                    ShowData(DateTime.Now);
                                    txtSoKhung.Clear();
                                }
                            }
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
                else
                {
                    lblOK.Text = "NG!";
                    lblOK.Visibility = Visibility.Visible;
                    lblOK.Foreground = Brushes.Red;
                }
            }

            thoigianan = new System.Windows.Forms.Timer();
            thoigianan.Tick += new EventHandler(ThoiGianAn_Tick);
            thoigianan.Interval = 1000; //1 s
            thoigianan.Start();
        }

        private System.Windows.Forms.Timer thoigianan;

        private int dem = 100;

        private void ThoiGianAn_Tick(object sender, EventArgs e)
        {
            dem--;
            if (dem == 0)
            {
                thoigianan.Stop();
                lblSoKhung.Visibility = Visibility.Hidden;
                lblSoLot.Visibility = Visibility.Hidden;
                lblMauSon.Visibility = Visibility.Hidden;
                lblLoaiXe.Visibility = Visibility.Hidden;
                lblOK.Visibility = Visibility.Hidden;
                dem = 100;
            }
        }
        private void btnMini_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
