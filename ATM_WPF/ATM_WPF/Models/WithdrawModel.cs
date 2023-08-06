using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_WPF.Models
{
    internal class WithdrawModel
    {
        private SharedDataModel _sharedDataModel;
        public WithdrawModel(SharedDataModel sharedDataModel)
        {
            _sharedDataModel = sharedDataModel;
        }

        public string WithdrawMoney(bool isLargeNominal, int amount)
        {
            Dictionary<int, int> banknotesDictionary;

            if (isLargeNominal)
            {
                banknotesDictionary = _sharedDataModel.ATM.WithdrawInLargeNominal(amount);
            }
            else
            {
                banknotesDictionary = _sharedDataModel.ATM.WithdrawInSmallNominal(amount);
            }

            if (banknotesDictionary != null)
            {
                var stringBuilder = new StringBuilder("Issued:\n");
                foreach (var banknote in banknotesDictionary)
                {
                    stringBuilder.Append($"{banknote.Key} RUB: {banknote.Value}\n");
                }

                return stringBuilder.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
