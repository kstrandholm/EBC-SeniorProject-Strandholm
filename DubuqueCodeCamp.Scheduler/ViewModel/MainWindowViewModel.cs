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
        private List<ProposedSchedule> _proposedSchedules = Schedule.GetProposedSchedule(DateTime.Now);

        /// <summary>
        /// List of Proposed Schedules currently in the database
        /// </summary>
        public List<ProposedSchedule> ProposedSchedules
        {
            get => _proposedSchedules;
            set => SetProperty(ref _proposedSchedules, value);
        }

        private List<Session> _sessions = Schedule.GetExistingSessions(DateTime.Today);

        /// <summary>
        /// List of Proposed Schedules currently in the database
        /// </summary>
        public List<Session> Sessions
        {
            get => _sessions;
            set => SetProperty(ref _sessions, value);
        }

        public ICommand CreateScheduleCommand { get; set; }

        public ICommand AddNewSessionCommand { get; set; }

        public MainWindowViewModel()
        {
            CreateScheduleCommand = new DelegateCommand(ExecuteCreateSchedule, CanExecuteCreateSchedule).ObservesProperty(() => ProposedSchedules);
            AddNewSessionCommand = new DelegateCommand(ExecuteAddSession, CanExecuteAddSession).ObservesProperty(() => Sessions);
        }

        private bool CanExecuteCreateSchedule()
        {
            return ProposedSchedules.Any();
        }

        private void ExecuteCreateSchedule()
        {
            Schedule.CreateProposedSchedule(DateTime.Today);
            ProposedSchedules = Schedule.GetProposedSchedule(DateTime.Today);
        }

        private bool CanExecuteAddSession()
        {
            return ProposedSchedules.Any();
        }

        private void ExecuteAddSession()
        {
            Schedule.CreateProposedSchedule(DateTime.Today);
            ProposedSchedules = Schedule.GetProposedSchedule(DateTime.Today);
        }
    }
}
