using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaiLieuWebsiteBackend.Models;
using TaiLieuWebsiteBackend.Services;
using TaiLieuWebsiteBackend.Dtos;
using TaiLieuWebsiteBackend.Response;

namespace TaiLieuWebsiteBackend.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsersAsync();
            if (response.StatusCode != 200)
                return StatusCode(response.StatusCode, response.ErrorMessage);

            return Ok(response.Data);
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            if (response.StatusCode != 200)
                return StatusCode(response.StatusCode, response.ErrorMessage);

            return Ok(response.Data);
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _userService.AddUserAsync(user);
            if (response.StatusCode != 201)
                return StatusCode(response.StatusCode, response.ErrorMessage);

            return CreatedAtAction(nameof(GetUserById), new { id = user.user_id }, user);
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.user_id)
                return BadRequest("ID người dùng không khớp");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userExistsResponse = await _userService.UserExistsAsync(id);
            if (!userExistsResponse.Data)
                return NotFound("Người dùng không tồn tại");

            var response = await _userService.UpdateUserAsync(user);
            if (response.StatusCode != 204)
                return StatusCode(response.StatusCode, response.ErrorMessage);

            return NoContent();
        }


        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userExistsResponse = await _userService.UserExistsAsync(id);
            if (!userExistsResponse.Data)
                return NotFound("Người dùng không tồn tại");

            var response = await _userService.DeleteUserAsync(id);
            if (response.StatusCode != 204)
                return StatusCode(response.StatusCode, response.ErrorMessage);

            return NoContent();
        }




        // POST: api/User/register
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            // Check if email already exists
            if (await _userService.EmailExistsAsync(userRegisterDto.Email))
            {
                return BadRequest(new { success = false, errorMessage = "không được trùng Email" });
            }

            var response = await _userService.RegisterUserAsync(userRegisterDto);
            if (response.StatusCode != 201)
                return StatusCode(response.StatusCode, new { success = false, errorMessage = response.ErrorMessage });

            return CreatedAtAction(nameof(GetUserById), new { id = response.Data.user_id }, new { success = true, data = response.Data });
        }


        // POST: api/User/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _userService.LoginUserAsync(userLoginDto);
            if (response.StatusCode != 200)
                return Unauthorized(response.ErrorMessage);

            return Ok(new { Token = response.Data.Token, User = response.Data.User });
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            string authHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return BadRequest(ApiResponse<object>.Error(400, "Authorization header is missing or invalid", "Bad Request"));
            }

            string token = authHeader.Substring(7);
            var user = await _userService.FetchAsync(token);

            if (user == null)
            {
                return NotFound(ApiResponse<object>.Error(404, "User not found", "Not Found"));
            }

            var response = await _userService.GetUserProfileAsync(user.user_id);
            if (response.StatusCode != 200)
            {
                return StatusCode(response.StatusCode, response.ErrorMessage);
            }

            return Ok(response.Data);
        }
    }
}
