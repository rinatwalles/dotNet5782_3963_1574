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
    /// Interaction logic for StationWindow.xaml
    /// </summary>
    public partial class StationWindow : Window
    {

        private BLApi.IBL ibl;
        StationToList PLStation;
        enum option { Add, Update };

        option op;
        public StationWindow(BLApi.IBL newIbl)    //add constructor
        {
            InitializeComponent();
            ibl = newIbl;
            PLStation = new StationToList();
            op = option.Add;

            this.Title = "Drone Addition";
            OptionButtun.Content = "ADD The Drone";

            ParcelIDLabel.Visibility = txtParcelID.Visibility = Visibility.Collapsed;
            BatteryLabel.Visibility = txtBattery.Visibility = Visibility.Collapsed;
            StatusLabel.Visibility = StatusComboBox.Visibility = Visibility.Collapsed;
            latitudeTextBox.Visibility = latitudeLabel.Visibility = Visibility.Collapsed;
            longitudeTextBox.Visibility = longitudeLabel.Visibility = Visibility.Collapsed;

            UpdateButton.Visibility = Visibility.Collapsed;
            DeliveryButton.Visibility = Visibility.Collapsed;

            OptionButtun.IsEnabled = false;
        }

        public StationWindow(BLApi.IBL newIbl, DroneToList d)//update constructor
        {
            InitializeComponent();

            ibl = newIbl;
            PLdDrone = d;
            op = option.Update;
            droneup.DataContext = PLdDrone;
            droneModel = d.Model;

            stationComboBox.Visibility = stationLabel.Visibility = Visibility.Collapsed;


            this.Title = "Drone Update";
            OptionButtun.Content = "Update The Drone";

            if (d.DroneStatus == DroneStatuses.Available)
            {
                UpdateButton.Content = "Charge Drone";
                DeliveryButton.Content = "Join Parcel To Drone";
                up = update.charge;
                del = delivery.Join;
            }
            if (d.DroneStatus == DroneStatuses.Maintenance)
            {
                UpdateButton.Content = "Discharge Drone";
                DeliveryButton.Visibility = Visibility.Collapsed;
                up = update.disCharge;
            }
            if (d.DroneStatus == DroneStatuses.Delivery)
            {
                Parcel parcel = ibl.GetParcel(d.ParcelNumber);
                if (parcel.PickedUpTime != DateTime.MinValue)
                {
                    UpdateButton.Visibility = Visibility.Collapsed;
                    DeliveryButton.Content = "Supply Parcel By Drone";
                    del = delivery.Supply;
                }

                else if (parcel.ScheduledTime != DateTime.MinValue)
                {
                    UpdateButton.Visibility = Visibility.Collapsed;
                    DeliveryButton.Content = "Collecting Parcel By Drone";
                    del = delivery.PickedUpParcel;
                }
            }
            txtID.IsEnabled = false;
            txtParcelID.IsEnabled = false;
            txtBattery.IsEnabled = false;
            WeightComboBox.IsEnabled = false;
            StatusComboBox.IsEnabled = false;
            longitudeTextBox.IsEnabled = false;
            latitudeTextBox.IsEnabled = false;

        }

        private void CloseButtun_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
