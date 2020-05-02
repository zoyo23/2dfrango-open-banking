using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OpenBankingMock.Controllers
{
    /*
     * Este controller está aqui apenas para simular a chamada da aplicação. Existe apenas em tempo de desenvolvimento
     */
    [Route("[controller]")]
    [ApiController]
    public class AplicacaoController : ControllerBase
    {
        public JsonResult Get(string token)
        {
            return new JsonResult(new { token = token });
        }
    }
}