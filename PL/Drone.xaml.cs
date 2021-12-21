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
    /// Interaction logic for Drone.xaml
    /// </summary>
    public partial class Drone : Window
    {
        private IBL.IBL ibl;
        IBL.BO.DroneToList PLdDrone;
        enum option { Add, Update};

        option op;
        public Drone(IBL.IBL newIbl)    //add constructor
        {
            InitializeComponent();
            ibl = newIbl;
            PLdDrone = new IBL.BO.DroneToList();
            op = option.Add;

            StatusComboBox.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatuses));
            WeightComboBox.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
            this.Title = "Drone Addition";
            OptionButtun.Content = "ADD The Drone";

            ParcelIDLabel.Visibility = txtParcelID.Visibility = Visibility.Collapsed;
            BatteryLabel.Visibility = txtBattery.Visibility = Visibility.Collapsed;
            StatusLabel.Visibility = StatusComboBox.Visibility = Visibility.Collapsed;
        }

        public Drone(IBL.IBL newIbl, IBL.BO.DroneToList d)//update constructor
        {
            InitializeComponent();
            ibl = newIbl;
            PLdDrone = d;
            op = option.Update;
            droneup.DataContext = PLdDrone ;
           // txtID.Content = d.Id;

            StatusComboBox.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatuses));
            WeightComboBox.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
            this.Title = "Drone Update";
            OptionButtun.Content = "Update The Drone";

        }



        private void txtBattrey_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtParcelID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtID_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void WeightComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            for (int i = 0; i < 3; ++i)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = (IBL.BO.WeightCategories)i;
                WeightComboBox.Items.Add(newItem);
            }
        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            for (int i = 0; i < 3; ++i)
            {
                ComboBoxItem newItem = new ComboBoxItem();
                newItem.Content = (IBL.BO.DroneStatuses)i;
                StatusComboBox.Items.Add(newItem);
            }
        }

        private void txtModel_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OptionButtun_Click(object sender, RoutedEventArgs e)
        {
            //if(op==0)
            // ibl.AddDrone(PLdDrone as IBL.BO.Drone, 3);
            //else
            ibl.UpdateDrone(PLdDrone.Id, PLdDrone.Model);

        }
    }
}
