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

            idTextBox.IsEnabled = false;
            longitudeTextBox.IsEnabled = false;
            latitudeTextBox.IsEnabled = false;

        }
        private bool buttunEnabled()
        {
            return idTextBox.Text != "" && nameTextBox.Text != "" && phoneTextBox.Text != "" && latitudeTextBox.Text != "" && longitudeTextBox.Text != "";
        }

        private void idTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buttunEnabled())
                OptionButtun.IsEnabled = true;
        }

        private void nameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buttunEnabled())
                OptionButtun.IsEnabled = true;
        }

        private void phoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buttunEnabled())
                OptionButtun.IsEnabled = true;
        }

        private void latitudeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buttunEnabled())
                OptionButtun.IsEnabled = true;
        }

        private void longitudeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buttunEnabled())
                OptionButtun.IsEnabled = true;
        }

        private void OptionButtun_Click(object sender, RoutedEventArgs e)
        {
            bool addedSuccessfully = true;
            if (op == 0)
            {
                Location locat = new Location()
                {
                    Longitude = double.Parse(longitudeTextBox.Text),
                    Latitude = double.Parse(latitudeTextBox.Text)
                };
                Customer Cl = new Customer()
                {
                    Id = int.Parse(idTextBox.Text),
                    Name = nameTextBox.Text,
                    Phone= phoneTextBox.Text,
                    Location = locat
                };
                try
                {
                    ibl.AddCustomer(Cl);
                }
                catch (DuplicateIdException ex)
                {
                    addedSuccessfully = false;
                    MessageBox.Show("The Customer id belong to another Customer. insert another one");
                    idTextBox.Text = "";
                }
                if (addedSuccessfully)
                {
                    MessageBox.Show("The Customer added successfully");
                    this.Close();
                }
            }
            else
            {
                ibl.UpdateCustomer(PLcustomer.Id, nameTextBox.Text, phoneTextBox.Text);
                MessageBox.Show("Update Succeeded!", "very nice", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
