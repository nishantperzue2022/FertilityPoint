
using FertilityPoint.DTO.SubCountyModule;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FertilityPoint.Data.Services.SubCountyModule
{
    public interface ISubCountyService
    {
        Task<SubCountyDTO> Create(SubCountyDTO subCountyDTO);
        Task<List<SubCountyDTO>> GetAll();
    }
}