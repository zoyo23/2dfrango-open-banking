using OpenBankingMock.Domain.Interfaces.Repositories;
using OpenBankingMock.Domain.Models.Entities;
using System;
using System.Threading.Tasks;

namespace OpenBankingMock.Domain.Repositories
{
    public class UsuarioHardcodedRepository : IUsuarioRepository
    {
        public Task<Usuario> BuscarPorCpf(string cpf)
        {
            return Task.FromResult(new Usuario
            {
                Cpf = cpf
            });
        }

        public Task<Usuario> BuscarPorCpfESenha(string cpf, string senha)
        {
            return Task.FromResult(new Usuario
            {
                Cpf = cpf,
                Nome = senha
            });
        }
    }
}
