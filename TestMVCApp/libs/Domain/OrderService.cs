

using TestMVCApp.libs.API;
using TestMVCApp.libs.Data.Entities;
using TestMVCApp.libs.Data.Repositories;
using TestMVCApp.Models;

namespace TestMVCApp.libs.Domain
{


    public class OrderService : IOrderService
    {

        private ICreditCardService _cardService;
        private IOrderRepository _orderRepository;

        public OrderService(ICreditCardService cardService, IOrderRepository orderRepository)
        {
            _cardService = cardService;
            _orderRepository = orderRepository;
        }


        public OrderService(string orderCode)
        {
            this.orderCode = orderCode;

        }
        public string orderCode { get; private set; }

        public OrderStatus Status {get; private set;}


        // bu servis ile siparişimiz oluşmuş oldu
        public void DoOrder(Cart cart, OrderInputModel model)
        {

            var cardModel = new CreditCardModel();
            cardModel.CardNumber = model.CreditDetail.Credit;
            cardModel.CVV = model.CreditDetail.CVV;
            cardModel.ValidThru = model.CreditDetail.ValidThru;
            cardModel.OwnerName = model.CreditDetail.CardUser;
        

            var result = _cardService.MakeCreditRequest(cardModel, cart.TotalPrice);

            //  ödeme alındıysa
            if (result == CreditServiceResult.Paid)
            {
                // sipariş oluşturabiliriz.

                var order = new Order();
                order.OrderCode = System.Guid.NewGuid().ToString();
                order.OrderDate = System.DateTime.Now;
                order.CustomerName = cardModel.OwnerName;
                order.ShipingAddress = model.CustomerDetail.ShipAddress;
                order.ShippedDate =  System.DateTime.Now;
                order.CustomerName = model.CustomerDetail.FullName;
                order.ContactNumber = model.CustomerDetail.PhoneNumber;

                foreach (var item in cart.CarItems)
                {
                    var OrderItem = new OrderItem();
                    OrderItem.Quantity = item.Quantity;
                    OrderItem.DiscountRate = 0;
                    OrderItem.ProductId = item.ProductId;
                    OrderItem.OrderId = order.OrderId;
                    

                    order.OrderItems.Add(OrderItem);

                }


                Status = OrderStatus.OK;
                orderCode = order.OrderId;
                _orderRepository.Create(order);

            }
            else {
                orderCode = null;
                Status = OrderStatus.Reject;
            }

            // Do Order sonrasında dbye kayıt atıp sipariş kodunu orderCode olarak propertye set edeceğiz.
        }

        public Order GetOrderByCode(string orderCode)
        {
            throw new System.NotImplementedException();
        }
    }

}