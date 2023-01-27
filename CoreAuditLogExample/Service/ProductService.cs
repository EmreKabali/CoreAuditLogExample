using CoreAuditLogExample.Models;
using CoreAuditLogExample.Repository;

namespace CoreAuditLogExample.Service
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository= productRepository;
        }
        public void Add(Product product)
        {
           _productRepository.Add(product);
        }

        public void Delete(Product product)
        {
           _productRepository.Delete(product);
        }

        public Product Get(int id)
        {
           return _productRepository.ProductGetById(id);
        }

        public void Update(Product product)
        {
          _productRepository.Update(product);
        }
    }
}
