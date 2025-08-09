using Dominio.DTOs;
using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Interfaces
{
    public interface ITransacaoRepository
    {
        Task<IEnumerable<Transacao>> ListarAsync(DateTime? inicio, DateTime? fim, long? categoriaId, int? tipo);
        Task<Transacao> ObterPorIdAsync(long id);
        Task<long> CriarAsync(Transacao request);
        Task AtualizarAsync(Transacao transacao);
        Task DeletarAsync(Transacao transacao);
    }
}
