using FertilityPoint.BLL.Repositories.ApplicationUserModule;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FertilityPoint.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DoctorsController : Controller
    {
        private readonly IApplicationUserRepository applicationUserRepository;

        public DoctorsController(IApplicationUserRepository applicationUserRepository)
        {
            this.applicationUserRepository = applicationUserRepository;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var doctors = (await applicationUserRepository.GetAllUsers()).Where(x => x.RoleName == "Doctor");

                return View(doctors);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
