using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OpenBankingMock.Domain.Interfaces.Services;
using OpenBankingMock.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace OpenBankingMock.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string clientId, string redirect, string param)
        {
            // TODO: Verificar clientId
            ViewData["clientId"] = clientId;
            ViewData["redirect"] = redirect;
            ViewData["param"] = param;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Login([FromServices] IAutorizacaoService autorizacaoService, string cpf, string senha, string clientId, string redirect, string param)
        {
            // TODO: Buscar configuração permitida de redirect usando o clientId
            var code = await autorizacaoService.GerarCodigoIdentificacao(cpf, senha, clientId);
            return Redirect(@$"{redirect}?code={code}" + (!string.IsNullOrEmpty(param) ? @$"&params={param}" : string.Empty));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
