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
    public enum Granularity { 正常, 扩大一倍, 缩小一倍,自适应 };
    public class Customer
    {
        public Granularity myGra { get; set; }
    }
    
    public partial class MainWindow : Fluent.RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            #region 启动时串口最大化显示
            Rect rc = SystemParameters.WorkArea; //获取工作区大小
            double scale = 0.8;
            this.Left = 0; //设置位置
            this.Top = 0;
            this.Width = rc.Width * scale;
            this.Height = rc.Height * scale;
            #endregion
            this.DataContext = new MainViewModel(rc.Height * scale, rc.Width * scale);
            Customer cm = new Customer();
            cm.myGra = Granularity.正常;
        }
        private void WatermarkComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
