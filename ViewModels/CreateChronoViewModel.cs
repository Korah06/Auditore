using Auditore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.ViewModels
{
    public class CreateChronoViewModel
    {
        private IChronoService _chronoService;
        public CreateChronoViewModel(IChronoService chronoService) 
        {
            _chronoService = chronoService;
        }

    }
}
