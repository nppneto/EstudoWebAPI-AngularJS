using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DevStore.Domain;
using DevStore.Infra.DataContexts;

namespace DevStore.Api.Controllers
{
    // Define o prefixo para todas as rotas.
    [RoutePrefix("api/v1/public")]
    public class ProductsController : ApiController
    {
        // instância do dbcontext... poderia ser de qualquer repositório
        private DevStoreDataContext db = new DevStoreDataContext();

        // Define rota do GetProducts, no caso... Por boas praticas, sempre no plural.
        [Route("products")]
        // Action do tipo HttpResponseMessage para retorno
        public HttpResponseMessage GetProducts()
        {
            var result = db.Products.Include("Category").ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("categories")]
        // Action do tipo HttpResponseMessage para retorno
        public HttpResponseMessage GetCategories()
        {
            var result = db.Categories.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // Sempre que entre chaves, um nome se torna parâmetro para a URL.
        [Route("categories/{categoryId}/products")]
        // Action do tipo HttpResponseMessage para retorno
        public HttpResponseMessage GetProductsByCategories(int categoryId)
        {
            // x receberá o resultado da comparação entre a propriedade CategoryId, da classe Product que está na IDBset<Product>, com o valor recebido por parâmetro.
            var result = db.Products.Include("Category").Where(x => x.CategoryId == categoryId).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("products")]
        [Route("categories/{categoryId}/products")]
        public HttpResponseMessage PostProduct(Product product)
        {
            if (product == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            // Sempre que trabalhamos com operações persistidas (dados e informações), fazemos tratamento com try/catch
            try
            {
                db.Products.Add(product);
                db.SaveChanges();

                var result = product;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao incluir produto.");
            }

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
        }

        //private bool ProductExists(int id)
        //{
        //    return db.Products.Count(e => e.Id == id) > 0;
        //}
    }
}