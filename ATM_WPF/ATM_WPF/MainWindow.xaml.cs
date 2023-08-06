using ATM_WPF.Models;
using ATM_WPF.ViewModels;
using ATM_WPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ATM_WPF
{
    public interface IViewer
    {
        void LoadViewAsync(ViewType typeView);
    }

    public enum ViewType
    {
        Main,
        Deposit,
        Withdraw
    }
    public partial class MainWindow : Window, IViewer
    {
        private SharedDataModel _sharedDataModel;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            _sharedDataModel = new SharedDataModel();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadViewAsync(ViewType.Main);
        }

        public async void LoadViewAsync(ViewType typeView)
        {
            switch (typeView)
            {
                case ViewType.Main:
                    var mainView = new MainUC();
                    var mainViewModel = new MainViewModel(this, _sharedDataModel);
                    mainView.DataContext = mainViewModel;
                    OutputView.Content = mainView;
                    break;

                case ViewType.Deposit:
                    var depositView = new DepositUC();
                    var depositViewModel = new DepositViewModel(this, _sharedDataModel);
                    depositView.DataContext = depositViewModel;
                    OutputView.Content = depositView;
                    break;

                case ViewType.Withdraw:
                    var withdrawView = new WithdrawUC();
                    var withdrawViewModel = new WithdrawViewModel(this, _sharedDataModel);
                    withdrawView.DataContext = withdrawViewModel;
                    OutputView.Content = withdrawView;
                    break;

                default:
                    break;
            }
        }
    }
}
