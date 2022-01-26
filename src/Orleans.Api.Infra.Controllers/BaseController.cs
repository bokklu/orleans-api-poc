using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Orleans.Api.Infra.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected async Task<IActionResult> HandleRequestAsync(Func<Task<IActionResult>> fn)
        {
            try
            {
                return await fn();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
