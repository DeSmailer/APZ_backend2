using BusinessLogicLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpPost]
        public Task<string> EncryptString([FromBody] test str)
        {
            return Task.FromResult(AuthenticationService.EncryptString(str.Str));
        }

        [HttpPost]
        public Task<string> DecryptString([FromBody] test str)
        {
            return Task.FromResult(AuthenticationService.DecryptString(str.Str));
        }
    }
}
