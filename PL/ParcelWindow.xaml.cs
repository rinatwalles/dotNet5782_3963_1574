using BO;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelWindow.xaml
    /// </summary>
    public partial class ParcelWindow : Window
    {
        private BLApi.IBL ibl;
        enum option { Add, Update };
        option op;
        Parcel PlParc;
        public ParcelWindow(BLApi.IBL newIbl)
        {
            InitializeComponent();
            ibl = newIbl;
            PlParc = new Parcel();
            op = option.Add;
            priorityComboBox.ItemsSource = Enum.GetValues(typeof(Priorities));
            weightComboBox.ItemsSource = Enum.GetValues(typeof(WeightCategories));

            this.Title = "Parcel Addition";
            OptionButtun.Content = "ADD The Parcel";
            battaryStatusLabel.Visibility = battaryStatusTextBox.Visibility = Visibility.Collapsed;
            idLabel.Visibility = idTextBox.Visibility = Visibility.Collapsed;
            ReceiverNameLabel.Visibility = nameReceiverTextBox.Visibility = Visibility.Collapsed;
            SenderNameLabel.Visibility = nameSenderTextBox1.Visibility = Visibility.Collapsed;
            //longitudeTextBox.Visibility = longitudeLabel.Visibility = Visibility.Collapsed;
            //UpdateButton.Visibility = Visibility.Collapsed;
            //DeliveryButton.Visibility = Visibility.Collapsed;
            idTextBox.IsEnabled = false;
        }
        public ParcelWindow(BLApi.IBL newIbl, Parcel parc)//update constructor
        {
            InitializeComponent();
            priorityComboBox.ItemsSource = Enum.GetValues(typeof(Priorities));
            weightComboBox.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            ibl = newIbl;
            PlParc = parc;
            op = option.Update;
            parcelup.DataContext = PlParc;




            this.Title = "Drone Update";
            OptionButtun.Content = "Update The Drone";
            //    this.Title = "Drone Update";
            //    OptionButtun.Content = "Update The Drone";

            //if (d.DroneStatus == DroneStatuses.Available)
            //{
            //    UpdateButton.Content = "Charge Drone";
            //    DeliveryButton.Content = "Join Parcel To Drone";
            //    up = update.charge;
            //    del = delivery.Join;
            //}
            //if (d.DroneStatus == DroneStatuses.Maintenance)
            //{
            //    UpdateButton.Content = "Discharge Drone";
            //    DeliveryButton.Visibility = Visibility.Collapsed;
            //    up = update.disCharge;
            //}
            //if (d.DroneStatus == DroneStatuses.Delivery)
            //{
            //    Parcel parcel = ibl.GetParcel(d.ParcelNumber);
            //    if (parcel.PickedUpTime != DateTime.MinValue)
            //    {
            //        UpdateButton.Visibility = Visibility.Collapsed;
            //        DeliveryButton.Content = "Supply Parcel By Drone";
            //        del = delivery.Supply;
            //    }
            //    else if (parcel.ScheduledTime != DateTime.MinValue)
            //    {
            //        UpdateButton.Visibility = Visibility.Collapsed;
            //        DeliveryButton.Content = "Collecting Parcel By Drone";
            //        del = delivery.PickedUpParcel;
            //    }
            //}
            //txtID.IsEnabled = false;
            //txtParcelID.IsEnabled = false;
            //txtBattery.IsEnabled = false;
            //WeightComboBox.IsEnabled = false;
            //StatusComboBox.IsEnabled = false;
            //longitudeTextBox.IsEnabled = false;
            //latitudeTextBox.IsEnabled = false;

        }
        //}
        private void OptionButtun_Click(object sender, RoutedEventArgs e)   //update/add option
        {
            bool addedSuccessfully = true;
            if (op == 0)
            {


                Parcel parc = new Parcel()
                {
                    Weight = (WeightCategories)weightComboBox.SelectedItem,
                    Priority = (Priorities)priorityComboBox.SelectedItem
                };
                int IdSend = int.Parse(idSenderTextBox3.Text);
                int IdReceive = int.Parse(idReceiverTextBox2.Text);
                //  ibl.AddParcel(parc, IdSend, IdReceive);
                try
                {
                    ibl.AddParcel(parc, IdSend, IdReceive);
                }
                catch (DuplicateIdException ex)
                {
                    addedSuccessfully = false;
                    MessageBox.Show("The parcel id belong to another parcel. insert another one");
                    idTextBox.Text = "";
                }
                catch (DeliveryProblems ex)
                {
                    addedSuccessfully = false;
                    MessageBox.Show("sender and reciver cannot be with the same id, insert again");
                    idSenderTextBox3.Text = "";
                    idReceiverTextBox2.Text = "";
                }
                if (addedSuccessfully)
                {
                    MessageBox.Show("The Drone added successfully");
                    this.Close();
                }
            }
            else
            {
                //ibl.UpdateDrone(PLdDrone.Id, PLdDrone.Model);
                MessageBox.Show("Update Succeeded!", "very nice", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                this.Close();
            }

            private void DroneLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            {
                int id = int.Parse(idTextBox1.Text);
                BO.Drone d = ibl.GetDrone(id);
                new PL.DroneWindow(ibl, d);
            }

            private void Button_Click_Close(object sender, RoutedEventArgs e)
            {
                this.Close();
            }
        }

        private void DroneLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
