using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Servcom.SGA.Domain.Configuracao;

namespace Servcom.SGA.Infra.Data.Mappings
{
    public class ConfiguracaoGeralMapping : IEntityTypeConfiguration<ConfiguracaoGeral>
    {
        public void Configure(EntityTypeBuilder<ConfiguracaoGeral> builder)
        {
                            
            builder.Ignore(a => a.ValidationResult);
            builder.Ignore(a => a.CascadeMode);
            builder.ToTable("ConfiguracaoGeral");
        }

        
    }
}
