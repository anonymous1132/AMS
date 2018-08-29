using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Presentation;
using System.Collections.ObjectModel;
using System.Data;
using MCADataTranslator.Helper;


namespace MCADataTranslator.Bll
{
   public class ImportDGViewType2:NotifyPropertyChanged
    {
 
        public ImportDGViewType2(string fp)
        {
            this.FilePath = fp;
            try
            {
                if (GetList())
                {
                    this.Comment = "待导入";
                }
                else 
                {
                    this.Comment = "文件校验失败";
                }
            }
            catch (Exception e)
            {
                this.Comment = e.Message;
            }
        }
        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; csvFile = new CsvFileObject(value); OnPropertyChanged("FilePath"); }
        }
        private CsvFileObject csvFile;

        public string FileName
        {
            get { return csvFile.FileName; }
        }
        private string _operationResult;
        public string OperationResult
        {
            get { return _operationResult; }
            set { _operationResult = value; OnPropertyChanged("OperationResult"); }

        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set { _comment = value; OnPropertyChanged("Comment"); }
        }



        public List<Type2ViewModel> list_type2;

        private ExcelOper excel;
        private bool GetList()
        {
            string filename = csvFile.FileName;
            if (!System.IO.File.Exists(_filePath)) return false;
            excel = new ExcelOper(_filePath);
            DataTable dt = excel.GetContentFromExcel();
            excel.Quit();
            int colSampleName = ColNo("Sample Name",dt,1);
            if (colSampleName == -1) return false;
            string colname = dt.Columns[colSampleName].ColumnName;
            int colAcqDateTime = ColNo("Acq", dt, 1);
            int colNa = ColNo("Na", dt, 0);
            int colAl = ColNo("Al", dt, 0);
            int colCa = ColNo("Ca", dt, 0);
            int colCr = ColNo("Cr", dt, 0);
            int colFe = ColNo("Fe", dt, 0);
            int colNi = ColNo("Ni", dt, 0);
            int colCu = ColNo("Cu", dt, 0);
            int colZn = ColNo("Zn", dt, 0);
            DataView dv = dt.DefaultView;
            dv.RowFilter = colname+" like '(%'";
            if (dv.Count == 0) return false;
            list_type2 = new List<Type2ViewModel>();
          
            foreach (DataRowView drv in dv)
            {
                Type2ViewModel type2 = new Type2ViewModel(filename);
                type2.strNa =colNa==-1?"": drv[colNa].ToString();
                type2.strAl =colAl==-1?"": drv[colAl].ToString();
                type2.strCa =colCa==-1?"": drv[colCa].ToString();
                type2.strCr =colCr==-1?"": drv[colCr].ToString();
                type2.strFe =colFe==-1?"": drv[colCr].ToString();
                type2.strNi =colNi==-1?"": drv[colCr].ToString();
                type2.strCu =colCu==-1?"": drv[colCu].ToString();
                type2.strZn =colZn==-1?"": drv[colZn].ToString();
                type2.SampleName = drv[colSampleName].ToString();
                type2.AcqDateTime =colAcqDateTime==-1?"": drv[colAcqDateTime].ToString();
                list_type2.Add(type2);
            }
            return true;
        }

        private int ColNo(string element, DataTable dt,int rowNo)
        {
            DataRowView drv = dt.DefaultView[rowNo];
            for (int i = 0; i < dt.Columns.Count;i++)
            {
              //  System.Windows.MessageBox.Show(element + " "+drv[i].ToString());
                if (drv[i].ToString().Contains(element))
                {
                   
                    return i;
                }
            }
            return -1;
        }

        private bool CheckIsFileExist()
        {
            if (this.Comment != "待导入")
            {
                this.OperationResult = "未执行";
                return true;
            }
            bool isexist = false;
            SqlHelper sqlhelper = new SqlHelper();
            string sql = "select FileName from MCA_Type2 where FileName='" + FileName + "'";
            sqlhelper.getSomeDate(sql);
            if (sqlhelper.dt.DefaultView.Count > 0)
            {
                isexist = true;
                this.OperationResult = "取消导入";
                this.Comment = "文件名在数据库中已经存在";
            }
            return isexist;
        }

        public void ImportDatas()
        {
            if (CheckIsFileExist())
            { return; }
            if (list_type2.Count == 0)
            {
                this.OperationResult = "取消导入";
                this.Comment = "没找到()待导入的内容";
            }
           // System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("zh-CN");
            string importdatetime = DateTime.Now.ToShortDateString();
            foreach (Type2ViewModel tvm in list_type2)
            {
                SqlHelper sqlHelper = new SqlHelper();
                string sql = "insert into MCA_Type2 (FileName,AcqDateTime,SampleName,Na,Al,Ca,Cr,Fe,Ni,Cu,Zn,ImportDateTime) values ('"+tvm.FileName+"','"+
                    tvm.AcqDateTime+"','"+tvm.SampleName+"',"+tvm.Na+","+tvm.Al+","+tvm.Ca+","+tvm.Cr+","+tvm.Fe+","+tvm.Ni+","+tvm.Cu+","+tvm.Zn+",'" + importdatetime+"')";
                sqlHelper.getSomeDate(sql);
            }
            this.OperationResult = "已导入";
            this.Comment = "导入成功";
        }

    }
}
