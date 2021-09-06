using GalaSoft.MvvmLight;
using Fluent;
using GalaSoft.MvvmLight.Command;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Data;
using LiveCharts;
using LiveCharts.Wpf;
using System.IO;
using System.Text;
using System;
using System.Windows;
using System.Windows.Media;
using ATOToolDemo.Model;
using LiveCharts.Configurations;
using MessageBox = System.Windows.MessageBox;

namespace ATOToolDemo.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region [properties]

        #region [日志文件单列车属性]

        /// <summary>
        /// 单列车的文件名索引
        /// </summary>
        private int curr_fileSingle_idx;
        public int Curr_fileSingle_idx
        {
            get { return curr_fileSingle_idx; }
            set
            {
                curr_fileSingle_idx = value;
                RaisePropertyChanged();
                if (Curr_fileSingle_idx >= 0)
                    Filename_logSingle = FileNames[Curr_fileSingle_idx];
            }
        }

        /// <summary>
        /// 单列车的列车名索引
        /// </summary>
        private int curr_trainSingle_idx;
        public int Curr_trainSingle_idx
        {
            get { return curr_trainSingle_idx; }
            set
            {
                curr_trainSingle_idx = value;
                RaisePropertyChanged();

            }
        }

        /// <summary>
        /// 单列车的列车名
        /// </summary>
        private BindingList<ComboBoxItem> trainNameList_LogSingle;
        public BindingList<ComboBoxItem> TrainNameList_LogSingle
        {
            get { return trainNameList_LogSingle; }
            set
            {
                trainNameList_LogSingle = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 单列车文件名
        /// </summary>
        private string filename_logSingle;
        public string Filename_logSingle
        {
            get { return filename_logSingle; }
            set
            {
                filename_logSingle = value;
                RaisePropertyChanged();
                ExcelHelper excelhelper_Single = new ExcelHelper(Filename_logSingle);
                Data_Single = excelhelper_Single.ExcelToDataTable("sheet1", true);
                TrainNameList_LogSingle = new BindingList<ComboBoxItem>();
                for (int i = 1; i < Data_Single.Rows.Count - 3000; i++)
                {
                    var item = new ComboBoxItem()
                    {
                        Content = Data_Single.Rows[i][0]
                    };
                    TrainNameList_LogSingle.Add(item);
                }
                #region 临时变量初始化
                ChartDatas paraTemp = new ChartDatas();
                paraTemp.Time = new BindingList<double>();
                paraTemp.Speed = new BindingList<double>();
                paraTemp.Status = new BindingList<double>();
                paraTemp.TargetAcc = new BindingList<double>();
                paraTemp.TargetSpeed = new BindingList<double>();
                paraTemp.TrainPosition = new BindingList<double>();
                paraTemp.TrainAcc = new BindingList<double>();
                paraTemp.DeltaAcc = new BindingList<double>();
                Double temp = 0;
                #endregion
                for (int i = 1; i < Data_Single.Rows.Count; i++)
                {
                    if (Data_Single.Rows[i][15].ToString() != "Error" &&
                        (paraTemp.TrainPosition.Count - 1 < 0 || Double.Parse(Data_Single.Rows[i][9].ToString()) + temp != paraTemp.TrainPosition[paraTemp.TrainPosition.Count - 1]) && Double.Parse(Data_Single.Rows[i][9].ToString()) != 0)
                    {
                        if (paraTemp.TrainPosition.Count - 1 < 0)
                        {
                            paraTemp.Time.Add(((i - 1) * 0.25));
                            paraTemp.Speed.Add(Double.Parse(Data_Single.Rows[i][2].ToString()));
                            paraTemp.TargetSpeed.Add(Double.Parse(Data_Single.Rows[i][3].ToString()));
                            paraTemp.TargetAcc.Add(Double.Parse(Data_Single.Rows[i][6].ToString()));
                            paraTemp.TrainAcc.Add(Double.Parse(Data_Single.Rows[i][6].ToString()));
                            paraTemp.DeltaAcc.Add(Double.Parse(Data_Single.Rows[i][7].ToString()));
                            paraTemp.TrainPosition.Add(Double.Parse(Data_Single.Rows[i][9].ToString()) + temp);
                            if (Data_Single.Rows[i][15].ToString() == "Traction")
                                paraTemp.Status.Add(2.0);
                            else if (Data_Single.Rows[i][15].ToString() == "Coast")
                                paraTemp.Status.Add(1.0);
                            else if (Data_Single.Rows[i][15].ToString() == "Brake")
                                paraTemp.Status.Add(0.0);
                            continue;
                        }
                        else if (paraTemp.TrainPosition[paraTemp.TrainPosition.Count - 1] - Double.Parse(Data_Single.Rows[i][9].ToString()) - temp < 0.1 &&
                            paraTemp.TrainPosition[paraTemp.TrainPosition.Count - 1] - Double.Parse(Data_Single.Rows[i][9].ToString()) - temp > 0)
                            continue;
                        paraTemp.Time.Add(((i - 1) * 0.25));
                        paraTemp.Speed.Add(Double.Parse(Data_Single.Rows[i][2].ToString()));
                        paraTemp.TargetSpeed.Add(Double.Parse(Data_Single.Rows[i][3].ToString()));
                        if (Double.Parse(Data_Single.Rows[i][6].ToString()) - paraTemp.TargetAcc[paraTemp.TrainAcc.Count - 1] > 10 || Double.Parse(Data_Single.Rows[i][6].ToString()) - paraTemp.TrainAcc[paraTemp.TrainAcc.Count - 1] < -10)
                            paraTemp.TrainAcc.Add(paraTemp.TrainAcc[paraTemp.TrainAcc.Count - 1]);
                        else
                            paraTemp.TrainAcc.Add(Double.Parse(Data_Single.Rows[i][6].ToString()));
                        if (Double.Parse(Data_Single.Rows[i][5].ToString()) - paraTemp.TargetAcc[paraTemp.TargetAcc.Count - 1] > 10 || Double.Parse(Data_Single.Rows[i][5].ToString()) - paraTemp.TargetAcc[paraTemp.TargetAcc.Count - 1] < -10)
                            paraTemp.TargetAcc.Add(paraTemp.TargetAcc[paraTemp.TargetAcc.Count - 1]);
                        else
                            paraTemp.TargetAcc.Add(Double.Parse(Data_Single.Rows[i][5].ToString()));
                        if (Double.Parse(Data_Single.Rows[i][7].ToString()) > 1000 || Double.Parse(Data_Single.Rows[i][7].ToString()) < -1000)
                            paraTemp.DeltaAcc.Add(1.0);
                        else
                            if (Double.Parse(Data_Single.Rows[i][7].ToString()) - paraTemp.DeltaAcc[paraTemp.DeltaAcc.Count - 1] > 10 || Double.Parse(Data_Single.Rows[i][7].ToString()) - paraTemp.DeltaAcc[paraTemp.DeltaAcc.Count - 1] < -10)
                            paraTemp.DeltaAcc.Add(paraTemp.DeltaAcc[paraTemp.DeltaAcc.Count - 1]);
                        else
                            paraTemp.DeltaAcc.Add(Double.Parse(Data_Single.Rows[i][7].ToString()));
                        if (paraTemp.TrainPosition.Count == 0)
                            paraTemp.TrainPosition.Add(Double.Parse(Data_Single.Rows[i][9].ToString()) + temp);
                        else
                        {
                            if (Double.Parse(Data_Single.Rows[i][9].ToString()) + temp > paraTemp.TrainPosition[paraTemp.TrainPosition.Count - 1])
                                paraTemp.TrainPosition.Add(Double.Parse(Data_Single.Rows[i][9].ToString()) + temp);
                            else
                            {
                                temp = paraTemp.TrainPosition[paraTemp.TrainPosition.Count - 1];
                                paraTemp.TrainPosition.Add(Double.Parse(Data_Single.Rows[i][9].ToString()) + temp);
                            }
                        }
                        if (Data_Single.Rows[i][15].ToString() == "Traction")
                            paraTemp.Status.Add(2.0);
                        else if (Data_Single.Rows[i][15].ToString() == "Coast")
                            paraTemp.Status.Add(1.0);
                        else if (Data_Single.Rows[i][15].ToString() == "Brake")
                            paraTemp.Status.Add(0.0);
                        else
                            paraTemp.Status.Add(-1.0);
                    }
                }
                
                MySinChartDatass = paraTemp;
            }
        }

        /// <summary>
        /// 单列车图像参数
        /// </summary>
        private ChartDatas mySinChartDatass;
        public ChartDatas MySinChartDatass
        {
            get { return mySinChartDatass; }
            set
            {
                mySinChartDatass = value;
                RaisePropertyChanged();
            }
        }

        private BindingList<string> mySinChartTypes;
        public BindingList<string> MySinChartTypes
        {
            get { return mySinChartTypes; }
            set
            {
                mySinChartTypes = value;
                RaisePropertyChanged();
            }
        }

        private int mySinChartTypesIdx;
        public int MySinChartTypesIdx
        {
            get { return mySinChartTypesIdx; }
            set {
                mySinChartTypesIdx = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region [日志文件多列车属性]

        /// <summary>
        /// 显示多列车展示的属性
        /// </summary>
        private BindingList<MultyTrainDataGrid> logFileProp_Mult;
        public BindingList<MultyTrainDataGrid> LogFileProp_Mult
        {
            get
            {
                return logFileProp_Mult;
            }
            set
            {
                logFileProp_Mult = value;
                RaisePropertyChanged();
            }
        }

        private int curr_fileMult_idx;
        public int Curr_fileMult_idx
        {
            get { return curr_fileMult_idx; }
            set
            {
                curr_fileMult_idx = value;
                RaisePropertyChanged();
                if (Curr_fileMult_idx >= 0)
                    Filename_logMult = FileNames[Curr_fileMult_idx];
            }
        }

        private string filename_logMult;
        public string Filename_logMult
        {
            get { return filename_logMult; }
            set
            {
                filename_logMult = value;
                RaisePropertyChanged();
                ExcelHelper excelhelper_Mult = new ExcelHelper(Filename_logMult);
                Data_Mult = excelhelper_Mult.ExcelToDataTable("sheet1", true);
                //TrainNameList_LogMult = new BindingList<ComboBoxItem>();
                for (int i = 1; i < Data_Mult.Rows.Count - 3000; i++)
                {
                    var item = new ComboBoxItem()
                    {
                        Content = Data_Mult.Rows[i][0]
                    };
                    //TrainNameList_LogMult.Add(item);
                }
                #region 临时变量初始化
                ChartDatas paraTemp = new ChartDatas();
                paraTemp.Time = new BindingList<double>();
                paraTemp.Speed = new BindingList<double>();
                paraTemp.Status = new BindingList<double>();
                paraTemp.TargetAcc = new BindingList<double>();
                paraTemp.TargetSpeed = new BindingList<double>();
                paraTemp.TrainPosition = new BindingList<double>();
                paraTemp.TrainAcc = new BindingList<double>();
                paraTemp.DeltaAcc = new BindingList<double>();
                Double temp = 0;
                #endregion
                for (int i = 1; i < Data_Mult.Rows.Count; i++)
                {
                    if (Data_Mult.Rows[i][15].ToString() != "Error" &&
                        (paraTemp.TrainPosition.Count - 1 < 0 ||
                        Double.Parse(Data_Mult.Rows[i][9].ToString()) + temp != paraTemp.TrainPosition[paraTemp.TrainPosition.Count - 1])
                        && Double.Parse(Data_Mult.Rows[i][9].ToString()) != 0)
                    {
                        if (paraTemp.TrainPosition.Count - 1 < 0)
                        {
                            paraTemp.Time.Add(((i - 1) * 0.25));
                            paraTemp.Speed.Add(Double.Parse(Data_Mult.Rows[i][2].ToString()));
                            paraTemp.TargetSpeed.Add(Double.Parse(Data_Mult.Rows[i][3].ToString()));
                            paraTemp.TargetAcc.Add(Double.Parse(Data_Mult.Rows[i][6].ToString()));
                            paraTemp.TrainAcc.Add(Double.Parse(Data_Mult.Rows[i][6].ToString()));
                            paraTemp.DeltaAcc.Add(Double.Parse(Data_Mult.Rows[i][7].ToString()));
                            paraTemp.TrainPosition.Add(Double.Parse(Data_Mult.Rows[i][9].ToString()) + temp);
                            if (Data_Mult.Rows[i][15].ToString() == "Traction")
                                paraTemp.Status.Add(2.0);
                            else if (Data_Mult.Rows[i][15].ToString() == "Coast")
                                paraTemp.Status.Add(1.0);
                            else if (Data_Mult.Rows[i][15].ToString() == "Brake")
                                paraTemp.Status.Add(0.0);
                            continue;
                        }
                        else if (paraTemp.TrainPosition[paraTemp.TrainPosition.Count - 1] - Double.Parse(Data_Mult.Rows[i][9].ToString()) - temp < 0.1 &&
                            paraTemp.TrainPosition[paraTemp.TrainPosition.Count - 1] - Double.Parse(Data_Mult.Rows[i][9].ToString()) - temp > 0)
                            continue;
                        paraTemp.Time.Add(((i - 1) * 0.25));
                        paraTemp.Speed.Add(Double.Parse(Data_Mult.Rows[i][2].ToString()));
                        paraTemp.TargetSpeed.Add(Double.Parse(Data_Mult.Rows[i][3].ToString()));
                        if (Double.Parse(Data_Mult.Rows[i][6].ToString()) - paraTemp.TargetAcc[paraTemp.TrainAcc.Count - 1] > 10 || Double.Parse(Data_Mult.Rows[i][6].ToString()) - paraTemp.TrainAcc[paraTemp.TrainAcc.Count - 1] < -10)
                            paraTemp.TrainAcc.Add(paraTemp.TrainAcc[paraTemp.TrainAcc.Count - 1]);
                        else
                            paraTemp.TrainAcc.Add(Double.Parse(Data_Mult.Rows[i][6].ToString()));
                        if (Double.Parse(Data_Mult.Rows[i][5].ToString()) - paraTemp.TargetAcc[paraTemp.TargetAcc.Count - 1] > 10 || Double.Parse(Data_Mult.Rows[i][5].ToString()) - paraTemp.TargetAcc[paraTemp.TargetAcc.Count - 1] < -10)
                            paraTemp.TargetAcc.Add(paraTemp.TargetAcc[paraTemp.TargetAcc.Count - 1]);
                        else
                            paraTemp.TargetAcc.Add(Double.Parse(Data_Mult.Rows[i][5].ToString()));
                        if (Double.Parse(Data_Mult.Rows[i][7].ToString()) > 1000 || Double.Parse(Data_Mult.Rows[i][7].ToString()) < -1000)
                            paraTemp.DeltaAcc.Add(1.0);
                        else
                            if (Double.Parse(Data_Mult.Rows[i][7].ToString()) - paraTemp.DeltaAcc[paraTemp.DeltaAcc.Count - 1] > 10 || Double.Parse(Data_Mult.Rows[i][7].ToString()) - paraTemp.DeltaAcc[paraTemp.DeltaAcc.Count - 1] < -10)
                            paraTemp.DeltaAcc.Add(paraTemp.DeltaAcc[paraTemp.DeltaAcc.Count - 1]);
                        else
                            paraTemp.DeltaAcc.Add(Double.Parse(Data_Mult.Rows[i][7].ToString()));
                        if (paraTemp.TrainPosition.Count == 0)
                            paraTemp.TrainPosition.Add(Double.Parse(Data_Mult.Rows[i][9].ToString()) + temp);
                        else
                        {
                            if (Double.Parse(Data_Mult.Rows[i][9].ToString()) + temp > paraTemp.TrainPosition[paraTemp.TrainPosition.Count - 1])
                                paraTemp.TrainPosition.Add(Double.Parse(Data_Mult.Rows[i][9].ToString()) + temp);
                            else
                            {
                                temp = paraTemp.TrainPosition[paraTemp.TrainPosition.Count - 1];
                                paraTemp.TrainPosition.Add(Double.Parse(Data_Mult.Rows[i][9].ToString()) + temp);
                            }
                        }
                        if (Data_Mult.Rows[i][15].ToString() == "Traction")
                            paraTemp.Status.Add(2.0);
                        else if (Data_Mult.Rows[i][15].ToString() == "Coast")
                            paraTemp.Status.Add(1.0);
                        else if (Data_Mult.Rows[i][15].ToString() == "Brake")
                            paraTemp.Status.Add(0.0);
                        else
                            paraTemp.Status.Add(-1.0);
                    }
                }
                MyMultyChartDatass.Add(paraTemp);
            }
        }

        private BindingList<string> myMultychartTypes;
        public BindingList<string> MyMultyChartTypes
        {
            get { return myMultychartTypes; }
            set
            {
                myMultychartTypes = value;
                RaisePropertyChanged();
            }
        }
        private int myMultychartTypesIdx;
        public int MyMultychartTypesIdx
        {
            get { return myMultychartTypesIdx; }
            set
            {
                myMultychartTypesIdx = value;
                RaisePropertyChanged();
            }
        }
        private BindingList<ChartDatas> myMultyChartDatass;
        public BindingList<ChartDatas> MyMultyChartDatass
        {
            get { return myMultyChartDatass; }
            set
            {
                myMultyChartDatass = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region [评价指标属性]
        private int curr_file_idx;
        public int Curr_file_idx
        {
            get { return curr_file_idx; }
            set
            {
                curr_file_idx = value;
                RaisePropertyChanged();
                if (Curr_file_idx >= 0)
                    Filename = FileNames[curr_file_idx];
            }
        }

        private int curr_train_idx;
        public int Curr_train_idx
        {
            get { return curr_train_idx; }
            set
            {
                curr_train_idx = value;
                RaisePropertyChanged();
            }
        }

        private int curr_MultProp_idx;
        public int Curr_MultProp_idx
        {
            get { return curr_MultProp_idx; }
            set
            {
                curr_MultProp_idx = value;
                RaisePropertyChanged();
            }
        }

        private BindingList<ComboBoxItem> trainNameList;
        public BindingList<ComboBoxItem> TrainNameList
        {
            get { return trainNameList; }
            set
            {
                trainNameList = value;
                RaisePropertyChanged();
            }
        }

        private string filename;
        public string Filename
        {
            get { return filename; }
            set
            {
                filename = value;
                RaisePropertyChanged();
                ExcelHelper excelhelper = new ExcelHelper(Filename);
                Data = excelhelper.ExcelToDataTable("sheet1", true);
                TrainNameList = new BindingList<ComboBoxItem>();
                for (int i = 1; i < Data.Rows.Count - 3000; i++)
                {
                    var item = new ComboBoxItem()
                    {
                        Content = Data.Rows[i][0]
                    };
                    TrainNameList.Add(item);
                }
            }
        }
        #region [指标属性]

        private string description; //指标1：描述
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                RaisePropertyChanged();
            }

        }

        private string eosa_time; //指标2：EOAS时间
        public string EOSA_Time
        {
            get { return eosa_time; }
            set
            {
                eosa_time = value; RaisePropertyChanged();
            }
        }

        private string dist;  //指标3：走行距离
        public string Dist
        {
            get { return dist; }
            set { dist = value; RaisePropertyChanged(); }
        }

        private string total_mileage; // 指标4：总里程
        public string Total_Mileage
        {
            get { return total_mileage; }
            set { total_mileage = value; RaisePropertyChanged(); }
        }

        private string train_speed;  //指标5： 列控速
        public string Train_Speed
        {
            get { return train_speed; }
            set { train_speed = value; RaisePropertyChanged(); }
        }

        private string braking_level;  //指标6：制动级位
        public string Braking_Level
        {
            get { return braking_level; }
            set { braking_level = value; RaisePropertyChanged(); }
        }

        #endregion


        #endregion

        #region [文件操作属性]
        private ObservableCollection<string> filenames; //日志里读取的文件名
        public ObservableCollection<string> FileNames
        {
            get { return filenames; }
            set
            {
                filenames = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<string> filenames_Sim; //日志里读取的文件名
        public ObservableCollection<string> FileNames_Sim
        {
            get { return filenames_Sim; }
            set
            {
                filenames_Sim = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region [Live chart]
        private BindingList<LiveChartParemeters> myCharts;
        public BindingList<LiveChartParemeters> MyCharts
        {
            get { return myCharts; }
            set
            {
                myCharts = value;
                RaisePropertyChanged();
            }
        }
        

        #endregion

        #region [ASC]
        private ObservableCollection<string> ascFileNames;
        public ObservableCollection<string> AscFileNames
        {
            get { return ascFileNames; }
            set
            {
                ascFileNames = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<string> ascSimFileNames;
        public ObservableCollection<string> AscSimFileNames
        {
            get { return ascSimFileNames; }
            set
            {
                ascSimFileNames = value;
                RaisePropertyChanged();
            }
        }

        private BindingList<string> data_Asc;
        public BindingList<string> Datas_Asc
        {
            get { return data_Asc; }
            set
            {
                data_Asc = value;
                RaisePropertyChanged();
            }
        }

        private BindingList<AscDatas> parames_Asc;
        public BindingList<AscDatas> Parames_Asc
        {
            get { return parames_Asc; }
            set
            {
                parames_Asc = value;
                RaisePropertyChanged();

            }
        }

        private string ascData;
        public string AscData
        {
            get { return ascData; }
            set
            {
                ascData = value;
                if (Last_Ascidx != -1)
                {
                    if (Parames_Asc[Last_Ascidx].Data != Last_AscData)
                    {
                        Vis_Change = Visibility.Visible;
                        parames_Asc[Last_Ascidx].Asc_Background = new SolidColorBrush(Colors.Green);
                    }
                }
                Last_Ascidx = Curr_Ascpara_idx;
                Last_AscData = AscData;
                RaisePropertyChanged();
            }
        }

        private int last_Ascidx;
        public int Last_Ascidx
        {
            get { return last_Ascidx; }
            set
            {
                last_Ascidx = value;
            }
        }

        private int curr_Ascfile_idx;
        public int Curr_Ascfile_idx
        {
            get { return curr_Ascfile_idx; }
            set
            {
                curr_Ascfile_idx = value;
                RaisePropertyChanged();

            }
        }

        private int curr_Ascpara_idx;
        public int Curr_Ascpara_idx
        {
            get { return curr_Ascpara_idx; }
            set
            {
                curr_Ascpara_idx = value;
                RaisePropertyChanged();
            }
        }

        private string last_AscData;
        public string Last_AscData
        {
            get { return last_AscData; }
            set { last_AscData = value; }
        }

        private List<string> save_NewDatas;
        public List<string> Save_NewDatas
        {
            get { return save_NewDatas; }
            set { save_NewDatas = value; }
        }


        #endregion

        #region [杂项]
        private string nowTime;
        public string NowTime
        {
            get { return nowTime; }
            set
            {
                nowTime = value;
                RaisePropertyChanged();
            }
        }

        private Visibility vis_Change;
        public Visibility Vis_Change
        {
            get { return vis_Change; }
            set
            {
                vis_Change = value;
                RaisePropertyChanged();
            }
        }

        private Visibility vis_Save;
        public Visibility Vis_Save
        {
            get { return vis_Save; }
            set
            {
                vis_Save = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #endregion

        #region [private properties]
        private DataTable Data
        {
            get; set;
        }
        private DataTable Data_Mult
        {
            get; set;
        }
        private DataTable Data_Single
        {
            get;
            set;
        }
        #endregion

        #region [commands]

        #region [评价指标命令]

        public RelayCommand UpdataIndicator { get; set; } //更新评价指标

        #endregion

        #region [文件操作命令]
        public RelayCommand SaveLogFileToExcelCommand { get; set; }  //另存为excel
        public RelayCommand ReadLogFilesCommand { get; set; } //日志读取文件

        public RelayCommand Read_AscFiles { get; set; }
        #endregion

        #region [单列车命令]
        public RelayCommand ShowSingleTrain { get; set; }

        #endregion

        #region [多列车命令]
        public RelayCommand AddLogFile { get; set; }
        public RelayCommand ShowMultTrain { get; set; }
        public RelayCommand DeleteMultTrain { get; set; }
        public RelayCommand MoveUp { get; set; }
        public RelayCommand MoveDown { get; set; }
        public RelayCommand UpdataMultTrain { get; set; }
        //public RelayCommand ChangeStatus { get; set; }
        //public RelayCommand ChangeAcc { get; set; }

        #endregion

        #region [ASC]
        public RelayCommand Show_AscDatas { get; set; }

        public RelayCommand Save_AscNewFiles { get; set; }
        #endregion

        #endregion

        #region [funcs]
        private void InitProperties()
        {
            AscSimFileNames = new ObservableCollection<string>();
            AscFileNames = new ObservableCollection<string>();
            FileNames = new ObservableCollection<string>();
            FileNames_Sim = new ObservableCollection<string>();
            LogFileProp_Mult = new BindingList<MultyTrainDataGrid>();
            Parames_Asc = new BindingList<AscDatas>();
            MyCharts = new BindingList<LiveChartParemeters>();
            MyMultyChartDatass = new BindingList<ChartDatas>();
            MyMultyChartTypes = new BindingList<string>() { "S-V图", "S-T图", "V-T图", "Status图", "ACC图", "不显示" };
            MySinChartTypes = new BindingList<string>() { "S-V图", "S-T图", "V-T图", "Status图", "ACC图", "不显示" };
            //MyChartTypes.IsStatusChanged = false; MyChartTypes.LastStatus = false;
            //MyChartTypes.IsAccChanged = false; MyChartTypes.LastAcc = false;
            //MyChartTypes.SVT = new BindingList<string>() { "S-V图", "S-T图", "V-T图", "不显示" };
            //thisTemp = -1;

            DateTime dt = DateTime.Now;
            NowTime = dt.ToLongDateString().ToString();

            Vis_Change = Visibility.Hidden;
            vis_Save = Visibility.Hidden;

            Curr_file_idx = -1;
            Curr_train_idx = -1;

            Curr_fileMult_idx = -1;
            Curr_fileSingle_idx = -1;
            Curr_trainSingle_idx = -1;
            Curr_Ascfile_idx = -1;
            MyMultychartTypesIdx = -1;
            MySinChartTypesIdx = -1;
        }   //初始化属性

        private void InitCommands()
        {
            ReadLogFilesCommand = new RelayCommand(readLogFiles);
            UpdataIndicator = new RelayCommand(updataIndicator);
            SaveLogFileToExcelCommand = new RelayCommand(saveLogFileToExcelCommand);
            AddLogFile = new RelayCommand(addLogFile);
            ShowSingleTrain = new RelayCommand(showSingleTrain);
            ShowMultTrain = new RelayCommand(showMultTrain);
            DeleteMultTrain = new RelayCommand(deleteMultTrain);
            MoveUp = new RelayCommand(moveUp);
            MoveDown = new RelayCommand(moveDown);
            Read_AscFiles = new RelayCommand(read_AscFiles);
            Show_AscDatas = new RelayCommand(show_AscDatas);
            Save_AscNewFiles = new RelayCommand(save_AscNewFiles);
            //ChangeStatus = new RelayCommand(changeStatus);
            //ChangeAcc = new RelayCommand(changeAcc);
        }  //初始化命令

        #region [文件操作]
        private void readLogFiles()
        {
            System.Windows.Forms.OpenFileDialog openfiledialog = new System.Windows.Forms.OpenFileDialog();
            if (openfiledialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileNames.Add(openfiledialog.FileName);
                var start_idx = openfiledialog.FileName.LastIndexOf("\\") + 1;
                FileNames_Sim.Add(openfiledialog.FileName.Substring(start_idx, openfiledialog.FileName.LastIndexOf(".") - start_idx));
            }
        }  //日志中读取文件

        private void saveLogFileToExcelCommand()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog = new OpenFileDialog();
            string temp_fileNames = "";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                openFileDialog.Filter = "(*.xlms)|*.xlms";
                temp_fileNames = openFileDialog.FileName;
            }
            ExcelHelper excel_create = new ExcelHelper(temp_fileNames);
            DataTable data_New = new DataTable();
            for (int i = 0; i <= 7; i++)
            {
                data_New.Columns.Add(new DataColumn(Data.Rows[0][i].ToString(), typeof(string)));
            }
            DataRow datarow_tmp;
            for (int i = 1; i < Data.Rows.Count; i++)
            {
                datarow_tmp = data_New.NewRow();
                for (int j = 0; j <= 7; j++)
                {
                    datarow_tmp[Data.Rows[0][j].ToString()] = Data.Rows[i][j].ToString();
                }
                data_New.Rows.Add(datarow_tmp);
            }
            excel_create.DataTableToExcel(data_New, "sheet1", true);

        } // 另存为excel文件

        #endregion

        #region [评价指标]
        private void updataIndicator()
        {
            if (Curr_train_idx >= 0)
            {

                Description = Data.Rows[Curr_train_idx][1].ToString();
                EOSA_Time = Data.Rows[Curr_train_idx][3].ToString();
                Total_Mileage = Data.Rows[Curr_train_idx][5].ToString();
                Dist = Data.Rows[Curr_train_idx][4].ToString();
                Train_Speed = Data.Rows[Curr_train_idx][6].ToString();
                Braking_Level = Data.Rows[Curr_train_idx][10].ToString();

            }
        }    //更新评价指标
        #endregion

        #region [多列车函数]
        private void addLogFile()
        {
            MultyTrainDataGrid propTmp = new MultyTrainDataGrid();
            propTmp.Gra_idx = -1;
            propTmp.FileName = FileNames_Sim[Curr_fileMult_idx];
            propTmp.ChartType = MyMultyChartTypes[MyMultychartTypesIdx];
            propTmp.Granularity = new ObservableCollection<string>() { "0.5倍", "1.5倍" };
            LogFileProp_Mult.Add(propTmp);

        } //多列车
        private void showMultTrain()
        {
            MyCharts.Clear();
            for (int j = 0; j < LogFileProp_Mult.Count; j++)
            {
                string thisTemp = " ";
                try {  thisTemp = LogFileProp_Mult[j].ChartType; }
                catch
                {
                    MessageBox.Show("您尚未选择图像类型！", "通知", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                
                if (thisTemp == "不显示")
                    continue;
                if (thisTemp == "V-T图"|| thisTemp == "S-T图"|| thisTemp == "S-V图")
                {
                    LiveChartParemeters temp_Chart = new LiveChartParemeters();
                    var mapper = Mappers.Xy<MeasureModel>()
                        .X(model => model.X)
                        .Y(model => model.Y);
                    Charting.For<MeasureModel>(mapper);
                    ChartValues<MeasureModel> valuesTemp = new ChartValues<MeasureModel>();
                    string titleTemp = "";
                    double max = 0;
                    if (thisTemp == "V-T图")
                    {
                        double max_time = 0;
                        for (int i = 0; i < MyMultyChartDatass[Curr_fileMult_idx].Speed.Count; i++)
                        {

                            valuesTemp.Add(new MeasureModel
                            {
                                X = MyMultyChartDatass[Curr_fileMult_idx].Time[i],
                                Y = MyMultyChartDatass[Curr_fileMult_idx].Speed[i]
                            });
                            if (MyMultyChartDatass[Curr_fileMult_idx].Speed[i] > max)
                            {
                                max = MyMultyChartDatass[Curr_fileMult_idx].Speed[i];
                            }
                            if (MyMultyChartDatass[Curr_fileMult_idx].Time[i] > max_time)
                            {
                                max_time = MyMultyChartDatass[Curr_fileMult_idx].Time[i];
                            }
                        }
                        temp_Chart.Title_X = "Time"; temp_Chart.Title_Y = "TrainSpeed";
                        temp_Chart.Width_MyChart = max_time * 10;
                        titleTemp = "V-T图";
                        temp_Chart.MaxValue_MyChart = max + 20;
                        temp_Chart.Step_X = 5;
                        temp_Chart.Step_Y = max / 2;
                    }
                    if (thisTemp == "S-T图")
                    {
                        double max_time = 0;
                        for (int i = 0; i < MyMultyChartDatass[Curr_fileMult_idx].TrainPosition.Count; i++)
                        {
                            valuesTemp.Add(new MeasureModel
                            {
                                X = MyMultyChartDatass[Curr_fileMult_idx].Time[i],
                                Y = MyMultyChartDatass[Curr_fileMult_idx].TrainPosition[i]
                            });
                            if (MyMultyChartDatass[Curr_fileMult_idx].TrainPosition[i] > max)
                            {
                                max = MyMultyChartDatass[Curr_fileMult_idx].TrainPosition[i];
                            }
                            if (MyMultyChartDatass[Curr_fileMult_idx].Time[i] > max_time)
                            {
                                max_time = MyMultyChartDatass[Curr_fileMult_idx].Time[i];
                            }
                        }
                        temp_Chart.Title_X = "Time"; temp_Chart.Title_Y = "TrainPosition";
                        temp_Chart.Width_MyChart = max_time * 10;
                        titleTemp = "S-T图";
                        temp_Chart.MaxValue_MyChart = max + 200;
                        temp_Chart.Step_X = 5;
                        temp_Chart.Step_Y = max / 2;
                    }
                    if (thisTemp == "S-V图")
                    {
                        double max_position = 0;
                        for (int i = 0; i < MyMultyChartDatass[Curr_fileMult_idx].TrainPosition.Count; i++)
                        {
                            valuesTemp.Add(new MeasureModel
                            {
                                Y = MyMultyChartDatass[Curr_fileMult_idx].Speed[i],
                                X = MyMultyChartDatass[Curr_fileMult_idx].TrainPosition[i]
                            });
                            if (MyMultyChartDatass[Curr_fileMult_idx].Speed[i] > max)
                            {
                                max = MyMultyChartDatass[Curr_fileMult_idx].Speed[i];
                            }
                            if (MyMultyChartDatass[Curr_fileMult_idx].TrainPosition[i] > max_position)
                            {
                                max_position = MyMultyChartDatass[Curr_fileMult_idx].TrainPosition[i];
                            }
                        }
                        temp_Chart.Title_Y = "TrainSpeed";
                        temp_Chart.Title_X = "TrainPosition";
                        temp_Chart.Width_MyChart = max_position * 10;
                        titleTemp = "S-V图";
                        temp_Chart.MaxValue_MyChart = max + 20;
                        temp_Chart.Step_X = 5;
                        temp_Chart.Step_Y = max / 2;
                    }
                    temp_Chart.MinValue_MyChart = 0;
                    temp_Chart.Height_MyChart = 150;
                    temp_Chart.SeriesCollection = new SeriesCollection{
                    new LineSeries
                        {
                    Values = valuesTemp,
                    LineSmoothness = 0,
                    PointGeometry = null,
                    Stroke=Brushes.Red,
                    StrokeThickness = 2,
                    Title = titleTemp,
                        } };
                    MyCharts.Add(temp_Chart);
                } 
                else if (thisTemp == "Status图")
                {
                    LiveChartParemeters temp_Chart = new LiveChartParemeters();
                    var mapper = Mappers.Xy<MeasureModel>()
                        .X(model => model.X)
                        .Y(model => model.Y);
                    Charting.For<MeasureModel>(mapper);
                    ChartValues<MeasureModel> valuesTemp = new ChartValues<MeasureModel>();
                    double max = 0;
                    for (int i = 1; i < MyMultyChartDatass[Curr_fileMult_idx].Status.Count; i++)
                    {
                        if ((i >= 1 && MyMultyChartDatass[Curr_fileMult_idx].Status[i - 1] != MyMultyChartDatass[Curr_fileMult_idx].Status[i]) || i == 0)
                            valuesTemp.Add(new MeasureModel
                            {
                                X = MyMultyChartDatass[Curr_fileMult_idx].TrainPosition[i],
                                Y = MyMultyChartDatass[Curr_fileMult_idx].Status[i],
                            });
                        if (MyMultyChartDatass[Curr_fileMult_idx].TrainPosition[i] >= max)
                        {
                            max = MyMultyChartDatass[Curr_fileMult_idx].TrainPosition[i];
                        }
                    }
                    temp_Chart.MaxValue_MyChart = 2;
                    temp_Chart.Height_MyChart = 100;
                    temp_Chart.Width_MyChart = max * 10; ;
                    temp_Chart.MinValue_MyChart = 0;
                    temp_Chart.Step_X = 10;
                    temp_Chart.Step_Y = 1;
                    temp_Chart.Title_X = "TrainPosition";
                    temp_Chart.Title_Y = "Status";
                    temp_Chart.Label_Y = new ObservableCollection<string>() { "Brake", "Coast", "Traction" };
                    temp_Chart.SeriesCollection = new SeriesCollection{
                    new StepLineSeries
                        {
                    Values = valuesTemp,
                    PointGeometry = null,
                    Stroke=Brushes.CadetBlue,
                    StrokeThickness = 2,
                    Fill= Brushes.Green,
                    AlternativeStroke= Brushes.Transparent,
                        },
                };
                    MyCharts.Add(temp_Chart);
                }
                else if (thisTemp == "ACC图")
                {
                    LiveChartParemeters temp_Chart = new LiveChartParemeters();
                    var mapper = Mappers.Xy<MeasureModel>()
                        .X(model => model.X)
                        .Y(model => model.Y);
                    Charting.For<MeasureModel>(mapper);
                    ChartValues<MeasureModel> valuesTemp1 = new ChartValues<MeasureModel>();
                    ChartValues<MeasureModel> valuesTemp2 = new ChartValues<MeasureModel>();
                    ChartValues<MeasureModel> valuesTemp3 = new ChartValues<MeasureModel>();

                    double max = 0;
                    double min = 0;
                    double max_position = 0;
                    for (int i = 0; i < MyMultyChartDatass[Curr_fileMult_idx].TrainAcc.Count; i++)
                    {
                        if (min >= MyMultyChartDatass[Curr_fileMult_idx].TrainAcc[i] && min - MyMultyChartDatass[Curr_fileMult_idx].TargetAcc[i] <= 10)
                            min = MyMultyChartDatass[Curr_fileMult_idx].TrainAcc[i];
                        if (min >= MyMultyChartDatass[Curr_fileMult_idx].TargetAcc[i] && min - MyMultyChartDatass[Curr_fileMult_idx].TargetAcc[i] <= 10)
                            min = MyMultyChartDatass[Curr_fileMult_idx].TargetAcc[i];
                        if (max <= MyMultyChartDatass[Curr_fileMult_idx].TrainAcc[i] && MyMultyChartDatass[Curr_fileMult_idx].TrainAcc[i] - max <= 10)
                            max = MyMultyChartDatass[Curr_fileMult_idx].TrainAcc[i];
                        if (max <= MyMultyChartDatass[Curr_fileMult_idx].TargetAcc[i] && MyMultyChartDatass[Curr_fileMult_idx].TrainAcc[i] - max <= 10)
                            max = MyMultyChartDatass[Curr_fileMult_idx].TargetAcc[i];
                        if (MyMultyChartDatass[Curr_fileMult_idx].TrainPosition[i] > max_position)
                        {
                            max_position = MyMultyChartDatass[Curr_fileMult_idx].TrainPosition[i];
                        }
                        valuesTemp1.Add(new MeasureModel
                        {
                            X = MyMultyChartDatass[Curr_fileMult_idx].TrainPosition[i],
                            Y = MyMultyChartDatass[Curr_fileMult_idx].DeltaAcc[i],
                        });
                        valuesTemp2.Add(new MeasureModel
                        {
                            X = MyMultyChartDatass[Curr_fileMult_idx].TrainPosition[i],
                            Y = MyMultyChartDatass[Curr_fileMult_idx].TrainAcc[i],
                        });
                        valuesTemp3.Add(new MeasureModel
                        {
                            X = MyMultyChartDatass[Curr_fileMult_idx].TrainPosition[i],
                            Y = MyMultyChartDatass[Curr_fileMult_idx].TargetAcc[i],
                        });
                    }
                    temp_Chart.MaxValue_MyChart = max + 0.5;
                    temp_Chart.Height_MyChart = 200;
                    temp_Chart.MinValue_MyChart = min - 0.3;
                    temp_Chart.Width_MyChart = max_position * 20;
                    temp_Chart.Step_X = 20;
                    temp_Chart.Step_Y = max / 2;
                    temp_Chart.Title_X = "TrainPosition";
                    temp_Chart.SeriesCollection = new SeriesCollection{
                    new ColumnSeries
                        {
                    Fill = Brushes.Black,
                    Values = valuesTemp1,
                    PointGeometry = null,
                    Stroke=Brushes.Blue,
                    Foreground = Brushes.Blue,
                    StrokeThickness = 6,
                    Title = "Delta ACC",
                    Visibility = Visibility.Visible,
                    MaxColumnWidth = 10,
                        },
                    new LineSeries
                    {
                    Fill=Brushes.Transparent,
                    Values = valuesTemp2,
                    LineSmoothness = 1,
                    PointGeometry = null,
                    Stroke=Brushes.DarkRed,
                    StrokeThickness = 2,
                    Title = "TrainACC"
                    },
                    new LineSeries{
                    Fill=Brushes.Transparent,
                    Values = valuesTemp3,
                    LineSmoothness = 1,
                    PointGeometry = null,
                    Stroke=Brushes.Coral,
                    StrokeThickness = 2,
                    Title="TargetACC"
                    },
                };
                    MyCharts.Add(temp_Chart);
                }
            }
        }

        private void deleteMultTrain()
        {

            try { LogFileProp_Mult.RemoveAt(Curr_MultProp_idx);
                showMultTrain();
            }
            catch
            {
                ;
            }

        }
        private void moveDown()
        {
            try
            {
                var idx = Curr_MultProp_idx;
                MultyTrainDataGrid tmp = LogFileProp_Mult[idx];
                if (LogFileProp_Mult.Count != idx + 1)
                {
                    LogFileProp_Mult.RemoveAt(idx);
                    LogFileProp_Mult.Insert(idx + 1, tmp);
                }
            }
            catch
            {


            }
        }
        private void moveUp()
        {
            try
            {
                var idx = Curr_MultProp_idx;
                MultyTrainDataGrid tmp = LogFileProp_Mult[idx];
                if (idx != 0)
                {
                    LogFileProp_Mult.RemoveAt(idx);
                    LogFileProp_Mult.Insert(idx - 1, tmp);
                }
            }
            catch
            {


            }
        }
        //private void changeStatus()
        //{
        //    if (MyChartTypes.LastStatus == true)
        //    {
        //        MyChartTypes.IsStatusChanged = false;
        //        MyChartTypes.LastStatus = false;
        //    }
        //    else if (MyChartTypes.LastStatus == false)
        //    {
        //        MyChartTypes.IsStatusChanged = true;
        //        MyChartTypes.LastStatus = true;
        //    }
        //}
        //private void changeAcc()
        //{
        //    if (MyChartTypes.LastAcc == true)
        //    {
        //        MyChartTypes.IsAccChanged = false;
        //        MyChartTypes.LastAcc = false;
        //    }
        //    else if (MyChartTypes.LastAcc == false)
        //    {
        //        MyChartTypes.IsAccChanged = true;
        //        MyChartTypes.LastAcc = true;
        //    }
        //}

        #endregion

        #region [单列车函数]
        private void showSingleTrain()
        {
            string thisTemp = "";
            try {  thisTemp = MySinChartTypes[MySinChartTypesIdx]; }
            catch { MessageBox.Show("您尚未选择图像类型！", "通知", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            MyCharts.Clear();
            if (thisTemp == "V-T图"|| thisTemp == "S-T图" || thisTemp == "S-V图")
            {
                LiveChartParemeters temp_Chart = new LiveChartParemeters();
                var mapper = Mappers.Xy<MeasureModel>()
                    .X(model => model.X)
                    .Y(model => model.Y);
                Charting.For<MeasureModel>(mapper);
                ChartValues<MeasureModel> valuesTemp = new ChartValues<MeasureModel>();
                string titleTemp = "";
                double max = 0;
                if (thisTemp == "V-T图")
                {
                    double max_time = 0;
                    for (int i = 0; i < MySinChartDatass.Speed.Count; i++)
                    {
                        valuesTemp.Add(new MeasureModel
                        {
                            X = MySinChartDatass.Time[i],
                            Y = MySinChartDatass.Speed[i]
                        });
                        if (MySinChartDatass.Speed[i] > max)
                        {
                            max = MySinChartDatass.Speed[i];
                        }
                        if (MySinChartDatass.Time[i] > max_time)
                        {
                            max_time = MySinChartDatass.Time[i];
                        }
                    }
                    temp_Chart.Title_X = "Time";
                    temp_Chart.Title_Y = "TrainSpeed";
                    temp_Chart.Width_MyChart = max_time * 10;
                    titleTemp = "V-T图";
                    temp_Chart.MaxValue_MyChart = max + 5;
                    temp_Chart.Step_X = 5;
                    temp_Chart.Step_Y = max / 10;
                }
                else if (thisTemp == "S-T图")
                {
                    double max_time = 0;
                    for (int i = 0; i < MySinChartDatass.TrainPosition.Count; i++)
                    {
                        valuesTemp.Add(new MeasureModel
                        {
                            X = MySinChartDatass.Time[i],
                            Y = MySinChartDatass.TrainPosition[i]
                        });
                        if (MySinChartDatass.TrainPosition[i] > max)
                        {
                            max = MySinChartDatass.TrainPosition[i];
                        }
                        if (MySinChartDatass.Time[i] > max_time)
                        {
                            max_time = MySinChartDatass.Time[i];
                        }
                    }
                    temp_Chart.Title_X = "Time";
                    temp_Chart.Title_Y = "TrainPosition";
                    temp_Chart.Width_MyChart = max_time * 10;
                    titleTemp = "S-T图";
                    temp_Chart.MaxValue_MyChart = max + 20;
                    temp_Chart.Step_X = 5;
                    temp_Chart.Step_Y = max / 10;
                }
                else if (thisTemp == "S-V图")
                {
                    double max_position = 0;
                    for (int i = 0; i < MySinChartDatass.TrainPosition.Count; i++)
                    {
                        valuesTemp.Add(new MeasureModel
                        {
                            Y = MySinChartDatass.Speed[i],
                            X = MySinChartDatass.TrainPosition[i]
                        });
                        if (MySinChartDatass.Speed[i] > max)
                        {
                            max = MySinChartDatass.Speed[i];
                        }
                        if (MySinChartDatass.TrainPosition[i] > max_position)
                        {
                            max_position = MySinChartDatass.TrainPosition[i];
                        }
                    }
                    temp_Chart.Title_Y = "TrainSpeed";
                    temp_Chart.Title_X = "TrainPosition";
                    temp_Chart.Width_MyChart = max_position * 10;
                    titleTemp = "S-V图";
                    temp_Chart.MaxValue_MyChart = max + 5;
                    temp_Chart.Step_X = 5;
                    temp_Chart.Step_Y = max / 10;
                }
                temp_Chart.MinValue_MyChart = 0;
                temp_Chart.Height_MyChart = 400;
                temp_Chart.SeriesCollection = new SeriesCollection{
                    new LineSeries
                        {
                    Values = valuesTemp,
                    LineSmoothness = 0,
                    PointGeometry = null,
                    Stroke=Brushes.Red,
                    StrokeThickness = 2,
                    Title = titleTemp,
                        },
                };
                MyCharts.Add(temp_Chart);
            }
            else if (thisTemp == "Status图")
            {
                LiveChartParemeters temp_Chart = new LiveChartParemeters();
                var mapper = Mappers.Xy<MeasureModel>()
                    .X(model => model.X)
                    .Y(model => model.Y);
                Charting.For<MeasureModel>(mapper);
                ChartValues<MeasureModel> valuesTemp = new ChartValues<MeasureModel>();
                double max = 0;
                for (int i = 1; i < MySinChartDatass.Status.Count; i++)
                {
                    if ((i >= 1 && MySinChartDatass.Status[i - 1] != MySinChartDatass.Status[i]) || i == 0)
                        valuesTemp.Add(new MeasureModel
                        {
                            X = MySinChartDatass.TrainPosition[i],
                            Y = MySinChartDatass.Status[i],
                        });
                    if (MySinChartDatass.TrainPosition[i] >= max)
                    {
                        max = MySinChartDatass.TrainPosition[i];
                    }
                }
                temp_Chart.MaxValue_MyChart = 2;
                temp_Chart.Height_MyChart = 400;
                temp_Chart.Width_MyChart = max * 10; ;
                temp_Chart.MinValue_MyChart = 0;
                temp_Chart.Step_X = 10;
                temp_Chart.Step_Y = 1;
                temp_Chart.Label_Y = new ObservableCollection<string>() { "Brake", "Coast", "Traction" };
                temp_Chart.Title_X = "TrainPosition";
                temp_Chart.Title_Y = "Status";
                temp_Chart.SeriesCollection = new SeriesCollection{
                    new StepLineSeries
                        {
                    Values = valuesTemp,
                    PointGeometry = null,
                    Stroke=Brushes.CadetBlue,
                    StrokeThickness = 2,
                    Fill= Brushes.Green,
                    AlternativeStroke= Brushes.Transparent,

                        },
                };
                MyCharts.Add(temp_Chart);
            }
            else if (thisTemp == "ACC图")
            {
                LiveChartParemeters temp_Chart = new LiveChartParemeters();
                var mapper = Mappers.Xy<MeasureModel>()
                    .X(model => model.X)
                    .Y(model => model.Y);
                Charting.For<MeasureModel>(mapper);
                ChartValues<MeasureModel> valuesTemp1 = new ChartValues<MeasureModel>();
                ChartValues<MeasureModel> valuesTemp2 = new ChartValues<MeasureModel>();
                ChartValues<MeasureModel> valuesTemp3 = new ChartValues<MeasureModel>();

                double max = 0;
                double min = 0;
                double max_position = 0;
                for (int i = 0; i < MySinChartDatass.TrainAcc.Count; i++)
                {
                    if (min >= MySinChartDatass.TrainAcc[i] && min - MySinChartDatass.TargetAcc[i] <= 10)
                        min = MySinChartDatass.TrainAcc[i];
                    if (min >= MySinChartDatass.TargetAcc[i] && min - MySinChartDatass.TargetAcc[i] <= 10)
                        min = MySinChartDatass.TargetAcc[i];
                    if (max <= MySinChartDatass.TrainAcc[i] && MySinChartDatass.TrainAcc[i] - max <= 10)
                        max = MySinChartDatass.TrainAcc[i];
                    if (max <= MySinChartDatass.TargetAcc[i] && MySinChartDatass.TrainAcc[i] - max <= 10)
                        max = MySinChartDatass.TargetAcc[i];
                    if (MySinChartDatass.TrainPosition[i] > max_position)
                    {
                        max_position = MySinChartDatass.TrainPosition[i];
                    }
                    valuesTemp1.Add(new MeasureModel
                    {
                        X = MySinChartDatass.TrainPosition[i],
                        Y = MySinChartDatass.DeltaAcc[i],
                    });
                    valuesTemp2.Add(new MeasureModel
                    {
                        X = MySinChartDatass.TrainPosition[i],
                        Y = MySinChartDatass.TrainAcc[i],
                    });
                    valuesTemp3.Add(new MeasureModel
                    {
                        X = MySinChartDatass.TrainPosition[i],
                        Y = MySinChartDatass.TargetAcc[i],
                    });
                }
                temp_Chart.MaxValue_MyChart = max + 0.3;
                temp_Chart.Height_MyChart = 400;
                temp_Chart.MinValue_MyChart = min - 0.3;
                temp_Chart.Width_MyChart = max_position * 20;
                temp_Chart.Step_X = 50;
                temp_Chart.Step_Y = 1;
                temp_Chart.Title_X = "TrainPosition";
                temp_Chart.SeriesCollection = new SeriesCollection{
                    new ColumnSeries
                        {
                    Fill = Brushes.Black,
                    Values = valuesTemp1,
                    PointGeometry = null,
                    Stroke=Brushes.Blue,
                    Foreground = Brushes.Blue,
                    StrokeThickness = 6,
                    Title = "Delta ACC",
                    Visibility = Visibility.Visible,
                    MaxColumnWidth = 10,
                        },
                    new LineSeries
                    {
                    Fill=Brushes.Transparent,
                    Values = valuesTemp2,
                    LineSmoothness = 1,
                    PointGeometry = null,
                    Stroke=Brushes.DarkRed,
                    StrokeThickness = 2,
                    Title = "TrainACC"
                    },
                    new LineSeries{
                    Fill=Brushes.Transparent,
                    Values = valuesTemp3,
                    LineSmoothness = 1,
                    PointGeometry = null,
                    Stroke=Brushes.Coral,
                    StrokeThickness = 2,
                    Title="TargetACC"
                    },
                };
                MyCharts.Add(temp_Chart);
            }
        }
        #endregion

        #region[ASC]
        private void read_AscFiles()
        {
            System.Windows.Forms.OpenFileDialog openfiledialog = new System.Windows.Forms.OpenFileDialog();
            if (openfiledialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                AscFileNames.Add(openfiledialog.FileName);
                var start_idx = openfiledialog.FileName.LastIndexOf("\\") + 1;
                AscSimFileNames.Add(openfiledialog.FileName.Substring(start_idx, openfiledialog.FileName.LastIndexOf(".") - start_idx));
                Curr_Ascpara_idx = -1;
            }
            MessageBox.Show("打开成功！", "通知", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void show_AscDatas()
        {
            try
            {
                Datas_Asc = new BindingList<string>();
                using (StreamReader read_Asc = new StreamReader(AscFileNames[Curr_Ascfile_idx], Encoding.Default))
                {
                    int lineCount = 0;
                    while (read_Asc.Peek() > 0)
                    {
                        lineCount++;
                        string temp = read_Asc.ReadLine();
                        if (!temp.Contains("#"))
                            Datas_Asc.Add(temp);
                    }
                }
                for (int i = 0; i < Datas_Asc.Count; i++)
                {
                    int idx_begin;
                    int idx_end;
                    int length;
                    AscDatas tempPara = new AscDatas();
                    string temp = Datas_Asc[i];

                    idx_end = temp.IndexOf("=");
                    tempPara.Name = temp.Substring(0, idx_end).TrimEnd();

                    idx_begin = temp.IndexOf("<") + 1;
                    idx_end = temp.IndexOf(">") - 1;
                    length = idx_end - idx_begin + 1;
                    try { tempPara.Range = temp.Substring(idx_begin, length); }
                    catch { tempPara.Range = ""; }

                    idx_begin = temp.IndexOf(">") + 1;
                    idx_end = temp.IndexOf("@") - 1;
                    length = idx_end - idx_begin + 1;
                    try { tempPara.Data = temp.Substring(idx_begin, length).Trim(); }
                    catch { tempPara.Data = temp.Substring(idx_begin).Trim(); }

                    idx_begin = temp.IndexOf("@") + 1;
                    tempPara.Tips = temp.Substring(idx_begin);

                    idx_begin = temp.IndexOf("(");
                    idx_end = temp.LastIndexOf(")");
                    length = idx_end - idx_begin;
                    try { tempPara.Data_Property = temp.Substring(idx_begin, length); }
                    catch { }

                    tempPara.Asc_Background = default;
                    Parames_Asc.Add(tempPara);
                }
                Vis_Save = Visibility.Visible;
                Last_Ascidx = -1;
            }
            catch { }
        }
        private void save_AscNewFiles()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Save_NewDatas = new List<string>();
                save_AscNewDatas();
                string fileNames = openFileDialog.FileName;
                StreamWriter ascWrite = new StreamWriter(fileNames);
                foreach (string item in Save_NewDatas)
                {
                    ascWrite.WriteLine(item);
                }
                ascWrite.Close();
            }
        }
        private void save_AscNewDatas()
        {
            for (int i = 0; i < Datas_Asc.Count - 2; i++)
            {
                string temp = Parames_Asc[i].Name + " + " + Parames_Asc[i].Data_Property + '<' + Parames_Asc[i].Range + '>'
                    + Parames_Asc[i].Data + " @" + Parames_Asc[i].Tips;
                Save_NewDatas.Add(temp);
            }
            Save_NewDatas.Add("#ATOEND");
            Save_NewDatas.Insert(0, "#ATO");
        }
        #endregion

        #endregion

        public MainViewModel()  //ViewModel构造函数
        {

            InitCommands();
            InitProperties();
        }
    }

}
