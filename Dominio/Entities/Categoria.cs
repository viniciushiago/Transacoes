using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class Categoria
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public TipoCategoria Tipo { get; set; }
        public bool Ativo { get; set; }
        public List<Transacao> Transacao { get; set; } = new List<Transacao>();
    }
}
