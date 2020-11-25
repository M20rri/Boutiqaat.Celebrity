using Boutiqaat.Celebrity.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Boutiqaat.Celebrity.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class ValuesController : Controller
    {
        [AllowAnonymous]
        [HttpGet, Route("api/Values/DefaultPage")]
        public string DefaultPage()
        {
            return "Default Page";
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet, Route("api/Values/AdminPage")]
        public string AdminPage()
        {
            return "Admin Page";
        }

        [Authorize(Roles = Role.ViseDien)]
        [HttpGet, Route("api/Values/ViseDienPage")]
        public string ViseDienPage()
        {
            return "Vise Dien Page";
        }

        [Authorize(Roles = Role.Client)]
        [HttpGet, Route("api/Values/ClientPage")]
        public string ClientPage()
        {
            return "Client Page";
        }

    }
}