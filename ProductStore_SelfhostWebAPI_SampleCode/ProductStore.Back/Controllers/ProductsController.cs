using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OwinSelfhostSample
{
     [EnableCors(origins: "http://localhost:2338", headers: "*", methods: "*")]
    public class ProductsController : ApiController
    {
        static readonly IProductRepository repository = new ProductRepository();

        // GET /api/products/GetAllProducts
        public IEnumerable<Product> GetAllProducts()
        {
            return repository.GetAll();
        }

        // GET /api/products/GetProduct/id
        public Product GetProduct(int id)
        {
            var product = repository.Get(id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return product;
        }

        //GET  /api/products/GetProductsByCategory/?category=category
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return repository
                .GetAll()
                .Where(r => string.Equals(r.Category, category))
                .Select(r => r);
        }

        // POST api/products insert;在Action 方法中我们需要使用 [FromBody] 属性标签来标明属性。
        public void Post([FromBody]Product product)
        {
            repository.Add(product);
        }

        // PUT api/products/5 update
        public void Put(int id, [FromBody]Product product)
        {
            product.Id = id;
            if (!repository.Update(product))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        // DELETE api/products/5 
        public void Delete(int id)
        {
            Product item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            repository.Remove(id);
        }
    } 
}
