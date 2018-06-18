using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DubuqueCodeCamp.DatabaseConnection;
using Prism.Commands;
using Prism.Mvvm;
using PropertyChanged;

namespace DubuqueCodeCamp.Scheduler
{
    [AddINotifyPropertyChangedInterface]
    public class MainWindowViewModel : BindableBase
    {
        private List<ProposedSchedule> _poposedSchedules = new List<ProposedSchedule>
        {
            new ProposedSchedule
            {
                ID = 5,
                Room = new Room(),
                Session = new Session(),
                Talk = new Talk(),
                DiagnosticInformation = "Testing",
                UpdateTime = DateTime.Now
            }
        };

        public List<ProposedSchedule> ProposedSchedules
        {
            get { return _poposedSchedules.ToList(); }
            set { SetProperty(ref _poposedSchedules, value); }
        }

        public ICommand UpdateCommand { get; set; }

        public MainWindowViewModel()
        {
            UpdateCommand = new DelegateCommand(Execute, CanExecute).ObservesProperty(() => ProposedSchedules);
        }

        private bool CanExecute()
        {
            return ProposedSchedules.Any();
        }

        private void Execute()
        {
            ProposedSchedules.First().UpdateTime = DateTime.Now;
        }
    }
}
