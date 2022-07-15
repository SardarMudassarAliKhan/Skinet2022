using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Skinet.Errors;
using Skinet.Infrastracture.Data;

namespace Skinet.Controllers
{
   
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _storeContext;

        public BuggyController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            var thing = _storeContext.Products.Find(42);
            if (thing == null)
            {
                return NotFound(new APIResponce(404));
            }
            return Ok();
        }
        [HttpGet("servererror")]
        public IActionResult GerServerError()
        {
            var thing = _storeContext.Products.Find(42);
            var thingtoReturn = thing.ToString();
            return Ok();
        }
        [HttpGet("BadRequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new APIResponce(400));
        }
        [HttpGet("BadRequest/{id}")]
        public IActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }
        [HttpGet(nameof(testauth))]
        [Authorize]
        public ActionResult<string> testauth()
        {
            return "Test Auth";
        }



    }
}
