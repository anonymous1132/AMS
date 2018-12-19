using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Presentation;
using System.Windows.Media;

namespace AMS.CIM.Caojin.SqlReplicationAnalysisTool.Model
{
    public class RunStatusGroupModel : NotifyPropertyChanged
    {
        private string targetTime;
        public string TargetTime { get {return targetTime; } set { targetTime = value;OnPropertyChanged("TargetTime");OnPropertyChanged("DtargetTime"); } }
        public DateTime DtargetTime
        {
            get
            {
                try
                {
                    return DateTime.ParseExact(targetTime, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                }
                catch (Exception e)
                {
                    throw new DateTimeParseException("RunStatusGroupModel.DtargetTime时间格式转换错误", e);
                }
            }
        }

        private List<RunStatusLineModel> lineModels = new List<RunStatusLineModel>();
        public List<RunStatusLineModel> LineModels
        {
            get { return lineModels; }
            set { lineModels = value;OnPropertyChanged("LineModels"); }
        }

        public void UpdateDelta()
        {
            foreach (var item in lineModels)
            {
                try
                {
                    item.DeltaTimeSpan = (DtargetTime - item.DSynchTime).Duration();
                }
                catch (DateTimeParseException)
                {
                    item.DeltaTimeSpan = TimeSpan.MaxValue;
                }
            }

        }

        //private System.Windows.Controls.DataGrid DataGrid;
        //public void SetDataGrid(System.Windows.Controls.DataGrid dataGrid)
        //{
        //    DataGrid = dataGrid;
        //}


        //private void SetDataRowForeground(object sender)
        //{
        //    var row = sender as System.Windows.Controls.DataGridRow;
        //    row.Foreground =new SolidColorBrush((Color)ColorConverter.ConvertFromString("Red"));
        //}
        
    }
}
