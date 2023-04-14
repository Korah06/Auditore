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
    public class TaskService : ITaskService
    {
        HttpClient _client;
        JsonSerializerOptions _serializerOptions;
        IHttpsClientHandlerService _httpsClientHandlerService;

        public MyTaskDto TasksDto { get; private set; }
        public List<MyTask> Tasks { get; private set; }

        public TaskService(IHttpsClientHandlerService service)
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

        public async Task<List<MyTask>> GetTasks(string token)
        {
            TasksDto = new MyTaskDto();
            Uri uri = new Uri(string.Format(HttpUris.GetMyTasks, string.Empty));
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    TasksDto = JsonSerializer.Deserialize<MyTaskDto>(content, _serializerOptions);
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
