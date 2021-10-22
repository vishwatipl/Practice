using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log.Models;

namespace log.Controllers
{
    public class myloginController : Controller
    {
        // GET: mylogin
        public ActionResult Index()
        {
            using (Dbmodel db = new Dbmodel())
            {
                return View(db.mylogins.ToList());
            }
        }


        // GET: mylogin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: mylogin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: mylogin/Create
        [HttpPost]
        public ActionResult Create(mylogin lg)
        {
            try
            {
                using (Dbmodel db = new Dbmodel())
                {
                    db.mylogins.Add(lg);
                    db.SaveChanges();
                }
                    // TODO: Add insert logic here

                    return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: mylogin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: mylogin/Edit/5
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

        // GET: mylogin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: mylogin/Delete/5
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
