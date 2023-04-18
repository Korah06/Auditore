using Auditore.Dtos.Request;
using Auditore.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Services.Interfaces
{
    public interface ITaskService
    {
        Task<List<MyTask>> GetTasks(string token);
        Task<bool> UpdateTasks(List<MyTask> tasks, string token);
        Task<bool> CreateTask(CreateTaskRequest dto, string token);
        Task<bool> DeleteTask(string taskId, string token);
        Task<bool> ModifyTask(MyTask task, string token);
    }
}
