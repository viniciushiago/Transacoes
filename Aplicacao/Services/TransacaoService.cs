using Aplicacao.DTOs;
using Aplicacao.Services.Interfaces;
using Dominio.Entities;
using Infraestrutura;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly AppDbContext _context;

        public TransacaoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransacaoResponse>> ListarAsync(DateTime? inicio, DateTime? fim,long? categoriaId, int? tipo)
        {
            var query = _context.Transacoes
                .Include(x => x.Categoria)
                .AsQueryable();

            if(inicio.HasValue) query = query.Where(t => t.Data >= inicio.Value);

            if(fim.HasValue) query = query.Where(t => t.Data <= fim.Value);

            if(categoriaId.HasValue) query = query.Where(t => t.CategoriaId == categoriaId.Value);

            if(tipo.HasValue) query = query.Where(t => (int)t.Categoria!.Tipo == tipo.Value);

            return await query
                .Select(t => new TransacaoResponse
                {
                    Id = t.Id,
                    Descricao = t.Descricao,
                    Valor = t.Valor,
                    Data = t.Data,
                    CategoriaId = t.CategoriaId,
                    CategoriaNome = t.Categoria.Nome,
                    Observacoes = t.Observacoes,
                    DataCriacao = t.DataCriacao,
                }).ToListAsync();
        }

        public async Task<TransacaoResponse> ObterPorIdAsync(long id)
        {
            var transacao = await _context.Transacoes
                .Include(x => x.Categoria)
                .FirsOrDefaultAsync(x => x.Id == id);

            if(transacao == null)
            {
                throw new Exception("Transação não encontrada.");
            }

            return new TransacaoResponse
            {
                Id = transacao.Id,
                Descricao = transacao.Descricao,
                Valor = transacao.Valor,
                Data = transacao.Data,
                CategoriaId = transacao.CategoriaId,
                CategoriaNome = transacao.CategoriaNome,
                Observacoes = transacao.Observacoes,
                DataCriacao = transacao.DataCriacao,
            };
        }

        public async Task<long> CriarAsync(CriarTransacaoRequest request)
        {
            if(request.Valor <= 0)
            {
                throw new ArgumentException("Valor deve ser maior que zero.");
            }

            if(request.Data > DateTime.Now)
            {
                throw new ArgumentException("Data não pode ser futura.");
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(x => x.Id == request.CategoriaId && x.Ativo);  

            if(categoria == null)
            {
                throw new ArgumentException("Categoria inválida ou inativa.");
            }

            var transacao = new Transacao
            {
                Descricao = request.Descricao,
                Valor = request.Valor,
                Data = request.Data,
                CategoriaId = request.CategoriaId,
                Observacoes = request.Observacoes,
                DataCriacao = DateTime.Now
            };

            _context.Transacoes.Add(transacao);
            await _context.SaveChangesAsync();

            return transacao.Id;
        } 

        public async Task AtualizarAsync(long id, AtualizarTransacaoRequest request)
        {
            var transacao = await _context.Transacoes.FindAsync(id);
            if(transacao == null)
            {
                throw new Exception("Transação não encontrada.");
            }

            if(request.Valor <= 0)
            {
                throw new ArgumentException("Valor deve ser maior que zero.");
            }

            if(request.Data > DateTime.Now)
            {
                throw new ArgumentException("Data não pode ser futura.");
            }

            var categoria = await _context.Categorias
                .FirstOrDefaultAsync(c => c.Id == request.CategoriaId && c.Ativo);

            if(categoria == null)
            {
                throw new ArgumentException("Categoria inválida ou inativa");
            }

            transacao.Descricao = request.Descricao;
            transacao.Valor = request.Valor;
            transacao.Data = request.Data;
            transacao.CategoriaId = request.CategoriaId;
            transacao.Observacoes = request.Observacoes;

            _context.Transacoes.Update(transacao);

            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(long id)
        {
            var transacao = await _context.Transacoes.FindAsync(id);
            if(transacao == null)
            {
                throw new Exception("Transação não encontrada.");
            }

            _context.Transacoes.Remove(transacao);
            await _context.SaveChangesAsync();
        }

    }
}
