using FertilityPoint.BLL.Repositories.AppointmentModule;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FertilityPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentRepository appointmentRepository;

        public AppointmentsController(IAppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;
        }
        public async Task<IActionResult> Index()
        {
            var appointments = (await appointmentRepository.GetAll()).OrderBy(x => x.FullName);

            return View(appointments);
        }

        public async Task<IActionResult> ApproveAppoinment()
        {
            var appointments = (await appointmentRepository.GetAll()).OrderBy(x => x.FullName);

            return View(appointments);
        }



    }
}
