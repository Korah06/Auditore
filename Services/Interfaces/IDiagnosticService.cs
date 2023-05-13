using Auditore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.Services.Interfaces
{
    public interface IDiagnosticService
    {
        Task<bool> CreateDiagnostic(Diagnostic dto, string token);
        Task<List<Diagnostic>> GetDiagnostics(string token);
        Task<Diagnostic> GetDiagnostic(string id, string token);
    }
}
