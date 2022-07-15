using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Skinet.Controllers
{
    [Route("error/{code}")]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController : BaseApiController
    {
        public IActionResult Error(int code)
        {
          return new ObjectResult(code);
        }
    }
}
