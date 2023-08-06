using ATM_WPF.Models;
using ATM_WPF.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ATM_WPF.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private IViewer _viewer;
        private SharedDataModel _sharedDataModel;
        private MainModel _mainModel;
        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public ICommand DepositCommand { get; set; }
        public ICommand WithdrawCommand { get; set; }

        public MainViewModel(
            IViewer viewer,
            SharedDataModel sharedDataModel)
        {
            _mainModel = new MainModel();
            _sharedDataModel = sharedDataModel;
            _viewer = viewer;

            DepositCommand = new RelayCommand(executeDepositCommand);
            WithdrawCommand = new RelayCommand(executeWithdrawCommand);

            refreshStatus();
        }

        private void executeDepositCommand()
        {
            _viewer.LoadViewAsync(ViewType.Deposit);
        }
        private void executeWithdrawCommand()
        {
            _viewer.LoadViewAsync(ViewType.Withdraw);
        }
        private void refreshStatus()
        {
            Status = _sharedDataModel.ATM.GetStatusString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
