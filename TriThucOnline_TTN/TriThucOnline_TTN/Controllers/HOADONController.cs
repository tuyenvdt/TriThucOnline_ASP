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
    public class HOADONController : Controller
    {
        private SQL_TriThucOnline_BanSachEntities db = new SQL_TriThucOnline_BanSachEntities();

        string connectionString = @"Data source = DESKTOP-QDK2G05\SQLEXPRESS ; Initial Catalog = SQL_TriThucOnline_BanSach ; integrated Security = True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dttbHOADON = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from HOADON", sqlCon);
                sqlDa.Fill(dttbHOADON);
            }
            return View(dttbHOADON);
        }

        // GET: NXB/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NXB/Create
        public ActionResult Create()
        {
            ViewBag.Masach = new SelectList(db.DAUSACHes, "Masach", "Tensach");
            ViewBag.IDUser = new SelectList(db.KHACHHANGs, "IDUser", "Username");

            return View(new HOADON());
        }

        // POST: NXB/Create
        [HttpPost]
        public ActionResult Create(HOADON hoadon)
        {

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "insert into HOADON OUTPUT inserted.Mahd values(@Masach,GETDATE(),@IDUser)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Mahd", hoadon.Mahd);
                sqlCmd.Parameters.AddWithValue("@Ngaymuahang", hoadon.Ngaymuahang);
                sqlCmd.Parameters.AddWithValue("@IDUser", hoadon.IDUser);

                sqlCmd.ExecuteNonQuery();
            }


            return RedirectToAction("Index");
        }

        // GET: NXB/Edit/5
        public ActionResult Edit(int id)
        {
            HOADON hoadon = new HOADON();
            DataTable dttbHOADON = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "select * from HOADON where Mahd = @Mahd";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@Mahd", id);
                sqlDa.Fill(dttbHOADON);
            }
            if (dttbHOADON.Rows.Count == 1)
            {
                hoadon.Mahd = Convert.ToInt32(dttbHOADON.Rows[0][0].ToString());
                hoadon.Ngaymuahang = DateTime.Parse(dttbHOADON.Rows[0][1].ToString());
                hoadon.IDUser = Convert.ToInt32(dttbHOADON.Rows[0][2].ToString());


                ViewBag.Masach = new SelectList(db.DAUSACHes, "Masach", "Tensach");
                ViewBag.IDUser = new SelectList(db.KHACHHANGs, "IDUser", "Username");


                return View(hoadon);
            }


            else return RedirectToAction("Index");



        }

        // POST: NXB/Edit/5
        [HttpPost]
        public ActionResult Edit(HOADON hoadon)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE HOADON set Masach=@Masach,Ngaymuahang = @Ngaymuahang,IDUser = @IDUser" +
                    " where Mahd=@Mahd";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Mahd", hoadon.Mahd);
                sqlCmd.Parameters.AddWithValue("@Ngaymuahang", hoadon.Ngaymuahang);
                sqlCmd.Parameters.AddWithValue("@IDUser", hoadon.IDUser);

                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: NXB/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE  from HOADON where Mahd = @Mahd";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Mahd", id);

                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // POST: NXB/Delete/5
    }
}
