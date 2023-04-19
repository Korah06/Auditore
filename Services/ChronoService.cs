using Auditore.Constants;
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

namespace Auditore.Services
{
    class ChronoService : IChronoService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        IHttpsClientHandlerService _httpsClientHandlerService;
        public ChronoService(IHttpsClientHandlerService service)
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
        public async Task<List<Chrono>> GetChronos(string token)
        {
            ChronoDto dto = new ChronoDto();
            List<Chrono> chronos = new List<Chrono>();
            Uri uri = new Uri(string.Format(HttpUris.GetChronos, string.Empty));
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    dto = JsonSerializer.Deserialize<ChronoDto>(content, _serializerOptions);

                    return dto.chronos;
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
