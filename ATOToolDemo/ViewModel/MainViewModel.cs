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

namespace ATOToolDemo.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region [properties]

        #region [日志文件单列车属性]
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
            }
        }

        #endregion

        #region [日志文件多列车属性]
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
        private BindingList<LogFile_Mult> logFileProp_Mult;
        public BindingList<LogFile_Mult> LogFileProp_Mult
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
                if (curr_fileMult_idx >= 0)
                    Filename_logMult = FileNames[curr_fileMult_idx];
            }
        }

        private int curr_trainMult_idx;
        public int Curr_trainMult_idx
        {
            get { return curr_trainMult_idx; }
            set
            {
                curr_trainMult_idx = value;
                RaisePropertyChanged();

            }
        }

        private BindingList<ComboBoxItem> trainNameList_LogMult;
        public BindingList<ComboBoxItem> TrainNameList_LogMult
        {
            get { return trainNameList_LogMult; }
            set
            {
                trainNameList_LogMult = value;
                RaisePropertyChanged();
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
                TrainNameList_LogMult = new BindingList<ComboBoxItem>();
                for (int i = 1; i < Data_Mult.Rows.Count - 3000; i++)
                {
                    var item = new ComboBoxItem()
                    {
                        Content = Data_Mult.Rows[i][0]
                    };
                    TrainNameList_LogMult.Add(item);
                }
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
        private ObservableCollection<string> labels_Y;
        public ObservableCollection<string> Labels_Y
        {
            get { return labels_Y; }
            set
            {
                labels_Y = value;
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

        private BindingList<AscData> parames_Asc;
        public BindingList<AscData> Parames_Asc
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
                RaisePropertyChanged();
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
            LogFileProp_Mult = new BindingList<LogFile_Mult>();
            Parames_Asc = new BindingList<AscData>();


            DateTime dt = DateTime.Now;
            NowTime = dt.ToLongDateString().ToString();

            Vis_Change = Visibility.Hidden;
            vis_Save = Visibility.Hidden;

            Curr_file_idx = -1;
            Curr_train_idx = -1;
            Curr_trainMult_idx = -1;
            Curr_fileMult_idx = -1;
            Curr_fileSingle_idx = -1;
            Curr_trainSingle_idx = -1;
            Curr_Ascfile_idx = -1;

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
            UpdataMultTrain = new RelayCommand(updataMultTrain);
            Read_AscFiles = new RelayCommand(read_AscFiles);
            Show_AscDatas = new RelayCommand(show_AscDatas);
            Save_AscNewFiles = new RelayCommand(save_AscNewFiles);
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
            ExcelHelper excel_create = new ExcelHelper("D:\\桌面\\工作簿2.xlsx");
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
            LogFile_Mult propTmp = new LogFile_Mult();
            propTmp.FileName_logMult = Data_Mult.Rows[Curr_trainMult_idx][1].ToString();
            propTmp.Train_logMult = Data_Mult.Rows[Curr_trainMult_idx][2].ToString();
            propTmp.Interval = new BindingList<ComboBoxItem>();
            propTmp.Granularity = new ObservableCollection<ComboBoxItem>();
            for (int i = 3; i < 8; i++)
            {

                var item = new ComboBoxItem()
                {
                    Content = Data_Mult.Rows[Curr_trainMult_idx][i]
                };
                propTmp.Interval.Add(item);
            }
            for (int i = 8; i < 16; i++)
            {
                var item = new ComboBoxItem()
                {
                    Content = Data_Mult.Rows[Curr_trainMult_idx][i]
                };
                propTmp.Granularity.Add(item);
            }
            LogFileProp_Mult.Add(propTmp);

        } //多列车
        private void showMultTrain()
        {
            ;
        }
        private void deleteMultTrain()
        {

            try { LogFileProp_Mult.RemoveAt(Curr_MultProp_idx); }
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
                LogFile_Mult tmp = LogFileProp_Mult[idx];
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
                LogFile_Mult tmp = LogFileProp_Mult[idx];
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
        private void updataMultTrain()
        {
            ;
        }
        #endregion

        #region [单列车函数]
        private void showSingleTrain()
        {
            Labels_X = new ObservableCollection<string>
            {

            };
            for (int i = 0; i < 50; i = i + 2)
            {
                labels_X.Add(i.ToString());
            }
            //Labels_Y = new ObservableCollection<string>
            //{
            //    "200","400","200","400"
            //};
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double> { 300, 500, 700, 400,200,300,500,900,700,500,300, 500, 700, 400,200,300,500,900,700,500 },
                    LineSmoothness = 0,
                    PointGeometry = null,
                    Stroke=System.Windows.Media.Brushes.Red,
                },
            };
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
                    AscData tempPara = new AscData();
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

                    tempPara.Asc_Background = new SolidColorBrush(Colors.White);
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
