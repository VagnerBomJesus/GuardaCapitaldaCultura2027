﻿using System;
using System.Collections.Generic;


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCapitaldaCultura2027.Models
{
    public class Reserva
    {
        [Key]
        public int ReservaId { get; set; }

        [ForeignKey("FK_EventoId")]
        public int EventoId { get; set; }

        [ForeignKey("FK_PessoaId")]
        public string PessoaId { get; set; }

        [Display(Name = "Nome")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 20 caracteres")]
        public string Nome { get; set; }

        public string Observacao { get; set; }

        [Display(Name = "Numero de pessoas para a Reserva")]
        [Range(1,Int32.MaxValue)]
        public int Numero_Reserva { get; set; }

        public Evento Evento { get; set; }

        // public Turista Turista { get; set; }
    }
}
