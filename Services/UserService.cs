﻿using Auditore.Constants;
using Auditore.Dtos.Request;
using Auditore.Dtos.Response;
using Auditore.Models;
using Auditore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
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
            _client = new HttpClient();
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
                await Application.Current.MainPage.DisplayAlert
                        ("Error", "Ha ocurrido algún error de red", "Aceptar");
                return null;
            }
        }

        public async Task<bool> Register(RegisterRequest user)
        {
            _client = new HttpClient();
            Uri uri = new Uri(string.Format(HttpUris.Register, string.Empty));
            try
            {
                user.avatar = "treeprofile";
                user.banner = "vicesky";
                string json = JsonSerializer.Serialize<RegisterRequest>(user, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response;
                response = await _client.PostAsync(uri, content);

                if(response.StatusCode == System.Net.HttpStatusCode.NotAcceptable) 
                {
                    await Application.Current.MainPage.DisplayAlert
                        ("Error", "Ya existe un usuario con la misma dirección de correo", "Aceptar");
                    return false;
                }

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return false;
            }
        }

        public async Task<User> GetUser(string token)
        {
            _client = new HttpClient();
            UserDto dto = new UserDto();
            Uri uri = new Uri(string.Format(HttpUris.getUser, string.Empty));
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    dto = JsonSerializer.Deserialize<UserDto>(content, _serializerOptions);
                    User user = dto.user;
                    return user;
                }
                return null;
            } catch(Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return null;
            }
        }

        public async Task<bool> GetRole(string token)
        {
            _client = new HttpClient();
            bool isAdmin = false;
            Uri uri = new Uri(string.Format(HttpUris.getRole, string.Empty));
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    MyRoleDto dto = JsonSerializer.Deserialize<MyRoleDto>(content, _serializerOptions);
                    isAdmin = dto.role;
                    return isAdmin;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteUser(string userId, string token)
        {
            _client = new HttpClient();
            Uri uri = new Uri(string.Format(HttpUris.deleteUser, string.Empty));
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _client.DefaultRequestHeaders.Add("id", userId);
                HttpResponseMessage response = await _client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Eliminado", "El usuario ha sido eliminado correctamente", "Aceptar");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return false;

        }

        public async Task<bool> ModifyUser(User user, string token)
        {
            _client = new HttpClient();
            Uri uri = new Uri(string.Format(HttpUris.modifyUser, string.Empty));
            ModifyUserRequest dto = new ModifyUserRequest
            {
                email = user.email, 
                password = user.password, 
                username = user.username,
                name = user.name,
                rol = user.rol,
                surname = user.surname,
                avatar = user.avatar,
                banner = user.banner
            };

            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _client.DefaultRequestHeaders.Add("id", user._id);
                using StringContent jsonContent = new(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(uri, jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Modificado", "El usuario ha sido modificado correctamente", "Aceptar");
                    return true;
                }
                await Application.Current.MainPage
                        .DisplayAlert("Error", "Ha ocurrido algun tipo de error", "Aceptar");
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return false;
            }

        }

        public async Task<List<User>> GetUsers(string token)
        {
            _client = new HttpClient();
            Uri uri = new Uri(string.Format(HttpUris.getUsers, string.Empty));
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    GetUsersDto dto = JsonSerializer.Deserialize<GetUsersDto>(content, _serializerOptions);
                    return dto.users;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return null;
        }
    }
}
