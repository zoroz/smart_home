using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartHome.Facade;
using SmartHome.Model;

namespace SmartHome.Controllers
{
    [Route("api/[controller]")]
    public class SeltronController : Controller
    {
        private readonly ISeltronFacade _seltronFacade;
        private readonly ILogger<SeltronController> _log;

        public SeltronController(ISeltronFacade seltronFacade, ILogger<SeltronController> log)
        {
            _seltronFacade = seltronFacade;
            _log = log;
        }

        // GET api/values
        [HttpGet]
        public async Task<string> Login(LoginRequest request)
        {
            var res = await _seltronFacade.Login(request.Username, request.Password);
            return "OK";
        }

    }
}
