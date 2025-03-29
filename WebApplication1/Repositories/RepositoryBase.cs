using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroClientes.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroClientes.Repositorio
{
    public class RepositorioBase<T> : IDisposable, IRepositorio<T> where T : class
    {
        protected ClienteContext ctx;

        public RepositorioBase(ClienteContext context)
        {
            ctx = context;
        }

        public async Task Adicionar(T obj)
        {
            ctx.Set<T>().Add(obj);
            await ctx.SaveChangesAsync();
        }

        public async Task Alterar(T obj)
        {
            ctx.Entry<T>(obj).State = EntityState.Modified;
            await ctx.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (ctx != null) ctx.Dispose();
        }

        public async Task Excluir(T obj)
        {
            // Soft delete
            if (obj is Cliente cliente)
            {
                cliente.Excluido = true;
                await Alterar(obj);
            }
            else
            {
                ctx.Set<T>().Remove(obj);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> Listar()
        {
            if (typeof(T) == typeof(Cliente))
            {
                return (IEnumerable<T>)await ctx.Set<Cliente>().Where(c => !c.Excluido).ToListAsync();
            }
            return await ctx.Set<T>().ToListAsync();
        }
    }
}