using System;

namespace DevStore.Domain
{
    public class Product
    {

        public Product()
        {
            this.AcquireDate = DateTime.Now;
        }


        public int Id { get; set; }

        public string Title { get; set; }

        public decimal Price { get; set; }

        public DateTime AcquireDate { get; set; }

        public bool IsActive { get; set; }

        // Chave estrangeira para entendimento do entity
        public int CategoryId { get; set; }

        // Virtual permite que a classe ou atributo seja sobrescrito em tempo de execução
        // referencia ao banco 1 pra 1 = Um produto tem uma categoria.
        // No caso de N para N, public virtual ICollection<Product> Products { get; set; } / public virtual ICollection<Category> Categories { get; set; }
        public virtual Category Category{ get; set; }


        public override string ToString()
        {
            return this.Title;
        }
    }
}
