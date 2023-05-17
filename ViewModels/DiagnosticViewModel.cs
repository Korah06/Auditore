using Auditore.Models;
using Auditore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditore.ViewModels
{
    public class DiagnosticViewModel : INotifyPropertyChanged
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

        private string _date;
        public string Date
        {
            get { return _date; }
            set
            {
                if (_date == value) { return; }
                _date = value;
                OnPropertyChanged(nameof(Date));

            }
        }
        private string _chronoName;
        public string ChronoName
        {
            get { return _chronoName; }
            set
            {
                if (_chronoName == value) { return; }
                _chronoName = value;
                OnPropertyChanged(nameof(ChronoName));

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
        public ObservableCollection<string> Tasks { get; set; } = new ObservableCollection<string>();
        public async void GetDiagnostic()
        {
            Diagnostic = await _diagnosticService.GetDiagnostic(
                Preferences.Default.Get("diagnosticId", ""), Preferences.Default.Get("token", ""));

            foreach(string d in Diagnostic.tasksId)
            {
                Tasks.Add(d);
            }
            string[] strings = Diagnostic.name.Split("-");
            ChronoName = strings[0];
            Date = strings[1];
        }
    }
}
