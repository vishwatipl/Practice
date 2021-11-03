using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Web.Security;
using tiplLogin.Models.login;
using System.Web.Services.Description;
namespace tiplLogin.Controllers
{
    public class UserController : Controller
    {
        string Connection = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        public string Message { get; private set; }
        public bool Status { get; private set; }
        public object ActionRequestBehavior { get; private set; }

        [HttpGet]
        // GET: User
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sqlcon = new SqlConnection(Connection))
            {
                sqlcon.Open();
                SqlDataAdapter sda = new SqlDataAdapter("select * from logandreg", sqlcon);
                sda.Fill(dt);
            }
            return View(dt);
        }
        private string GenerateOtp()
        {
            int min = 100000;
            int max = 999900;
            int otp = 0;
            Random rdm = new Random();
            otp = rdm.Next(min, max);
            return otp.ToString();
            // return "123456";
        }
        [NonAction]
        [ValidateAntiForgeryToken]
        public void SendVerificationLinkEmail(String EmailID, int otp)
        {
            //Random rdm = new Random();
            /*string otp = Convert.ToInt32(rdm.Next(100000, 999900).ToString());*/
            var verifyUrl =" / User / " + "VerifyAccount" + " / ";
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var formEmail = new MailAddress("tathyainfotechtest@gmail.com", "WebPage");
            var toEmail = new MailAddress(EmailID);
            var fromEmailPassword = "Tipl@12345!";

            string subject = " ";
            string body = " ";
            subject = "Your Account is successfully Created.";
            body = "<br/><br/>We are excited to tell you that your online Shop Acccount is " +
                 "Successfully created . Please click on the below link Verify your account" +
                 "<br/><br/><a href='" + otp + "'>" + otp + "</a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(formEmail.Address, fromEmailPassword)
            };
            using (var message = new MailMessage(formEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
        // GET: User/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new regismodel());
        }
        // POST: User/Create
        [HttpPost]
        public ActionResult Create(regismodel regs)
        {
            {
                using (SqlConnection sqlcon = new SqlConnection(Connection))
                {
                    Random rdm = new Random();
                    int otp = Convert.ToInt32(rdm.Next(100000, 999900).ToString());
                    sqlcon.Open();
                    string query = "insert into logandreg ([Firstname],[Lastname],[Mobile],[Email],[Gender],[Password],[Cpassword],[Profile],[Otp]) VALUES(@Firstname,@Lastname,@Mobile,@Email,@Gender,@Password,@cPassword,@Profile,@Otp)";
                    SqlCommand sqcmd = new SqlCommand(query, sqlcon);
                    sqcmd.Parameters.AddWithValue("@Firstname", regs.Firstname);
                    sqcmd.Parameters.AddWithValue("@Lastname", regs.Lastname);
                    sqcmd.Parameters.AddWithValue("@Mobile", regs.Mobile);
                    sqcmd.Parameters.AddWithValue("@Email", regs.Email);
                    sqcmd.Parameters.AddWithValue("@Gender", regs.Gender);
                    sqcmd.Parameters.AddWithValue("@Password", regs.Password);
                    sqcmd.Parameters.AddWithValue("@Cpassword", regs.Cpassword);
                    sqcmd.Parameters.AddWithValue("@Profile", regs.Profile);
                    sqcmd.Parameters.AddWithValue("@Otp",otp);
                    Session["email"] = regs.Email.ToString();
                    sqcmd.ExecuteNonQuery();
                    SendVerificationLinkEmail(regs.Email,otp);
                    Message = "registation Successfully Done . Account activation link " +
                    "has been sent to your email id:" + regs.Email;
                    Status = true;
                }
                return RedirectToAction("Opt");
            }
        }
        //public bool IsEmailExit(string Email)
        //{
        //    SqlConnection sqlcon = new SqlConnection(Connection);
        //    SqlCommand cmd = new SqlCommand("select Email from  logandreg where email='" + Email + "'", sqlcon);

        //    sqlcon.Open();
        //    SqlDataReader sdr = cmd.ExecuteReader();
        //    if (sdr.Read())
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
        //[NonAction]
        //public bool IsEmailExist(string Email)
        //{
        //    SqlConnection sqlcon = new SqlConnection(Connection);
        //    SqlCommand cmd = new SqlCommand("select Email from  logandreg where email='" + Email + "'", sqlcon);

        //    sqlcon.Open();


        //    SqlDataReader sdr = cmd.ExecuteReader();

        //    if (sdr.Read())
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
        public ActionResult  Logout()
        {
            return View();
        }
        
        [NonAction]
        [ValidateAntiForgeryToken]
        public void UpdateOtp(string email)
        {
            using (SqlConnection sqlcon = new SqlConnection(Connection))
            {

                //sqlcon.Open();
                //string query = "update logandreg SET Otp=@Otp where email=@Email";
                //SqlCommand sqcmd = new SqlCommand(query, sqlcon);
                //sqcmd.Parameters.AddWithValue("@Otp", "1234");
                //sqcmd.ExecuteNonQuery();
            }
        }
        public int Update(string strQuery)
        {
            SqlConnection sqlcon = new SqlConnection(Connection);
            try
            {
                if (ConnectionState.Closed == sqlcon.State)
                {
                    sqlcon.Open();
                }
                using (SqlCommand cmd = new SqlCommand(strQuery, sqlcon))
                {
                    cmd.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
            finally
            {
                sqlcon.Close();
            }
        }
        [HttpGet]
        public ActionResult Opt()
        {
            return View();
        }
        private bool IsValidUser(int otp)
        {
            var mail = Session["email"];
            bool IsValid = false;
            SqlConnection sqlcon = new SqlConnection(Connection);
            string query = "select Otp from logandreg where Email='" + mail + "'";
            //string query = "select Otp=@otp from logandreg where Email=@mail";
            DataTable dt = new DataTable();
            if (ConnectionState.Closed == sqlcon.State)
            {
                sqlcon.Open();
            }
            using (SqlCommand cmd = new SqlCommand(query, sqlcon))
            {
                var adapter = new SqlDataAdapter(cmd);
                adapter.Fill(dt);
                
            }
            //Error = null;
            //return dt;
            if (dt.Rows.Count > 0)
            {
                int dbotp = Convert.ToInt32(dt.Rows[0]["Otp"]);
                if (otp == dbotp)
                {
                IsValid = true;
                }
            }
            return IsValid;
        }
        [HttpGet]
        public ActionResult loginUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        private bool IsLoginUser(string Email, string Password)
        {
            // bool active = true;
            SqlConnection sqlcon = new SqlConnection(Connection);
            bool IsValid = false;
            string query = "select * from logandreg where Email=@email and Password=@Password and IsEmailVerified=1";

            using (SqlCommand cmd = new SqlCommand(query, sqlcon))
            {
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", Password);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                sqlcon.Open();
                int i = cmd.ExecuteNonQuery();
                sqlcon.Close();
                if (dt.Rows.Count > 0)
                {
                    IsValid = true;
                }
            }
            return IsValid;
        }

        public int GenerateOTP()
        {
            Random rdm = new Random();
            string otp = rdm.Next(100000, 999900).ToString();
            return Convert.ToInt32(otp);
        }
        public void ResetOtp()
            {
            string _Email = Session["email"].ToString();
            int _Otp = GenerateOTP();
            // OTP Updattion Start here


            string strOtpUpdateQuery ="Update logandreg set Otp =" + _Otp + " where Email='" + _Email + "'";

            int _queryResponse = Update(strOtpUpdateQuery);
            if (_queryResponse == 1) 
            {
                SendVerificationLinkEmail(_Email, _Otp);
            }
            
            // OTP Updation End
        }
        private string GetOtp()
        {
            return GenerateOtp();
        }
        [HttpPost]
        public ActionResult Opt(regismodel model, string text)
        {
            if (model.Otp == 0)
            {
                ResetOtp();
                return View();
            }
            //else*/
            //{
            if (IsValidUser(model.Otp))
            {
                return RedirectToAction("matchotp");
            }
            //}
            return View();
            // return RedirectToAction("welcome", "User", new { Otp = model.Otp });
        }
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(regismodel log)
        {
            if (IsValidUser(log.Otp))
            {
                FormsAuthentication.SetAuthCookie(log.Email, false);
                return RedirectToAction("Create");
            }
            else
            {
                ModelState.AddModelError("", "Your Email and password is incorrect");
            }
            return View();
        }

        public ActionResult matchotp()
        {
            return View();
        }
        public ActionResult otppass(string text)
        {
            if (text == "presult")
            {
                ResetOtp();
                return View();
            }
            //if (id == 1)
            //{
            //    resendOtp();
            //}
            //else
            //{
            //}
            return View(new regismodel());
        }
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpGet]
            // GET: User/Edit/5
            public ActionResult Edit(int id)
            {
                return View();
            }
        // POST: User/Edit/5
        [HttpPost]
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
            // GET: User/Delete/5
            public ActionResult Delete(int id)
            {
                return View();
            }

            // POST: User/Delete/5
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


