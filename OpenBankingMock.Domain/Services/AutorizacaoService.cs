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

        public async Task<string> GerarToken(string cpf, string senha, string clientId)
        {
            // TODO: Verificar também ClientId
            Usuario usuario = await _usuarioRepository.BuscarPorCpfESenha(cpf, senha);
            return usuario.GerarTokenAutorizacao(clientId);
        }
    }
}
