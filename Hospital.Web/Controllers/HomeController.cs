using System;
using System.Web.Mvc;

namespace Hospital.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHospitalReadService _readService;

        public HomeController(IHospitalReadService readService)
        {
            _readService = readService;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult Stats()
        {
            var model = _readService.GetHospitalStats();
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult Commands()
        {
            return PartialView();
        }

    }
}
