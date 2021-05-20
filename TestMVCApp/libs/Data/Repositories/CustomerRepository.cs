using System.Collections.Generic;
using System.Linq;
using TestMVCApp.libs.Data.Contexts;
using TestMVCApp.libs.Data.Entities;

namespace TestMVCApp.libs.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Customer entity)
        {
            _context.Customers.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(string Id)
        {
           var p =  _context.Customers.Find(Id);
           _context.Customers.Remove(p);
           _context.SaveChanges();

        }

        public Customer Find(string Id)
        {
            // her user oluşunca default da customer olulştursak bu sıkıntı olmaz ama biz user oluşturduk-tan sonra customer oluşturduğumuz için customer sistemde olmayabilir. bundan idden değer bulunamayacak durumlarda biz find kullanamayız.
            return _context.Customers.FirstOrDefault(x=> x.Id == Id);
        }

        public List<Customer> toList()
        {
            return _context.Customers.ToList();
        }

        public void Update(string Id, Customer entity)
        {
            var dbProduct = _context.Customers.Find(Id);
            _context.Entry<Customer>(dbProduct).CurrentValues.SetValues(entity); // databasdeki halinin şuanki değerini entityden gelen ile değiştir.
            _context.SaveChanges();
        
        }
    }
}