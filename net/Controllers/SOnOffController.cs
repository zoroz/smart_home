using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartHome.Facade;

namespace SmartHome.Controllers
{
    [Route("api/[controller]")]
    public class SOnOffController : Controller
    {
        private readonly ISOnOffFacade _sOnOffFacade;
        private readonly ILogger<SOnOffController> _log;

        public SOnOffController(ISOnOffFacade sOnOffFacade, ILogger<SOnOffController> log)
        {
            _sOnOffFacade = sOnOffFacade;
            _log = log;
        }

        // GET api/values
        [HttpGet]
        public async Task<string> Login([FromQuery]string userName, [FromQuery]string password)
        {
            var res = await _sOnOffFacade.Login(userName, password);
            return "OK";
        }

    }
}
