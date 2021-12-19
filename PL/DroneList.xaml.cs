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
            ibl = newIbl;
            //droneToListListView.ItemsSource = ibl.GetAllDrones();
            StatusSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatuses));
            WeightSelector.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
            InitializeComponent();
            
        }

        private void StatusSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(StatusSelector.SelectedIndex == -1&& WeightSelector.SelectedIndex==-1)
                droneToListListView.ItemsSource = ibl.GetAllDrones(dr=>dr.Id==dr.Id);
            else if (StatusSelector.SelectedIndex == -1)
                droneToListListView.ItemsSource = ibl.GetAllDrones(dr => dr.Weight == (IBL.BO.WeightCategories)WeightSelector.SelectedItem);
            else if (WeightSelector.SelectedIndex == -1)
                droneToListListView.ItemsSource = ibl.GetAllDrones(dr => dr.DroneStatus == (IBL.BO.DroneStatuses)StatusSelector.SelectedItem);
            else
                droneToListListView.ItemsSource = ibl.GetAllDrones(dr => dr.DroneStatus == (IBL.BO.DroneStatuses)StatusSelector.SelectedItem && dr.Weight == (IBL.BO.WeightCategories)WeightSelector.SelectedItem);
        }
    }
}
