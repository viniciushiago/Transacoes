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
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext context)
        {
            _context = context;   
        }
        public async Task<Categoria?> ObterAtivaPorIdAsync(long id)
        {
            return await _context.Set<Categoria>()
                .FirstOrDefaultAsync(c => c.Id == id && c.Ativo);
        }
    }
}
