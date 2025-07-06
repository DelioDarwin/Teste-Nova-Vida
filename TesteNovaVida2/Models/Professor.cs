using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteNovaVida2.Models
{
    public class Professor

    {
        [Display(Name = "IdProfessor")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdProfessor {get; set;}
        
        [Display(Name="NomeProfessor")]
        [Required(ErrorMessage = "Preenchimento obrigatório !")]
        [DataType(DataType.Text, ErrorMessage = "O campo nome está Vazio")]
        public string NomeProfessor {get; set;}  

       
        [ForeignKey("IdProfessor")]
        public List<Aluno>? Alunos { get; set; }

    }
}
