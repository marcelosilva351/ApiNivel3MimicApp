using API1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API1.Data
{
    public class MimicContext : DbContext
    {
        public DbSet<Palavra> Palavras { get; set; }
        public MimicContext(DbContextOptions options) : base(options)
        {
        }
    }
}
