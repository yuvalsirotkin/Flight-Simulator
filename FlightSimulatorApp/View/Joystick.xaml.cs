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
        private double elevator;
        private double rudder;
        private Point point = new Point();
        private readonly Storyboard centerKnob;
        private double canvasWidth, canvasHeight;
        //private double prevRudder, prevElevator;
       
        
        public delegate void OnScreenJoystickEventHandler(Joystick sender, VirtualJoystickEventArgs args);
        public event OnScreenJoystickEventHandler IsMoved;

        public Joystick()
        {
            
            InitializeComponent();
            isClicked = false;
            centerKnob = Knob.Resources["CenterKnob"] as Storyboard;
        }

        public static readonly DependencyProperty RudderProperty =
        DependencyProperty.Register("Rudder", typeof(double), typeof(Joystick), null);

        public static readonly DependencyProperty ElevatorProperty =
            DependencyProperty.Register("Elevator", typeof(double), typeof(Joystick), null);

        public double Elevator
        {
            get { return Convert.ToDouble(GetValue(ElevatorProperty)); }
            set
            {
                if (value != elevator)
                {
                    // Check if the value is out of range or not.
                    if (value > 1)
                    {
                        value = 1;
                    }
                    else if (value < -1)
                    {
                        value = -1;
                    }
                    elevator = value;
                    // Set the ElevatorValueProperty with our value after we checked his range.
                    SetValue(ElevatorProperty, value);
                }
            }
        }

        public double Rudder
        {
            get { return Convert.ToDouble(GetValue(RudderProperty)); }
            set {
                // Update rudder only if the value has been changed.
                if (value != rudder)
                {
                    // Check if the value is out of range or not.
                    if (value > 1)
                    {
                        value = 1;
                    }
                    else if (value < -1)
                    {
                        value = -1;
                    }
                    rudder = value;
                    // Set the RudderValueProperty with our value after we checked his range.
                    SetValue(RudderProperty, value);
                }
            }
        }

        private void centerKnob_Completed(object sender, EventArgs e)
        {
        }


        // brings the knob to the middle
        private void Knob_MouseLeave(object sender, MouseEventArgs e)
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
        }

     

        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            canvasWidth = Base.ActualWidth - KnobBase.ActualWidth;
            canvasHeight = Base.ActualHeight - KnobBase.ActualHeight;
            if (e.ChangedButton == MouseButton.Left)
            {
                point.X = e.GetPosition(this).X;
                point.Y = e.GetPosition(this).Y;
                Knob.CaptureMouse();
                centerKnob.Stop();
            }
        }

        private void Base_MouseMove(object sender, MouseEventArgs e)
        {
            if (isClicked)
                Knob_MouseMove(sender, e);
        }
        // This function update the appropriate variables when the MouseMove.
        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            // If left button was clicked.
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // If the mouse is not captured then return.
                if (!Knob.IsMouseCaptured) return;
                Point newPos = e.GetPosition(this);
                Point deltaPos = new Point(newPos.X - point.X, newPos.Y - point.Y);
                Point updateKnobPoint = UpdateKnobPosition(deltaPos.X, deltaPos.Y);
                // Normalize rudder and elevator.
                Rudder = (deltaPos.X / (canvasWidth / 2));
                Elevator = (-deltaPos.Y / (canvasWidth / 2));
                knobPosition.X = updateKnobPoint.X;
                knobPosition.Y = updateKnobPoint.Y;
            }
        }
        // This function reset the values.
        public void ResetValues()
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
            Rudder = 0;
            Elevator = 0;
            centerKnob.Begin();
            Knob.ReleaseMouseCapture();
        }
        // A function that updates the values of the knob according to whether it is in/on a circle or not.
        private Point UpdateKnobPosition(double x, double y)
        {
            double powOfXY = Math.Pow(x, 2) + Math.Pow(y, 2);
            double powDiameterOfBase = Math.Pow(canvasWidth / 2, 2);
            // The coordinates are in or on the circle.
            if (powOfXY <= powDiameterOfBase)
            {
                // this coordinates are good.
                return new Point(x, y);
            }
            // Else powOfXY > powDiameterOfBase - thats mean that the coordinates are out of the circle.
            return new Point(( x * canvasWidth / 2 ) / Math.Sqrt(powOfXY), (y * canvasWidth / 2) / Math.Sqrt(powOfXY));
        }
        
        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            centerKnob.Begin();
            UIElement element = (UIElement)Knob;
            element.ReleaseMouseCapture();
            Rudder = 0;
            Elevator = 0;
        }

    }
}