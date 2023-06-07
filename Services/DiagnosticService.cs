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
using System.Threading.Tasks;

namespace Auditore.Services
{
    public class DiagnosticService : IDiagnosticService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        IHttpsClientHandlerService _httpsClientHandlerService;

        public string token { get; private set; }

        public DiagnosticService(IHttpsClientHandlerService service)
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

        public async Task<bool> CreateDiagnostic(Diagnostic diagnostic, string token)
        {
            _client = new HttpClient();
            string[] arr;
            Uri uri = new Uri(string.Format(HttpUris.CreateDiagnostic, string.Empty));
            if (diagnostic!=null)
            {
                if (diagnostic.tasksId.Count() == null || diagnostic.tasksId.Count() == 0)
                {
                    arr = new string[0];
                }
                else
                {
                    arr = new string[diagnostic.tasksId.Count()];

                }
            }
            
            arr = diagnostic.tasksId.ToArray();
            Dtos.Request.CreateDiagnostic dto = new Dtos.Request.CreateDiagnostic
            {
                tasksId = arr,
                idCategory = diagnostic.idCategory,
                idUser = diagnostic.idUser,
                name = diagnostic.name,
                repeats = diagnostic.repeats,
                restMinutes = diagnostic.restMinutes,
                workMinutes = diagnostic.workMinutes,
                _id = diagnostic._id
            };
            try
            {

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using StringContent jsonContent = new(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync(uri, jsonContent);

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

        public async Task<List<Diagnostic>> GetDiagnostics(string token)
        {
            DiagnosticsDto dto = new ();
            Uri uri = new Uri(string.Format(HttpUris.GetDiagnostics, string.Empty));
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    dto = JsonSerializer.Deserialize<DiagnosticsDto>(content, _serializerOptions);
                    
                    return dto.diagnostics;
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }


            return null;
        }

        public async Task<Diagnostic> GetDiagnostic(string id, string token)
        {
            DiagnosticDto dto = new();
            _client = new HttpClient();
            Uri uri = new Uri(string.Format(HttpUris.GetDiagnostic, string.Empty));
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _client.DefaultRequestHeaders.Add("id", id);

                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    dto = JsonSerializer.Deserialize<DiagnosticDto>(content, _serializerOptions);

                    return dto.diagnostic;
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }


            return null;
        }


    }
}
