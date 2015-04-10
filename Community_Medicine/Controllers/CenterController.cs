using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Community_Medicine.Data;
using Community_Medicine.Models;

namespace Community_Medicine.Controllers
{
    public class CenterController : Controller
    {
        private DistrictGateway dg = new DistrictGateway();

        private CenterGateway cg = new CenterGateway();

        private MedicineGateway mg = new MedicineGateway();

        private ThanaGateway tg = new ThanaGateway();

        private DoctorGateway docg = new DoctorGateway();


        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }



        //second version
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string name, int distid, int thanaid) {
            //[Bind(Include = "Name, DistrictId, ThanaId")] Center c

            distid++;

            int newthanaid = tg.GetThanaId(distid, thanaid);

            Center c = new Center() {
                Name = name,
                DistrictId = distid,
                ThanaId = newthanaid
            };


            if (ModelState.IsValid) {
                var ctrl = new AccountController();
                if (cg.Save(c)) {

                    ctrl.Register(new RegisterViewModel() {
                        UserName = c.Name,
                        Password = "password", //c.DistrictId.ToString()
                        ConfirmPassword = "password" //c.DistrictId.ToString()
                    });

                    

                    TempData["Username"] = name;
                    return RedirectToAction("Create", "Center");
                }


                return View(c);
            }
            return View();
        }
        
        
        //second version
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(string name, int distid, int thanaid)
        //{
        //    //[Bind(Include = "Name, DistrictId, ThanaId")] Center c

        //    Center c = new Center() {
        //        Name = name,
        //        DistrictId = distid,
        //        ThanaId = thanaid
        //    };


        //    if (ModelState.IsValid)
        //    {
        //        var ctrl = new AccountController();
        //        if (cg.Save(c))
        //        {

        //            ctrl.Register(new RegisterViewModel()
        //            {
        //                UserName = c.Name,
        //                Password = "password", //c.DistrictId.ToString()
        //                ConfirmPassword = "password" //c.DistrictId.ToString()
        //            });

        //            return RedirectToAction("index", "Center");
        //        }


        //        return View(c);
        //    }
        //    return View();
        //}

        [Authorize]
        public ActionResult Index()
        {

            return View();
        }


        [Authorize]
        public ActionResult SeeMedReport()
        {

            string user = User.Identity.Name;


            //getting the current logged in center
            Center currentCenter = cg.GetCenter(user);

            List<CenterMedicineLinker> currentLinkers = cg.GetCenterMedicineLinker(currentCenter.Id);

            MedAmountDictionary med = mg.GetMedicineAmount(currentLinkers);

            return View(med);
        }

        public JsonResult GetAllDistricts()
        {
            var allDists = dg.GetAll();

            return Json(allDists, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllThanas(int distId)
        {
            var thanas = tg.GetAllThanas(distId);

            return Json(thanas, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllCenters(int distid, int thanaid)
        {
            var centers = cg.GetCenters(distid, thanaid);
            return Json(centers, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Test(string name , int distid, int thanaid) {
            Center c = new Center() {
                Name = name,
                DistrictId = distid,
                ThanaId = thanaid
            };

            var ctrl = new AccountController();
            if (cg.Save(c)) {

                ctrl.Register(new RegisterViewModel() {
                    UserName = c.Name,
                    Password = "password", //c.DistrictId.ToString()
                    ConfirmPassword = "password" //c.DistrictId.ToString()
                });

                return Json(true, JsonRequestBehavior.AllowGet);
            }

            //cg.Save(c);


            return Json(true, JsonRequestBehavior.AllowGet);

        }


        [Authorize]
        [HttpGet]
        public ActionResult CreateDoctor()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDoctor([Bind(Include = "Name, Degree, Specialization")] Doctor d)
        {
            string user = User.Identity.Name;

            Center currentCenter = cg.GetCenter(user);

            d.CenterId = currentCenter.Id;

            if (docg.Save(d))
            {
                ViewBag.Message = "Successful";
            }

            return View();
        }


        [Authorize]
        public ActionResult GiveTreatMent()
        {
            return View();
        }


        public JsonResult GetVoterInfo(string voterid)
        {
            string loadString = "http://nerdcastlebd.com/web_service/voterdb/index.php/voters/voter/" + voterid;

            XDocument xdoc = XDocument.Load(loadString);

            Patient p = new Patient();


            var mainVoter = xdoc.Descendants("voter");

            foreach (XElement element in mainVoter)
            {
                var name = element.Descendants("name");
                var adderss = element.Descendants("address");
                var dateofbirth = element.Descendants("date_of_birth");

                

                foreach (var names in name)
                {
                    p.Name = (string)names;
                }

                foreach (var ad in adderss) {
                    p.Address = (string)ad;
                }

                foreach (var birth in dateofbirth) {
                    p.BirthDay = (string)birth;
                }
            }

            return Json(p, JsonRequestBehavior.AllowGet);
        }

    }

}