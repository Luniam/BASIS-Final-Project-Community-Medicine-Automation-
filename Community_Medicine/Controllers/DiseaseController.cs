using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Community_Medicine.Data;
using Community_Medicine.Models;

namespace Community_Medicine.Controllers
{
    public class DiseaseController : Controller
    {
        private DiseaseGateway dGate = new DiseaseGateway();


        public ActionResult Create() {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Description, Treatment, Drugs")] Disease d)
        {

            if (ModelState.IsValid) {
                if (dGate.Save(d))
                    return RedirectToAction("index", "Home");
                return View(d);
            }

            return View(d);
        }
	}
}