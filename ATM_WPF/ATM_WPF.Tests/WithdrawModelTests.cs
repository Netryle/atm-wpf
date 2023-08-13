using ATM_WPF.AtmClasses;
using ATM_WPF.Models;

namespace ATM_WPF.Tests
{
    [TestFixture]
    public class WithdrawModelTests
    {
        private WithdrawModel _withdrawModel;
        private SharedDataModel _sharedDataModel;
        private ATM _atm;

        [SetUp]
        public void Setup()
        {
            _sharedDataModel = new SharedDataModel();
            _withdrawModel = new WithdrawModel(_sharedDataModel);
            _atm = _sharedDataModel.ATM;
        }

        [Test]
        public void WithdrawMoneyLargeNominal_ShouldReturnIssuedBanknotesString()
        {
            bool isLargeNominal = true;
            int amount = 6500;

            string result = _withdrawModel.WithdrawMoney(isLargeNominal, amount);
            string expected = "Issued:\n" +
                "5000 RUB: 1\n" +
                "1000 RUB: 1\n" +
                "500 RUB: 1\n";

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void WithdrawMoneySmallNominal_ShouldReturnIssuedBanknotesString()
        {
            bool isLargeNominal = false;
            int amount = 5370;

            string result = _withdrawModel.WithdrawMoney(isLargeNominal, amount);
            string expected = "Issued:\n" +
                "500 RUB: 10\n" +
                "200 RUB: 1\n" +
                "100 RUB: 1\n" +
                "50 RUB: 1\n" +
                "10 RUB: 2\n";

            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void WithdrawMoney_ShouldReturnNullIfNotEnoughBanknotes()
        {
            bool isLargeNominal = false;
            int amount = 999999999;

            string result = _withdrawModel.WithdrawMoney(isLargeNominal, amount);

            Assert.IsNull(result);
        }
    }
}
