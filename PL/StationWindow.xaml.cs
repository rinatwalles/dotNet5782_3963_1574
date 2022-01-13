using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {

        private BLApi.IBL ibl;
        Station PLStation;
        enum option { Add, Update };

        option op;
        public StationWindow(BLApi.IBL newIbl)    //add constructor
        {
            InitializeComponent();
            ibl = newIbl;
            PLStation = new Station();
            op = option.Add;

            this.Title = "Station Addition";
            OptionButtun.Content = "ADD The Station";

            OptionButtun.IsEnabled = false;
        }

        public StationWindow(BLApi.IBL newIbl, Station s)//update constructor
        {
            InitializeComponent();

            ibl = newIbl;
            PLStation = s;
            op = option.Update;
            stationGrid.DataContext = PLStation;

            droneChargingDataGrid.ItemsSource = ibl.GetDroneChargingPerStation(PLStation.Id);
            //droneChargingDataGrid.IsReadOnly = true;

            this.Title = "Station Update";
            OptionButtun.Content = "Update The Station";

            idTextBox.IsEnabled = false;
            longitudeTextBox.IsEnabled = false;
            latitudeTextBox.IsEnabled = false;
        }

        private void CloseButtun_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)///function that makes the user enters only numbers
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void EnglishLettersValidationTextBox(object sender, TextCompositionEventArgs e)///function that makes the user enters only capital English Letters
        {
            Regex regex = new Regex("[^A-Z]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private bool buttunEnabled()
        {
            return idTextBox.Text != "" && nameTextBox.Text != "" && availableChargeSlotsTextBox.Text != "" && latitudeTextBox.Text != "" && longitudeTextBox.Text != "";
        }

        private void availableChargeSlotsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (buttunEnabled())
                OptionButtun.IsEnabled = true;
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
                Station Vl = new Station()
                {
                    Id = int.Parse(idTextBox.Text),
                    Name = nameTextBox.Text,
                    AvailableChargeSlots = int.Parse(availableChargeSlotsTextBox.Text),
                    Location = locat
                };
                try
                {
                    ibl.AddStation(Vl);
                }
                catch (DuplicateIdException ex)
                {
                    addedSuccessfully = false;
                    MessageBox.Show("The Station id belong to another Station. insert another one");
                    idTextBox.Text = "";
                }
                if (addedSuccessfully)
                {
                    MessageBox.Show("The Station added successfully");
                    this.Close();
                }
            }
            else
            {
                ibl.UpdateStation(PLStation.Id, nameTextBox.Text, availableChargeSlotsTextBox.Text);
                MessageBox.Show("Update Succeeded!", "very nice", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                this.Close();
            } 
        }
    }
}
