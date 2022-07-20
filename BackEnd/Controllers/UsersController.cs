using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using BackEnd.Data;
using BackEnd.Interfaces;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    // TODO a readme.md file is generally suggested in such cases
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // TODO not used?
        private static readonly ILog log =
            LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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
            var users = await _service.GetUsersAsync();
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
        public async Task<ActionResult> PutUser(int id, User user)
        {
            // TODO use of DTO model
            // TODO there are no model restrictions for this to return invalid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO we usually validate within the service
            if (!await _service.ValidateUser(user))
            {
                // TODO it's best to explicitly state the error
                return BadRequest("Not a valid user");
            }

            // TODO preferable to use await instead of result otherwise you are synchronous
            if (!await _service.UpdateUser(id, user))
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