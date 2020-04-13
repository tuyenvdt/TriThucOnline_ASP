using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TriThucOnline_TTN.Models;


namespace TriThucOnline_TTN.Controllers
{
    public class HomeController : Controller
    {
        private SQL_TriThucOnline_BanSachEntities db = new SQL_TriThucOnline_BanSachEntities();
        string connectionString = @"Data source = DESKTOP-QDK2G05\SQLEXPRESS ; Initial Catalog = SQL_TriThucOnline_BanSach ; integrated Security = True";

        public ActionResult Index()
        {
            
            DataTable dttbDAUSACH = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from DAUSACH ", sqlCon); /*inner join THELOAI on DAUSACH.MaTL = THELOAI.MaTL where THELOAI.MaTL = 1*/
                sqlDa.Fill(dttbDAUSACH);
            }
            return View(dttbDAUSACH);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}