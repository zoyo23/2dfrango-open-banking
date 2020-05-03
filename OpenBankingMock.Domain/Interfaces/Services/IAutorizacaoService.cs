using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OpenBankingMock.Domain.Interfaces.Services
{
    public interface IAutorizacaoService
    {
        Task<string> GerarCodigoIdentificacao(string cpf, string senha, string clientId);
    }
}
