using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using TriThucOnline_TTN.Models;
using System.Net;

namespace TriThucOnline_TTN.Areas.Admin.Controllers
{
    public class NXBQTController : Controller

    {
        private SQL_TriThucOnline_BanSachEntities db = new SQL_TriThucOnline_BanSachEntities();

        string connectionString = @"Data source = DESKTOP-QDK2G05\SQLEXPRESS ; Initial Catalog = SQL_TriThucOnline_BanSach ; integrated Security = True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dttbNXB = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from NXB", sqlCon);
                sqlDa.Fill(dttbNXB);
            }
            return View(dttbNXB);
        }

        // GET: NXB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) // không có ID 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NXB nxb = db.NXBs.Find(id);
            if (nxb == null) // có ID nhưng không tìm thấy thông tin về id đó
            {
                return HttpNotFound();
            }
            else
            {
                DataTable dttbNXB = new DataTable();
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {

                    sqlCon.Open();
                    string SQL = "DECLARE @id INT; set @id = "+id+"; select * from DAUSACH" +
                        " inner join NXB on DAUSACH.MaNXB = NXB.MaNXB where NXB.MaNXB = @id";
                    SqlDataAdapter sqlDa = new SqlDataAdapter(SQL, sqlCon); /**/
                    sqlDa.Fill(dttbNXB);
                }
                return View(dttbNXB);
            }
        }

        // GET: NXB/Create
        public ActionResult Create()
        {
            return View(new NXB());
        }

        // POST: NXB/Create
        [HttpPost]
        public ActionResult Create(NXB nxb)
        {

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "insert into NXB OUTPUT inserted.MaNXB values(@TenNXB)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@MaNXB", nxb.MaNXB);
                sqlCmd.Parameters.AddWithValue("@TenNXB", nxb.TenNXB);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: NXB/Edit/5
        public ActionResult Edit(int id)
        {
            NXB nxb = new NXB();
            DataTable dttbNXB = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "select * from NXB where MaNXB = @MaNXB";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@MaNXB", id);
                sqlDa.Fill(dttbNXB);
            }
            if (dttbNXB.Rows.Count == 1)
            {
                nxb.MaNXB = Convert.ToInt32(dttbNXB.Rows[0][0].ToString());
                nxb.TenNXB = dttbNXB.Rows[0][1].ToString();
                return View(nxb);
            }
            else return RedirectToAction("Index");

        }

        // POST: NXB/Edit/5
        [HttpPost]
        public ActionResult Edit(NXB nxb)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE NXB SET TenNXB =@TenNXB where MaNXB = @MaNXB";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@MaNXB", nxb.MaNXB);
                sqlCmd.Parameters.AddWithValue("@TenNXB", nxb.TenNXB);
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
                string query = "DELETE  from NXB  where MaNXB = @MaNXB";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@MaNXB", id);

                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // POST: NXB/Delete/5


    }
}
