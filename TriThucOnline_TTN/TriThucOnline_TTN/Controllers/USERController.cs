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

    public class USERController : Controller
    {
        string connectionString = @"Data source = DESKTOP-QDK2G05\SQLEXPRESS ; Initial Catalog = SQL_TriThucOnline_BanSach ; integrated Security = True";


        // GET: USER
        public ActionResult Index()
        {
            DataTable dttbKHACHHANG = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("select * from KHACHHANG", sqlCon);
                sqlDa.Fill(dttbKHACHHANG);
            }
            return View(dttbKHACHHANG);
        }

        // GET: USER/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: USER/Create
        public ActionResult Create()
        {
            return View(new KHACHHANG());
        }

        // POST: USER/Create
        [HttpPost]
        public ActionResult Create(KHACHHANG khachhang)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "insert into KHACHHANG output inserted.IDUser values(@UserName,@Diachi,@SDT,@Email,@Password,@PicUser, @UserType)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@IDUser", khachhang.IDUser);
                sqlCmd.Parameters.AddWithValue("@Username", khachhang.Username);
                sqlCmd.Parameters.AddWithValue("@Diachi", khachhang.Diachi);
                sqlCmd.Parameters.AddWithValue("@SDT", khachhang.SDT);
                sqlCmd.Parameters.AddWithValue("@Email", khachhang.Email);
                sqlCmd.Parameters.AddWithValue("@Password", khachhang.Password);
                sqlCmd.Parameters.AddWithValue("@PicUser", khachhang.PicUser);
                sqlCmd.Parameters.AddWithValue("@UserType", khachhang.UserType);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: USER/Edit/5
        public ActionResult Edit(int id)
        {
            KHACHHANG khachhang = new KHACHHANG();
            DataTable dttbKHACHHANG = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "select * from KHACHHANG  where IDUser = @IDUser";
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@IDUser", id);
                sqlDa.Fill(dttbKHACHHANG);


            }
            if (dttbKHACHHANG.Rows.Count == 1)
            {
                khachhang.IDUser = Convert.ToInt32(dttbKHACHHANG.Rows[0][0].ToString());
                khachhang.Username = dttbKHACHHANG.Rows[0][1].ToString();
                khachhang.Diachi = dttbKHACHHANG.Rows[0][2].ToString();
                khachhang.SDT = Convert.ToInt32(dttbKHACHHANG.Rows[0][3].ToString());
                khachhang.Email = dttbKHACHHANG.Rows[0][4].ToString();
                khachhang.Password = dttbKHACHHANG.Rows[0][5].ToString();
                khachhang.PicUser = dttbKHACHHANG.Rows[0][6].ToString();

                return View(khachhang);
            }
            else return RedirectToAction("Index");
        }

        // POST: USER/Edit/5
        [HttpPost]
        public ActionResult Edit(KHACHHANG khachhang)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "UPDATE KHACHHANG set Username = @Username,Diachi = @Diachi,SDT=@SDT,Email=@Email,Password=@Password,PicUser=@PicUser where IDUser =@IDUser";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@IDUser", khachhang.IDUser);
                sqlCmd.Parameters.AddWithValue("@Username", khachhang.Username);
                sqlCmd.Parameters.AddWithValue("@Diachi", khachhang.Diachi);
                sqlCmd.Parameters.AddWithValue("@SDT", khachhang.SDT);
                sqlCmd.Parameters.AddWithValue("@Email", khachhang.Email);
                sqlCmd.Parameters.AddWithValue("@Password", khachhang.Password);
                sqlCmd.Parameters.AddWithValue("@PicUser", khachhang.PicUser);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // GET: USER/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "DELETE from KHACHHANG where IDUser = @IDUser";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@IDUser", id);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // POST: USER/Delete/5


    }
}
