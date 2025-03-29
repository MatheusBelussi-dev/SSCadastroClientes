using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroClientes.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroClientes.Repositorio
{
    public class ClienteRepositorio : RepositorioBase<Cliente>
    {
        public ClienteRepositorio(ClienteContext context) : base(context)
        {
        }

        public async Task<Cliente> Consultar(int id)
        {
            return await ctx.Clientes.FirstOrDefaultAsync(c => c.Id == id && !c.Excluido);
        }

        public async Task<IEnumerable<Cliente>> Filtrar(string nome, string documento)
        {
            var query = ctx.Clientes.Where(c => !c.Excluido);

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(c => c.Nome.Contains(nome));

            if (!string.IsNullOrEmpty(documento))
                query = query.Where(c => c.Documento.Contains(documento));

            return await query.ToListAsync();
        }
    }
}