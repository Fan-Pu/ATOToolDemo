using System;
using System.Collections.Generic;
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
    }
}
