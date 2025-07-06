using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TesteNovaVida2.Models
{
    public class Aluno

    {
        [Display(Name = "IdAluno")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdAluno {get; set;}

        [Display(Name = "IdProfessor")]
        [ForeignKey("Professor")]
        public int IdProfessor {get; set;}
        
        [Display(Name="NomeAluno")]
        [Required(ErrorMessage = "Preenchimento obrigatório !")]
        [DataType(DataType.Text, ErrorMessage = "O campo nome está Vazio")]
        public string? NomeAluno {get; set;}  

        [Display(Name="ValorMensalidade")]
        [Required(ErrorMessage = "Preenchimento obrigatório !")]
        [DataType(DataType.Currency)]
        public decimal? ValorMensalidade {get; set;}  

        [Display(Name="DataVencimento")]
        public DateTime DataVencimento {get; set;}  

        //[IgnoreDataMember]
        public virtual Professor? Professor { get; set; }



    }
}
