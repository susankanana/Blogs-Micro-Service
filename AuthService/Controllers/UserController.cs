using AuthService.Models.Dtos;
using AuthService.Services.IServices;
using BlogsMessageBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;
        private readonly ResponseDto _response;
        private readonly IConfiguration _configuration;
        public UserController(IUser usr, IConfiguration configuration)
        {
            _userService = usr;
            _response = new ResponseDto();
            _configuration = configuration;

        }

        [HttpPost("Register")]
        public async Task<ActionResult<ResponseDto>> RegisterUser(RegisterUserDto registerUserDto)
        {
            var res = await _userService.RegisterUser(registerUserDto);
            if(string.IsNullOrEmpty(res))
            {
                //success
                _response.Result = "User Registered successfully";

                //add message to queue
                var message = new UserMessageDto()
                {
                    Name = registerUserDto.Name,
                    Email = registerUserDto.Email,
                };

                var mb = new MessageBus();
                await mb.PublishMessage(message, _configuration.GetValue<string>("ServiceBus:register"));

                return Created("", _response);
            }
            _response.ErrorMessage = res;
            _response.IsSuccess = false;

            return BadRequest(_response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ResponseDto>> LoginUser(LoginRequestDto loginRequestDto)
        {
            var res = await _userService.LoginUser(loginRequestDto);
            if (res.User != null)
            {
                //success
                _response.Result = res;
                return Created("", _response);
            }
            _response.ErrorMessage = "Invalid Credentials";
            _response.IsSuccess = false;

            return BadRequest(_response);
        }
        [HttpPost("AssignRoles")]
        public async Task<ActionResult<ResponseDto>> AssignRole(RegisterUserDto registerUserDto) //or AssignRoleDto will give you few columns to fill
        {
            var res = await _userService.AssignUserRoles(registerUserDto.Email, registerUserDto.Role);
            if (res)
            {
                _response.Result = res;
                return Ok(_response);
            }
            _response.ErrorMessage = "Error Ocurred";
            _response.Result = res;
            _response.IsSuccess= false;
            return BadRequest(_response);
        }

        
        

    }
}
