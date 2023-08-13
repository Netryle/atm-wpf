using ATM_WPF.AtmClasses;
namespace ATM_WPF.Tests
{
    [TestFixture]
    public class ATMTests
    {
        private ATM _atm;

        [SetUp]
        public void Setup()
        {
            _atm = new ATM();
        }

        [Test]
        public void ATM_Initialization_ShouldHaveNonZeroBalance()
        {
            int expectedBalance = _atm.Balance;

            Assert.IsTrue(expectedBalance > 0);
        }

        [Test]
        public void AddBanknotes_ShouldIncreaseBanknoteCount()
        {
            int nominal = 100;
            int countToAdd = 10;

            var tempAmountOfBanknotes = _atm.GetBanknoteByNominal(nominal).Count;
            bool success = _atm.AddBanknotes(nominal, countToAdd);

            Assert.IsTrue(success);
            Assert.AreEqual(countToAdd + tempAmountOfBanknotes, _atm.GetBanknoteByNominal(nominal).Count);
        }

        [Test]
        public void WithdrawInSmallNominal_ShouldReturnValidBanknotes()
        {
            int amountToWithdraw = 400;

            Dictionary<int, int> withdrawResult = _atm.WithdrawInSmallNominal(amountToWithdraw);

            Assert.IsNotNull(withdrawResult);
            Assert.AreEqual(amountToWithdraw, CalculateTotalAmount(withdrawResult));
        }

        [Test]
        public void WithdrawInLargeNominal_ShouldReturnValidBanknotes()
        {
            int amountToWithdraw = 1500;

            Dictionary<int, int> withdrawResult = _atm.WithdrawInLargeNominal(amountToWithdraw);

            Assert.IsNotNull(withdrawResult);
            Assert.AreEqual(amountToWithdraw, CalculateTotalAmount(withdrawResult));
        }

        private int CalculateTotalAmount(Dictionary<int, int> banknotes)
        {
            int totalAmount = 0;

            foreach (var entry in banknotes)
            {
                totalAmount += entry.Key * entry.Value;
            }

            return totalAmount;
        }
    }
}