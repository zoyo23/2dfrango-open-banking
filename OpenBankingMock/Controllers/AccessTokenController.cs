using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenBankingMock.Domain.Interfaces.Services;

namespace OpenBankingMock.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccessTokenController : ControllerBase
    {
        public async Task<JsonResult> Get([FromServices]IAutorizacaoService autorizacaoService, string clientId, string clientSecret, string redirect, string code)
        {
            string accessToken = await autorizacaoService.GerarAccessToken(clientId, clientSecret, redirect, code);

            return new JsonResult(new
            {
                accessToken = accessToken
            });
        }
    }
}