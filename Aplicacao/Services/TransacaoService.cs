using Aplicacao.Interfaces;
using Dominio.DTOs;
using Dominio.Entities;
using Infraestrutura;
using Infraestrutura.Interfaces;
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
        private readonly ITransacaoRepository _transacaoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public TransacaoService(ITransacaoRepository transacaoRepository, ICategoriaRepository categoriaRepository)
        {
            _transacaoRepository = transacaoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IEnumerable<TransacaoResponse>> ListarAsync(DateTime? inicio, DateTime? fim,long? categoriaId, int? tipo)
        {
            var transacoes = await _transacaoRepository.ListarAsync(inicio, fim, categoriaId, tipo);
            
            return transacoes
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
                }).ToList();
        }

        public async Task<TransacaoResponse> ObterPorIdAsync(long id)
        {
            var transacao = await _transacaoRepository.ObterPorIdAsync(id);

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
                CategoriaNome = transacao.Categoria.Nome,
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

            var categoria = await _categoriaRepository.ObterAtivaPorIdAsync(request.CategoriaId);  

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

            await _transacaoRepository.CriarAsync(transacao);

            return transacao.Id;
        } 

        public async Task AtualizarAsync(long id, AtualizarTransacaoRequest request)
        {
            var transacao = await _transacaoRepository.ObterPorIdAsync(id);
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

            var categoria = await _categoriaRepository.ObterAtivaPorIdAsync(request.CategoriaId);

            if(categoria == null)
            {
                throw new ArgumentException("Categoria inválida ou inativa");
            }

            transacao.Descricao = request.Descricao;
            transacao.Valor = request.Valor;
            transacao.Data = request.Data;
            transacao.CategoriaId = request.CategoriaId;
            transacao.Observacoes = request.Observacoes;

            await _transacaoRepository.AtualizarAsync(transacao);
        }

        public async Task DeletarAsync(long id)
        {
            var transacao = await _transacaoRepository.ObterPorIdAsync(id);
            if(transacao == null)
            {
                throw new Exception("Transação não encontrada.");
            }

            await _transacaoRepository.DeletarAsync(transacao);
        }

    }
}
