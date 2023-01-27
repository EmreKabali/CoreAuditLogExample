using CoreAuditLogExample.Models;

namespace CoreAuditLogExample.Service
{
    public interface IProductService
    {
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        Product Get(int id);
    }
}
