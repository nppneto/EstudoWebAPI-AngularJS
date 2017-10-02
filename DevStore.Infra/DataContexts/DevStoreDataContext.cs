using DevStore.Domain;
using DevStore.Infra.Mappings;
using System.Data.Entity;

namespace DevStore.Infra.DataContexts
{
    // Espelho para o banco de dados.
    // Cria um contexto. Praticamente cria um banco de dados virtual na memória.
    public class DevStoreDataContext : DbContext
    {
        public DevStoreDataContext() 
            : base("DevStoreConnectionString")
        {
            // Atribui um inicializador para o DevStoreDataContext, que será um novo objeto do DevStoreDataContextInitializer.
            Database.SetInitializer<DevStoreDataContext>(new DevStoreDataContextInitializer());
        }

        // Trará os dados de uma tabela, no caso Products, para uma lista.
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        // Responsável pela instância/referência dos mapeamentos para o banco de dados.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            base.OnModelCreating(modelBuilder);
        }
    }

    // Toda vez que houver uma mudança nas classes modelo (Product, Category e etc), o banco automáticamente será dropado e criado novamente.
    public class DevStoreDataContextInitializer : DropCreateDatabaseIfModelChanges<DevStoreDataContext>
    {
        protected override void Seed(DevStoreDataContext context)
        {
            context.Categories.Add(new Category { Id = 1, Title = "Informática" });
            context.Categories.Add(new Category { Id = 2, Title = "Games" });
            context.Categories.Add(new Category { Id = 3, Title = "Papelaria" });
            context.SaveChanges();

            context.Products.Add(new Product { Id = 1, CategoryId = 1, IsActive = true, Title = "Teclado Razer BlackWidow", Price = 560 });
            context.Products.Add(new Product { Id = 2, CategoryId = 1, IsActive = true, Title = "Mouse Razer Naga", Price = 799 });
            context.Products.Add(new Product { Id = 3, CategoryId = 1, IsActive = true, Title = "Mouse Pad Razer", Price = 150 });

            context.Products.Add(new Product { Id = 4, CategoryId = 2, IsActive = true, Title = "Playstation 4 Especial Edition", Price = 1500 });
            context.Products.Add(new Product { Id = 5, CategoryId = 2, IsActive = true, Title = "God Of War 4", Price = 299 });
            context.Products.Add(new Product { Id = 6, CategoryId = 2, IsActive = true, Title = "HeadSet Sony 7.1", Price = 700  });

            context.Products.Add(new Product { Id = 7, CategoryId = 3, IsActive = true, Title = "Caderno Tilibra", Price = 35 });
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
