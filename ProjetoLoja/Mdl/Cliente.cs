using ProjetoLoja.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjetoLoja.Mdl
{
    public class Cliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required] 
        public EnTipoPessoa TipoPessoa { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 14)]
        public string CpfCnpj { get; set; } = string.Empty;

        [Required]
        public DateTime? DataNascimento { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

    }
}
