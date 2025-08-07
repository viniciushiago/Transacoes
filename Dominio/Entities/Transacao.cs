using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class Transacao
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public long CategoriaId { get; set; }
        public string? Observacoes { get; set; }
        public DateTime DataCriacao { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
