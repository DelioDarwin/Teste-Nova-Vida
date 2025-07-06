using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TesteNovaVida2.Models;

namespace TesteNovaVida2.Data
{
    public class TesteNovaVida2Context : DbContext
    {
        public TesteNovaVida2Context (DbContextOptions<TesteNovaVida2Context> options)
            : base(options)
        {}

        public DbSet<Professor> Professor { get; set; } = default!;
        public DbSet<Aluno> Aluno { get; set; } = default!;

        
    }
}
