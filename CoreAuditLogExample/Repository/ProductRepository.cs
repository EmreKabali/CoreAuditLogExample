using CoreAuditLogExample.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreAuditLogExample.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void Add(Product product)
        {
            using (var context=new ProductDbContext())
            {
                var addedEntry = context.Entry<Product>(product);
                addedEntry.State = Microsoft.EntityFrameworkCore.EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(Product product)
        {
            using (var context = new ProductDbContext())
            {
                var addedEntry = context.Entry<Product>(product);
                addedEntry.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public Product ProductGetById(int id)
        {
            using (var context = new ProductDbContext())
            {
                return context.Set<Product>().SingleOrDefault(x => x.Id == id);
            }
        }

        public void Update(Product product)
        {
            using (var context = new ProductDbContext())
            {
                var addedEntry = context.Entry<Product>(product);
                addedEntry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
