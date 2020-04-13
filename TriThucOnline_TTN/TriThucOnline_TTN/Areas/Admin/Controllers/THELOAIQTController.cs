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
    public class THELOAIQTController : Controller
    {

        private SQL_TriThucOnline_BanSachEntities db = new SQL_TriThucOnline_BanSachEntities();


        string connectionString = @"Data source = DESKTOP-QDK2G05\SQLEXPRESS ; Initial Catalog = SQL_TriThucOnline_BanSach ; integrated Security = True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dttbTHELOAI = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from THELOAI", sqlCon);
                sqlDa.Fill(dttbTHELOAI);
            }
            return View(dttbTHELOAI);
        }

        // GET: NXB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) // không có ID 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THELOAI theloai = db.THELOAIs.Find(id);
            if (theloai == null) // có ID nhưng không tìm thấy thông tin về id đó
            {
                return HttpNotFound();
            }
            else
            {
                DataTable dttbTHELOAI = new DataTable();
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {

                    sqlCon.Open();
                    string SQL = "DECLARE @id INT; set @id = " + id + "; select * from DAUSACH" +
                        " inner join THELOAI on DAUSACH.MaTL = THELOAI.MaTL where THELOAI.MaTL = @id";
                    SqlDataAdapter sqlDa = new SqlDataAdapter(SQL, sqlCon); /**/
                    sqlDa.Fill(dttbTHELOAI);
                }
                return View(dttbTHELOAI);
            }
        }

        // GET: NXB/Create
        public ActionResult Create()
        {
            return View(new THELOAI());
        }

        // POST: NXB/Create
        [HttpPost]
        public ActionResult Create(THELOAI theloai)
        {

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "insert into THELOAI OUTPUT inserted.MaTL values(@TenTL)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@MaTL", theloai.MaTL);
                sqlCmd.Parameters.AddWithValue("@TenTL", theloai.TenTL);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: NXB/Edit/5
        public ActionResult Edit(int id)
        {
            THELOAI theloai = new THELOAI();
            DataTable dttbTHELOAI = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "select * from THELOAI where MaTL = @MaTL";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@MaTL", id);
                sqlDa.Fill(dttbTHELOAI);
            }
            if (dttbTHELOAI.Rows.Count == 1)
            {
                theloai.MaTL = Convert.ToInt32(dttbTHELOAI.Rows[0][0].ToString());
                theloai.TenTL = dttbTHELOAI.Rows[0][1].ToString();
                return View(theloai);
            }
            else return RedirectToAction("Index");

        }

        // POST: NXB/Edit/5
        [HttpPost]
        public ActionResult Edit(THELOAI theloai)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE THELOAI SET TenTL =@TenTL where MaTL = @MaTL";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@MaTL", theloai.MaTL);
                sqlCmd.Parameters.AddWithValue("@TenTL", theloai.TenTL);
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
                string query = "DELETE  from THELOAI  where MaTL = @MaTL";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@MaTL", id);

                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // POST: NXB/Delete/5


    }
}
