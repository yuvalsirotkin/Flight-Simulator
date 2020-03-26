using FlightSimulator.Model;
using FlightSimulator.Model.EventArgs;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;


namespace FlightSimulatorApp.View
{
    
    public partial class Joystick : UserControl
    {
        private bool isClicked;
        private Point point = new Point();
        

        public Joystick()
        {
            InitializeComponent();
            isClicked = false;
        }

        public static readonly DependencyProperty RudderProperty =
        DependencyProperty.Register("Rudder", typeof(double), typeof(Joystick), null);

        public static readonly DependencyProperty ElevatorProperty =
            DependencyProperty.Register("Elevator", typeof(double), typeof(Joystick), null);

        public double Rudder
        {
            get { return Convert.ToDouble(GetValue(RudderProperty)); }
            set { SetValue(RudderProperty, value); }
        }

        public double Elevator
        {
            get { return Convert.ToDouble(GetValue(ElevatorProperty)); }
            set { SetValue(ElevatorProperty, value); }
        }

        // brings the knob to the middle
        private void Knob_MouseLeave(object sender, MouseEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
        }

        //sets the knob
        private void centerKnob_Completed(Object sender, EventArgs e)
        {
            //knobPosition.X = 0;
            //knobPosition.Y = 0;
        }

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                point = e.GetPosition(this);
                isClicked = true;
            }
        }

        private void Base_MouseMove(object sender, MouseEventArgs e)
        {
            if (isClicked)
                Knob_MouseMove(sender, e);
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            
            if (isClicked)
            {
                double x = (e.GetPosition(this).X - point.X);
                double y = (e.GetPosition(this).Y - point.Y);

                if (Math.Sqrt(x * x + y * y) < (Base.Width - KnobBase.Width) / 2)
                {
                    knobPosition.X = x;
                    knobPosition.Y = y;
                }

                    Rudder = x / 2 * (Base.Width - KnobBase.Width);
                Elevator = y / 2 * (Base.Width - KnobBase.Width);

            }
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
            Rudder = 0;
            Elevator = 0;
            this.isClicked = false;
        
            
        }

    }
}