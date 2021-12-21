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
    public partial class DroneList : Window
    {
        private IBL.IBL ibl;
        public DroneList(IBL.IBL newIbl)
        {
            InitializeComponent();
            ibl = newIbl;
            IEnumerable<IBL.BO.DroneToList> lst = ibl.GetAllDrones();
            droneToListListView.ItemsSource = ibl.GetAllDrones();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatuses));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
           
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(StatusSelector.SelectedIndex == -1&& WeightSelector.SelectedIndex==-1)
                droneToListListView.ItemsSource = ibl.GetAllDrones();
            else if (StatusSelector.SelectedIndex == -1)
                droneToListListView.ItemsSource = ibl.GetAllDrones(dr => dr.Weight == (IBL.BO.WeightCategories)WeightSelector.SelectedItem);
            else if (WeightSelector.SelectedIndex == -1)
                droneToListListView.ItemsSource = ibl.GetAllDrones(dr => dr.DroneStatus == (IBL.BO.DroneStatuses)StatusSelector.SelectedItem);
            else
                droneToListListView.ItemsSource = ibl.GetAllDrones(dr => dr.DroneStatus == (IBL.BO.DroneStatuses)StatusSelector.SelectedItem && dr.Weight == (IBL.BO.WeightCategories)WeightSelector.SelectedItem);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new PL.Drone(ibl).ShowDialog();
        }

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StatusSelector.SelectedIndex == -1 && WeightSelector.SelectedIndex == -1)
                droneToListListView.ItemsSource = ibl.GetAllDrones();
            else if (StatusSelector.SelectedIndex == -1)
                droneToListListView.ItemsSource = ibl.GetAllDrones(dr => dr.Weight == (IBL.BO.WeightCategories)WeightSelector.SelectedItem);
            else if (WeightSelector.SelectedIndex == -1)
                droneToListListView.ItemsSource = ibl.GetAllDrones(dr => dr.DroneStatus == (IBL.BO.DroneStatuses)StatusSelector.SelectedItem);
            else
                droneToListListView.ItemsSource = ibl.GetAllDrones(dr => dr.DroneStatus == (IBL.BO.DroneStatuses)StatusSelector.SelectedItem && dr.Weight == (IBL.BO.WeightCategories)WeightSelector.SelectedItem);
        }

        private void droneToListListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show(droneToListListView.SelectedItem.ToString());
            
            IBL.BO.DroneToList d = droneToListListView.SelectedItem as IBL.BO.DroneToList;
            new PL.Drone(ibl, d).ShowDialog();
        }
    }
}
