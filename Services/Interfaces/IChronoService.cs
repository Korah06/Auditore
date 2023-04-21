using Auditore.Dtos.Request;
using Auditore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Services.Interfaces
{
    public interface IChronoService
    {
        Task<List<Chrono>> GetChronos(string token);
        Task<bool> CreateChrono(CreateChronoRequest dto, string token);
        Task<bool> DeleteChrono(string id, string token);
    }
}
