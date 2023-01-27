using CoreAuditLogExample.Models;

namespace CoreAuditLogExample.Repository
{
    public interface IProductRepository
    {
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        Product ProductGetById(int id);
    }
}
