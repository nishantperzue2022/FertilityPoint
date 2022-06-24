using FertilityPoint.BLL.Repositories.AppointmentModule;
using FertilityPoint.DAL.Modules;
using FertilityPoint.DTO.AppointmentModule;
using FertilityPoint.Services.EmailModule;
using FertilityPoint.Services.SMSModule;
using Microsoft.AspNetCore.Identity;
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

        private readonly IMessagingService messagingService;

        private readonly IMailService mailService;

        private readonly UserManager<AppUser> userManager;

        public AppointmentsController(IMailService mailService, UserManager<AppUser> userManager, IMessagingService messagingService, IAppointmentRepository appointmentRepository)
        {
            this.appointmentRepository = appointmentRepository;

            this.messagingService = messagingService;

            this.userManager = userManager;

            this.mailService = mailService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var appointments = (await appointmentRepository.GetAll()).Where(x => x.Status == 0).OrderBy(x => x.CreateDate);

                return View(appointments);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                TempData["Error"] = "Something went wrong";

                return RedirectToAction("Login", "Account", new { area = "" });
            }
        }

        public async Task<IActionResult> ApproveAppoinment(Guid Id, string approvedBy)
        {
            try
            {
                var user = await userManager.FindByEmailAsync(User.Identity.Name);

                approvedBy = user.Id;

                var result = await appointmentRepository.ApproveAppointment(Id, approvedBy);

                if (result == true)
                {
                    var get_appointment = (await appointmentRepository.GetById(Id));

                    var appointment = new AppointmentDTO()
                    {
                        FirstName = get_appointment.FirstName,

                        PhoneNumber = get_appointment.PhoneNumber,

                        AppointmentDate = get_appointment.AppointmentDate,

                        Email = get_appointment.Email,

                        CreateDate = get_appointment.CreateDate,
                    };

                    var sendSMS = messagingService.ApprovalNotificationSMS(appointment);

                    var sendMail = mailService.AppointmentApprovalNotification(appointment);

                    return Json(new { success = true, responseText = "Appointment has been successfully approved" });
                }
                else
                {
                    return Json(new { success = false, responseText = "Appointment has not been  approved" });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }
    }
}
