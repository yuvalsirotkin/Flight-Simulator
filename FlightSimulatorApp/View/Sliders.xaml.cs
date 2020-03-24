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

namespace FlightSimulatorApp.View
{
    /// <summary>
    /// Interaction logic for Sliders.xaml
    /// </summary>
    public partial class Sliders : UserControl
    {
        public Sliders()
        {
            InitializeComponent();
        }

        private void ThrottleSliderVal(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ThrottleValue.Text = ThrottleSlider.Value.ToString();
        }
        private void AileronSliderVal(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            AileronValue.Text = AileronSlider.Value.ToString();
        }
        private void RudderSliderVal(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            RudderValue.Text = RudderSlider.Value.ToString();
        }
        private void ElevaorSliderVal(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ElevaorValue.Text = ElevaorSlider.Value.ToString();
        }

    }
}
