using System.ComponentModel.DataAnnotations;

namespace CadastorHeroisTeste.Models
{
    public class SuperPoder
    {
        [Key]
        public int Id { get; set; }
        public string? Descricao { get; set; }

    }
}
