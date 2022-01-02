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

            IEnumerable<StationToList> lst = ibl.GetAllStationsWithAvailableSlots();
            var txt = from StationToList item in lst
                      select item.Id;
            //stationComboBox.ItemsSource = txt;


            //this.Title = "Drone Addition";
            //OptionButtun.Content = "ADD The Drone";

            //ParcelIDLabel.Visibility = txtParcelID.Visibility = Visibility.Collapsed;
            //BatteryLabel.Visibility = txtBattery.Visibility = Visibility.Collapsed;
            //StatusLabel.Visibility = StatusComboBox.Visibility = Visibility.Collapsed;
            //latitudeTextBox.Visibility = latitudeLabel.Visibility = Visibility.Collapsed;
            //longitudeTextBox.Visibility = longitudeLabel.Visibility = Visibility.Collapsed;

            //UpdateButton.Visibility = Visibility.Collapsed;
            //DeliveryButton.Visibility = Visibility.Collapsed;

            //OptionButtun.IsEnabled = false;
        }
    }
}
