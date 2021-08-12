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
    public class LogFile_Mult : ViewModelBase
    {
        private string fileName_logMult; //日志文件名
        public string FileName_logMult
        {
            get { return fileName_logMult; }
            set
            {
                fileName_logMult = value;
                RaisePropertyChanged();
            }
        }

        private string train_logMult;
        public string Train_logMult
        {
            get { return train_logMult; }
            set
            {
                train_logMult = value;
                RaisePropertyChanged();
            }
        }

        private BindingList<ComboBoxItem> interval;
        public BindingList<ComboBoxItem> Interval
        {
            get { return interval; }
            set
            {
                interval = value;
                RaisePropertyChanged();

            }
        }

        private ObservableCollection<ComboBoxItem> granularity;
        public ObservableCollection<ComboBoxItem> Granularity
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

        private int interval_idx;
        public int Interval_idx
        {
            get { return interval_idx; }
            set
            {
                interval_idx = value;
                RaisePropertyChanged();
            }
        }

    }
}
