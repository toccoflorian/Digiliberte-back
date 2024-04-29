using DTO.Dates;
using DTO.Motorization;
using IServices;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Main.Controllers
{
        [Route("api/[controller]/[action]")]
        [ApiController]
        public class MotorizationController : ControllerBase
        {
            public readonly IMotorizationService _motorizationServices;
            public MotorizationController(IMotorizationService motirizationServices)
            {
                _motorizationServices = motirizationServices;
            }

        /// <summary>
        /// Create a Motorization into the db, use a DTO for Create        
        /// </summary>
        /// <param name="updateOneMotorization">DTO of Motorization for update</param>
        /// <returns>Returns a DTO of the updated motorization</returns>
        [HttpPost]
            public async Task<ActionResult<GetOneMotorizationDTO?>> CreateOneMotorization(CreateOneMotorizationDTO createOneMotorizationDTO)
        {
            try
            {
                return Ok(await _motorizationServices.CreateOneMotorizationAsync(createOneMotorizationDTO));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            }

        /// <summary>
        /// Put a Motorization into the db, use a DTO for update        
        /// </summary>
        /// <param name="updateOneMotorization">DTO of Motorization for update</param>
        /// <returns>Returns a DTO of the updated motorization</returns>
        [HttpPut]

        public async Task<ActionResult<GetOneMotorizationDTO?>> UpdateMotorization(GetOneMotorizationDTO getOneMotorization)
        {
            try
            {
                return Ok(await _motorizationServices.UpdateOneMotorizationByIdAsync(getOneMotorization));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        ///// <summary>
        ///// Dlete a Motorization into the db, use a DTO for delete        
        ///// 
        ///// /// </summary>
        ///// <param name="deleteOneMotorization">DTO of Motorization for delete</param>
        ///// <returns>Returns a DTO of the deleted motorization</returns>
        //[HttpDelete]
        //public async Task<ActionResult<GetOneMotorizationDTO?>> DeleteOneMotorizationByIdAsync(int Id)
        //{
        //    try
        //    {
        //        return Ok(await _motorizationServices.DeleteOneMotorizationByIdAsync(Id));
        //    }
        //    catch (Exception ex) { return BadRequest(ex.Message); }
        //}

        /// <summary>
        /// Get a Motorization By Id into the db, use a Id       
        /// /// </summary>
        /// <param name="GetOneMotorizationById">DTO of Motorization for GetOneMotorization</param>
        /// <returns>Returns a DTO of the GetOneMotorizationById Motorization</returns>
        [HttpGet]
        public async Task<ActionResult<GetOneMotorizationDTO?>> GetOneMotorizationByIdAsync(int Id)
        {
            // Utilisez le service pour récupérer le modèle par son ID
            try
            {
                return Ok(await _motorizationServices.GetOneMotorizationByIdAsync(Id));
            }
            catch (Exception ex) { return BadRequest(ex.Message); }
        }

        /// <summary>
        /// Get all motorizations
        /// </summary>
        /// <returns>List of motorization DTOs</returns>
        [HttpGet("all")]
        public async Task<ActionResult<List<GetOneMotorizationDTO>>> GetAllMotorizationsAsync()
        {
            try
            {
                var motorizations = await _motorizationServices.GetAllMotorizationsAsync();
                return Ok(motorizations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
    }

