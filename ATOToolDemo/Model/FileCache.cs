using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATOToolDemo.Model
{
    public class FileCache : ViewModelBase
    {
        private string fileName; //文件名
        public string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                RaisePropertyChanged();
            }
        }
        private string fileType; //
        public string FileType
        {
            get { return fileType; }
            set
            {
                fileType = value;
                RaisePropertyChanged();
            }
        }
    }
}
