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
            droneToListDataGrid.ItemsSource = lst;
            droneToListDataGrid.IsReadOnly = true;
            StatusSelector.ItemsSource = Enum.GetValues(typeof(DroneStatuses));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(WeightCategories));
            //LatColunm.Binding.StringFormat = ibl.GetSexagesimalLatitude();
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filterListDrones();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new PL.DroneWindow(ibl).Show();
        }

        private void filterListDrones()
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

        private void WeightSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filterListDrones();
        }

        private void droneToListDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DroneToList d = droneToListDataGrid.SelectedItem as DroneToList;
            Drone dr = ibl.GetDrone(d.Id);
            new PL.DroneWindow(ibl, dr).Show();
            droneToListDataGrid.ItemsSource = ibl.GetAllDrones();
            filterListDrones();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            droneToListDataGrid.ItemsSource = ibl.GetAllDrones();
            filterListDrones();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            List<DroneToList> lst1 = new List<DroneToList>();
            List<IGrouping<DroneStatuses, DroneToList>> result =  ibl.GetAllDrones()
                                                                  .GroupBy (b=>b.DroneStatus) 
                                                                   .ToList();
            foreach (IGrouping<DroneStatuses, DroneToList> g in result)
            {
                switch (g.Key)
                {
                    case (DroneStatuses)0:
                        {

                            foreach (DroneToList n in g)
                                lst1.Add(n);
                            break;
                        }
                    case (DroneStatuses)1:
                        {
                            foreach (DroneToList n in g)
                                lst1.Add(n);
                            break;
                        }
                    case (DroneStatuses)2:
                        {
                            foreach (DroneToList n in g)
                                lst1.Add(n);
                            break;
                        }
                }
            }
            droneToListDataGrid.ItemsSource = lst1;
         //  filterListDrones();

        }

        private void clearButtun_Click(object sender, RoutedEventArgs e)
        {
            droneToListDataGrid.ItemsSource = (from drone in ibl.GetAllDrones()
                                               orderby drone.Id
                                               select drone);
           WeightSelector.SelectedIndex = -1;
          StatusSelector.SelectedIndex = -1;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
