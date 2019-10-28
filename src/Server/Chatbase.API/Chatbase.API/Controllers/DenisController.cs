using System;
using Microsoft.AspNetCore.Mvc;

namespace Chatbase.API.Controllers
{
    public class DenisController: Controller
    {
        public DenisController()
        {

        }

        [Route("hello")]
        public IActionResult Foo()
        {
            return Content("Hello World");
        } 
    }
}
