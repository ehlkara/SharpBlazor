using Microsoft.AspNetCore.Components;
using Sharp_Models;
using SharpWeb_Client.Service.IService;

namespace SharpWeb_Client.Pages.Authentication
{
    public partial class Login
    {
        private SignInRequestDto SignInRequest = new();
        public bool IsProcessing { get; set; } = false;
        public bool ShowSignInErrors { get; set; }
        public string Errors { get; set; }

        [Inject]
        public IAuthenticationService _autService { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        private async Task LoginUser()
        {
            ShowSignInErrors = false;
            IsProcessing = true;
            var result = await _autService.Login(SignInRequest);
            if (result.IsAuthSuccessful)
            {
                //registration is successful
                _navigationManager.NavigateTo("/");
            }
            else
            {
                //failure
                Errors = result.ErrorMessage;
                ShowSignInErrors = true;
            }
            IsProcessing = false;
        }
    }
}
