using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Servcom.SGA.Domain.Usuarios;
using System.Linq;

namespace Servcom.SGA.Infra.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(u => u.Nome)
               .HasColumnType("varchar(150)")
               .IsRequired();

            builder.Property(u => u.Setor)
          .HasColumnType("varchar(150)")
          .IsRequired();


            builder.Property(u => u.Sigla)
               .HasColumnType("varchar(3)")
               .IsRequired();

            builder.Ignore(u => u.ValidationResult);

            builder.Ignore(u => u.CascadeMode);

            builder.ToTable("Usuarios");
        }
    }
}
