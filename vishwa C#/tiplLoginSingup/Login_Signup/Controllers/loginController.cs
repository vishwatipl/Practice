using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Login_Signup.Models;

namespace Login_Signup.Controllers
{
    public class loginController : Controller
    {
        string connection = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        [HttpGet]
        public ActionResult Registarion()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connection))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM LogAndReg", sqlCon);
                sqlDa.Fill(dt);
            }
            return View(dt);
        }

            // GET: login/Details/5
            public ActionResult Details(int id)
        {
            return View();
        }

        // GET: login/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new registraion());
        }

        // POST: login/Create
        [HttpPost]
        public ActionResult CreateRegt(registraion regs)
        {
            using (SqlConnection sqlcon = new SqlConnection(connection))
            {
                sqlcon.Open();
                string query = "INSERT INTO Reg VALUES(@Firstname,@Lastname,@Mobile,@Email,@Password,@Cpassword,@Profile,@IsEmailVerified,@Otp)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@Firstname", regs.Firstname);
                sqlCmd.Parameters.AddWithValue("@Lastname", regs.Lastname);
                sqlCmd.Parameters.AddWithValue("@Mobile", regs.Mobile);
                sqlCmd.Parameters.AddWithValue("@Email", regs.Email);
                sqlCmd.Parameters.AddWithValue("@Password", regs.Password);
                sqlCmd.Parameters.AddWithValue("@Cpassword", regs.Cpassword);
                sqlCmd.Parameters.AddWithValue("@Profile", regs.Profile);
                sqlCmd.Parameters.AddWithValue("@IsEmailVerified", regs.IsEmailVerified);
                sqlCmd.Parameters.AddWithValue("@Otp", regs.Otp);
                sqlCmd.ExecuteNonQuery();
               
            }
            return RedirectToAction("Registarion");
        }

        // GET: login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        

        // POST: login/Edit/5
        [HttpPost]
        public ActionResult CreateRegistration(registraion regs)
        {
            using (SqlConnection sqlcon = new SqlConnection(connection))
            {
                sqlcon.Open();
                string query = "INSERT INTO Reg VALUES(@Firstname,@Lastname,@Mobile,@Email,@Password,@Cpassword,@Profile,@IsEmailVerified,@Otp)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlcon);
                sqlCmd.Parameters.AddWithValue("@Firstname", regs.Firstname);
                sqlCmd.Parameters.AddWithValue("@Lastname", regs.Lastname);
                sqlCmd.Parameters.AddWithValue("@Mobile", regs.Mobile);
                sqlCmd.Parameters.AddWithValue("@Email", regs.Email);
                sqlCmd.Parameters.AddWithValue("@Password", regs.Password);
                sqlCmd.Parameters.AddWithValue("@Cpassword", regs.Cpassword);
                sqlCmd.Parameters.AddWithValue("@Profile", regs.Profile);
                sqlCmd.Parameters.AddWithValue("@IsEmailVerified", regs.IsEmailVerified);
                sqlCmd.Parameters.AddWithValue("@Otp", regs.Otp);
                sqlCmd.ExecuteNonQuery();

            }
            return RedirectToAction("Registarion");
        }
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
