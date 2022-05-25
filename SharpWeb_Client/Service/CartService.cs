﻿using Blazored.LocalStorage;
using Sharp_Common;
using SharpWeb_Client.Service.IService;
using SharpWeb_Client.ViewModels;

namespace SharpWeb_Client.Service
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;

        public CartService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task DecrementCart(ShoppingCart cartToDecrement)
        {
            var cart = await _localStorage.GetItemAsync<List<ShoppingCart>>(SD.ShoppingCart);

            //if count is 0 or 1 then we remove the item
            for(int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ProductId == cartToDecrement.ProductId && cart[i].ProductPriceId == cartToDecrement.ProductPriceId)
                {
                    if (cart[i].Count==1 || cart[i].Count == 0)
                    {
                        cart.Remove(cart[i]);
                    }
                    else
                    {
                        cart[i].Count -= cartToDecrement.Count;
                    }
                }
            }
            await _localStorage.SetItemAsync(SD.ShoppingCart, cart);
        }

        public async Task IncrementCart(ShoppingCart cartToAdd)
        {
            var cart = await _localStorage.GetItemAsync<List<ShoppingCart>>(SD.ShoppingCart);

            bool itemInCart = false;

            if(cart == null)
            {
                cart = new List<ShoppingCart>();
            }

            foreach (var obj in cart)
            {
                if(obj.ProductId == cartToAdd.ProductId && obj.ProductPriceId == cartToAdd.ProductPriceId)
                {
                    itemInCart = true;
                    obj.Count = cartToAdd.Count;
                }
            }
            if (!itemInCart)
            {
                cart.Add(new ShoppingCart()
                {
                    ProductId = cartToAdd.ProductId,
                    ProductPriceId = cartToAdd.ProductPriceId,
                    Count = cartToAdd.Count
                });
            }
            await _localStorage.SetItemAsync(SD.ShoppingCart, cart);
        }
    }
}
