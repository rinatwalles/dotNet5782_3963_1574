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
using BO;
using BLApi;
using System.ComponentModel;
using System.Threading;

namespace PL
{
    /// <summary>
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class DroneWindow : Window
    {
        private BLApi.IBL ibl;
        Drone PLdDrone;
        enum option { Add, Update};
        enum update {charge,disCharge }
        enum delivery { Join, PickedUpParcel, Supply }
        enum autoUpdate { start, stop}

        option op;
        update up=update.charge;
        delivery del=delivery.Join;
        autoUpdate aU = autoUpdate.start;
        BackgroundWorker droneUp;

        public DroneWindow(BLApi.IBL newIbl)    //add constructor
        {
            InitializeComponent();
            ibl = newIbl;

            StatusComboBox.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            WeightComboBox.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            priorityComboBox.ItemsSource = Enum.GetValues(typeof(Priorities));
            parcelState.Visibility = Visibility.Collapsed;
            PLdDrone = new Drone();
            op = option.Add;

            
            IEnumerable<StationToList> lst = ibl.GetAllStationsWithAvailableSlots();
            var txt = from StationToList item in lst
                      orderby item.Id
                      select item.Id;
            stationComboBox.ItemsSource = txt;


            this.Title = "Drone Addition";
            OptionButtun.Content = "ADD The Drone";

            ParcelIDLabel.Visibility = txtParcelID.Visibility = Visibility.Collapsed;
            BatteryLabel.Visibility = txtBattery.Visibility = Visibility.Collapsed;
            StatusLabel.Visibility = StatusComboBox.Visibility = Visibility.Collapsed;
            latitudeTextBox.Visibility= latitudeLabel.Visibility = Visibility.Collapsed;
            longitudeTextBox.Visibility=longitudeLabel.Visibility = Visibility.Collapsed;
            grid3.Visibility = Visibility.Collapsed;
            grid2.Visibility = Visibility.Collapsed;
            parcelStateLabel.Visibility  = Visibility.Collapsed;
            priorityLabel.Visibility = priorityComboBox.Visibility = Visibility.Collapsed;
            transportDistanceLabel.Visibility = transportDistanceTextBox.Visibility = Visibility.Collapsed;
            weightComboBox1Label.Visibility = weightComboBox1.Visibility = Visibility.Collapsed;
            UpdateButton.Visibility = Visibility.Collapsed;
            DeliveryButton.Visibility = Visibility.Collapsed;

            OptionButtun.IsEnabled = false ;

            
        }

        private void DroneUp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            droneup.DataContext = PLdDrone;
        }

        private void DroneUp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void DroneUp_DoWork(object sender, DoWorkEventArgs e)
        {
            double battery = PLdDrone.BatteryStatus;
            while (droneUp.CancellationPending != true)
            {
                switch (PLdDrone.DroneStatus)
                {
                    case DroneStatuses.Available:
                        ibl.autoUpdate(PLdDrone);
                        droneUp.ReportProgress(1);

                        Thread.Sleep(1000);
                        break;
                    case DroneStatuses.Delivery:
                        ibl.autoUpdate(PLdDrone);
                        droneUp.ReportProgress(1);

                        Thread.Sleep(1000);
                        break;
                    case DroneStatuses.Maintenance:
                        for (int i = 1; i <=5; i++)
                        {
                            PLdDrone.BatteryStatus += 10;
                            droneUp.ReportProgress(i);
                            Thread.Sleep(1000);
                        }
                        break;
                    default:
                        break;
                }

            }
        }

