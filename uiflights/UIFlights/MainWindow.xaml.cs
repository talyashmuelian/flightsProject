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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;
using BE.Flights;
using System.ComponentModel;
using System.Globalization;
using System.Threading;

namespace UIFlights
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBL bl = BLImp.theInstance();
        public MainWindow()
        {
            try
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");//set the language to english
                InitializeComponent();
                mainGrid.DataContext = new FlightsViewModel(myMap, Resources);
                historyGrid.DataContext = new FlightsHistoryViewModel();
                DateGrid.DataContext = new HolidayViewModel();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

    }
}