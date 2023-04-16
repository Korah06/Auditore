using Auditore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Dtos.Request
{
    public class UpdateTasksDto
    {
        public List<UpdateTask> Tasks {  get; set; }
    }
}
