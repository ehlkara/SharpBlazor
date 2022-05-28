using Microsoft.AspNetCore.Components;
using Sharp_Models;
using SharpWeb_Client.Service.IService;
using System.Web;

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

		public string ReturnUrl { get; set; }

		private async Task LoginUser()
        {
            ShowSignInErrors = false;
            IsProcessing = true;
            var result = await _autService.Login(SignInRequest);
            if (result.IsAuthSuccessful)
            {
                //registration is successful
                var absoluteUri = new Uri(_navigationManager.Uri);
                var queryParam = HttpUtility.ParseQueryString(absoluteUri.Query);
                ReturnUrl = queryParam["returnUrl"];
				if (string.IsNullOrEmpty(ReturnUrl))
				{
                    _navigationManager.NavigateTo("/");
                }
				else
				{
                    _navigationManager.NavigateTo("/" + ReturnUrl);
				}
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
