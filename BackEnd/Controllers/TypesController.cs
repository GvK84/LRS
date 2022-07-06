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
        private readonly IUserService _service;

        public TypesController(IUserService mainservice)
        {
            _service = mainservice;
        }


        // GET: api/Types
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserType>>> GetTypes()
        {
            var types = await _service.GetTypes();
            return Ok(types);
        }

        // GET: api/Types/5
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
