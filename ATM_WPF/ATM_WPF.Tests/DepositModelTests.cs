using ATM_WPF.AtmClasses;
using ATM_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_WPF.Tests
{
    public class DepositModelTests
    {
        private SharedDataModel _sharedDataModel;
        private DepositModel _depositModel;
        private ATM _atm;

        [SetUp]
        public void Setup()
        {
            _sharedDataModel = new SharedDataModel();
            _depositModel = new DepositModel(_sharedDataModel);
            _atm = _sharedDataModel.ATM;
        }

        [Test]
        public void DepositMoney_ShouldReturnTrueIfDepositedSuccessfully()
        {
            var banknotesToDeposit = new Dictionary<int, int>
            {
                { 100, 5 },
                { 50, 10 }
            };

            var nominal100TempCount = _atm.GetBanknoteByNominal(100).Count;
            var nominal50TempCount = _atm.GetBanknoteByNominal(50).Count;

            bool result = _depositModel.DepositMoney(banknotesToDeposit);

            Assert.IsTrue(result);
            Assert.AreEqual(5 + nominal100TempCount, _atm.GetBanknoteByNominal(100).Count);
            Assert.AreEqual(10 + nominal50TempCount, _atm.GetBanknoteByNominal(50).Count);
        }

        [Test]
        public void DepositMoney_ShouldReturnFalseIfDepositFails()
        {
            var banknotesToDeposit = new Dictionary<int, int>
            {
                { 100, 500 },
                { 50, 500 }
            };

            bool result = _depositModel.DepositMoney(banknotesToDeposit);

            Assert.IsFalse(result);
        }
    }
}
