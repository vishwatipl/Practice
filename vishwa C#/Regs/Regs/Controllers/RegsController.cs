﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Regs.Models;

namespace Regs.Controllers
{
    public class RegsController : Controller
    {
        // GET: Regs
        public ActionResult Index()
        {
            return View();
        }

        // GET: Regs/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Regs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Regs/Create
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

        // GET: Regs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Regs/Edit/5
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

        // GET: Regs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Regs/Delete/5
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
