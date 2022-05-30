using Sharp_Models;
using SharpWeb_Client.ViewModels;

namespace SharpWeb_Client.Service.IService
{
    public interface IPaymentService
    {
        Task<SuccessModelDto> Checkout(StripePaymentDto model);
    }
}
