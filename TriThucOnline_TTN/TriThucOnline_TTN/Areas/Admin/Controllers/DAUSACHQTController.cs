using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Web.Mvc;
using TriThucOnline_TTN.Models;

namespace TriThucOnline_TTN.Areas.Admin.Controllers
{
    public class DAUSACHQTController : Controller
    {
        private SQL_TriThucOnline_BanSachEntities db = new SQL_TriThucOnline_BanSachEntities();

        string connectionString = @"Data source = DESKTOP-QDK2G05\SQLEXPRESS ; Initial Catalog = SQL_TriThucOnline_BanSach ; integrated Security = True";
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dttbDAUSACH = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from DAUSACH ", sqlCon); /*inner join THELOAI on DAUSACH.MaTL = THELOAI.MaTL where THELOAI.MaTL = 2*/
                sqlDa.Fill(dttbDAUSACH);
            }
            return View(dttbDAUSACH);
        }

        // GET: NXB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAUSACH dAUSACH = db.DAUSACHes.Find(id);
            if (dAUSACH == null)
            {
                return HttpNotFound();
            }
            return View(dAUSACH);
        }

        // GET: NXB/Create
        public ActionResult Create()
        {
            ViewBag.MaTG = new SelectList(db.TACGIAs, "MaTG", "TenTG");
            ViewBag.MaTL = new SelectList(db.THELOAIs, "MaTL", "TenTL");
            ViewBag.MaNXB = new SelectList(db.NXBs, "MaNXB", "TenNXB");

            return View(new DAUSACH());
        }

        // POST: NXB/Create
        [HttpPost]
        public ActionResult Create(DAUSACH dausach)
        {

            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "insert into DAUSACH OUTPUT inserted.Masach values(@Tensach,@MaNXB,@MaTL,@MaTG,@Namxuatban,@Price,@PicBook,@Sotrang,@Bocucsach,@Trichdan)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Masach", dausach.Masach);
                sqlCmd.Parameters.AddWithValue("@Tensach", dausach.Tensach);
                sqlCmd.Parameters.AddWithValue("@MaNXB", dausach.MaNXB);
                sqlCmd.Parameters.AddWithValue("@MaTL", dausach.MaTL);
                sqlCmd.Parameters.AddWithValue("@MaTG", dausach.MaTG);
                sqlCmd.Parameters.AddWithValue("@Namxuatban", dausach.Namxuatban);
                sqlCmd.Parameters.AddWithValue("@Price", dausach.Price);
                sqlCmd.Parameters.AddWithValue("@PicBook", dausach.PicBook);
                sqlCmd.Parameters.AddWithValue("@Sotrang", dausach.Sotrang);
                sqlCmd.Parameters.AddWithValue("@Bocucsach", dausach.Bocucsach);
                sqlCmd.Parameters.AddWithValue("@Trichdan", dausach.Trichdan);
                sqlCmd.ExecuteNonQuery();
            }


            return RedirectToAction("Index");
        }

        // GET: NXB/Edit/5
        public ActionResult Edit(int id)
        {
            DAUSACH dausach = new DAUSACH();
            DataTable dttbDAUSACH = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "select * from DAUSACH where Masach = @Masach";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@Masach", id);
                sqlDa.Fill(dttbDAUSACH);
            }
            if (dttbDAUSACH.Rows.Count == 1)
            {
                dausach.Masach = Convert.ToInt32(dttbDAUSACH.Rows[0][0].ToString());
                dausach.Tensach = dttbDAUSACH.Rows[0][1].ToString();
                dausach.MaNXB = Convert.ToInt32(dttbDAUSACH.Rows[0][2].ToString());
                dausach.MaTL = Convert.ToInt32(dttbDAUSACH.Rows[0][3].ToString());
                dausach.MaTG = Convert.ToInt32(dttbDAUSACH.Rows[0][4].ToString());
                dausach.Namxuatban = Convert.ToInt32(dttbDAUSACH.Rows[0][5].ToString());
                dausach.Price = Convert.ToInt32(dttbDAUSACH.Rows[0][6].ToString());
                dausach.PicBook = dttbDAUSACH.Rows[0][7].ToString();
                dausach.Sotrang = Convert.ToInt32(dttbDAUSACH.Rows[0][8].ToString());
                dausach.Bocucsach = dttbDAUSACH.Rows[0][9].ToString();
                dausach.Trichdan = dttbDAUSACH.Rows[0][10].ToString();

                ViewBag.MaTG = new SelectList(db.TACGIAs, "MaTG", "TenTG");
                ViewBag.MaTL = new SelectList(db.THELOAIs, "MaTL", "TenTL");
                ViewBag.MaNXB = new SelectList(db.NXBs, "MaNXB", "TenNXB");

                return View(dausach);
            }


            else return RedirectToAction("Index");



        }

        // POST: NXB/Edit/5
        [HttpPost]
        public ActionResult Edit(DAUSACH dausach)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE DAUSACH set Tensach=@Tensach,MaNXB=@MaNXB,MaTL=@MaTL,MaTG=@MaTG,Namxuatban=@Namxuatban,Price=@Price," +
                    "PicBook=@PicBook,Sotrang=@Sotrang,Bocucsach=@Bocucsach,Trichdan=@Trichdan where Masach=@Masach";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Masach", dausach.Masach);
                sqlCmd.Parameters.AddWithValue("@Tensach", dausach.Tensach);
                sqlCmd.Parameters.AddWithValue("@MaNXB", dausach.MaNXB);
                sqlCmd.Parameters.AddWithValue("@MaTL", dausach.MaTL);
                sqlCmd.Parameters.AddWithValue("@MaTG", dausach.MaTG);
                sqlCmd.Parameters.AddWithValue("@Namxuatban", dausach.Namxuatban);
                sqlCmd.Parameters.AddWithValue("@Price", dausach.Price);
                sqlCmd.Parameters.AddWithValue("@PicBook", dausach.PicBook);
                sqlCmd.Parameters.AddWithValue("@Sotrang", dausach.Sotrang);
                sqlCmd.Parameters.AddWithValue("@Bocucsach", dausach.Bocucsach);
                sqlCmd.Parameters.AddWithValue("@Trichdan", dausach.Trichdan);
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
                string query = "DELETE  from DAUSACH where Masach = @Masach";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@Masach", id);

                sqlCmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }

        // POST: NXB/Delete/5
    }
}
