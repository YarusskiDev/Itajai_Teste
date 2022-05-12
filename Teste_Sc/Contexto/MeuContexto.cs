using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste_Sc.Models;

namespace Teste_Sc.Contexto
{
    public class MeuContexto:DbContext
    {

        public MeuContexto(DbContextOptions<MeuContexto> options)
           : base(options)
        {
        }
        public DbSet<Distritos> Distritos { get; set; }

    }
}
