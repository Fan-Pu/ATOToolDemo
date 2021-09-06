using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATOToolDemo.Model;
using GalaSoft.MvvmLight;
using LiveCharts;

namespace ATOToolDemo.ViewModel
{
    public class LiveChartParemeters: ViewModelBase
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
        private ObservableCollection<string> label_Y;
        public ObservableCollection<string> Label_Y
        {
            get { return label_Y; }
            set { label_Y = value;
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

        private double width_Mychart;
        public double Width_MyChart
        {
            get { return width_Mychart; }
            set { width_Mychart = value;
                RaisePropertyChanged();
            }
        }

        private double step_X;
        public double Step_X
        {
            get { return step_X; }
            set { step_X = value;
                RaisePropertyChanged();
            }
        }
        private double step_Y;
        public double Step_Y
        {
            get { return step_Y; }
            set
            {
                step_Y = value;
                RaisePropertyChanged();
            }
        }

        private double maxValue_MyChart;
        public double MaxValue_MyChart
        {
            get { return maxValue_MyChart; }
            set
            {
                maxValue_MyChart = value;
                RaisePropertyChanged();
            }
        }

        private double minValue_MyChart;
        public double MinValue_MyChart
        {
            get { return minValue_MyChart; }
            set
            {
                minValue_MyChart = value;
                RaisePropertyChanged();
            }
        }

        private string title_X;
        public string Title_X
        {
            get { return title_X; }
            set {
                title_X = value;
                RaisePropertyChanged();
            }
        }

        private string title_Y;

        public string Title_Y
        {
            get { return title_Y; }
            set {
                title_Y = value;
                RaisePropertyChanged();
            }
        }
        


    }
}
