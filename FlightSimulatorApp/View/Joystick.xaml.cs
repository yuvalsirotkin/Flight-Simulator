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

        ////sets the knob
        //private void centerKnob_Completed(Object sender, EventArgs e)
        //{
        //    prevRudder = prevElevator = 0;
        //}

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
            return ClosestIntersection(canvasWidth / 2.0, new Point(-x, -y));
        }
        // Function that returns the closest intersection to lineEnd point.
        public Point ClosestIntersection(double radius, Point lineEnd)
        {
            Point intersection1;
            Point intersection2;
            // Find the intersections point.
            FindLineCircleIntersections(radius, lineEnd, out intersection1, out intersection2);
            double dist1 = Math.Sqrt(Math.Pow(-intersection1.X, 2) + Math.Pow(-intersection1.Y, 2));
            double dist2 = Math.Sqrt(Math.Pow(-intersection2.X, 2) + Math.Pow(-intersection2.Y, 2));
            // Checking which point is the closest.
            if (dist1 < dist2) { return intersection1; }
            return intersection2;
        }
        // Find the points of intersection.
        private void FindLineCircleIntersections(double radius, Point point2, out Point intersection1, out Point intersection2)
        {
            double dx, dy, A, C, delta, t;
            dx = point2.X;
            dy = point2.Y;
            // Calculate A,C for line equation (there is no need to calculate B because it will always be zero).
            A = dx * dx + dy * dy;
            C = -radius * radius;
            // Delta for finding solutions.
            delta = -4 * A * C;
            // Two solutions.
            t = ((Math.Sqrt(delta)) / (2 * A));
            intersection1 = new Point(t * dx, t * dy);
            t = ((-Math.Sqrt(delta)) / (2 * A));
            intersection2 = new Point(t * dx, t * dy);
        }

    private void Knob_MouseMove1(object sender, MouseEventArgs e)
        {
            // If left button was clicked.
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // If the mouse is not captured then return.
                if (!Knob.IsMouseCaptured) return;
                Point newPos = e.GetPosition(Base);
                Point deltaPos = new Point(newPos.X - point.X, newPos.Y - point.Y);
                Point updateKnobPoint = UpdateKnobPosition(deltaPos.X, deltaPos.Y);
                // Normalize rudder and elevator.
                Rudder = (deltaPos.X / (canvasWidth / 2));
                Elevator = (-deltaPos.Y / (canvasWidth / 2));
                knobPosition.X = updateKnobPoint.X;
                knobPosition.Y = updateKnobPoint.Y;
            }
        }

        private Point UpdateKnobPosition1(double x, double y)
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
            //return ClosestIntersection(canvasWidth / 2.0, new Point(-x, -y));
            return new Point(x/Math.Sqrt(powOfXY), y/Math.Sqrt(powOfXY));
        }

        private void Knob_MouseMove11(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!Knob.IsMouseCaptured) return;

                Point deltaPos = new Point(e.GetPosition(this).X - point.X, e.GetPosition(this).Y - point.Y);

                double distance = Math.Round(Math.Sqrt(deltaPos.X * deltaPos.X + deltaPos.Y * deltaPos.Y));
                if (distance < canvasWidth / 2 && distance < canvasHeight / 2)
                

                    // switched locations and div by 124
                    Elevator = -deltaPos.Y / 124;
                    Rudder = deltaPos.X / 124;

                    knobPosition.X = deltaPos.X;
                    knobPosition.Y = deltaPos.Y;

                    //if (IsMoved == null ||
                    //    (!(Math.Abs(prevRudder - Rudder) > Rudder) && !(Math.Abs(prevElevator - Elevator) > Elevator)))
                    //    return;

                    //IsMoved?.Invoke(this, new VirtualJoystickEventArgs { Rudder = Rudder, Elevator = Elevator });
                    //prevRudder = Rudder;
                    //prevElevator = Elevator;
                
                //else
                //{
                //    this.outOfBound = true;
                //    knobPosition.X = deltaPos.X / distance;
                //    knobPosition.Y = deltaPos.Y / distance;
                //    Mouse.Capture(Knob);
                //    //Elevator = -deltaPos.Y / distance / 124;
                //    //Rudder = deltaPos.X / distance / 124;
                //    //if (IsMoved == null ||
                //    //     (!(Math.Abs(prevRudder - Rudder) > Rudder) && !(Math.Abs(prevElevator - Elevator) > Elevator)))
                //    //    return;

                //    //IsMoved?.Invoke(this, new VirtualJoystickEventArgs { Rudder = Rudder, Elevator = Elevator });
                //    //prevRudder = Rudder;
                //    //prevElevator = Elevator;

                //    //if (Rudder > 1 || Elevator > 1)
                //    //{
                //    //    Console.WriteLine("why??");
                //    //}
                //    //if (IsMoved == null ||
                //    //  (!(Math.Abs(prevRudder - Rudder) > Rudder) && !(Math.Abs(prevElevator - Elevator) > Elevator)))
                //    //    return;

                //    //IsMoved?.Invoke(this, new VirtualJoystickEventArgs { Rudder = Rudder, Elevator = Elevator });
                //    //prevRudder = Rudder;
                //    //prevElevator = Elevator;

                //    ////Console.Write(deltaPos.X);
                //    ////Console.WriteLine(deltaPos.Y);
                //    ////double mycalcInRadians = Math.Asin(Math.Abs(e.GetPosition(this).X) / Math.Abs(e.GetPosition(this).Y));
                //    ////if (e.GetPosition(this).X < 0 && e.GetPosition(this).Y <0)
                //    ////{

                //    ////}
                //    ////Console.WriteLine(mycalcInRadians);
                //}
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