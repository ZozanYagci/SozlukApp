﻿using Blazored.LocalStorage;
using SozlukApp.WebApp.Infrastructure.Extensions;

namespace SozlukApp.WebApp.Infrastructure.Auth
{
    public class AuthTokenHandler:DelegatingHandler
    {
        private readonly ISyncLocalStorageService _syncLocalStorageService;

        public AuthTokenHandler(ISyncLocalStorageService syncLocalStorageService)
        {
            _syncLocalStorageService = syncLocalStorageService;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _syncLocalStorageService.GetToken();

            if(!string.IsNullOrEmpty(token) && request.Headers.Authorization is null)
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
