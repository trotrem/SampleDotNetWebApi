using BuberBreakfast.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers
{
    [ApiController]
    public class ErrorsController: ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            var error = HttpContext.Features.Get<IExceptionHandlerFeature>();

            // TODO verify what this logs with application insights. We want to keep the stacktrace in the logs.
            if (error?.Error is HttpException httpException)
            {
                return StatusCode(httpException.StatusCode, httpException.Message);
            }

            return Problem();
        }
    }
}
