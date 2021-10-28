using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using webpage.Models;
using System.Data.Entity.Validation;

namespace webpage.Controllers
{
    public class webController : Controller
    {
        // GET: web
        [HttpGet]
        public ActionResult web()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult web([Bind(Exclude = "IsEmailVerified,ActivationCode")] User user)
        {
            bool Status = false;
            string Message = "";

            if (ModelState.IsValid)
            {
                var IsExit = IsEmailExit(user.EmailID);
                if (IsExit)
                {
                    ModelState.AddModelError("EmailExit", "Email Already Exits ");
                    return View(User);
                }
                #region Generate Activation Code
                user.ActivationCode = Guid.NewGuid();
                #endregion

                #region Password Hasing
                user.Password = Crypto.Hash(user.Password);

                #endregion
                user.IsEmailVerified = false;

                #region Save to Database
                using (dbmodel db = new dbmodel())
                {
                    db.Users.Add(user);
                    db.SaveChanges();

                    SendVerificationLinkEmail(user.EmailID, user.ActivationCode.ToString());
                    Message = "Webpage successfully Done. account activation link" +
                        "has been send to email id:" + user.EmailID;
                    Status = true;
                }
                #endregion
            }
            else
            {

                Message = "Invalid Request";
            }
            ViewBag.Message = Message;
            ViewBag.Status = Status;

            return View();
        }

        [HttpGet]
        public ActionResult VerifyAccount(String id)
        {
            bool Status = false;
            using (dbmodel db = new dbmodel())
            {
                db.Configuration.ValidateOnSaveEnabled = false;

                var v = db.Users.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if (v != null)
                {
                    v.IsEmailVerified = true;
                    db.SaveChanges();
                    Status = true;

                }
                else
                {
                    ViewBag.Message = "Invalid Request";

                }
            }
            ViewBag.Status = Status;
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login, string ReturnUrl)
        {
            string message = "";
            using (dbmodel db = new dbmodel())
            {
                var v = db.Users.Where(a => a.EmailID == login.EmailID).FirstOrDefault();
                if (v != null)
                {
                    if (string.Compare(Crypto.Hash(login.Password), v.Password) == 0)
                    {
                        int timeout = login.Rememberme ? 525600 : 20;
                        var ticket = new FormsAuthenticationTicket(login.EmailID, login.Rememberme, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);

                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    { 
                        message = "Invalid creeatial pro";
                    }
                }
                else
                {
                    message = "Invalid creeatial pro";
                }
            }
            ViewBag.Message = message;
            return View();
        }
        //Logout
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "web");
        }

        [NonAction]
        public bool IsEmailExit(string EmailID)
        {
            using (dbmodel db = new dbmodel())
            {
                var v = db.Users.Where(a => a.EmailID == EmailID).FirstOrDefault();
                return v != null;

            }

        }
        [NonAction]
        public void SendVerificationLinkEmail(String EmailID, String ActivationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/web/ResetPassword/" + ActivationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var formEmail = new MailAddress("tathyainfotechtest@gmail.com", "WebPage");
            var toEmail = new MailAddress(EmailID);
            var fromEmailPassword = "Tipl@12345!";

            string subject = " ";
            string body = " ";
            if (emailFor == "VerifyAccount")
            {
                subject = "Your account is success created";
                body = "<br/><br/> we are created" + "successfully create webpage" + "<br/><br/><a href ='" + link + "'>" + link + "</a>";

            }
            else if (emailFor == "ResetPassword")
            {
                subject = "Resert password";
                body = "hi,<br/><br/>we got request for reset you account password.please click on the below link to reset your password" +
                    "<br/><br/><a href =" + link + ">Reset Password</a>";
            }
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
        //Forgotpassword
        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(String EmailId)
        {
            
                string Message = "";
                bool status = false;

                using (dbmodel db = new dbmodel())
                {
                    var account = db.Users.Where(a => a.EmailID == EmailId).FirstOrDefault();
                    if (account != null)
                    {
                        string ResetCode = Guid.NewGuid().ToString();
                        SendVerificationLinkEmail(account.EmailID, ResetCode, "ResetPassword");
                        account.ResetPasswordCode = ResetCode;
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.SaveChanges();
                        Message = "Reset Password link has been send to your Emailid";
                    }

                    else
                    {
                        Message = "Account not Found";
                    }
                }
                ViewBag.Message = Message;
                return View();
            
            
        }
        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }
        public ActionResult ResetPassword(String id)
        {

                using (dbmodel db = new dbmodel())
                {

                    var user = db.Users.Where(a => a.ResetPasswordCode == id).FirstOrDefault();
                    if (user != null)
                    {
                        ResetPasswordModel model = new ResetPasswordModel();
                        model.ResetCode = id;
                        return View(model);
                    }
                    else
                    {
                        return HttpNotFound();
                    }

                }
          }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            
                var message = "";
                if (ModelState.IsValid)
                {
                    using (dbmodel db = new dbmodel())
                    {
                        var user = db.Users.Where(a => a.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                        if (user != null)
                        {
                            user.Password = Crypto.Hash(model.NewPassword);
                            user.ResetPasswordCode = "";
                            db.Configuration.ValidateOnSaveEnabled = false;
                            db.SaveChanges();
                            message = "New password updated successfully";
                        }
                    }
                }
               else
               {
                    message = "somthing invalid";
               }
                ViewBag.Message = message;
                return View(model);
        }
            
    }
}