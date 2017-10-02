using DevStore.Domain;
using System.Data.Entity.ModelConfiguration;

namespace DevStore.Infra.Mappings
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            ToTable("Product");

            HasKey(x => x.Id);

            Property(x => x.Title).HasMaxLength(160).IsRequired();
            Property(x => x.Price).IsRequired();
            Property(x => x.AcquireDate).IsRequired();

            //IsActive não será necessário pois é um booleano.

            // Responsável por gerar a junção dos produtos com as categorias na geração do banco.
            HasRequired(x => x.Category);
            
            // Se fossem N para N, colocariamos HasMany no mapeamento dos produtos e das categorias.
        }
    }
}
