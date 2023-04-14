﻿using Auditore.Constants;
using Auditore.Dtos.Request;
using Auditore.Dtos.Response;
using Auditore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Auditore.Services
{
    public class UserService : IUserService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        IHttpsClientHandlerService _httpsClientHandlerService;

        public string token { get; private set; }

        public UserService(IHttpsClientHandlerService service)
        {
#if DEBUG
            _httpsClientHandlerService = service;
            HttpMessageHandler handler = _httpsClientHandlerService.GetPlatformMessageHandler();
            if (handler != null)
                _client = new HttpClient(handler);
            else
                _client = new HttpClient();
#else
                _client = new HttpClient();
#endif
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<string> Login(LoginRequest user)
        {
            Uri uri = new Uri(string.Format(HttpUris.Login, string.Empty));

            try
            {
                string json = JsonSerializer.Serialize<LoginRequest>(user, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response;
                response = await _client.PostAsync(uri, content);

                if (!response.IsSuccessStatusCode) 
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "El email insertado no es correcto", "Aceptar");
                        return null;
                    }else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "La contraseña insertada no es correcta", "Aceptar");
                        return null;
                    }
                }

                string result = await response.Content.ReadAsStringAsync();
                LoginResponse lresponse = JsonSerializer.Deserialize<LoginResponse>(result, _serializerOptions);
                token = lresponse.Token;
                return token;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return null;
            }
        }
    }
}
