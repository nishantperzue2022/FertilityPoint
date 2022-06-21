
using FertilityPoint.DTO.CountyModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FertilityPoint.Data.Services.CountyModule
{
    public interface ICountyService
    {
        Task<CountyDTO> Create(CountyDTO countyDTO);
        Task<List<CountyDTO>> GetAll();
    }
}