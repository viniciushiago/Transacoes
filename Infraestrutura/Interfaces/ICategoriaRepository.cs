using Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestrutura.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<Categoria?> ObterAtivaPorIdAsync(long id);
    }
}
