using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Servcom.SGA.Domain.Atendimentos;
using Servcom.SGA.Domain.Configuracao;

namespace Servcom.SGA.Infra.Data.Mappings
{
    public class ConfiguracaoConteudoMapping : IEntityTypeConfiguration<ConfiguracaoConteudo>
    {
        public void Configure(EntityTypeBuilder<ConfiguracaoConteudo> builder)
        {
                            
            builder.Ignore(a => a.ValidationResult);
            builder.Ignore(a => a.CascadeMode);
            builder.ToTable("ConfiguracaoConteudo");
        }

        
    }
}
