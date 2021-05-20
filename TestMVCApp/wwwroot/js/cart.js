


  // js class
  class CartModel {

    ProductId;
    Quantity;

    constructor(productId, quantity) {
      this.ProductId = productId;
      this.Quantity = quantity;
    }

  }


  AddtoCart = (productId) => {

    let quantity = Number($(`#productQuantity_${productId}`).val());

    const cart = new CartModel(productId, quantity);

    console.log('cart', cart);

    $.ajax({
      method: 'post',
      url: '/Cart/AddToCart/',
      data: cart,
      dataType: "json",
      success: function (response) {
        alert("Ürün sepetinize eklendi");
        console.log('response', response);
        renderCartDetail(response);
      },
      error: function (err) {
        alert("Sepete eklerken bir hata meydana geldi!");
        console.log('err', err);
      }

    })

  }

  renderCartDetail = (data) => {

      let template = '';

   $.each(data.items, (key, item) => {

      template += ` <li class="list-group-item d-flex justify-content-between align-items-center">
          ${item.description} x ${item.quantity}
          <span class="badge badge-primary badge-pill">${item.salesPrice}</span>
        </li>`
    });

    template += ` <li class="list-group-item active d-flex justify-content-between align-items-center">
          Toplam : ${data.total} TL
          <span class="badge badge-primary badge-pill"><a class='btn btn-secondary' href='/Order/DoOrder'> Sipariş Ver </a></span>
        </li>`


    $('.list-group').empty();
    $('.list-group').append(template);

  }



