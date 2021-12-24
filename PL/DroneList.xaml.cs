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
        private IBL.IBL ibl;
        public DroneListWindow(IBL.IBL newIbl)
        {
            InitializeComponent();
            ibl = newIbl;
            IEnumerable<IBL.BO.DroneToList> lst = ibl.GetAllDrones();
            droneToListDataGrid.ItemsSource = ibl.GetAllDrones();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatuses));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
           
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(StatusSelector.SelectedIndex == -1&& WeightSelector.SelectedIndex==-1)
                droneToListDataGrid.ItemsSource = ibl.GetAllDrones();
            else if (StatusSelector.SelectedIndex == -1)
                droneToListDataGrid.ItemsSource = ibl.GetAllDrones(dr => dr.Weight == (IBL.BO.WeightCategories)WeightSelector.SelectedItem);
            else if (WeightSelector.SelectedIndex == -1)
                droneToListDataGrid.ItemsSource = ibl.GetAllDrones(dr => dr.DroneStatus == (IBL.BO.DroneStatuses)StatusSelector.SelectedItem);
            else
                droneToListDataGrid.ItemsSource = ibl.GetAllDrones(dr => dr.DroneStatus == (IBL.BO.DroneStatuses)StatusSelector.SelectedItem && dr.Weight == (IBL.BO.WeightCategories)WeightSelector.SelectedItem);
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
                droneToListDataGrid.ItemsSource = ibl.GetAllDrones(dr => dr.Weight == (IBL.BO.WeightCategories)WeightSelector.SelectedItem);
            else if (WeightSelector.SelectedIndex == -1)
                droneToListDataGrid.ItemsSource = ibl.GetAllDrones(dr => dr.DroneStatus == (IBL.BO.DroneStatuses)StatusSelector.SelectedItem);
            else
                droneToListDataGrid.ItemsSource = ibl.GetAllDrones(dr => dr.DroneStatus == (IBL.BO.DroneStatuses)StatusSelector.SelectedItem && dr.Weight == (IBL.BO.WeightCategories)WeightSelector.SelectedItem);
        }

        private void droneToListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IBL.BO.DroneToList d = droneToListDataGrid.SelectedItem as IBL.BO.DroneToList;
            new PL.DroneWindow(ibl, d).ShowDialog();
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
