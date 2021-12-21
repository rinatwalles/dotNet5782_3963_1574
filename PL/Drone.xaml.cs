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
    public partial class DroneWindow : Window
    {
        private IBL.IBL ibl;
        IBL.BO.DroneToList PLdDrone;
        enum option { Add, Update};

        option op;
        public DroneWindow(IBL.IBL newIbl)    //add constructor
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
            OptionButtun.IsEnabled = false;
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
            if (txtID.Text != "" && txtModel.Text != "" && WeightComboBox.SelectedIndex!= -1)
                OptionButtun.IsEnabled = true;
        }

        private void WeightComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (txtID.Text != "" && txtModel.Text != "" && WeightComboBox.SelectedIndex != -1)
                OptionButtun.IsEnabled = true;
        }

        private void StatusComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        //    for (int i = 0; i < 3; ++i)
        //    {
        //        ComboBoxItem newItem = new ComboBoxItem();
        //        newItem.Content = (IBL.BO.DroneStatuses)i;
        //        StatusComboBox.Items.Add(newItem);
        //    }
        }

        private void txtModel_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtID.Text != "" && txtModel.Text != "" && WeightComboBox.SelectedIndex != -1)
                OptionButtun.IsEnabled = true;
        }

        private void OptionButtun_Click(object sender, RoutedEventArgs e)
        {
            bool addedSuccessfully = true;
            if (op == 0)
            {
                IBL.BO.Drone Dl = new IBL.BO.Drone()
                {
                    Id = PLdDrone.Id,
                    Model = PLdDrone.Model,
                    Weight = PLdDrone.Weight
                };
                try 
                {
                    ibl.AddDrone(Dl, 1);
                }
                catch (Exception ex)
                {
                    addedSuccessfully = false;
                    MessageBox.Show(ex.Message);
                }
                if(addedSuccessfully)
                    MessageBox.Show("The Drone added successfully");

            }
            else
                ibl.UpdateDrone(PLdDrone.Id, PLdDrone.Model);

        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
