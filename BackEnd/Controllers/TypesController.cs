using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models;
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
        private readonly ITypeService _typeService;

        public TypesController()
        {
            _typeService = new TypeService(new TypeRepository(new LRS_DBContext()));
        }


        // GET: api/Types
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserType>>> GetTypes()
        {
            var types = await _typeService.GetTypes();
            return Ok(types);
        }

        // GET: api/Types/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserType>> GetType(int id)
        {
            var type = await _typeService.GetTypeByID(id);

            if (type == null)
            {
                //return NotFound();
                return StatusCode(500);
            }

            return type;
        }




    }
}
