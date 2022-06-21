using AutoMapper;

using FertilityPoint.Data.Modules;
using FertilityPoint.DTO.CountyModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FertilityPoint.Data.Services.CountyModule
{
    public class CountyService : ICountyService
    {
        private readonly ApplicationDbContext context;

        private readonly  IMapper mapper;

        public CountyService(IMapper mapper, ApplicationDbContext context)
        {
            this.context = context;

            this.mapper = mapper;
        }


        public async Task<CountyDTO> Create(CountyDTO countyDTO)
        {
            try
            {
                County county = mapper.Map<County>(countyDTO);

                context.Counties.Add(county);

                await context.SaveChangesAsync();

                return countyDTO;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                return null;
            }
        }

        public async Task<List<CountyDTO>> GetAll()
        {
            var user = await context.Counties.ToListAsync();

            var dest = mapper.Map<List<CountyDTO>>(user);

            return dest;


            //var records = await context.Counties.ToListAsync();

            //var k = mapper.Map<List<County>, List<CountyDTO>>(records);

            //return k;





        }
    }
}
