using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ManHinhHienThi.XuongLapRap
{
    public partial class Chassis : Window
    {
        public Chassis()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            showdata();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            txtNgayThang.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            showdata();
        }

        public class Xe
        {
            public string loaixe { get; set; }
            public int thuchienngay { get; set; }
        }
        
        public void showdata()
        {
            string sql = string.Format("SELECT [Ca],[ThoiGianVao],[ThoiGianRa] FROM[THACOKIA].[dbo].[WorkingTime] where Ca='Ca1'");
            string sql1 = string.Format("select COUNT (VinCode) as demngay from [THACOKIA].[dbo].[ScanCount] where DAY(THACOKIA.dbo.ScanCount.TimeStamp)='{0}' and MONTH(THACOKIA.dbo.ScanCount.TimeStamp)='{1}' and YEAR(THACOKIA.dbo.ScanCount.TimeStamp)='{2}' and Station='CHASSIS'", DateTime.Now.ToString("dd"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy"));
            string sql3 = string.Format("select count(VinCode) as thuchienthang from [THACOKIA].[dbo].[ScanCount] where MONTH(THACOKIA.dbo.ScanCount.TimeStamp)='{0}' and YEAR(THACOKIA.dbo.ScanCount.TimeStamp)='{1}' and Station='CHASSIS'", DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy"));
            using (SqlConnection mycon = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    int thoigian = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(sql, mycon); 
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (thoigian>=Convert.ToInt32(reader["ThoiGianVao"]) && thoigian <= Convert.ToInt32(reader["ThoiGianRa"]))
                        {
                            string sql2 = string.Format("SELECT * FROM[THACOKIA].[dbo].[PLAN] where Station = 'CHASSIS' and SHIFT='Ca1'");
                            SqlCommand cmd2 = new SqlCommand(sql2, mycon);
                            SqlDataReader reader2 = cmd2.ExecuteReader();
                            while (reader2.Read())
                            {
                                lblKeHoachNgay.Text = reader2["DayPlan"].ToString();
                                lblKeHoachThang.Text = reader2["MonthPlan"].ToString();
                            }
                        }
                        else
                        {
                            string sql2 = string.Format("SELECT * FROM[THACOKIA].[dbo].[PLAN] where Station = 'CHASSIS' and SHIFT='Ca2'");
                            SqlCommand cmd2 = new SqlCommand(sql2, mycon);
                            SqlDataReader reader2 = cmd2.ExecuteReader();
                            while (reader2.Read())
                            {
                                lblKeHoachNgay.Text = reader2["DayPlan"].ToString();
                                lblKeHoachThang.Text = reader2["MonthPlan"].ToString();
                            }
                        }
                    }
                    SqlCommand cmd1 = new SqlCommand(sql1, mycon);
                    SqlDataReader reader1 = cmd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        lblThucHienNgay.Text = reader1["demngay"].ToString();
                    }

                    SqlCommand cmd3 = new SqlCommand(sql3, mycon);
                    SqlDataReader reader3 = cmd3.ExecuteReader();
                    while (reader3.Read())
                    {
                        lblThucHienThang.Text = reader3["thuchienthang"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống: " + ex.ToString(), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    mycon.Close();
                }
            }
        }
    }
}
