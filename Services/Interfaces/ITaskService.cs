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
    }
}
