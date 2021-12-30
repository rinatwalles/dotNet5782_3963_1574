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
    /// Interaction logic for DroneList.xaml
    /// </summary>
    public partial class DroneListWindow : Window
    {
        private BLApi.IBL ibl;
        public DroneListWindow(BLApi.IBL newIbl)
        {
            InitializeComponent();
            ibl = newIbl;
            IEnumerable<DroneToList> lst = ibl.GetAllDrones();
            droneToListDataGrid.ItemsSource = ibl.GetAllDrones();
            droneToListDataGrid.IsReadOnly = true;
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
           
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(StatusSelector.SelectedIndex == -1&& WeightSelector.SelectedIndex==-1)
                droneToListDataGrid.ItemsSource = ibl.GetAllDrones();
            else if (StatusSelector.SelectedIndex == -1)
                droneToListDataGrid.ItemsSource = ibl.GetAllDrones(dr => dr.Weight == (WeightCategories)WeightSelector.SelectedItem);
            else if (WeightSelector.SelectedIndex == -1)
                droneToListDataGrid.ItemsSource = ibl.GetAllDrones(dr => dr.DroneStatus == (DroneStatuses)StatusSelector.SelectedItem);
            else
                droneToListDataGrid.ItemsSource = ibl.GetAllDrones(dr => dr.DroneStatus == (DroneStatuses)StatusSelector.SelectedItem && dr.Weight == (WeightCategories)WeightSelector.SelectedItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new PL.DroneWindow(ibl).Show();
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StatusSelector.SelectedIndex == -1 && WeightSelector.SelectedIndex == -1)
                droneToListDataGrid.ItemsSource = ibl.GetAllDrones();
            else if (StatusSelector.SelectedIndex == -1)
                droneToListDataGrid.ItemsSource = ibl.GetAllDrones(dr => dr.Weight == (WeightCategories)WeightSelector.SelectedItem);
            else if (WeightSelector.SelectedIndex == -1)
                droneToListDataGrid.ItemsSource = ibl.GetAllDrones(dr => dr.DroneStatus == (DroneStatuses)StatusSelector.SelectedItem);
            else
                droneToListDataGrid.ItemsSource = ibl.GetAllDrones(dr => dr.DroneStatus == (DroneStatuses)StatusSelector.SelectedItem && dr.Weight == (WeightCategories)WeightSelector.SelectedItem);
        }

        private void droneToListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DroneToList d = droneToListDataGrid.SelectedItem as DroneToList;
            new PL.DroneWindow(ibl, d).Show();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            droneToListDataGrid.ItemsSource = ibl.GetAllDrones();
        }
    }
}
