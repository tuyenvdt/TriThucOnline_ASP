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
    public class TACGIAQTController : Controller
    {
        private SQL_TriThucOnline_BanSachEntities db = new SQL_TriThucOnline_BanSachEntities();

        string connectionString = @"Data source = DESKTOP-QDK2G05\SQLEXPRESS ; Initial Catalog = SQL_TriThucOnline_BanSach ; integrated Security = True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dttbTACGIA = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from TACGIA", sqlCon);
                sqlDa.Fill(dttbTACGIA);
            }
            return View(dttbTACGIA);
        }

        // GET: NXB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) // không có ID 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TACGIA tacgia = db.TACGIAs.Find(id);
            if (tacgia == null) // có ID nhưng không tìm thấy thông tin về id đó
            {
                return HttpNotFound();
            }
            else
            {
                DataTable dttbTACGIA = new DataTable();
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {

                    sqlCon.Open();
                    string SQL = "DECLARE @id INT; set @id = " + id + "; select * from DAUSACH" +
                        " inner join TACGIA on DAUSACH.MaTG = TACGIA.MaTG where DAUSACH.MaTG = @id";
                    SqlDataAdapter sqlDa = new SqlDataAdapter(SQL, sqlCon); /**/
                    sqlDa.Fill(dttbTACGIA);
                }
                return View(dttbTACGIA);
            }
        }

        // GET: NXB/Create
        public ActionResult Create()
        {
            return View(new TACGIA());
        }

        // POST: NXB/Create
        [HttpPost]
        public ActionResult Create(TACGIA tacgia)
        {

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "insert into TACGIA OUTPUT inserted.MaTG values(@TenTG,@Thongtingioithieu,@PicTG)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@MaTG", tacgia.MaTG);
                sqlCmd.Parameters.AddWithValue("@TenTG", tacgia.TenTG);
                sqlCmd.Parameters.AddWithValue("@Thongtingioithieu", tacgia.Thongtingioithieu);
                sqlCmd.Parameters.AddWithValue("@PicTG", tacgia.PicTG);
                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // GET: NXB/Edit/5
        public ActionResult Edit(int id)
        {
            TACGIA tacgia = new TACGIA();
            DataTable dttbTACGIA = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "select * from TACGIA where MaTG = @MaTG";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@MaTG", id);
                sqlDa.Fill(dttbTACGIA);
            }
            if (dttbTACGIA.Rows.Count == 1)
            {
                tacgia.MaTG = Convert.ToInt32(dttbTACGIA.Rows[0][0].ToString());
                tacgia.TenTG = dttbTACGIA.Rows[0][1].ToString();
                tacgia.Thongtingioithieu = dttbTACGIA.Rows[0][2].ToString();
                tacgia.PicTG = dttbTACGIA.Rows[0][3].ToString();

                return View(tacgia);
            }
            else return RedirectToAction("Index");

        }

        // POST: NXB/Edit/5
        [HttpPost]
        public ActionResult Edit(TACGIA tacgia)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE TACGIA SET TenTG =@TenTG ,Thongtingioithieu = @Thongtingioithieu , PicTG = @PicTG where MaTG = @MaTG";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@MaTG", tacgia.MaTG);
                sqlCmd.Parameters.AddWithValue("@TenTG", tacgia.TenTG);
                sqlCmd.Parameters.AddWithValue("@Thongtingioithieu", tacgia.Thongtingioithieu);
                sqlCmd.Parameters.AddWithValue("@PicTG", tacgia.PicTG);
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
                string query = "DELETE  from TACGIA  where MaTG = @MaTG";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@MaTG", id);

                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // POST: NXB/Delete/5


    }
}
