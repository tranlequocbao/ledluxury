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

namespace ManHinhHienThi.XuongHan
{
    /// <summary>
    /// Interaction logic for HienthiSLXuongHan.xaml
    /// </summary>
    public partial class HienthiSLXuongHan : Window
    {
        public HienthiSLXuongHan()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            txtNgayThang.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            showdata();
        }

        public class _ItemTongHop
        {
            public string loaixe { get; set; }
            
            public string soluong { get; set; }
        }

        public void showdata()
        {
            string sql = string.Format("SELECT * FROM[THACOKIA].[dbo].[WorkingTime] where Ca='Ca1'");
            string sql3 = string.Format(" select count(Vin_Code) as thuchienthang from THACOKIA.dbo.QTSX where BodyPrinted=1 and MONTH(DayBodyPrinted)='{0}' and YEAR(DayBodyPrinted)='{1}'", DateTime.Now.ToString("MM"), DateTime.Now.ToString("yyyy"));

            using (SqlConnection mycon = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ToString()))
            {
                try
                {
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(sql, mycon);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int thoigian = Convert.ToInt32(DateTime.Now.ToString("HHmmss"));
                        if (thoigian >= Convert.ToInt32(reader["ThoiGianVao"]) && thoigian <= Convert.ToInt32(reader["ThoiGianRa"]))
                        {
                            string sql2 = string.Format("SELECT * FROM[THACOKIA].[dbo].[PLAN] where Station = 'HAN' and SHIFT='Ca1'");
                            SqlCommand cmd2 = new SqlCommand(sql2, mycon);
                            SqlDataReader reader2 = cmd2.ExecuteReader();
                            while (reader2.Read())
                            {
                                lblKeHoachNgay.Text = reader2["DayPlan"].ToString();
                                lblKeHoachThang.Text = reader2["MonthPlan"].ToString();
                            }

                            string thoigianvao = DateTime.Now.ToString("yyyy-MM-dd") + " " + reader["ThoiGianVao"].ToString().Substring(0, 2) + ":" + reader["ThoiGianVao"].ToString().Substring(2, 2) + ":" + reader["ThoiGianVao"].ToString().Substring(4, 2);
                            string thoigianra = DateTime.Now.ToString("yyyy-MM-dd") + " " + reader["ThoiGianRa"].ToString().Substring(0, 2) + ":" + reader["ThoiGianRa"].ToString().Substring(2, 2) + ":" + reader["ThoiGianRa"].ToString().Substring(4, 2);
                            
                            string sql4 = string.Format("Select DISTINCT THACOKIA.dbo.Barcode.CarName, count(THACOKIA.dbo.QTSX.BodyPrinted) as ct from THACOKIA.dbo.QTSX,THACOKIA.dbo.Barcode where THACOKIA.dbo.QTSX.BodyPrinted=1 and [THACOKIA].[dbo].[QTSX].[Model]=[THACOKIA].[dbo].[Barcode].[Model] and THACOKIA.dbo.QTSX.DayBodyPrinted>'{0}' and THACOKIA.dbo.QTSX.DayBodyprinted<'{1}' group by THACOKIA.dbo.Barcode.CarName order by THACOKIA.dbo.Barcode.CarName asc", thoigianvao, thoigianra);
                            SqlCommand cmd4 = new SqlCommand(sql4, mycon);
                            List<_ItemTongHop> _Items = new List<_ItemTongHop>();
                            SqlDataReader read4 = cmd4.ExecuteReader();
                            int id = 0;
                            while (read4.Read())
                            {
                                id++;
                                _Items.Add(new _ItemTongHop() { loaixe = read4["CarName"].ToString(), soluong = read4["ct"].ToString() });
                            }
                            lstTongHop.ItemsSource = _Items;

                            string sql6 = string.Format("Select count(THACOKIA.dbo.QTSX.BodyPrinted) as st from THACOKIA.dbo.QTSX where THACOKIA.dbo.QTSX.BodyPrinted = 1 and THACOKIA.dbo.QTSX.DayBodyPrinted > '{0}' and THACOKIA.dbo.QTSX.DayBodyprinted < '{1}'", thoigianvao, thoigianra);
                            SqlCommand cmd6 = new SqlCommand(sql6, mycon);
                            SqlDataReader read6 = cmd6.ExecuteReader();

                            while (read6.Read())
                            {
                                lblThucHienNgay.Text = read6["st"].ToString();
                            }
                        }
                        else
                        {
                            string sql5 = string.Format("SELECT * FROM[THACOKIA].[dbo].[WorkingTime] where Ca='Ca2'");
                            SqlCommand cmd5 = new SqlCommand(sql5, mycon);
                            SqlDataReader reader5 = cmd5.ExecuteReader();
                            while (reader5.Read())
                            {
                                string thoigianvao = DateTime.Now.ToString("yyyy-MM-dd") + " " + reader5["ThoiGianVao"].ToString().Substring(0, 2) + ":" + reader5["ThoiGianVao"].ToString().Substring(2, 2) + ":" + reader5["ThoiGianVao"].ToString().Substring(4, 2);
                                string thoigianra = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd") + " " + reader5["ThoiGianRa"].ToString().Substring(0, 2) + ":" + reader5["ThoiGianRa"].ToString().Substring(2, 2) + ":" + reader5["ThoiGianRa"].ToString().Substring(4, 2);
                                
                                string sql4 = string.Format(" Select  DISTINCT THACOKIA.dbo.Barcode.CarName, count(THACOKIA.dbo.QTSX.BodyPrinted) as ct from THACOKIA.dbo.QTSX,THACOKIA.dbo.Barcode where  THACOKIA.dbo.QTSX.BodyPrinted=1 and THACOKIA.dbo.QTSX.DayBodyPrinted>'{0}' and THACOKIA.dbo.QTSX.DayBodyprinted<'{1}' group by THACOKIA.dbo.Barcode.CarName order by THACOKIA.dbo.Barcode.CarName asc", thoigianvao, thoigianra);
                                SqlCommand cmd4 = new SqlCommand(sql4, mycon);
                                List<_ItemTongHop> _Items = new List<_ItemTongHop>();
                                SqlDataReader read4 = cmd4.ExecuteReader();
                               
                                while (read4.Read())
                                {
                                    _Items.Add(new _ItemTongHop() { loaixe = read4["CarName"].ToString(), soluong = read4["ct"].ToString() });
                                }
                                lstTongHop.ItemsSource = _Items;

                                string sql6 = string.Format("Select count(THACOKIA.dbo.QTSX.BodyPrinted) as st from THACOKIA.dbo.QTSX where THACOKIA.dbo.QTSX.BodyPrinted = 1 and THACOKIA.dbo.QTSX.DayBodyPrinted > '{0}' and THACOKIA.dbo.QTSX.DayBodyprinted < '{1}'", thoigianvao, thoigianra);
                                SqlCommand cmd6 = new SqlCommand(sql6, mycon);
                                SqlDataReader read6 = cmd6.ExecuteReader();

                                while (read6.Read())
                                {
                                    lblThucHienNgay.Text = read6["st"].ToString();
                                }
                            }
                            string sql2 = string.Format("SELECT * FROM[THACOKIA].[dbo].[PLAN] where Station = 'HAN' and SHIFT='Ca2'");
                            SqlCommand cmd2 = new SqlCommand(sql2, mycon);
                            SqlDataReader reader2 = cmd2.ExecuteReader();
                            while (reader2.Read())
                            {
                                lblKeHoachNgay.Text = reader2["DayPlan"].ToString();
                                lblKeHoachThang.Text = reader2["MonthPlan"].ToString();
                            }
                        }
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
