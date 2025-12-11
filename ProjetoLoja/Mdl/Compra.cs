using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoLoja.Mdl
{
    public class Compra
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime DataCompra { get; set; } = DateTime.UtcNow;

        // Calculado a partir dos itens
        public decimal ValorTotal => Items?.Sum(i => i.SubTotal) ?? 0m;

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public Cliente Cliente { get; set; } = null!;

        public List<CompraItem> Items { get; set; } = new();
    }
}
