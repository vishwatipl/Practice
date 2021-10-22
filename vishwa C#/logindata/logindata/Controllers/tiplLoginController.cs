using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using logindata.Models;

namespace logindata.Controllers
{
    public class tiplLoginController : Controller
    {
        // GET: tiplLogin/Index
        public ActionResult Index()
        {
          

                return View();
        }

        // GET: tiplLogin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: tiplLogin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tiplLogin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: tiplLogin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: tiplLogin/Edit/5
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

        // GET: tiplLogin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: tiplLogin/Delete/5
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
