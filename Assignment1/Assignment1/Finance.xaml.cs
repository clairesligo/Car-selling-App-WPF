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
using System.Windows.Shapes;

namespace Assignment1
{
    /// <summary>
    /// Interaction logic for Finance.xaml
    /// </summary>
    public partial class Finance : Window
    {
        public Finance()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow main = Owner as MainWindow;
            cbxMonths.Items.Add("12 Months");
            cbxMonths.Items.Add("24 Months");
            cbxMonths.Items.Add("36 Months");
            cbxMonths.Items.Add("48 Months");
            try
            {
                Vehicle v = main.lbxCars.SelectedItem as Vehicle;
                lblCarPrice.Content = String.Format("{0}", v.Price);
            }
            catch
            {
                MessageBox.Show("Please select a vehicle from the list", "OK", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                Close();
            }
        }

        private void cbxMonths_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainWindow main = Owner as MainWindow;
            
                Vehicle v = main.lbxCars.SelectedItem as Vehicle;
                double price = v.Price;
                double outcome = 0;

                string pick = cbxMonths.SelectedValue.ToString();

                switch (pick)
                {
                    case "12 Months":
                        outcome = ((price * 0.75) + price) / 12;
                        break;
                    case "24 Months":
                        outcome = ((price * 0.75) + price) / 24;
                        break;
                    case "36 Months":
                        outcome = ((price * 0.75) + price) / 36;
                        break;
                    case "48 Months":
                        outcome = ((price * 0.75) + price) / 48;
                        break;
                }//end switch
                lblOutput.Content = String.Format("Price per month:{0:C2}\n *Inclusive of 7.5% interest", outcome);
        }
    }//end class
}//end ns
