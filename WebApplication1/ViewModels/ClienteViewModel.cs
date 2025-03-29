using AutoMapper;
using CadastroClientes.Models;
using System.ComponentModel.DataAnnotations;

namespace CadastroClientes.ViewModel
{
    public class ClienteIndex
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Tipo")]
        public string Tipo { get; set; }

        [Display(Name = "Documento")]
        public string Documento { get; set; }

        [Display(Name = "Data Cadastro")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }
    }

    public class ClienteAdicionar
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "{0}, preenchimento obrigatório.")]
        public string Nome { get; set; }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "{0}, preenchimento obrigatório.")]
        public string Tipo { get; set; }

        [Display(Name = "Documento")]
        [Required(ErrorMessage = "{0}, preenchimento obrigatório.")]
        public string Documento { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "{0}, preenchimento obrigatório.")]
        public string Telefone { get; set; }
    }

    public class ClienteAlterar : ClienteAdicionar
    {
        public int Id { get; set; }
    }

    public class ClienteExcluir
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Tipo { get; set; }
    }

    public class ClienteViewModel : Profile
    {
        public ClienteViewModel()
        {
            CreateMap<Cliente, ClienteIndex>();
            CreateMap<Cliente, ClienteAlterar>();
            CreateMap<Cliente, ClienteExcluir>();

            CreateMap<ClienteAdicionar, Cliente>()
                .ForMember(dest => dest.DataCadastro, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.Excluido, opt => opt.MapFrom(src => false));

            CreateMap<ClienteAlterar, Cliente>()
                .ForMember(dest => dest.DataCadastro, opt => opt.Ignore())
                .ForMember(dest => dest.Excluido, opt => opt.Ignore());
        }
    }
}