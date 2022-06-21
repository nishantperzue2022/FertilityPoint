using FertilityPoint.DTO.AppointmentModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FertilityPoint.Data.Services.AppointmentModule
{
    public interface IAppoinmentService
    {
        Task<List<AppointmentDTO>> GetAll();
        Task<AppointmentDTO> Create(AppointmentDTO appointmentDTO);
    }
}