using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ATOToolDemo.ViewModel
{
    public class AscDatas:ViewModelBase
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value;   
            }
        }

        private string data;
        public string Data
        {
            get { return data; }
            set { data = value;    
            }
        }

        private string range;
        public string Range
        {
            get { return range; }
            set { range = value;}
        }

        private string tips;
        public string Tips
        {
            get { return tips; }
            set { tips = value;  
            }
        }

        private SolidColorBrush asc_Background;
        public SolidColorBrush Asc_Background
        {
            get { return asc_Background; }
            set
            {
                asc_Background = value;
                RaisePropertyChanged();
            }
        }

        private string data_property;
        public string Data_Property
        {
            get { return data_property; }
            set { data_property = value; }
        }


    }
}
