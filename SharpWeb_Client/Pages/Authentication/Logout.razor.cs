using Microsoft.AspNetCore.Components;
using SharpWeb_Client.Service.IService;

namespace SharpWeb_Client.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService _autService { get; set; }
        [Inject]
        public NavigationManager _navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await _autService.Logout();
            _navigationManager.NavigateTo("/");
        }
    }
}
