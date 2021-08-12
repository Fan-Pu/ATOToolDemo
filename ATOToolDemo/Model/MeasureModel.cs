using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOToolDemo.Model
{
    public class MeasureModel : ViewModelBase
    {
        private double x;
        public double X
        {
            get { return x; }
            set
            {
                x = value;
                RaisePropertyChanged();
            }
        }

        private double y;
        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                RaisePropertyChanged();
            }
        }
    }
}
