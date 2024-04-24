using DTO.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    /// <summary>
    /// Class for all Model Tools
    /// </summary>
    public class ModelRepository
    {
        public DatabaseContext Context { get; set; }
        public ModelRepository(DatabaseContext databaseContext)  // Dependancy injections
        {
            this.Context = databaseContext;
        }
        /// <summary>
        /// Create a model based on Creation DTO, Name string, Co2 double, Year Int
        /// </summary>
        /// <param name="createModelDTO"></param>
        /// <returns>GetOneModelDTO</returns>
        public async Task<GetOneModelDTO> CreateModelAsync(CreateOneModelDTO createModelDTO)
        {

            Model newModel = new Model
            {
                Label = createModelDTO.Name,
                CO2 = createModelDTO.Co2,
                Year = createModelDTO.Year
            };

            await Context.Models.AddAsync(newModel);
            await Context.SaveChangesAsync();

            return new GetOneModelDTO
            {
                Id = newModel.Id,
                Name = newModel.Label,
                Co2 = newModel.CO2,
                Year = newModel.Year
            };
        }
    }
}
