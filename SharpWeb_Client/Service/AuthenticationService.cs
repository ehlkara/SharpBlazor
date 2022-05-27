﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using Sharp_Common;
using Sharp_Models;
using SharpWeb_Client.Service.IService;
using System.Net.Http.Headers;
using System.Text;

namespace SharpWeb_Client.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthenticationService(HttpClient client, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _client = client;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }

        public async Task<SignInResponseDto> Login(SignInRequestDto signInRequest)
        {
            var content = JsonConvert.SerializeObject(signInRequest);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("api/account/signin", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<SignInResponseDto>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                await _localStorage.SetItemAsync(SD.Local_Token, result.Token);
                await _localStorage.SetItemAsync(SD.Local_UserDetails, result.UserDto);
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                return new SignInResponseDto() { IsAuthSuccessful = true };
            }
            else
            {
                return result;
            }
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public Task<SignUpResponseDto> RegisterUser(SignUpRequestDto signUpRequest)
        {
            throw new NotImplementedException();
        }
    }
}
