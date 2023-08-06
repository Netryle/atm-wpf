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

namespace ATM_WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для DepositUC.xaml
    /// </summary>
    public partial class DepositUC : UserControl
    {
        public DepositUC()
        {
            InitializeComponent();
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            string newText = textBox.Text.Insert(textBox.CaretIndex, e.Text);

            if (!int.TryParse(newText, out int result) || result < 0 || int.Parse(newText) > 500)
            {
                e.Handled = true;
            }
        }
    }
}
