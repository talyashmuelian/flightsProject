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

namespace UIFlights
{
    /// <summary>
    /// Interaction logic for Gadget.xaml
    /// </summary>
    public partial class Gadget : Window
    {
        public Gadget(FlightModel flightModel)
        {
            InitializeComponent();
            GadgetVM gadgetVM = new GadgetVM(flightModel);
            this.DataContext = gadgetVM;
        }
        private void window_leftButton(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
