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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TKAC_Application
{
    /// <summary>
    /// Interaction logic for Workshop.xaml
    /// </summary>
    public partial class Workshop : UserControl
    {
        public Workshop()
        {
            InitializeComponent();
            Sanluongngay sln = new Sanluongngay();
            //sln.Porcentagem = (Convert.ToDouble(lblThucTeNgay.Text) / Convert.ToDouble(lblKeHoachNgay.Text)) * 100;
            //sln.Porcentagem1 = (Convert.ToDouble(lblThucTeThang.Text) / Convert.ToDouble(lblKeHoachThang.Text)) * 100;
            sln.Titulo = "Thực tế trong ngày";
            sln.Titulo1 = "Thực tế trong tháng";
            DataContext = new ConsumoViewModel(sln);
        }
        internal class Sanluongngay
        {
            public string Titulo { get; set; }
            public double Porcentagem { get; set; }
            public string Titulo1 { get; set; }
            public double Porcentagem1 { get; set; }
        }
        internal class ConsumoViewModel
        {
            public List<Sanluongngay> Sanluongngay { get; private set; }
            public ConsumoViewModel(Sanluongngay sln)
            {
                Sanluongngay = new List<Sanluongngay>();
                Sanluongngay.Add(sln);
            }
        }


    }
}
