using System.ComponentModel.DataAnnotations;

namespace CadastorHeroisTeste.Models
{
    public class Heroi
    {
        [Key]
        public int Id { get; set; }

        public string? Nome { get; set; }

           }
}
