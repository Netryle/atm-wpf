using ATM_WPF.AtmClasses;

namespace ATM_WPF.Models
{
    public class SharedDataModel
    {
        public ATM ATM { get; set; }
        public SharedDataModel()
        {
            ATM = new ATM();
        }
    }
}
