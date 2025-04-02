using Microsoft.EntityFrameworkCore;
using TesteCarrefour.Entity;
using TesteCarrefour.Repository.Mapper;

namespace TesteCarrefour.Services;

public class LancamentoDbContext(DbContextOptions<LancamentoDbContext> options) : DbContext(options)
{
    public DbSet<Lancamento> Lancamentos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new LancamentoConfiguration().Configure(modelBuilder.Entity<Lancamento>());
        base.OnModelCreating(modelBuilder);
    }
}