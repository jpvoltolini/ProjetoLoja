using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoLoja.Mdl
{
    public class Produto
    {
        /// <summary>
        /// Código de barras do produto
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Nome do Produto
        /// </summary>
        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        [Range(0.01, double.MaxValue)]
        public double Preco { get; set; }

        public string? Marca { get; set; }
    }
}
