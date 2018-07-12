using DubuqueCodeCamp.DatabaseConnection;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Prism.Events;

namespace DubuqueCodeCamp.Scheduler
{
    public class ScheduleViewModel : BindableBase
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

        public ICommand CreateScheduleCommand { get; set; }
        public ScheduleViewModel(IEventAggregator eventAggregator)
        {
            CreateScheduleCommand =
                new DelegateCommand(ExecuteCreateSchedule, CanExecuteCreateSchedule).ObservesProperty(() => ProposedSchedules);
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
    }
}





<PropertyChanged />
