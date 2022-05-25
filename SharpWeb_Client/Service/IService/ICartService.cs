using SharpWeb_Client.ViewModels;

namespace SharpWeb_Client.Service.IService
{
    public interface ICartService
    {
        Task DecrementCart(ShoppingCart cartToDecrement);
        Task IncrementCart(ShoppingCart cartToAdd);
    }
}
