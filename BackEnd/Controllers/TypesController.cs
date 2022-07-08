using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Data;
using BackEnd.Interfaces;
using BackEnd.Repositories;
using BackEnd.Services;

namespace BackEnd.Controllers
{
    [Route("api/Types")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly IMainService _service;

        /// <summary>Initializes a new instance of the <see cref="TypesController" /> class.</summary>
        /// <param name="mainservice">The mainservice.</param>
        public TypesController(IMainService mainservice)
        {
            _service = mainservice;
        }


        // GET: api/Types
        /// <summary>Gets the types.</summary>
        /// <returns>Result</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserType>>> GetTypes()
        {
            var types = await _service.GetTypes();
            return Ok(types);
        }

        // GET: api/Types/5
        /// <summary>Gets the type by its identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Result</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserType>> GetType(int id)
        {
            var type = await _service.GetType(id);

            if (type == null)
            {
                //return NotFound();
                return StatusCode(500);
            }

            return type;
        }




    }
}
