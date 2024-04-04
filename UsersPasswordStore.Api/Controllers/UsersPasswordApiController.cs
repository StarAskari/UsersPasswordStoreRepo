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
        public IActionResult InsertNewUser(List<UsersPassword> lstUsersPass)
        {
           
            try
            {

                  var UsersPass = _userService.InsertNewUser(lstUsersPass);

               
                
                return Ok(UsersPass);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetpasswordList")]
        public IActionResult GetPasswordList()
        {
            try
            {
                var passwordList = _userService.GetPasswordList();
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


        [HttpGet("GetSingleUserspassword")]
        public IActionResult GetSingleList()
        {
            try
            {
                var singleUsersPassword = _userService.GetSingleItem();
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

        [HttpGet("GetSingleUsersDecryptPassword")]
        public IActionResult GetSingleDecryptPassList()
        {
            try
            {
                var singleUsersPassword = _userService.GetSingleItem();
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

        [HttpPut("UpdatePasswordofUsers")]
        public IActionResult UpdatePasswordofUsers(string newPass,string oldPass)
        {
            try
            {

                    _userService.UpdatePassword(newPass, oldPass);
                    return Ok("Entry Updated");
               
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpDelete("RemoveCacheData")]
        public IActionResult RemoveDta()
        {
            try
            {
                bool isDelete = _userService.RemoveCache();

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