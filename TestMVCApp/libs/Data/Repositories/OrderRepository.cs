

using System.Collections.Generic;
using System.Linq;
using TestMVCApp.libs.Data.Contexts;
using TestMVCApp.libs.Data.Entities;

namespace TestMVCApp.libs.Data.Repositories
{

    public class OrderRepository : IOrderRepository
    {
        private ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Order entity)
        {
           _context.Orders.Add(entity);
           

            foreach (var item in entity.OrderItems)
            {
                _context.OrderItems.Add(item);
            }

            _context.SaveChanges();
        }

        public void Delete(string Id)
        {
             var order =  _context.Orders.Find(Id);
           _context.Orders.Remove(order);
           _context.SaveChanges();
        }

        public Order Find(string Id)
        {
            return _context.Orders.Find(Id);
        }

        public List<Order> toList()
        {
           return _context.Orders.ToList();
        }

        public void Update(string Id, Order entity)
        {
           var dbOrder = _context.Orders.Find(Id);
            _context.Entry<Order>(dbOrder).CurrentValues.SetValues(entity); // databasdeki halinin şuanki değerini entityden gelen ile değiştir.
            _context.SaveChanges();
        }
    }

}