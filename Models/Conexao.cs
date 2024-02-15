using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CrudUser.Models
{
    public class Conexao : DbContext
    {
        public Conexao(DbContextOptions<Conexao> options) : base(options)
        {

        }
        public DbSet<Pessoas> Pessoas { get; set; }
    }

}
