using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenBankingMock.Domain.Interfaces.Services;

namespace OpenBankingMock.Controllers
{
    /*
     * Este controller está aqui apenas para simular a chamada da aplicação. Existe apenas em tempo de desenvolvimento
     */
    [Route("[controller]")]
    [ApiController]
    public class AplicacaoController : ControllerBase
    {
        public async Task<JsonResult> Get([FromServices]IAutorizacaoService autorizacaoService, string code)
        {
            string accessToken = await autorizacaoService.GerarAccessToken("123", "123", "123", code);
            return new JsonResult(new { accessToken = accessToken });
        }
    }
}