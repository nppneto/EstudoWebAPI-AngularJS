using DevStore.Domain;
using System.Data.Entity.ModelConfiguration;

namespace DevStore.Infra.Mappings
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            // Desativar a pluralização na tabela do banco
            ToTable("Category");

            // Lambda Expression = X recebe o ID informando que é a chave primaria da tabela.
            HasKey(x => x.Id);
            Property(x => x.Title).HasMaxLength(60).IsRequired();
        }
    }
}
