
using AutoMapper;
using CadastroClientes.Models;
using CadastroClientes.Repositorio;
using CadastroClientes.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace CadastroClientes.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteRepositorio _rep;
        private readonly IMapper _mapper;

        public ClienteController(ClienteRepositorio rep, IMapper mapper)
        {
            _rep = rep;
            _mapper = mapper;
        }

        public async Task<ActionResult> Index(string nome, string documento, int page = 1)
        {
            ViewBag.nome = nome;
            ViewBag.documento = documento;

            IEnumerable<Cliente> listaClientes;
            if (string.IsNullOrEmpty(nome) && string.IsNullOrEmpty(documento))
            {
                listaClientes = await _rep.Listar();
            }
            else
            {
                listaClientes = await _rep.Filtrar(nome, documento);
            }

            IEnumerable<ClienteIndex> destino = _mapper.Map<IEnumerable<ClienteIndex>>(listaClientes);
            int pageSize = 20;
            return View(destino.OrderByDescending(c => c.Id).ToPagedList(page, pageSize));
        }

        public ActionResult Adicionar()
        {
            ViewBag.Tipos = new SelectList(new List<string> { "PF", "PJ" });
            return View(new ClienteAdicionar());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Adicionar(ClienteAdicionar view)
        {
            if (ModelState.IsValid)
            {
                
                if ((view.Tipo == "PF" && !ValidarCPF(view.Documento)) ||
                    (view.Tipo == "PJ" && !ValidarCNPJ(view.Documento)))
                {
                    ModelState.AddModelError("Documento", $"Documento inválido para tipo {view.Tipo}");
                    ViewBag.Tipos = new SelectList(new List<string> { "PF", "PJ" });
                    return View(view);
                }

                Cliente cliente = _mapper.Map<Cliente>(view);
                try
                {
                    await _rep.Adicionar(cliente);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewBag.Tipos = new SelectList(new List<string> { "PF", "PJ" });
            return View(view);
        }

        public async Task<ActionResult> Alterar(int id)
        {
            Cliente cliente = await _rep.Consultar(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            ClienteAlterar view = _mapper.Map<ClienteAlterar>(cliente);
            ViewBag.Tipos = new SelectList(new List<string> { "PF", "PJ" }, cliente.Tipo);
            return View(view);
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Alterar(ClienteAlterar view)
        {
            if (ModelState.IsValid)
            {
               
                if ((view.Tipo == "PF" && !ValidarCPF(view.Documento)) ||
                    (view.Tipo == "PJ" && !ValidarCNPJ(view.Documento)))
                {
                    ModelState.AddModelError("Documento", $"Documento inválido para tipo {view.Tipo}");
                    ViewBag.Tipos = new SelectList(new List<string> { "PF", "PJ" }, view.Tipo);
                    return View(view);
                }

                Cliente cliente = await _rep.Consultar(view.Id);
                if (cliente == null)
                {
                    return HttpNotFound();
                }

                _mapper.Map(view, cliente);
                try
                {
                    await _rep.Alterar(cliente);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewBag.Tipos = new SelectList(new List<string> { "PF", "PJ" }, view.Tipo);
            return View(view);
        }

        public async Task<ActionResult> Excluir(int id)
        {
            Cliente cliente = await _rep.Consultar(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            ClienteExcluir view = _mapper.Map<ClienteExcluir>(cliente);
            return View(view);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Excluir")]
        public async Task<ActionResult> ConfirmarExclusao(int id)
        {
            Cliente cliente = await _rep.Consultar(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            try
            {
                await _rep.Excluir(cliente);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(_mapper.Map<ClienteExcluir>(cliente));
            }
        }

        #region Métodos de Validação

        private bool ValidarCPF(string cpf)
        {
            return true; 
        }

        private bool ValidarCNPJ(string cnpj)
        {
            return true; 
        }

        #endregion
    }
}