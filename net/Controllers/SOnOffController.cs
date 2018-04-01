using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartHome.Facade;
using SmartHome.Model;

namespace SmartHome.Controllers
{
    [Route("[controller]")]
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
        public async Task<string> Login(LoginRequest request)
        {
            var res = await _sOnOffFacade.Login(request.Username, request.Password);
            return "OK";
        }

    }
}
