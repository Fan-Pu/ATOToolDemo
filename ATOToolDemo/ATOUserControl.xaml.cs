using ATOToolDemo.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ATOToolDemo
{
    /// <summary>
    /// ATOUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class ATOUserControl : UserControl
    {
        public ATOUserControl()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }
        //private void InitAxis()
        //{
        //    myAxisX.Separator.IsEnabled = false;
        //    myAxisX.Separator.Step = 2;
        //    myAxisX.Title = "Time";
        //    myAxisY.FontSize = 15;
        //    myAxisY.Separator.Step = 100;
        //    myAxisY.Separator.IsEnabled = false;
        //    myAxisY.Title = "Y";
        //}
    }
}
