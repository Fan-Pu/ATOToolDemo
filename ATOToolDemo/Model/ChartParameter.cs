using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace ATOToolDemo.Model
{
    public class ChartParameter:ViewModelBase
    {
        private BindingList<double> time;
        public BindingList<double> Time
        {
            get { return time; }
            set { time = value;
                RaisePropertyChanged();
            }
        }

        private BindingList<double> speed;
        public BindingList<double> Speed
        {
            get { return speed; }
            set { speed = value; RaisePropertyChanged(); }
        }

        private BindingList<double> targetSpeed;
        public BindingList<double> TargetSpeed
        {
            get { return targetSpeed; }
            set { targetSpeed = value; RaisePropertyChanged(); }
        }

        private BindingList<double> targetAcc;
        public BindingList<double> TargetAcc
        {
            get { return targetAcc; }
            set { targetAcc = value; RaisePropertyChanged(); }
        }

        private BindingList<double> trainAcc;
        public BindingList<double> TrainAcc
        {
            get { return trainAcc; }
            set { trainAcc = value; RaisePropertyChanged(); }
        }

        private BindingList<double> deltaAcc;
        public BindingList<double> DeltaAcc
        {
            get { return deltaAcc; }
            set { deltaAcc = value; RaisePropertyChanged(); }
        }

        private BindingList<double> trainPosition;
        public BindingList<double> TrainPosition
        {
            get { return trainPosition; }
            set { trainPosition = value;
                RaisePropertyChanged();
            }
        }

        private BindingList<double> status;
        public BindingList<double> Status
        {
            get { return status; }
            set { status = value;
                RaisePropertyChanged();
            }
        }


    }
}
