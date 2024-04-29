using DTO.Dates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepositories
{
    public interface IDateRepository
    {
        public Task<GetOneDateDTO> CreateAsync(DateTime date);
        public Task<GetOneDateDTO> GetDateByIdAsync(int id);
        public Task<GetOneDateDTO> GetDateWithRentsAsync(int id);
    }
}
