using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.Model
{
    public class PropertyChangedEventArgs
    {
        private Double val = 0;
        private string name = "";
        private string path = "";

        public string Name
        {
            get { return this.name; }
            set
            {
                this.name = value;
            }
        }

        public Double Val
        {
            get { return this.val; }
            set
            {
                this.val = value;
            }
        }

        public string Path
        {
            get { return this.path; }
            set
            {
                this.path = value;
            }
        }
    }
}
