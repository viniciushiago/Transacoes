using Dominio.DTOs;
using Dominio.Entities;
using Infraestrutura.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Repositories
{
    public class TransacaoRepository : ITransacaoRepository
    {
        private readonly AppDbContext _context;

        public TransacaoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AtualizarAsync(Transacao transacao)
        {
            _context.Transacoes.Update(transacao);
            await _context.SaveChangesAsync();
        }

        public async Task<long> CriarAsync(Transacao transacao)
        {
            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();
            return transacao.Id;
        }

        public async Task DeletarAsync(Transacao transacao)
        {
            _context.Transacoes.Remove(transacao);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Transacao>> ListarAsync(DateTime? inicio, DateTime? fim, long? categoriaId, int? tipo)
        {
            var query = _context.Transacoes
                .Include(x => x.Categoria)
                .AsQueryable();

            if (inicio.HasValue) query = query.Where(t => t.Data >= inicio.Value);

            if (fim.HasValue) query = query.Where(t => t.Data <= fim.Value);

            if (categoriaId.HasValue) query = query.Where(t => t.CategoriaId == categoriaId.Value);

            if (tipo.HasValue) query = query.Where(t => (int)t.Categoria!.Tipo == tipo.Value);
            
            return await query.ToListAsync();
        }

        public async Task<Transacao?> ObterPorIdAsync(long id)
        {
            return await _context.Transacoes
               .Include(x => x.Categoria)
               .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
