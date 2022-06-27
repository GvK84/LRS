using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Models;

namespace BackEnd.Controllers
{
    [Route("api/Types")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly LRS_DBContext _context;

        public TypesController(LRS_DBContext context)
        {
            _context = context;
        }

        // GET: api/Types
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserType>>> GetTypes()
        {
            return await _context.UserTypes.ToListAsync();
        }

        // GET: api/Types/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserType>> GetType(int id)
        {
            var type = await _context.UserTypes.FindAsync(id);

            if (type == null)
            {
                return NotFound();
            }

            return type;
        }

      
    }
}
