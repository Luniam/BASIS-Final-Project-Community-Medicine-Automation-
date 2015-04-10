using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Community_Medicine.Data;
using Community_Medicine.Models;

namespace Community_Medicine.Controllers
{

    public class MedicineController : Controller
    {

        private MedicineGateway mGate = new MedicineGateway();
        private CenterGateway cGate = new CenterGateway();

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Dose")] Medicine m) {

            if (ModelState.IsValid)
            {
                if (mGate.Save(m))
                    return RedirectToAction("index", "Home");
                return View(m);
            }
            
            return View(m);
        }


        [HttpPost]
        public ActionResult SendMedicine([Bind(Include = "CenterId, MedicineId, Qty")] CenterMedicineLinker cml)
        {

            ViewBag.CenterId = new SelectList(cGate.GetAll(), "Id", "Name");
            ViewBag.MedicineId = new SelectList(mGate.GetAll(), "Id", "Name");

            if (ModelState.IsValid)
            {
                if (cGate.SaveCenterMedicineLinker(cml))
                {
                    ViewBag.Message = "Successful";
                    //return RedirectToAction("index", "Home");
                    return View();
                }

                return View();
            }

            return View();
        }


        [HttpGet]
        public ActionResult SendMedicine()
        {
            //ViewBag.CenterId = new SelectList(cGate.GetAll(), "Id", "Name");
            //ViewBag.MedicineId = new SelectList(mGate.GetAll(), "Id", "Name");
            return View();
        }

        public JsonResult GetAllMeds() {
            var allmeds = mGate.GetAll();

            return Json(allmeds, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SendMeds(int centerid, int medid, int qty)
        {
            CenterMedicineLinker cml = new CenterMedicineLinker()
            {
                CenterId = centerid,
                MedicineId = medid,
                Qty = qty
            };

            if (cGate.SaveCenterMedicineLinker(cml)) {
                
                
                return Json(true, JsonRequestBehavior.AllowGet);

            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

	}
}