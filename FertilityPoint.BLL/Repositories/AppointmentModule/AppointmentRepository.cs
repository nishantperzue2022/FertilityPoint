using AutoMapper;
using FertilityPoint.DAL.Modules;
using FertilityPoint.DTO.AppointmentModule;
using FertilityPoint.DTO.PatientModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FertilityPoint.BLL.Repositories.AppointmentModule
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext context;

        private readonly IMapper mapper;
        public AppointmentRepository(IMapper mapper, ApplicationDbContext context)
        {
            this.context = context;

            this.mapper = mapper;
        }
        public async Task<AppointmentDTO> Create(AppointmentDTO appointmentDTO)
        {
            try
            {
                var appointmentDate = appointmentDTO.AppointmentDate.ToShortTimeString();

                appointmentDTO.CreateDate = DateTime.Now;

                appointmentDTO.Id = Guid.NewGuid();

                appointmentDTO.AppointmentDate = Convert.ToDateTime(appointmentDate);

                var appointment = mapper.Map<Appointment>(appointmentDTO);

                context.Appointments.Add(appointment);

                await context.SaveChangesAsync();

                UpdateSlot(appointmentDTO);

                return appointmentDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
        public bool UpdateSlot(AppointmentDTO appointmentDTO)
        {
            var getSlot = context.TimeSlots.Find(appointmentDTO.TimeId);

            if (getSlot != null)
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    getSlot.IsBooked = 1;

                    getSlot.AppointmentDate = appointmentDTO.AppointmentDate;

                    transaction.Commit();

                    context.SaveChanges();
                }
                return true;
            }

            return false;
        }

        //public async Task<List<AppointmentDTO>> GetAll()
        //{
        //    try
        //    {
        //        var data = await context.Appointments.ToListAsync();

        //        var appointmnets = mapper.Map<List<Appointment>, List<AppointmentDTO>>(data);

        //        return appointmnets;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);

        //        return null;
        //    }

        //}

        public async Task<List<AppointmentDTO>> GetAll()
        {
            try
            {
                var appointments = (from appointment in context.Appointments

                                    join patient in context.Patients on appointment.PatientId equals patient.Id

                                    join timslot in context.TimeSlots on appointment.TimeId equals timslot.Id

                                    select new AppointmentDTO()
                                    {
                                        Id = appointment.Id,

                                        Status = appointment.Status,

                                        CreateDate = appointment.CreateDate,

                                        AppointmentDate = appointment.AppointmentDate,

                                        FirstName = patient.FirstName,

                                        LastName = patient.LastName,

                                        TimeId = appointment.TimeId,

                                        TimeSlot = timslot.FromTime.ToString("h:mm tt") + " - " + timslot.FromTime.ToString("h:mm tt"),

                                        ToTime = timslot.ToTime,

                                    }).ToListAsync();


                return await appointments;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }

        }



        public AppointmentDTO GetTransaction(Guid Id)
        {
            try
            {
                var appointments = (from appointment in context.Appointments

                                    join patient in context.Patients on appointment.PatientId equals patient.Id

                                    join timslot in context.TimeSlots on appointment.TimeId equals timslot.Id

                                    join payment in context.MpesaPayments on appointment.TransactionNumber equals payment.TransactionNumber

                                    where appointment.Id == Id

                                    select new AppointmentDTO()
                                    {
                                        Id = appointment.Id,

                                        Status = appointment.Status,

                                        CreateDate = appointment.CreateDate,

                                        AppointmentDate = appointment.AppointmentDate,

                                        FirstName = patient.FirstName,

                                        LastName = patient.LastName,

                                        PhoneNumber = patient.PhoneNumber,

                                        PaidByNumber = payment.PhoneNumber,

                                        TransactionNumber = payment.TransactionNumber,

                                        TransactionDate = payment.TransactionDate,

                                        Amount = payment.Amount,

                                        Email = patient.Email,

                                        TimeId = appointment.TimeId,

                                        TimeSlot = timslot.FromTime.ToString("h:mm tt") + " - " + timslot.FromTime.ToString("h:mm tt"),

                                        ToTime = timslot.ToTime,

                                    }).FirstOrDefault();

                return appointments;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
