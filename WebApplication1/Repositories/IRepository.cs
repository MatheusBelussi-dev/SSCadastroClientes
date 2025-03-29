using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroClientes.Repositorio
{
    public interface IRepositorio<T> where T : class
    {
        Task Adicionar(T obj);
        Task Alterar(T obj);
        Task Excluir(T obj);
        Task<IEnumerable<T>> Listar();
    }
}