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
    internal class WithdrawViewModel : INotifyPropertyChanged
    {
        private IViewer _viewer;
        private SharedDataModel _sharedDataModel;
        private WithdrawModel _withdrawModel;
        private string _withdrawSum;
        private bool _isLargeNominal = true;
        public string WithdrawSum
        {
            get { return _withdrawSum; }
            set
            {
                _withdrawSum = value;
                OnPropertyChanged(nameof(WithdrawSum));
            }
        }
        public bool LargeNominal
        {
            get { return _isLargeNominal; }
            set
            {
                _isLargeNominal= value;
                OnPropertyChanged(nameof(LargeNominal));
            }
        }
        public bool SmallNominal
        {
            get { return !_isLargeNominal; }
            set
            {
                _isLargeNominal = !value;
                OnPropertyChanged(nameof(SmallNominal));
            }
        }

        public ICommand WithdrawCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public WithdrawViewModel(
            IViewer viewer,
            SharedDataModel sharedDataModel)
        {
            _sharedDataModel = sharedDataModel;
            _viewer = viewer;
            _withdrawModel = new WithdrawModel(_sharedDataModel);

            WithdrawCommand = new RelayCommand(executeWithdrawCommand);
            BackCommand = new RelayCommand(executeBackCommand);
        }

        private void executeWithdrawCommand()
        {
            if (WithdrawSum == null || int.Parse(WithdrawSum) == 0)
            {
                PopupManager.ShowWarning("Enter the withdrawal amount");
                return;
            }
            else if (int.Parse(WithdrawSum) % 10 != 0)
            {
                PopupManager.ShowWarning("Withdrawal amount must be a multiple of: 10");
                return;
            }

            var result = _withdrawModel
                .WithdrawMoney(_isLargeNominal, int.Parse(_withdrawSum));

            if(result != null) 
            {
                PopupManager.ShowMessage(result, "Success");
                _viewer.LoadViewAsync(ViewType.Main);
            }
            else
            {
                PopupManager.ShowWarning("Not enough banknotes to issue");
            }
        }
        private void executeBackCommand()
        {
            _viewer.LoadViewAsync(ViewType.Main);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