        public DroneWindow(BLApi.IBL newIbl, Drone d)//update constructor
        {
            InitializeComponent();
            droneUp = new BackgroundWorker();
            droneUp.DoWork += DroneUp_DoWork;
            droneUp.ProgressChanged += DroneUp_ProgressChanged;
            droneUp.RunWorkerCompleted += DroneUp_RunWorkerCompleted;
            droneUp.WorkerSupportsCancellation = true;
            droneUp.WorkerReportsProgress = true;
            StatusComboBox.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            WeightComboBox.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            priorityComboBox.ItemsSource = Enum.GetValues(typeof(Priorities));
            weightComboBox1.ItemsSource=Enum.GetValues(typeof(WeightCategories));

             ibl = newIbl;
            PLdDrone = d;
            op = option.Update;
            droneup.DataContext=PLdDrone;
            


            this.Title = "Drone Update";
            OptionButtun.Content = "Update The Drone";
            autoUp.Content = "Auto Update";
            stationComboBox.Visibility = stationLabel.Visibility = Visibility.Collapsed;

            if (d.DroneStatus == DroneStatuses.Available)
            {
                UpdateButton.Content = "Charge Drone";
                DeliveryButton.Content = "Join Parcel To Drone";
                up = update.charge;
                del = delivery.Join;
                grid3.Visibility = Visibility.Collapsed;
                grid2.Visibility = Visibility.Collapsed;
                ParcelIDLabel.Visibility= txtParcelID.Visibility = Visibility.Collapsed;
                parcelStateLabel.Visibility= parcelState.Visibility = Visibility.Collapsed;
                priorityLabel.Visibility=priorityComboBox.Visibility = Visibility.Collapsed;
                transportDistanceLabel.Visibility= transportDistanceTextBox.Visibility= Visibility.Collapsed;
                weightComboBox1Label.Visibility= weightComboBox1.Visibility = Visibility.Collapsed;

            }
            if (d.DroneStatus == DroneStatuses.Maintenance)
            {
                UpdateButton.Content = "Discharge Drone";
                DeliveryButton.Visibility = Visibility.Collapsed;
                up = update.disCharge;
                grid3.Visibility = Visibility.Collapsed;
                grid2.Visibility = Visibility.Collapsed;
                ParcelIDLabel.Visibility = txtParcelID.Visibility = Visibility.Collapsed;
                parcelStateLabel.Visibility = parcelState.Visibility = Visibility.Collapsed;
                priorityLabel.Visibility = priorityComboBox.Visibility = Visibility.Collapsed;
                transportDistanceLabel.Visibility = transportDistanceTextBox.Visibility = Visibility.Collapsed;
                weightComboBox1Label.Visibility = weightComboBox1.Visibility = Visibility.Collapsed;
            }
            if (d.DroneStatus == DroneStatuses.Delivery)
            {
                parcelState.IsChecked = d.ParcelInDelivery.ParcelState;

                Parcel parcel = ibl.GetParcel(d.ParcelInDelivery.Id);
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
            weightComboBox1.IsEnabled = false;
            StatusComboBox.IsEnabled= false;
            longitudeTextBox.IsEnabled = false;
            latitudeTextBox.IsEnabled = false;
            priorityComboBox.IsEnabled = false;
        }



        private void OptionButtun_Click(object sender, RoutedEventArgs e)   //update/add option
        {
            bool addedSuccessfully = true;
            if (op == 0)
            {
                Drone Dl = new Drone()
                {
                    Id = int.Parse(txtID.Text),
                    Model = txtModel.Text,
                    Weight = (WeightCategories)WeightComboBox.SelectedItem
                };
                try
                {
                    ibl.AddDrone(Dl, (int)stationComboBox.SelectedItem);
                }
                catch (DuplicateIdException ex)
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
            {    PLdDrone.Model = txtModel.Text;
               ibl.UpdateDrone(PLdDrone.Id, PLdDrone.Model);
                MessageBox.Show("Update Succeeded!", "very nice", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                this.Close();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool worked = true; 
            try
            {
                if (del == delivery.Join)
                    ibl.joinParcelToDrone(PLdDrone.Id);
                else if (del == delivery.Supply)
                    ibl.supplyParceByDrone(PLdDrone.Id);
                else
                    ibl.PickedUpParcelByDrone(PLdDrone.Id);
            }
            catch (Exception ex)
            {
                worked = false;
                MessageBox.Show(ex.Message, "problem", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                /////
                this.Close();
            }

            if (worked)
            {
                MessageBox.Show("Update Succeeded!", "very nice", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            bool worked = true;
            try
            {
                if (up == update.charge)
                    ibl.droneToCharge(PLdDrone.Id);
                else
                {
                    string time = Microsoft.VisualBasic.Interaction.InputBox("Insert time (in minutes) of drone charging", "insert", "50");
                    ibl.ReleaseDroneFromCharge(PLdDrone.Id, TimeSpan.Parse(time));
                }
            }
            catch (Exception ex)
            {
                worked = false;
                MessageBox.Show(ex.Message, "problem", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                this.Close();
            }
            if (worked)
            {
                MessageBox.Show("Update Succeeded!", "very nice", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)    //close window botton
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
            Regex regex = new Regex("[^A-Z|^a-z|]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void stationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)  //station id ComboBox
        {
            if (txtID.Text != "" && txtModel.Text != "" && WeightComboBox.SelectedIndex != -1 && stationComboBox.SelectedIndex != -1)
                OptionButtun.IsEnabled = true;
        }

        private void WeightComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txtID.Text != "" && txtModel.Text != "" && WeightComboBox.SelectedIndex != -1 && stationComboBox.SelectedIndex != -1)
                OptionButtun.IsEnabled = true;
        }

        private void txtID_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtID.Text != "" && txtModel.Text != "" && WeightComboBox.SelectedIndex != -1 && stationComboBox.SelectedIndex != -1)
                OptionButtun.IsEnabled = true;
        }


        private void txtModel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtID.Text != "" && txtModel.Text != "" && WeightComboBox.SelectedIndex != -1 && stationComboBox.SelectedIndex != -1)
                OptionButtun.IsEnabled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (aU == autoUpdate.start)
            {
               
                    this.Cursor = Cursors.Wait;
                    droneUp.RunWorkerAsync();
                    autoUp.Content = "Stop Auto Update";
                    aU = autoUpdate.stop;

            }
            else
                droneUp.CancelAsync();
        }
    }
}
