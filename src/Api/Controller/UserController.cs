using Api.Core;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Api.Service;
using Infrastructure.Entities;
using Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly IGetAllUserService GetAllUsersService;
        private readonly IUpdateUserInfoService UpdateUserInfoService;
        private readonly IDeleteUserService DeleteUserService;

        public ServiceController(
            IGetAllUserService GetAllUsersService,
            IUpdateUserInfoService UpdateUserInfoService,
            IDeleteUserService DeleteUserService
            )
        {
            this.GetAllUsersService = GetAllUsersService;
            this.UpdateUserInfoService = UpdateUserInfoService;
            this.DeleteUserService = DeleteUserService;
        }

        [HttpGet]
        [Authorize]
        [Route("getAllUserss")]
        public async Task<IActionResult> GetAllUserss()
        {
            var response = await GetAllUsersService.ExecuteAsync();
            
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpPut]
        [Route("updateUserInfo")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserInfoServiceRequest request)
        {
            var response = await UpdateUserInfoService.ExecuteAsync(request);
            
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [Authorize]
        [HttpDelete]
        [Route("deleteUser")]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserServiceRequest request)
        {
            var response = await DeleteUserService.ExecuteAsync(request);
            
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}