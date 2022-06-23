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
    [Route("api/Titles")]
    [ApiController]
    public class TitlesController : ControllerBase
    {
        private readonly LRS_DBContext _context;

        public TitlesController(LRS_DBContext context)
        {
            _context = context;
        }

        // GET: api/Titles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserTitle>>> GetTitles()
        {
            return await _context.UserTitles.ToListAsync();
        }

        // GET: api/Titles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserTitle>> GetTitle(int id)
        {
            var title = await _context.UserTitles.FindAsync(id);

            if (title == null)
            {
                return NotFound();
            }

            return title;
        }

      
    }
}
