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
        public ParcelWindow(BLApi.IBL newIbl)
        {
            InitializeComponent();
            ibl = newIbl;
            Parcel parcel = new Parcel();
            op = option.Add;

            priorityComboBox.ItemsSource = Enum.GetValues(typeof(Priorities));
            weightComboBox.ItemsSource = Enum.GetValues(typeof(WeightCategories));

            IEnumerable<CustomerToList> lst = ibl.GetAllCustomers();
            


            this.Title = "Parcel Addition";
            OptionButtun.Content = "ADD The Parcel";

            //ParcelIDLabel.Visibility = txtParcelID.Visibility = Visibility.Collapsed;
            //BatteryLabel.Visibility = txtBattery.Visibility = Visibility.Collapsed;
            //StatusLabel.Visibility = StatusComboBox.Visibility = Visibility.Collapsed;
            //latitudeTextBox.Visibility = latitudeLabel.Visibility = Visibility.Collapsed;
            //longitudeTextBox.Visibility = longitudeLabel.Visibility = Visibility.Collapsed;

            //UpdateButton.Visibility = Visibility.Collapsed;
            //DeliveryButton.Visibility = Visibility.Collapsed;

            //OptionButtun.IsEnabled = false;
        }
        private void OptionButtun_Click(object sender, RoutedEventArgs e)   //update/add option
        {
            bool addedSuccessfully = true;
            //if (op == 0)
            //{
            //    Drone Dl = new Drone()
            //    {
            //        Id = int.Parse(idTextBox.Text),
            //        Model = txtModel.Text,
            //        Weight = (WeightCategories)WeightComboBox.SelectedItem
            //    };
            //    try
            //    {
            //        ibl.AddDrone(Dl, (int)stationComboBox.SelectedItem);
            //    }
            //    catch (DuplicateIdException ex)
            //    {
            //        addedSuccessfully = false;
            //        MessageBox.Show("The Drone id belong to another drone. insert another one");
            //        txtID.Text = "";
            //    }
            //    if (addedSuccessfully)
            //    {
            //        MessageBox.Show("The Drone added successfully");
            //        this.Close();
            //    }
            //}
            //else
            //{
            //    ibl.UpdateDrone(PLdDrone.Id, PLdDrone.Model);
            //    MessageBox.Show("Update Succeeded!", "very nice", MessageBoxButton.OKCancel, MessageBoxImage.Information);
            //    this.Close();
            //}

        }
    }
}
