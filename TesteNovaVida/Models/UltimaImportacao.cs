
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

namespace TesteNovaVida.Models
{
    public class UltimaImportacao
    {
        [Display(Name = "IdUltimaImportacao")]
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int IdUltimaImportacao {get; set;}

        [Display(Name="DataHora")]
        public DateTime DataHora {get; set;}  


    }
}
