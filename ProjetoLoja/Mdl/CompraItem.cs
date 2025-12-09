using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoLoja.Mdl
{
    public class CompraItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ProdutoId { get; set; }

        [Required]
        public Produto Produto { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantidade { get; set; } = 1;

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PrecoUnitario { get; set; }

        public decimal SubTotal => Quantidade * PrecoUnitario;
    }
}
