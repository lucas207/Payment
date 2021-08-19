using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payment.API.Controllers
{
    public class MyBaseController : ControllerBase
    {
        public bool HasNotifications { get; private set; }
        //public IActionResult Response(Command command)
        //{
        //    if (HasNotifications)
        //        return Ok(command);
        //    else
        //        return BadRequest("vai maluko");
        //}
    }
}
