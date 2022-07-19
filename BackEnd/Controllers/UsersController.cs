using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEnd.Data;
using BackEnd.Interfaces;
using BackEnd.Repositories;
using BackEnd.Services;

namespace BackEnd.Controllers
{
    [Route("api/Users")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        

        private readonly IMainService _service;

        /// <summary>Initializes a new instance of the <see cref="UsersController" /> class.</summary>
        /// <param name="mainservice">The mainservice.</param>
        public UsersController(IMainService mainservice)
        {
            _service = mainservice;
        }



        // GET: api/Users
        /// <summary>Gets the users.</summary>
        /// <returns>Result</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _service.GetUsers();
            return Ok(users);
        }

        // GET: api/Users/active
        /// <summary>Gets the active users.</summary>
        /// <returns>Result</returns>
        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<User>>> GetActiveUsers()
        {
            var users = await _service.GetActiveUsers();
            return Ok(users);
        }

        
        // GET: api/Users/5
        /// <summary>Gets the user by its identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Result</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _service.GetUser(id);

            if (user == null)
            {
                //return NotFound();
                return StatusCode(500);
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>Updates the user.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="user">The user.</param>
        /// <returns>Result</returns>
        [HttpPut("{id}")]
        public ActionResult PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_service.ValidateUser(user).Result)
            {
                return BadRequest("Not a valid user");
            }

            if (!_service.UpdateUser(id, user).Result) 
            { 
                return StatusCode(500); 
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>Creates the user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>Result</returns>
        [HttpPost]
        public ActionResult<User> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_service.ValidateUser(user).Result)
            {
                return BadRequest("Not a valid user");
            }
            if (!_service.CreateUser(user).Result)
            {
                return StatusCode(500);
            }
            return NoContent();
        }

        // DELETE: api/Users/5
        /// <summary>Deletes the user.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Result</returns>
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            if (!_service.DeleteUser(id).Result)
            {
                return StatusCode(500);
            }
            return NoContent();
        }

        // GET: api/Users/titles
        /// <summary>Gets the titles.</summary>
        /// <returns>Result</returns>
        [HttpGet("titles")]
        public async Task<ActionResult<IEnumerable<UserTitle>>> GetTitles()
        {
            var titles = await _service.GetTitles();
            return Ok(titles);
        }

        // GET: api/Users/titles/5
        /// <summary>Gets the title by its identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Result</returns>
        [HttpGet("titles/{id}")]
        public async Task<ActionResult<UserTitle>> GetTitle(int id)
        {
            var title = await _service.GetTitle(id);

            if (title == null)
            {
                //return NotFound();
                return StatusCode(500);
            }

            return title;
        }

        // GET: api/Users/types
        /// <summary>Gets the types.</summary>
        /// <returns>Result</returns>
        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<UserType>>> GetTypes()
        {
            var types = await _service.GetTypes();
            return Ok(types);
        }

        // GET: api/Users/types/5
        /// <summary>Gets the type by its identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Result</returns>
        [HttpGet("types/{id}")]
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
