using FI.AtividadeEntrevista.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAtividadeEntrevista.Models
{
    public class BeneficiariosModel
    {
        [Required]
        [MaxLength(15)]
        [CPF(ErrorMessage = "CPF do Beneficiário Invalido")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Nome do Beneficiário requerido ")]
        public string Nome { get; set; }
    }
}