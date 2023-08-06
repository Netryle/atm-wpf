using ATM_WPF.AtmClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_WPF.Models
{
    internal class SharedDataModel
    {
        public ATM ATM { get; set; }
        public SharedDataModel()
        {
            ATM = new ATM();
        }
    }
}
