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
        public ParcelWindow(BLApi.IBL newIbl)   //add
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
            longitudeTextBox.Visibility = LongitudeLabel.Visibility = Visibility.Collapsed;
            latitudeTextBox.Visibility = LatitudeLabel.Visibility = Visibility.Collapsed;
            requestedTimeLabel.Visibility = requestedTimeDatePicker.Visibility = Visibility.Collapsed;
            ScheduledTimeLabel.Visibility = scheduledTimeDatePicker.Visibility = Visibility.Collapsed;
            PickedTimeLabel.Visibility = pickedUpTimeDatePicker.Visibility = Visibility.Collapsed;
            DeliveredTimeLabel.Visibility = deliveredTimeDatePicker.Visibility = Visibility.Collapsed;
            DroneLabel.Visibility = idDroneTextBox1.Visibility = Visibility.Collapsed;
            idTextBox.IsEnabled = false;
            senderButton.Visibility = recieverButton.Visibility = droneButton.Visibility = Visibility.Collapsed;
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

            if(newIbl.getParcelState(parc.Id)!=ParcelStates.Scheduled && newIbl.getParcelState(parc.Id) != ParcelStates.PickedUp)
                droneButton.Visibility = Visibility.Collapsed;

            this.Title = "Parcel Update";
            OptionButtun.Content = "Update The Parcel";
            OptionButtun.Visibility = Visibility.Collapsed;
            
        }
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
                    MessageBox.Show("The Parcel added successfully");
                    this.Close();
                }
            }
            else
            {
                //ibl.UpdateDrone(PLdDrone.Id, PLdDrone.Model);
                MessageBox.Show("Update Succeeded!", "very nice", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                this.Close();
            }
        }

            private void Button_Click_Close(object sender, RoutedEventArgs e)
            {
                this.Close();
            } 

        private void Button_Click_Drone(object sender, RoutedEventArgs e)
        {
            //int id = int.Parse(idDroneTextBox1.Text);
            BO.Drone d = ibl.GetDrone(PlParc.Id);
            new PL.DroneWindow(ibl, d).Show();
        }

        private void Button_Click_reciever(object sender, RoutedEventArgs e)
        {
            BO.Customer c = ibl.GetCustomer(PlParc.Receiver.Id);
            new PL.CustomerWindow(ibl, c, true).Show();
        }

        private void Button_Click_sender(object sender, RoutedEventArgs e)
        {
            BO.Customer c = ibl.GetCustomer(PlParc.Sender.Id);
            new PL.CustomerWindow(ibl, c, true).Show();
        }
    }
}
