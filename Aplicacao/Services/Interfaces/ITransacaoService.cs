using Aplicacao.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Services.Interfaces
{
    public interface ITransacaoService
    {
        Task<IEnumerable<TransacaoResponse>> ListarAsync(DateTime? inicio, DateTime? fim, long? categoriaId, int? tipo);
        Task<TransacaoResponse> ObterPorIdAsync(long id);
        Task<long> CriarAsync(CriarTransacaoRequest request);
        Task AtualizarAsync(long id, AtualizarTransacaoRequest request);
        Task DeletarAsync(long id);
    }
}
