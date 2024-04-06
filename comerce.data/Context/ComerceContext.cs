using comerce.domain.model;
using comerce.domain.Model;
using Microsoft.EntityFrameworkCore;

namespace comerce.data.context
{
    public class ComerceContext : DbContext
    {
        public ComerceContext(DbContextOptions<ComerceContext> options) : base(options)
        {
            
        }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<__ScriptMigrationHistory> __ScriptMigrationHistory { get; set; }
    }
}