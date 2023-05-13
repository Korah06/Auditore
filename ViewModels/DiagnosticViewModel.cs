using Auditore.Models;
using Auditore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.ViewModels
{
    public class DiagnosticViewModel
    {
        private Diagnostic _diagnostic;
        public Diagnostic Diagnostic
        {
            get { return _diagnostic; }
            set
            {
                if (_diagnostic == value) { return; }
                _diagnostic = value;
                OnPropertyChanged(nameof(Diagnostic));

            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IDiagnosticService _diagnosticService;
        public DiagnosticViewModel(IDiagnosticService diagnosticService)
        {
            _diagnosticService = diagnosticService;
            GetDiagnostic();
        }

        public async void GetDiagnostic()
        {
            Diagnostic = await _diagnosticService.GetDiagnostic(
                Preferences.Default.Get("diagnosticId", ""), Preferences.Default.Get("token", ""));
        }
    }
}
