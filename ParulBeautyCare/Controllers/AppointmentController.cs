using ParulBeautyCare.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParulBeautyCare.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Apoitment
        public ActionResult Index()
        {
            BookAppointmentViewModel em = new BookAppointmentViewModel();
            var list = new List<string>() { "09:00", "10:00", "11:30", "01:30","02:00" , "03:00" , "04:30"};
            ViewBag.list = list;
            return View(em);
        }
    
    }
}