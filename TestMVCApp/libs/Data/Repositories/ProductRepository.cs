using System.Collections.Generic;
using System.Linq;
using TestMVCApp.libs.Data.Contexts;
using TestMVCApp.libs.Data.Entities;

namespace TestMVCApp.libs.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Product entity)
        {
            _context.Products.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(string Id)
        {
           var p =  _context.Products.Find(Id);
           _context.Products.Remove(p);
           _context.SaveChanges();

        }

        public Product Find(string Id)
        {
            return _context.Products.Find(Id);
        }

        public List<Product> toList()
        {
            return _context.Products.ToList();
        }

        public void Update(string Id, Product entity)
        {
            var dbProduct = _context.Products.Find(Id);
            _context.Entry<Product>(dbProduct).CurrentValues.SetValues(entity); // databasdeki halinin şuanki değerini entityden gelen ile değiştir.
            _context.SaveChanges();
        
        }
    }
}