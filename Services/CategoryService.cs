using Auditore.Constants;
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
    public class CategoryService : ICategoryService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        IHttpsClientHandlerService _httpsClientHandlerService;
        public CategoryService(IHttpsClientHandlerService service)
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


        public async Task<List<Category>> GetCategories(string token)
        {
            CategoryDto dto = new CategoryDto();
            Uri uri = new Uri(string.Format(HttpUris.GetMyCategories, string.Empty));
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    dto = JsonSerializer.Deserialize<CategoryDto>(content, _serializerOptions);
                    return dto.Categories;
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }
            return null;
        }

        public async Task<string> CreateCategory(string catName, string token)
        {
            Uri uri = new Uri(string.Format(HttpUris.CreateCategory, string.Empty));
            CreateCategoryRequest dto = new CreateCategoryRequest();
            dto.name = catName;
            var r = new Random();
            dto.color = Color.FromRgb(r.Next(0, 255), r.Next(0, 255), r.Next(0, 255)).ToHex();
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using StringContent jsonContent = new(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(uri,jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    string newCatId = JsonSerializer.Deserialize<string>(content, _serializerOptions);
                    return newCatId;
                }
                return null;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return null;
            }
        }
    }
}
