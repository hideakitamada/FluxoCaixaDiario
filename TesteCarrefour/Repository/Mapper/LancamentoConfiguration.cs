using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteCarrefour.Entity;

namespace TesteCarrefour.Repository.Mapper;

public class LancamentoConfiguration
{
    public void Configure(EntityTypeBuilder<Lancamento> builder)
    {
        builder.ToTable("Lancamentos"); // Define o nome da tabela

        builder.HasKey(l => l.Id); // Define a chave primária
        builder.Property(l => l.Id).ValueGeneratedOnAdd(); // Auto-incremento

        builder.Property(l => l.Valor)
               .IsRequired()
               .HasColumnType("decimal(18,2)"); // Define precisão para valores monetários

        builder.Property(l => l.DataLancamento)
               .IsRequired()
               .HasColumnType("datetime"); // Define o tipo de dado no banco

        builder.Property(l => l.Tipo)
               .IsRequired();
    }
}