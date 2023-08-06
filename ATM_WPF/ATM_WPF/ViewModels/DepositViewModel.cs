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
    internal class DepositViewModel : INotifyPropertyChanged
    {
        private IViewer _viewer;
        private SharedDataModel _sharedDataModel;
        private DepositModel _depositModel;

        private Dictionary<int, int> _nominals = new Dictionary<int, int>
        {
            { 10, 0 },
            { 50, 0 },
            { 100, 0 },
            { 200, 0 },
            { 500, 0 },
            { 1000, 0 },
            { 2000, 0 },
            { 5000, 0 }
        };
        private int _totalSum => _nominals.Sum(n => n.Key * n.Value);
        private string _totalSumString => "Total amount: "
            + _totalSum.ToString()
            + " RUB";

        public string TotalSum 
        {
            get { return _totalSumString; }
        }
        public string Nominal10
        {
            get { return _nominals[10].ToString(); }
            set
            {
                _nominals[10] = int.Parse(value);
                OnPropertyChanged(nameof(Nominal10));
                OnPropertyChanged(nameof(TotalSum));
            }
        }
        public string Nominal50
        {
            get { return _nominals[50].ToString(); }
            set
            {
                _nominals[50] = int.Parse(value);
                OnPropertyChanged(nameof(Nominal50));
                OnPropertyChanged(nameof(TotalSum));
            }
        }
        public string Nominal100
        {
            get { return _nominals[100].ToString(); }
            set
            {
                _nominals[100] = int.Parse(value);
                OnPropertyChanged(nameof(Nominal100));
                OnPropertyChanged(nameof(TotalSum));
            }
        }
        public string Nominal200
        {
            get { return _nominals[200].ToString(); }
            set
            {
                _nominals[200] = int.Parse(value);
                OnPropertyChanged(nameof(Nominal200));
                OnPropertyChanged(nameof(TotalSum));
            }
        }
        public string Nominal500
        {
            get { return _nominals[500].ToString(); }
            set
            {
                _nominals[500] = int.Parse(value);
                OnPropertyChanged(nameof(Nominal500));
                OnPropertyChanged(nameof(TotalSum));
            }
        }
        public string Nominal1000
        {
            get { return _nominals[1000].ToString(); }
            set
            {
                _nominals[1000] = int.Parse(value);
                OnPropertyChanged(nameof(Nominal1000));
                OnPropertyChanged(nameof(TotalSum));
            }
        }
        public string Nominal2000
        {
            get { return _nominals[2000].ToString(); }
            set
            {
                _nominals[2000] = int.Parse(value);
                OnPropertyChanged(nameof(Nominal2000));
                OnPropertyChanged(nameof(TotalSum));
            }
        }
        public string Nominal5000
        {
            get { return _nominals[5000].ToString(); }
            set
            {
                _nominals[5000] = int.Parse(value);
                OnPropertyChanged(nameof(Nominal5000));
                OnPropertyChanged(nameof(TotalSum));
            }
        }

        public ICommand DepositCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public DepositViewModel(
            IViewer viewer,
            SharedDataModel sharedDataModel)
        {
            _sharedDataModel = sharedDataModel;
            _viewer = viewer;
            _depositModel = new DepositModel(_sharedDataModel);

            DepositCommand = new RelayCommand(executeDepositCommand);
            BackCommand = new RelayCommand(executeBackCommand);
        }

        private void executeDepositCommand()
        {
            if (_totalSum == 0)
            {
                PopupManager.ShowWarning("You have not deposited a single banknote");
                return;
            }

            if (_depositModel.DepositMoney(_nominals))
            {
                PopupManager.ShowMessage("Money has been successfully deposited", "Success");
                _viewer.LoadViewAsync(ViewType.Main);
            }
            else
            {
                PopupManager.ShowWarning("Money was not deposited. There is not enough space in cash cassettes");
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
