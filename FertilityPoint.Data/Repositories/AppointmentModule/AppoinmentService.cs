using AutoMapper;
using FertilityPoint.DTO.AppointmentModule;
using FertilityPoint.Data.Modules;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FertilityPoint.Data.Services.AppointmentModule
{
    public class AppoinmentService : IAppoinmentService
    {
        private readonly ApplicationDbContext context;

        private readonly IMapper mapper;
        public AppoinmentService(IMapper mapper,ApplicationDbContext context)
        {
            this.context = context;

            this.mapper = mapper;
        }
    
        public async Task<AppointmentDTO> Create(AppointmentDTO appointmentDTO)
        {
            try
            {              
                appointmentDTO.CreateDate = DateTime.Now;

                var appointmnet = mapper.Map<Appointment>(appointmentDTO);

                context.Appointments.Add(appointmnet);

                await context.SaveChangesAsync();

                return appointmentDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<List<AppointmentDTO>> GetAll()
        {
            try
            {
                var data = await context.Appointments.ToListAsync();

                var appointmnets = mapper.Map<List<Appointment>, List<AppointmentDTO>>(data);

                return appointmnets;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }
    }
}
