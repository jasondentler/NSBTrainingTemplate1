using System;
using System.Web.Mvc;
using Microsoft.Web.Mvc;

namespace Hospital.Web.Controllers
{
    public class PatientController : Controller
    {
        private readonly IHospitalReadService _hospitalReadService;
        private readonly IHospitalWriteService _hospitalWriteService;
        
        public PatientController(IHospitalReadService hospitalReadService, IHospitalWriteService hospitalWriteService)
        {
            _hospitalReadService = hospitalReadService;
            _hospitalWriteService = hospitalWriteService;
        }

        [HttpGet]
        public ViewResult Index()
        {
            var model = _hospitalReadService.GetAllAdmittedPatients();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var model = _hospitalReadService.GetPatientDetails(id);
            
            if (model == null)
                return HttpNotFound();

            return View(model);
        }

        [HttpGet]
        public ViewResult WaitingForBeds()
        {
            var model = _hospitalReadService.GetWaitingForBeds();
            return View(model);
        }

        [HttpGet, AjaxOnly]
        public JsonResult GetAvailableBeds()
        {
            var model = _hospitalReadService.GetAvailableBeds();
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost]
        public RedirectToRouteResult Admit(Guid id)
        {
            _hospitalWriteService.AdmitPatient(id, DateTimeOffset.UtcNow);
            return this.RedirectToAction(c => c.Edit(id));
        }

        [HttpPost]
        public RedirectToRouteResult AssignBed(PatientDetails model)
        {
            if (model.BedAssignment.HasValue)
                _hospitalWriteService.AssignBed(model.PatientId, model.BedAssignment.Value);
            return this.RedirectToAction(c => c.Edit(model.PatientId));
        }

        [HttpPost]
        public RedirectToRouteResult Discharge(Guid id)
        {
            _hospitalWriteService.DischargePatient(id, DateTimeOffset.UtcNow);
            return this.RedirectToAction(c => c.Edit(id));
        }


    }
}
