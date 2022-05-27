using Sharp_Models;

namespace SharpWeb_Client.Service.IService
{
    public interface IAuthenticationService
    {
        Task<SignUpResponseDto> RegisterUser(SignUpRequestDto signUpRequestDto);
        Task<SignInResponseDto> Login(SignInRequestDto signInRequestDto);
        Task Logout();
    }
}
