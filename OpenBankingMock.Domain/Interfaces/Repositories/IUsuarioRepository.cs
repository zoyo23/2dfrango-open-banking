﻿using OpenBankingMock.Domain.Models.Entities;
using System.Threading.Tasks;

namespace OpenBankingMock.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> BuscarPorCpf(string cpf);
        Task<Usuario> BuscarPorCpfESenha(string cpf, string senha);
    }
}
