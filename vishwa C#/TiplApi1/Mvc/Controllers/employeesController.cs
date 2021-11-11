using Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Mvc.Controllers
{
    public class employeesController : Controller
    {
        // GET: employees
        public ActionResult Index()
        {
            IEnumerable<mvcemployeemodel> empList;
            HttpResponseMessage response = GlobalVariables.webapiclient.GetAsync("employees").Result;
            empList = response.Content.ReadAsAsync<IEnumerable<mvcemployeemodel>>().Result;
            return View(empList);
        }
        public ActionResult AddOrEdit(int id = 0)
        {
            if(id==0)
            return View(new mvcemployeemodel());
            else
            {
                HttpResponseMessage response = GlobalVariables.webapiclient.GetAsync("employees/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcemployeemodel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(mvcemployeemodel emp)
        {
            if (emp.e_id == 0)
            {
                HttpResponseMessage response = GlobalVariables.webapiclient.PostAsJsonAsync("employees", emp).Result;
                TempData["SuccessMessage"] = "saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.webapiclient.PutAsJsonAsync("employees/"+emp.e_id,emp).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.webapiclient.DeleteAsync("employees/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted data successfully...";

            return RedirectToAction("Index");
        }
    }
}