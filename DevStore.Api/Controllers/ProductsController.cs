using DevStore.Domain;
using DevStore.Infra.DataContexts;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DevStore.Api.Controllers
{
    // origins = quem poderá acessar a api
    // headers = quais headers a api aceitará
    // methods = quais métodos a api aceitará
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    // Define o prefixo para todas as rotas.
    [RoutePrefix("api/v1/public")]
    public class ProductsController : ApiController
    {
        //// instância do dbcontext... poderia ser de qualquer repositório
        private DevStoreDataContext db = new DevStoreDataContext();

        [Route("products")]
        public HttpResponseMessage GetProducts()
        {
            var result = db.Products.Include("Category").ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("categories")]
        public HttpResponseMessage GetCategories()
        {
            var result = db.Categories.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("categories/{categoryId}/products")]
        public HttpResponseMessage GetProductsByCategories(int categoryId)
        {
            var result = db.Products.Include("Category").Where(x => x.CategoryId == categoryId).ToList();
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
 
        [HttpPost]
        [Route("products")]
        public HttpResponseMessage PostProduct(Product product)
        {
            if (product == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            // Operações persistidas (dados e informações) sempre com try/catch
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

        [HttpPatch]
        [Route("products")]
        // Atualiza parcialmente um produto
        public HttpResponseMessage PatchProduct(Product product)
        {
            if (product == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                // Informo que o produto foi modificado
                db.Entry<Product>(product).State = EntityState.Modified;
                db.SaveChanges();

                var result = product;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar o produto");
            }

        }

        [HttpPut]
        [Route("products")]
        // Atualiza completamente um produto
        public HttpResponseMessage PutProduct(Product product)
        {
            if (product == null)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            try
            {
                // Informo que o produto foi modificado
                db.Entry<Product>(product).State = EntityState.Modified;
                db.SaveChanges();

                var result = product;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao alterar o produto");
            }
        }

        [HttpDelete]
        [Route("products")]
        public HttpResponseMessage DeleteProduct(int productId)
        {
            // Ou int non-nullable -- if(productId <= 0)
            if(productId <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                db.Products.Remove(db.Products.Find(productId));
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, "Produto excluído!");
            }
            catch (Exception)
            {

                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Falha ao excluir o produto");
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