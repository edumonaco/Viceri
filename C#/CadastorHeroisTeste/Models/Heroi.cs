using System.ComponentModel.DataAnnotations;

namespace CadastorHeroisTeste.Models
{
    public class Heroi
    {
        [Key]
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? NomeHeroi { get; set; }        
        public DateTime DataNascimento { get; set; }
        public decimal Altura {  get; set; }    
        public decimal Peso { get; set; }
        public virtual ICollection<SuperPoder> SuperPoderes { get; set; } = new HashSet<SuperPoder>();

    }
}
