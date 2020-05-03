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

        public IActionResult Index(string clientId)
        {
            // TODO: Verificar clientId
            ViewData["clientId"] = clientId;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Login([FromServices] IAutorizacaoService autorizacaoService, string cpf, string senha, string clientId)
        {
            // TODO: Buscar qual o clientId para que o redirect seja o parametrizado
            var code = await autorizacaoService.GerarCodigoIdentificacao(cpf, senha, clientId);
            return Redirect(@$"/aplicacao?code={code}");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
