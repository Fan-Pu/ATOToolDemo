using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using LiveCharts;

namespace ATOToolDemo.ViewModel
{
    public class LiveChartDatas:ViewModelBase
    {
        private SeriesCollection seriesCollection;
        public SeriesCollection SeriesCollection
        {
            get { return seriesCollection; }
            set
            {
                seriesCollection = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<string> labels_X;
        public ObservableCollection<string> Labels_X
        {
            get { return labels_X; }
            set
            {
                labels_X = value;
                RaisePropertyChanged();
            }
        }

        private double height_Mychart;
        public double Height_MyChart
        {
            get { return height_Mychart; }
            set { height_Mychart = value;
                RaisePropertyChanged();
            }
        }

    }
}
