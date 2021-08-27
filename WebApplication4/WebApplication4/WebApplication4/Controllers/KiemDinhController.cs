using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class KiemDinhController : Controller
    {
        THACOKIAEntities1 entities = new THACOKIAEntities1();
        // GET: Home

        public class CarModel
        {
            public string CarName { get; set; }
            public int SoLuong { get; set; }
        }
        // GET: KiemDinh
        public ActionResult Index()
        {
            DateTime date = DateTime.Now;

            var thn = entities.ScanCounts
                .Where(x => x.Station == "PDI" && x.TimeStamp.Day == date.Day
                                                && x.TimeStamp.Month == date.Month
                                                && x.TimeStamp.Year == date.Year).ToList();
            ViewBag.thnn = thn.Count();

            var tht = entities.ScanCounts
                    .Where(x => x.Station == "PDI" && x.TimeStamp.Month == date.Month
                                                    && x.TimeStamp.Year == date.Year).ToList();
            ViewBag.thtt = tht.Count();

            var khn = entities.PLANs
                .Where(x => x.Station == "PDI" && x.SHIFT == "Ca1").FirstOrDefault();
            ViewBag.khnn = khn.DayPlan.ToString();

            var kht = entities.PLANs
                .Where(x => x.Station == "PDI" && x.SHIFT == "Ca1").FirstOrDefault();
            ViewBag.khtt = kht.MonthPlan.ToString();

            string query = "Select DISTINCT THACOKIA.dbo.Barcode.CarName, count(THACOKIA.dbo.QTSX.Vin_Code) as SoLuong from THACOKIA.dbo.QTSX,THACOKIA.dbo.Barcode,THACOKIA.dbo.ScanCount where THACOKIA.dbo.ScanCount.Station='PDI' and THACOKIA.dbo.ScanCount.VinCode=THACOKIA.dbo.QTSX.Vin_Code and THACOKIA.dbo.QTSX.Model=THACOKIA.dbo.Barcode.Model and day(THACOKIA.dbo.ScanCount.TimeStamp)={0} and MONTH(THACOKIA.dbo.ScanCount.TimeStamp)={1} and YEAR(THACOKIA.dbo.ScanCount.TimeStamp)={2} group by THACOKIA.dbo.Barcode.CarName  order by THACOKIA.dbo.Barcode.CarName asc";
            var list = entities.Database.SqlQuery<CarModel>(query, date.Day, date.Month, date.Year).ToList();
            ViewBag.vv = list;
            return View();
            
        }
    }
}