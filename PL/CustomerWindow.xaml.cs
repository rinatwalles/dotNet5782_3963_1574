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
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private BLApi.IBL ibl;
        CustomerToList PLcustomer;
        enum option { Add, Update };
        option op;

        public CustomerWindow(BLApi.IBL newIbl)    //add constructor
        {
            InitializeComponent();
            ibl = newIbl;
            PLcustomer = new CustomerToList();
            op = option.Add;


            this.Title = "Customer Addition";
            OptionButtun.Content = "ADD The Customer";


            OptionButtun.IsEnabled = false;
        }

        public CustomerWindow(BLApi.IBL newIbl, CustomerToList c)//update constructor
        {
            InitializeComponent();


            ibl = newIbl;
            PLcustomer = c;
            op = option.Update;
            customerDetails.DataContext = PLcustomer;


            this.Title = "Drone Update";
            OptionButtun.Content = "Update The Drone";

            txtID.IsEnabled = false;
            txtParcelID.IsEnabled = false;
            txtBattery.IsEnabled = false;
            WeightComboBox.IsEnabled = false;
            StatusComboBox.IsEnabled = false;
            longitudeTextBox.IsEnabled = false;
            latitudeTextBox.IsEnabled = false;

        }
        private void idTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
    }
}
