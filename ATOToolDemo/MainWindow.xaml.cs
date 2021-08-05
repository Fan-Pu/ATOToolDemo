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
using System.Windows.Forms;
namespace ATOToolDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Fluent.RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
            InitAxis();
        }
        private void InitAxis()
        {
            hobushAxisX.Separator.IsEnabled = false;
            hobushAxisX.Separator.Step = 1;
            hobushAxisX.Title = "Time";
            hobushAxisX.FontSize = 15;
            hobushAxisY.Separator.Step = 200;
            hobushAxisY.Separator.IsEnabled = false;
            hobushAxisY.Title = "Y";
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void WatermarkComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
