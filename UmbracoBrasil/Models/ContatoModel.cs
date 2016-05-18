using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UmbracoBrasil.Models
{
    public class ContatoModel
    {

        [Required]
        public string Assunto { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        public string Mensagem { get; set; }       
    }
}