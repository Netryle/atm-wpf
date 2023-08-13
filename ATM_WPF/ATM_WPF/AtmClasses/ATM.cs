using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATM_WPF.AtmClasses
{
    public class ATM
    {
        private List<Banknote> _banknotes;
        public int Balance => _banknotes
            .Sum(banknote => banknote.Nominal * banknote.Count);

        public ATM()
        {
            _banknotes = new List<Banknote>();
            InitializeATM();
        }
        private void InitializeATM()
        {
            var random = new Random();
            var maxAmountOfBanknotes = 500;
            int[] nominals = { 10, 50, 100, 200, 500, 1000, 2000, 5000};

            foreach (var nominal in nominals)
            {
                _banknotes.Add(new Banknote(
                nominal,
                random.Next(250, 450),
                maxAmountOfBanknotes)
                );
            }
        }
        
        private Dictionary<int, int> GetDictionaryOfWithdrawableBanknotes(
            IEnumerable<Banknote> banknotes, 
            int amount
            )
        {
            var withdrawResult = new Dictionary<int, int>();

            foreach (var banknote in banknotes)
            {
                while (amount >= banknote.Nominal && banknote.Count > 0)
                {
                    amount -= banknote.Nominal;
                    banknote.Count--;

                    if (withdrawResult.ContainsKey(banknote.Nominal))
                    {
                        withdrawResult[banknote.Nominal]++;
                    }
                    else
                    {
                        withdrawResult[banknote.Nominal] = 1;
                    }
                }
            }

            if (amount == 0)
            {
                return withdrawResult;
            }
            else
            {
                foreach (var entry in withdrawResult)
                {
                    _banknotes.First(b => b.Nominal == entry.Key).Count += entry.Value;
                }

                return null;
            }
        }

        public Banknote GetBanknoteByNominal(int nominal)
        {
            return _banknotes.FirstOrDefault(b => b.Nominal == nominal);
        }

        public bool AddBanknotes(int nominal, int count)
        {
            var banknote = GetBanknoteByNominal (nominal);
            if (banknote != null && banknote.Count + count <= banknote.MaxCount)
            {
                banknote.AddBanknotes(count);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Dictionary<int, int> WithdrawInSmallNominal(int amount)
        {
            var banknotes = _banknotes.Where(b => b.Nominal <= 500).OrderByDescending(b => b.Nominal);
            var withdrawResult = GetDictionaryOfWithdrawableBanknotes(banknotes, amount);

            return withdrawResult;
        }

        public Dictionary<int, int> WithdrawInLargeNominal(int amount)
        {
            var banknotes = _banknotes.OrderByDescending(b => b.Nominal);
            var withdrawResult = GetDictionaryOfWithdrawableBanknotes(banknotes, amount);

            return withdrawResult;
        }

        public string GetStatusString()
        {
            StringBuilder status = new StringBuilder();

            status.Append($"ATM Balance: {Balance}\nAvailable Banknotes:\n");
            foreach(var banknote in _banknotes) 
            {
                status.Append(banknote.ToString());
            }

            return status.ToString();
        }
    }
}
