using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

public class Cart {

    [JsonPropertyName("items")]
    public List<CartItem> CarItems { get; set; } = new List<CartItem>();

    [JsonPropertyName("total")]
    public decimal TotalPrice { 
      get {
        return CarItems.Sum(x=> x.SalesPrice * x.Quantity); 
      } 
    }

}


public class CartItem {

    [JsonPropertyName("productId")]
    public string ProductId { get; set; }

     [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

      [JsonPropertyName("description")]
    public string Description { get; set; }


     [JsonPropertyName("salesPrice")]
    public decimal SalesPrice { get; set; }

}

// sepete atılan ürünler adetleri bu model içersinde saklanaca


// Cart Items => Kazak 2 adet 10 20 Gömlek 1 adet 30 30

// Cart Model ise CartItems ve TotalPrice => 3 adet ürün 50 TL
