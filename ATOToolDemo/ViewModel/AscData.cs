using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System.Windows;

namespace ATOToolDemo.ViewModel
{
    public class AscData:ViewModelBase
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value;
                
            }
        }

        private int data;
        public int Data
        {
            get { return data; }
            set { data = value;
                RaisePropertyChanged();
            }
        }

        private string range;

        public string Range
        {
            get { return range; }
            set { range = value;
              
            }
        }

        private string tips;

        public string Tips
        {
            get { return tips; }
            set { tips = value;
               
            }
        }

    }
}
