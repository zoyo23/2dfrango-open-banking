using OpenBankingMock.Domain.Interfaces.Repositories;
using OpenBankingMock.Domain.Interfaces.Services;
using OpenBankingMock.Domain.Models.Entities;
using System.Threading.Tasks;

namespace OpenBankingMock.Domain.Services
{
    public class AutorizacaoService : IAutorizacaoService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AutorizacaoService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<string> GerarCodigoIdentificacao(string cpf, string senha, string clientId)
        {
            // TODO: Verificar também ClientId
            Usuario usuario = await _usuarioRepository.BuscarPorCpfESenha(cpf, senha);
            return usuario.GerarCodigoAutorizacao(clientId);
        }

        public async Task<string> GerarAccessToken(string clientId, string clientSecret, string redirect, string code)
        {
            // TODO: Validar clientId e clientSecret.
            // TODO: Verificar se o redirect está igual
            var cpf = Usuario.ValidarCodigoAutorizacao(clientId, code);
            Usuario usuario = await _usuarioRepository.BuscarPorCpf(cpf);
            return usuario.GerarAccessToken(clientId);
        }
    }
}
