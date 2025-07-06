using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TesteNovaVida.Models;

namespace TesteNovaVida.Data
{
    public class TesteNovaVidaContext : DbContext
    {
        public TesteNovaVidaContext (DbContextOptions<TesteNovaVidaContext> options)
            : base(options)
        {}

        public DbSet<Professor> Professor { get; set; } = default!;
        public DbSet<Aluno> Aluno { get; set; } = default!;
        public DbSet<UltimaImportacao> UltimaImportacao { get; set; } = default!;
        
        
    }
}
