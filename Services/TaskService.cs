using Auditore.Constants;
using Auditore.Dtos.Request;
using Auditore.Dtos.Response;
using Auditore.Models;
using Auditore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                    Tasks = TasksDto.Tasks;
                    return Tasks;
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }


            return null;
        }

        public async Task<List<MyTask>> GetTasksCategory(string catId,string token)
        {
            _client = new HttpClient();
            TasksDto = new MyTaskDto();
            Uri uri = new Uri(string.Format(HttpUris.GetMyCategoryTasks, string.Empty));
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _client.DefaultRequestHeaders.Add("id", catId);

                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    TasksDto = JsonSerializer.Deserialize<MyTaskDto>(content, _serializerOptions);
                    Tasks = TasksDto.Tasks;
                    return Tasks;
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }


            return null;
        }

        public async Task<bool> UpdateTasks(List<MyTask> tasks, string token)
        {
            _client = new HttpClient();
            List<UpdateTask> Utasks = new List<UpdateTask>();
            foreach(var task in tasks)
            {
                Utasks.Add(new UpdateTask 
                {
                    _id = task._id,
                    categoryId = task.CategoryId,
                    completed = task.Completed, 
                    description = task.Description,
                    endDate = task.EndDate,
                    name = task.Name,
                    startDate = task.StartDate,
                    taskColor = task.TaskColor,
                    userId = task.UserId,
                    __v = task.V+1
                });
            }
            UpdateTasksDto dto = new UpdateTasksDto();
            dto.Tasks = Utasks;
            Uri uri = new Uri(string.Format(HttpUris.UpdateMyTasks, string.Empty));
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using StringContent jsonContent = new(JsonSerializer.Serialize(dto.Tasks), Encoding.UTF8,"application/json");

                HttpResponseMessage response = await _client.PutAsync(uri,jsonContent);

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

        public async Task<bool> CreateTask(CreateTaskRequest dto, string token)
        {
            Uri uri = new Uri(string.Format(HttpUris.CreateTask, string.Empty));
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
            }catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteTask(string taskId,string token)
        {
            _client = new HttpClient();
            Uri uri = new Uri(string.Format(HttpUris.DeleteTask, string.Empty));
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _client.DefaultRequestHeaders.Add("id", taskId);
                HttpResponseMessage response = await _client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Eliminado", "La tarea ha sido eliminada correctamente", "Aceptar");
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

        public async Task<bool> ModifyTask(MyTask task ,string token)
        {
            _client = new HttpClient();
            Uri uri = new Uri(string.Format(HttpUris.ModifyTask, string.Empty));
            ModifyTaskRequest dto = new ModifyTaskRequest
            {
                categoryId = task.CategoryId,
                completed = task.Completed,
                description = task.Description,
                endDate = task.EndDate,
                name = task.Name,
                startDate = task.StartDate,
                userId = task.UserId
            };
            try
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                _client.DefaultRequestHeaders.Add("id", task._id);
                using StringContent jsonContent = new(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(uri, jsonContent);
                if (response.IsSuccessStatusCode)
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Modificado", "La tarea ha sido modificada correctamente", "Aceptar");
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

    }
}
