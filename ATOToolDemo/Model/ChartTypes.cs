using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace ATOToolDemo.Model
{
    public class ChartTypes:ViewModelBase
    {

        private BindingList<string> svt;
        public BindingList<string> SVT
        {
            get { return svt; }
            set
            {
                svt = value;
                RaisePropertyChanged();
            }
        }

        private int svt_Index;
        public int SVT_Index
        {
            get { return svt_Index; }
            set { svt_Index = value;
                RaisePropertyChanged();
            }
        }

        private bool isStatusChanged;
        public bool IsStatusChanged
        {
            get { return isStatusChanged; }
            set { isStatusChanged = value;
                RaisePropertyChanged();
            }
        }

        private bool lastStatus;
        public bool LastStatus
        {
            get { return lastStatus; }
            set { lastStatus = value;
                RaisePropertyChanged();
            }
        }

        private bool isAccChanged;
        public bool IsAccChanged
        {
            get { return isAccChanged; }
            set { isAccChanged = value;
                RaisePropertyChanged();
            }
        }

        private bool lastAcc;
        public bool LastAcc
        {
            get { return lastAcc; }
            set { lastAcc = value;
                RaisePropertyChanged();
            }
        }

    }
}
