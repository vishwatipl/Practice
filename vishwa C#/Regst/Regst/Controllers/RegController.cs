using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Regst.Models;

namespace Regst.Controllers
{
    public class RegController : Controller
    {
        public ActionResult Index()
        {
            using (dbmodel db = new dbmodel())

            {
                return View(db.Regs.ToList());
            }
        }
        // GET: Reg/Details/5
        public ActionResult Details(int id)
        {
            using (dbmodel db = new dbmodel())
            {
                return View(db.Regs.Where(x => x.Firstname ==firstname).FirstOrDefault());
            }
        }

        // GET: Reg/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Reg/Create
        [HttpPost]
        public ActionResult Create(Reg res)
        {
            try
            {
                using(dbmodel db = new dbmodel())
                {
                    db.Regs.Add(res);
                    db.SaveChanges();

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Reg/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Reg/Edit/5
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

        // GET: Reg/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Reg/Delete/5
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
