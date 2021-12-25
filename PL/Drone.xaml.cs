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
using System.Text.RegularExpressions;

namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        private IBL.IBL ibl;
        IBL.BO.DroneToList PLdDrone;
        enum option { Add, Update};
        enum update {charge,disCharge }
        enum delivery { Join, PickedUpParcel, Supply }

        option op;
        update up=update.charge;
        delivery del=delivery.Join;
        public DroneWindow(IBL.IBL newIbl)    //add constructor
        {
            InitializeComponent();
            ibl = newIbl;
            PLdDrone = new IBL.BO.DroneToList();
            op = option.Add;

            StatusComboBox.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatuses));
            WeightComboBox.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));

            IEnumerable<IBL.BO.StationToList> lst = ibl.GetAllStationsWithAvailableSlots();
            var txt = from IBL.BO.StationToList item in lst
                      select item.Id;
            stationComboBox.ItemsSource = txt;


            this.Title = "Drone Addition";
            OptionButtun.Content = "ADD The Drone";

            ParcelIDLabel.Visibility = txtParcelID.Visibility = Visibility.Collapsed;
            BatteryLabel.Visibility = txtBattery.Visibility = Visibility.Collapsed;
            StatusLabel.Visibility = StatusComboBox.Visibility = Visibility.Collapsed;
            latitudeTextBox.Visibility= latitudeLabel.Visibility = Visibility.Collapsed;
            longitudeTextBox.Visibility=longitudeLabel.Visibility = Visibility.Collapsed; 

            UpdateButton.Visibility = Visibility.Collapsed;
            DeliveryButton.Visibility = Visibility.Collapsed;

            OptionButtun.IsEnabled = false ;
        }

        public DroneWindow(IBL.IBL newIbl, IBL.BO.DroneToList d)//update constructor
        {
            InitializeComponent();
            StatusComboBox.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatuses));
            WeightComboBox.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));

            ibl = newIbl;
            PLdDrone = d;
            op = option.Update;
            droneup.DataContext = PLdDrone ;

            stationComboBox.Visibility = stationLabel.Visibility = Visibility.Collapsed;


            this.Title = "Drone Update";
            OptionButtun.Content = "Update The Drone";

            if(d.DroneStatus==IBL.BO.DroneStatuses.Available)
            {
                UpdateButton.Content = "Charge Drone";
                DeliveryButton.Content = "Join Parcel To Drone";
                up = update.charge;
                del = delivery.Join;
            }
            if(d.DroneStatus == IBL.BO.DroneStatuses.Maintenance)
            {
                UpdateButton.Content = "Discharge Drone";
                DeliveryButton.Visibility = Visibility.Collapsed;
                up = update.disCharge;
            }
            if (d.DroneStatus == IBL.BO.DroneStatuses.Delivery)
            {
                IBL.BO.Parcel parcel = ibl.GetParcel(d.ParcelNumber);
                if (parcel.PickedUpTime != DateTime.MinValue)
                {
                    UpdateButton.Visibility = Visibility.Collapsed;
                    DeliveryButton.Content = "Supply Parcel By Drone";
                    del = delivery.Supply;
                }

                if (parcel.ScheduledTime != DateTime.MinValue)
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
            StatusComboBox.IsEnabled= false;
            longitudeTextBox.IsEnabled = false;
            latitudeTextBox.IsEnabled = false;

        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OptionButtun_Click(object sender, RoutedEventArgs e)   //update/add option
        {
            bool addedSuccessfully = true;
            if (op == 0)
            {
                IBL.BO.Drone Dl = new IBL.BO.Drone()
                {
                    Id = int.Parse(txtID.Text),
                    Model = txtModel.Text,
                    Weight = (IBL.BO.WeightCategories)WeightComboBox.SelectedItem
                };
                try 
                {
                    ibl.AddDrone(Dl, (int)stationComboBox.SelectedItem);
                }
                catch (BL.DuplicateIdException ex)
                {
                    addedSuccessfully = false;
                    MessageBox.Show("The Drone id belong to another drone. insert another one");
                    txtID.Text = "";
                }
                if (addedSuccessfully)
                {
                    MessageBox.Show("The Drone added successfully");
                    this.Close();
                }
            }
            else
            {                ibl.UpdateDrone(PLdDrone.Id, PLdDrone.Model);
                MessageBox.Show("Update Succeeded!", "very nice", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                this.Close();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (del == delivery.Join)
                ibl.joinParcelToDrone(PLdDrone.Id);
            else if (del == delivery.Supply)
                ibl.supplyParceByDrone(PLdDrone.Id);
            else
                ibl.PickedUpParcelByDrone(PLdDrone.Id);
            this.Close();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (up == update.charge)
                ibl.droneToCharge(PLdDrone.Id);
            else
            {
                string time=Microsoft.VisualBasic.Interaction.InputBox("Insert time (in minutes) of drone charging", "insert", "50");
                ibl.ReleaseDroneFromCharge(PLdDrone.Id,TimeSpan.Parse(time));
            }
            
            this.Close();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)    //close window botton
        {
            this.Close();
        }

        private void txtID_TextChanged_1(object sender, TextChangedEventArgs e)    //txtID textbox
        {
            if (txtID.Text != "" && txtModel.Text != "" && WeightComboBox.SelectedIndex != -1 && stationComboBox.SelectedIndex != -1)
                OptionButtun.IsEnabled = true;            
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

        private void txtModel_TextChanged(object sender, TextChangedEventArgs e)  //txtModel textbox
        {
            if (txtID.Text != "" && txtModel.Text != "" && WeightComboBox.SelectedIndex != -1 && stationComboBox.SelectedIndex != -1)
                OptionButtun.IsEnabled = true;
        }

        private void WeightComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)//Weight ComboBox  
        {
            if (txtID.Text != "" && txtModel.Text != "" && WeightComboBox.SelectedIndex != -1 && stationComboBox.SelectedIndex != -1)
                OptionButtun.IsEnabled = true;            
        }

        private void stationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)  //station id ComboBox
        {
            if (txtID.Text != "" && txtModel.Text != "" && WeightComboBox.SelectedIndex != -1 && stationComboBox.SelectedIndex != -1)
                OptionButtun.IsEnabled = true;
        }
    }
}
