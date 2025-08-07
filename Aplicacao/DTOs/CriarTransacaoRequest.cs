using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.DTOs
{
    public class CriarTransacaoRequest
    {
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public long CategoriaId { get; set; }
        public string? Observacoes { get; set; }
    }
}
