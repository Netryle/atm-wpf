using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_WPF.AtmClasses
{
    internal class Banknote
    {
        public int Nominal { get; private set; }
        public int Count { get; set; }
        public int MaxCount { get; private set; }

        public Banknote(int nominal, int count, int maxCount)
        {
            if(count > maxCount) 
            {
                throw new ArgumentException("Count value is greater than the maxCount value");
            }
            Nominal = nominal;
            MaxCount = maxCount;
            Count = count;
        }
        public Banknote(int nominal, int maxCount)
        {
            Nominal = nominal;
            MaxCount = maxCount;
            Count = 0;
        }

        public void AddBanknotes(int count)
        {
                Count += count;
        }

        public void WithdrawBanknotes(int count) 
        {
                Count -= count;
        }

        public string ToString()
        {
            return $"{Nominal} RUB – {Count} / {MaxCount}\n";
        }
    }
}
