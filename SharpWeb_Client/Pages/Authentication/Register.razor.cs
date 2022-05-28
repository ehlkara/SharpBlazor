using Microsoft.AspNetCore.Components;
using Sharp_Models;
using SharpWeb_Client.Service.IService;

namespace SharpWeb_Client.Pages.Authentication
{
    public partial class Register
    {
        private SignUpRequestDto SignUpRequest = new();
        public bool IsProcessing { get; set; } = false;
        public bool ShowRegistrationErrors { get; set; }
        public IEnumerable<string> Errors { get; set; }

        [Inject]
        public IAuthenticationService _autService { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        private async Task RegisterUser()
        {
            ShowRegistrationErrors = false;
            IsProcessing = true;
            var result = await _autService.RegisterUser(SignUpRequest);
            if (result.IsRegisterationSuccessful)
            {
                //registration is successful
                _navigationManager.NavigateTo("/login");
            }
            else
            {
                //failure
                Errors = result.Errors;
                ShowRegistrationErrors = true;
            }
            IsProcessing = false;
        }
    }
}

