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
        private readonly Storyboard centerKnob;
        private double canvasWidth, canvasHeight;
        private double prevRudder, prevElevator;

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
            prevRudder = prevElevator = 0;
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
            prevRudder = prevElevator = 0;
        }

        private void Base_MouseMove(object sender, MouseEventArgs e)
        {
            if (isClicked)
                Knob_MouseMove(sender, e);
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {

            //if (e.LeftButton == MouseButtonState.Pressed)
            //{
            //    double x = (e.GetPosition(this).X - point.X);
            //    double y = (e.GetPosition(this).Y - point.Y);
            //    if (Math.Sqrt(x * x + y * y) < (Base.Width - KnobBase.Width) / 2)
            //    {
            //        knobPosition.X = x;
            //        knobPosition.Y = y;
            //    }                
            //    Rudder = x / (2 * (Base.Width - KnobBase.Width));
            //    Elevator = y / (2 * (Base.Width - KnobBase.Width));

            //}

            if (!Knob.IsMouseCaptured) return;

            Point deltaPos = new Point(e.GetPosition(this).X - point.X, e.GetPosition(this).Y - point.Y);

            double distance = Math.Round(Math.Sqrt(deltaPos.X * deltaPos.X + deltaPos.Y * deltaPos.Y));
            if (distance < canvasWidth / 2 && distance < canvasHeight / 2)
            {

                // switched locations and div by 124
                Elevator = -deltaPos.Y / 124;
                Rudder = deltaPos.X / 124;

                knobPosition.X = deltaPos.X;
                knobPosition.Y = deltaPos.Y;

                if (IsMoved == null ||
                    (!(Math.Abs(prevRudder - Rudder) > Rudder) && !(Math.Abs(prevElevator - Elevator) > Elevator)))
                    return;
                
                IsMoved?.Invoke(this, new VirtualJoystickEventArgs { Rudder = Rudder, Elevator = Elevator });
                prevRudder = Rudder;
                prevElevator = Elevator;
            } else
            {
                knobPosition.X = deltaPos.X / distance;
                knobPosition.Y = deltaPos.Y / distance;
                Elevator = -deltaPos.Y / distance/ 124;
                Rudder = deltaPos.X / distance / 124;
                
                //if (Rudder > 1 || Elevator > 1)
                //{
                //    Console.WriteLine("why??");
                //}
                //if (IsMoved == null ||
                //  (!(Math.Abs(prevRudder - Rudder) > Rudder) && !(Math.Abs(prevElevator - Elevator) > Elevator)))
                //    return;

                //IsMoved?.Invoke(this, new VirtualJoystickEventArgs { Rudder = Rudder, Elevator = Elevator });
                //prevRudder = Rudder;
                //prevElevator = Elevator;

                ////Console.Write(deltaPos.X);
                ////Console.WriteLine(deltaPos.Y);
                ////double mycalcInRadians = Math.Asin(Math.Abs(e.GetPosition(this).X) / Math.Abs(e.GetPosition(this).Y));
                ////if (e.GetPosition(this).X < 0 && e.GetPosition(this).Y <0)
                ////{

                ////}
                ////Console.WriteLine(mycalcInRadians);
            }


        }

        //private void Knob_MouseMove(object sender, MouseEventArgs e)
        //{

        //    //if (e.LeftButton == MouseButtonState.Pressed)
        //    //{
        //    //    double x = (e.GetPosition(this).X - point.X);
        //    //    double y = (e.GetPosition(this).Y - point.Y);
        //    //    if (Math.Sqrt(x * x + y * y) < (Base.Width - KnobBase.Width) / 2)
        //    //    {
        //    //        knobPosition.X = x;
        //    //        knobPosition.Y = y;
        //    //    }                
        //    //    Rudder = x / (2 * (Base.Width - KnobBase.Width));
        //    //    Elevator = y / (2 * (Base.Width - KnobBase.Width));

        //    //}

        //    if (!Knob.IsMouseCaptured) return;

        //    Point deltaPos = new Point(e.GetPosition(this).X - point.X, e.GetPosition(this).Y - point.Y);

        //    double distance = Math.Round(Math.Sqrt(deltaPos.X * deltaPos.X + deltaPos.Y * deltaPos.Y));
        //    if (distance >= canvasWidth / 2 || distance >= canvasHeight / 2)
        //        return;
        //    // switched locations and div by 124
        //    Elevator = -deltaPos.Y / 124;
        //    Rudder = deltaPos.X / 124;

        //    knobPosition.X = deltaPos.X;
        //    knobPosition.Y = deltaPos.Y;

        //    if (IsMoved == null ||
        //        (!(Math.Abs(prevRudder - Rudder) > Rudder) && !(Math.Abs(prevElevator - Elevator) > Elevator)))
        //        return;

        //    IsMoved?.Invoke(this, new VirtualJoystickEventArgs { Rudder = Rudder, Elevator = Elevator });
        //    prevRudder = Rudder;
        //    prevElevator = Elevator;


        //}

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