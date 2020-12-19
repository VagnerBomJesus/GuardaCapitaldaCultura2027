﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GuardaCapitaldaCultura2027.Models
{
    public class Evento
    {

        [Key]
        public int EventoId { get; set; }


        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Por favor, insira o seu Nome")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 20 caracteres")]

        public string Name { get; set; }

        [Required(ErrorMessage = "Por favor, insira a Descrição")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "A descrição deve ter entre 10 e 500 caracteres")]

        public string Descricao { get; set; }

        [Display(Name = "Data de Realização")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        //[DataType(DataType.Date)]
        public DateTime Data_realizacao { get; set; }


        [Display(Name = "Lotação Maxima")]
        public int Lotacao_max { get; set; }

        [Display(Name = "Lotação Ocupada")]
        public int Lotacao_Ocupada { get; set; }
        
        [ForeignKey("FK_MunicipioId")]
        [Display(Name = "Municipio")]
        public int MunicipioId { get; set; }

        public Municipio Municipio { get; set; }

        public ICollection<Municipio> Municipios { get; set; }
    }
}
