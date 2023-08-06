using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ATM_WPF.Models
{
    internal class DepositModel
    {
        private SharedDataModel _sharedDataModel;
        public DepositModel(SharedDataModel sharedDataModel)
        {
            _sharedDataModel = sharedDataModel;
        }

        public bool DepositMoney(Dictionary<int, int> banknotesToDeposit)
        {
            using (var transaction = new TransactionScope())
            {
                foreach (var banknote in banknotesToDeposit.Keys)
                {
                    if (!_sharedDataModel.ATM.AddBanknotes(banknote, banknotesToDeposit[banknote]))
                    {
                        transaction.Dispose();
                        return false;
                    }
                }

                transaction.Complete();
                return true;
            }
        }
    }
}
