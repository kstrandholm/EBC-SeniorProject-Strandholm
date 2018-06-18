using DubuqueCodeCamp.DatabaseConnection;
using Prism.Mvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Prism.Commands;
using Prism.Events;

namespace DubuqueCodeCamp.Scheduler
{
    [AddINotifyPropertyChangedInterface]
    public class SessionsViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        private List<Session> _sessions = Schedule.GetExistingSessions(DateTime.Today);

        /// <summary>
        /// List of Proposed Schedules currently in the database
        /// </summary>
        public List<Session> Sessions
        {
            get => _sessions;
            set => SetProperty(ref _sessions, value);
        }

        public ICommand AddNewSessionCommand { get; set; }

        public SessionsViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            AddNewSessionCommand = new DelegateCommand(ExecuteAddSession, CanExecuteAddSession).ObservesProperty(() => Sessions);
        }

        private bool CanExecuteAddSession()
        {
            return true;
        }

        private void ExecuteAddSession()
        {
            _eventAggregator.GetEvent<UpdateEvent>().Publish("testing");
        }
    }
}
