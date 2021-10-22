using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using newform.Models;
using PagedList;


namespace newform.Controllers
{
    public class myloginController : Controller
    {
        // GET: mylogin
        dhmodel dh = new dhmodel();


        public ActionResult Index(int ? page)
        {
            var pageNumber = page ?? 1;
            var pagesize = 2;
            var mylogin = dh.mylogins.OrderBy(x => x.mobile).ToPagedList(pageNumber, pagesize);
            
            {
                return View(mylogin);
                   
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
        public ActionResult Create(mylogin col)
        {
            try
            {
                using (dhmodel dh = new dhmodel())
                {
                    dh.mylogins.Add(col);
                    dh.SaveChanges();
                        

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
