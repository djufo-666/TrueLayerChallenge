using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TrueLayerChallenge.WebApi.Controllers
{
    [AllowAnonymous]
    [Route("ping")]
    [ApiController]
    public class PingController : Controller
    {
        [HttpGet]
        public object Get()
        {
            return new { Success = true };
        }
    }
}
