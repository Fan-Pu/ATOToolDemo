using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using GalaSoft.MvvmLight;

namespace ATOToolDemo.ViewModel
{
    public class MultyTrainDataGrid : ViewModelBase
    {
        private string fileName; //日志文件名
        public string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                RaisePropertyChanged();
            }
        }
        private string chartType;
        public string ChartType
        {
            get { return chartType; }
            set { chartType = value;
                RaisePropertyChanged();

            }
        }


        private ObservableCollection<string> granularity;
        public ObservableCollection<string> Granularity
        {
            get { return granularity; }
            set
            {
                granularity = value;
                RaisePropertyChanged();
            }
        }

        private int gra_idx;
        public int Gra_idx
        {
            get { return gra_idx; }
            set
            {
                gra_idx = value;
                RaisePropertyChanged();
            }
        }

      
    }
}
