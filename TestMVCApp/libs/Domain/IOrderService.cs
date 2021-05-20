

using TestMVCApp.libs.Data.Entities;
using TestMVCApp.Models;

namespace TestMVCApp.libs.Domain
{

  public enum OrderStatus {
    OK,
    Reject
  }


    public interface IOrderService: IDomainService
    {
          string orderCode {get;}

          OrderStatus Status {get;}


            // cart bilgileri siparişe çevrilecek
            void DoOrder(Cart cart, OrderInputModel model);
            Order GetOrderByCode(string orderCode);
            

    }

}