//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FlightSimulatorApp.Model
//{
//    public class PropertyChangedEventArgs
//    {
        
//        private Double val = 0;
//        private string name = "";
//        private string path = "";
//        public PropertyChangedEventArgs() { }

//        public PropertyChangedEventArgs(string propName)
//        {
//            this.name = propName;
//        }

//        public string Name
//        {
//            get { return this.name; }
//            set
//            {
//                this.name = value;
//            }
//        }

//        public Double Val
//        {
//            get { return this.val; }
//            set
//            {
//                this.val = value;
//            }
//        }

//        public string Path
//        {
//            get { return this.path; }
//            set
//            {
//                this.path = value;
//            }
//        }
//    }
//}


////using System;
////using System.Collections.Generic;
////using System.Linq;
////using System.Text;
////using System.Threading.Tasks;
////using System.ComponentModel;

////namespace FlightSimulatorApp.Model
////{
////    public class PropertyChangedEventArgs : System.ComponentModel.PropertyChangedEventArgs
////    {
////        private Double val = 0;
////        private string name = "";
////        private string path = "";

////        public PropertyChangedEventArgs(string propertyName) : base(propertyName) { }
////        public PropertyChangedEventArgs() : base("NVP") { }

////        public string Name
////        {
////            get { return this.name; }
////            set
////            {
////                this.name = value;
////            }
////        }

////        public Double Val
////        {
////            get { return this.val; }
////            set
////            {
////                this.val = value;
////            }
////        }

////        public string Path
////        {
////            get { return this.path; }
////            set
////            {
////                this.path = value;
////            }
////        }


////        public static Model.PropertyChangedEventArgs converPropertyChangedEventArgs(System.ComponentModel.PropertyChangedEventArgs e)
////        {
////            Model.PropertyChangedEventArgs e1 = e as Model.PropertyChangedEventArgs;

////            if (e1 == null) throw new NotImplementedException(); // if you got here, you used the wrong PropertyChangedEventArgs //
////            return e1;
////        }

////    }

////}


