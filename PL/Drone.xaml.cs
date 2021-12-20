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
        public Drone(IBL.IBL newIbl)    //add constructor
        {
            InitializeComponent();
            ibl = newIbl;

            StatusComboBox.ItemsSource = Enum.GetValues(typeof(IBL.BO.DroneStatuses));
            WeightComboBox.ItemsSource = Enum.GetValues(typeof(IBL.BO.WeightCategories));
            this.Title = "Drone Addition";
            OptionButtun.Content = "ADD The Drone";


        }

        public Drone(IBL.IBL newIbl, IBL.BO.Drone d) //update constructor
        {
            InitializeComponent();
            ibl = newIbl;

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
    }
}
