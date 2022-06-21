using AutoMapper;

using FertilityPoint.Data.Modules;
using FertilityPoint.DTO.AppointmentModule;
using FertilityPoint.DTO.CountyModule;
using FertilityPoint.DTO.SubCountyModule;

namespace FertilityPoint.Data.MapperProfiles
{
   public class MapperProfile :Profile
    {
        public MapperProfile()
        {
            CreateMap<County, CountyDTO>().ReverseMap();

            CreateMap<SubCounty, SubCountyDTO>().ReverseMap();

            CreateMap<Appointment, AppointmentDTO>().ReverseMap();
        }
    }
}
