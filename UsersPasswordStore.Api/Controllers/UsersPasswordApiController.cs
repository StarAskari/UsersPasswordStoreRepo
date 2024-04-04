using Microsoft.AspNetCore.Mvc;
using UsersPasswordStore.Application.Interfaces;
using UsersPasswordStore.Domain.Model;

namespace UsersPasswordStore.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersPasswordApiController : ControllerBase
    {

        private readonly IUserService _userService;


        public UsersPasswordApiController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("InsertUsersPassword")]
        public IActionResult InsertNewUser(UsersPassword usersPassword)
        {

            try
            {

                _userService.InsertNewUser(usersPassword);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetAllPasswordOfUser")]
        public IActionResult GetAllPasswordOfUser(string username)
        {
            try
            {
                var passwordList = _userService.GetAllPasswordOfUser(username);
                if (passwordList != null)
                {
                    return Ok(passwordList);
                }
                else
                {
                    return NotFound("No password list found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpGet("GetSingleOrDefaultItem")]
        public IActionResult GetSingleOrDefaultItem(string username, int id)
        {
            try
            {
                var singleUsersPassword = _userService.GetSingleOrDefaultItem(username, id);
                if (singleUsersPassword != null)
                {
                    return Ok(singleUsersPassword);
                }
                else
                {
                    return NotFound("No single UserPassWord found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetSingleOrDefaultItemWithDecryptedPassword")]
        public IActionResult GetSingleOrDefaultItemWithDecryptedPassword(string username, int id)
        {
            try
            {
                var singleUsersPassword = _userService.GetSingleOrDefaultItemWithDecryptedPassword(username, id);
                if (singleUsersPassword != null)
                {
                    return Ok(singleUsersPassword);
                }
                else
                {
                    return NotFound("No single UserPassWord found.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(UsersPassword user)
        {
            try
            {

                _userService.UpdateUser(user);
                return Ok("Entry Updated");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpDelete("DeleteUser")]
        public IActionResult DeleteUser(UsersPassword user)
        {
            try
            {
                bool isDelete = _userService.DeleteUser(user);

                if (isDelete == true)
                {


                    return Ok("Data successfully Removed From cache");
                }
                else
                {
                    return NotFound("Not Deleted");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


    }
}