using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using MCADataTranslator.Helper;
using System.Collections.ObjectModel;
using MCADataTranslator.Bll;
using System.Data;
using System.Data.SqlClient;
namespace MCADataTranslator.Content
{
    /// <summary>
    /// ImportAppearenceEnhanced.xaml 的交互逻辑
    /// </summary>
    public partial class ImportAppearenceEnhanced : UserControl
    {
        public ImportAppearenceEnhanced()
        {
            InitializeComponent();
        }

        private ObservableCollection<ImportDGViewModel> obc_import;
        private CSVValueViewModel vm_csv = new CSVValueViewModel();

        private void button_selectfile_Click(object sender, RoutedEventArgs e)
        {
            string[] filepaths = selectfile();
            if (filepaths == null)
            {
                obc_import = new ObservableCollection<ImportDGViewModel>();
                return;
            }
            ProgressRing.IsActive = true;
            Thread t = new Thread(new ParameterizedThreadStart(ThreadWork));
            t.IsBackground = true;
            t.Start(filepaths);
        }

        private void ThreadWork(object filepaths)
        {
            obc_import = OBCImportVM((string[])filepaths);
            
            Dispatcher.Invoke(new Action(() => {
                DG1.ItemsSource = obc_import;
                button_import.IsEnabled = true;
                ProgressRing.IsActive = false;
            }));
            }

        private void button_import_Click(object sender, RoutedEventArgs e)
        {

            foreach (ImportDGViewModel dg in obc_import)
            {
                ImportDGViewModel dg_c = dg;
                if (!string.IsNullOrEmpty(dg_c.CsvType))
                {
                    ImportData(dg_c);
                    dg.Comment = dg_c.Comment;
                    dg.Csv_VM = dg_c.Csv_VM;
                    dg.OperationResult = dg_c.OperationResult;
                }
                else { dg.OperationResult = "未执行"; }
            }

        }

        private string[] selectfile()
        {
            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
            openFile.Filter = "CSV(逗号分隔)(*.csv)|*.csv";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFile.Multiselect = true;
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return null;
            }
            return openFile.FileNames;
        }

        //赋值obc_import,并执行检查
        private ObservableCollection<ImportDGViewModel> OBCImportVM(string[] filepath)
        {
            ObservableCollection<ImportDGViewModel> obc = new ObservableCollection<ImportDGViewModel>();
            foreach (string fp in filepath)
            {
                ImportDGViewModel idgvm = new ImportDGViewModel(fp);
                CheckFileInOBC(idgvm);
                obc.Add(idgvm);
            }
            return obc;
        }

        //检查文件内容
        private void CheckFileInOBC( ImportDGViewModel dGViewModel)
        {
            if (!File.Exists(dGViewModel.FilePath))
            {
                dGViewModel.Comment = "File Not Exists";
                return;
            }

            ExcelOper excel = new ExcelOper(dGViewModel.FilePath);
            try
            {
                // CsvHelper csv = new CsvHelper();
               
                //DataTable dt = csv.readCsvTxt(dGViewModel.FilePath);
                DataTable dt = excel.GetContentFromExcel();
                if (dt.DefaultView[5][1].ToString().Trim() == "Ag")
                {
                    dGViewModel.CsvType = "Ag";
                }
                else if (dt.DefaultView[5][1].ToString().Trim() == "W")
                {
                    dGViewModel.CsvType = "W";
                }
                else
                {
                    dGViewModel.Comment = "未能识别文件内容";
                    return;
                }
                dGViewModel.SampleComment = dt.DefaultView[3][1].ToString();
                dGViewModel.Csv_VM = new CSVValueViewModel();
                dGViewModel.Csv_VM = CSVVM(dGViewModel.CsvType, dt);
                dGViewModel.FileDir = dGViewModel.Csv_VM.FileDir;
            }
            catch (Exception)
            {
                dGViewModel.Comment = "未能识别文件内容";
                return;
            }
            finally { excel.Quit(); }
            
        }

        //数据库操作
        private void ImportData(ImportDGViewModel dGViewModel)
        {
            if (CheckIsSampleCommentExist(dGViewModel))
            {
                // string sql = "select "+ dGViewModel.CsvType+" from MCA_Pool where SampleComment='"+dGViewModel.SampleComment+"'";   
                //SqlHelper sqlHelper = new SqlHelper();
                // sqlHelper.getSomeDate(sql);
                // bool isExist = (bool)(sqlHelper.dt.DefaultView[0][0]);
                bool isExist = dGViewModel.CsvType == "Ag" ? dGViewModel.Csv_VM.Ag : dGViewModel.Csv_VM.W;
                if (isExist)
                {
                    dGViewModel.Comment = "该数据已经存在";
                    dGViewModel.OperationResult = "取消导入";
                }
                else
                {
                    ImportPicker( dGViewModel);
                }
            }
            else
            {
                ImportMCAPoolDBTable(dGViewModel.Csv_VM);
                ImportPicker(dGViewModel);
            }
                
        }

        //根据type导入Ag或W表
        private void ImportPicker( ImportDGViewModel dGViewModel)
        {
            if (dGViewModel.CsvType == "Ag")
            {
                try
                { ImportMACAgDBTable(dGViewModel.Csv_VM); dGViewModel.OperationResult = "导入成功"; }
                catch (Exception e)
                { dGViewModel.Comment = e.Message; dGViewModel.OperationResult = "导入失败"; return; }
            }
            if (dGViewModel.CsvType == "W")
            {
                try
                {
                    ImportMCAWDBTable(dGViewModel.Csv_VM); dGViewModel.OperationResult = "导入成功";
                }
                catch (Exception e)
                { dGViewModel.Comment = e.Message; dGViewModel.OperationResult = "导入失败"; return; }
            }

            UpdateMCAPoolDBTable(dGViewModel);

        }

        //获取csv数据
        private CSVValueViewModel CSVVM(string type,DataTable dt)
        {
            CSVValueViewModel csvvm = new CSVValueViewModel();
            if (type == "Ag")
            {

                ObservableCollection<AgSeriesViewModel> agvm_list = new ObservableCollection<AgSeriesViewModel>();
                    for (int i = 0; i < dt.DefaultView.Count - 19 - 8; i++)
                    {
                        agvm_list.Add(GetAgData(i,dt));
                   }
                csvvm.agvm_list = agvm_list;
            }
            else if(type =="W")
            {
                ObservableCollection<WSeriesViewModel> wvm_list = new ObservableCollection<WSeriesViewModel>();
                for (int i = 0; i < dt.DefaultView.Count - 19 - 8; i++)
                {
                    wvm_list.Add(GetWData(i, dt));
                }
                csvvm.wvm_list = wvm_list;
            }
           
            csvvm.User = dt.DefaultView[0][1].ToString();
            csvvm.DataTime = dt.DefaultView[1][1].ToString();
            csvvm.Sample = dt.DefaultView[2][1].ToString();
            csvvm.SampleComment = dt.DefaultView[3][1].ToString();
            csvvm.ConditionName = dt.DefaultView[5][1].ToString();
            csvvm.ConditionComment = dt.DefaultView[6][1].ToString();
            csvvm.Repeat = dt.DefaultView[8][1].ToString();
            csvvm.RepeatMax = dt.DefaultView[9][1].ToString();
            csvvm.Recipe = dt.DefaultView[11][1].ToString();
            csvvm.RecipeComment = dt.DefaultView[12][1].ToString();
            csvvm.Version = dt.DefaultView[14][1].ToString();
            string filepath = dt.DefaultView[15][1].ToString();
            csvvm.FileDir = Path.GetDirectoryName(filepath);
            csvvm.FileName = Path.GetFileName(filepath);
            return csvvm;
        }

        private AgSeriesViewModel GetAgData(int i,DataTable dt_ag)
        {
            AgSeriesViewModel agvm = new AgSeriesViewModel();
            agvm.Index = dt_ag.DefaultView[i + 19][0].ToString();
            agvm.Ag_X = dt_ag.DefaultView[i + 19][1].ToString();
            agvm.Ag_Y = dt_ag.DefaultView[i + 19][2].ToString();
            agvm.Ag_R = dt_ag.DefaultView[i + 19][3].ToString();
            agvm.Ag_T = dt_ag.DefaultView[i + 19][4].ToString();
            agvm.Ag_IncidentAngle = dt_ag.DefaultView[i + 19][5].ToString();
            agvm.Ag_Z = dt_ag.DefaultView[i + 19][6].ToString();
            agvm.Ag_Phi = dt_ag.DefaultView[i + 19][7].ToString();
            agvm.Ag_Si_E10 = dt_ag.DefaultView[i + 19][8].ToString();
            agvm.Ag_Si_C = dt_ag.DefaultView[i + 19][9].ToString();
            agvm.Ag_Si_R = dt_ag.DefaultView[i + 19][10].ToString();
            agvm.Ag_Si_LLD = dt_ag.DefaultView[i + 19][11].ToString();
            agvm.Ag_Ge_E10 = dt_ag.DefaultView[i + 19][12].ToString();
            agvm.Ag_Ge_C = dt_ag.DefaultView[i + 19][13].ToString();
            agvm.Ag_Ge_R = dt_ag.DefaultView[i + 19][14].ToString();
            agvm.Ag_Ge_LLD = dt_ag.DefaultView[i + 19][15].ToString();
            agvm.Ag_As_E10 = dt_ag.DefaultView[i + 19][16].ToString();
            agvm.Ag_As_C = dt_ag.DefaultView[i + 19][17].ToString();
            agvm.Ag_As_R = dt_ag.DefaultView[i + 19][18].ToString();
            agvm.Ag_As_LLD = dt_ag.DefaultView[i + 19][19].ToString();
            agvm.Ag_Na_E10 = dt_ag.DefaultView[i + 19][20].ToString();
            agvm.Ag_Na_C = dt_ag.DefaultView[i + 19][21].ToString();
            agvm.Ag_Na_R = dt_ag.DefaultView[i + 19][22].ToString();
            agvm.Ag_Na_LLD = dt_ag.DefaultView[i + 19][23].ToString();
            agvm.Ag_Mg_E10 = dt_ag.DefaultView[i + 19][24].ToString();
            agvm.Ag_Mg_C = dt_ag.DefaultView[i + 19][25].ToString();
            agvm.Ag_Mg_R = dt_ag.DefaultView[i + 19][26].ToString();
            agvm.Ag_Mg_LLD = dt_ag.DefaultView[i + 19][27].ToString();
            agvm.Ag_Al_E10 = dt_ag.DefaultView[i + 19][28].ToString();
            agvm.Ag_Al_C = dt_ag.DefaultView[i + 19][29].ToString();
            agvm.Ag_Al_R = dt_ag.DefaultView[i + 19][30].ToString();
            agvm.Ag_Al_LLD = dt_ag.DefaultView[i + 19][31].ToString();
            return agvm;
        }

        private WSeriesViewModel GetWData(int i,DataTable dt_w)
        {
            WSeriesViewModel wvm = new WSeriesViewModel();
            wvm.Index = dt_w.DefaultView[i + 19][0].ToString();
            wvm.W_X = dt_w.DefaultView[i + 19][1].ToString();
            wvm.W_Y = dt_w.DefaultView[i + 19][2].ToString();
            wvm.W_R = dt_w.DefaultView[i + 19][3].ToString();
            wvm.W_T = dt_w.DefaultView[i + 19][4].ToString();
            wvm.W_IncidentAngle = dt_w.DefaultView[i + 19][5].ToString();
            wvm.W_Z = dt_w.DefaultView[i + 19][6].ToString();
            wvm.W_Phi = dt_w.DefaultView[i + 19][7].ToString();
            wvm.W_Si_E10 = dt_w.DefaultView[i + 19][8].ToString();
            wvm.W_Si_C = dt_w.DefaultView[i + 19][9].ToString();
            wvm.W_Si_R = dt_w.DefaultView[i + 19][10].ToString();
            wvm.W_Si_LLD = dt_w.DefaultView[i + 19][11].ToString();
            wvm.W_S_E10 = dt_w.DefaultView[i + 19][12].ToString();
            wvm.W_S_C = dt_w.DefaultView[i + 19][13].ToString();
            wvm.W_S_R = dt_w.DefaultView[i + 19][14].ToString();
            wvm.W_S_LLD = dt_w.DefaultView[i + 19][15].ToString();
            wvm.W_Cl_E10 = dt_w.DefaultView[i + 19][16].ToString();
            wvm.W_Cl_C = dt_w.DefaultView[i + 19][17].ToString();
            wvm.W_Cl_R = dt_w.DefaultView[i + 19][18].ToString();
            wvm.W_Cl_LLD = dt_w.DefaultView[i + 19][19].ToString();
            wvm.W_K_E10 = dt_w.DefaultView[i + 19][20].ToString();
            wvm.W_K_C = dt_w.DefaultView[i + 19][21].ToString();
            wvm.W_K_R = dt_w.DefaultView[i + 19][22].ToString();
            wvm.W_K_LLD = dt_w.DefaultView[i + 19][23].ToString();
            wvm.W_Ca_E10 = dt_w.DefaultView[i + 19][24].ToString();
            wvm.W_Ca_C = dt_w.DefaultView[i + 19][25].ToString();
            wvm.W_Ca_R = dt_w.DefaultView[i + 19][26].ToString();
            wvm.W_Ca_LLD = dt_w.DefaultView[i + 19][27].ToString();
            wvm.W_Ti_E10 = dt_w.DefaultView[i + 19][28].ToString();
            wvm.W_Ti_C = dt_w.DefaultView[i + 19][29].ToString();
            wvm.W_Ti_R = dt_w.DefaultView[i + 19][30].ToString();
            wvm.W_Ti_LLD = dt_w.DefaultView[i + 19][31].ToString();
            wvm.W_V_E10 = dt_w.DefaultView[i + 19][32].ToString();
            wvm.W_V_C = dt_w.DefaultView[i + 19][33].ToString();
            wvm.W_V_R = dt_w.DefaultView[i + 19][34].ToString();
            wvm.W_V_LLD = dt_w.DefaultView[i + 19][35].ToString();
            wvm.W_Cr_E10 = dt_w.DefaultView[i + 19][36].ToString();
            wvm.W_Cr_C = dt_w.DefaultView[i + 19][37].ToString();
            wvm.W_Cr_R = dt_w.DefaultView[i + 19][38].ToString();
            wvm.W_Cr_LLD = dt_w.DefaultView[i + 19][39].ToString();
            wvm.W_Mn_E10 = dt_w.DefaultView[i + 19][40].ToString();
            wvm.W_Mn_C = dt_w.DefaultView[i + 19][41].ToString();
            wvm.W_Mn_R = dt_w.DefaultView[i + 19][42].ToString();
            wvm.W_Mn_LLD = dt_w.DefaultView[i + 19][43].ToString();
            wvm.W_Fe_E10 = dt_w.DefaultView[i + 19][44].ToString();
            wvm.W_Fe_C = dt_w.DefaultView[i + 19][45].ToString();
            wvm.W_Fe_R = dt_w.DefaultView[i + 19][46].ToString();
            wvm.W_Fe_LLD = dt_w.DefaultView[i + 19][47].ToString();
            wvm.W_Co_E10 = dt_w.DefaultView[i + 19][48].ToString();
            wvm.W_Co_C = dt_w.DefaultView[i + 19][49].ToString();
            wvm.W_Co_R = dt_w.DefaultView[i + 19][50].ToString();
            wvm.W_Co_LLD = dt_w.DefaultView[i + 19][51].ToString();
            wvm.W_Ni_E10 = dt_w.DefaultView[i + 19][52].ToString();
            wvm.W_Ni_C = dt_w.DefaultView[i + 19][53].ToString();
            wvm.W_Ni_R = dt_w.DefaultView[i + 19][54].ToString();
            wvm.W_Ni_LLD = dt_w.DefaultView[i + 19][55].ToString();
            wvm.W_Cu_E10 = dt_w.DefaultView[i + 19][56].ToString();
            wvm.W_Cu_C = dt_w.DefaultView[i + 19][57].ToString();
            wvm.W_Cu_R = dt_w.DefaultView[i + 19][58].ToString();
            wvm.W_Cu_LLD = dt_w.DefaultView[i + 19][59].ToString();
            wvm.W_Zn_E10 = dt_w.DefaultView[i + 19][60].ToString();
            wvm.W_Zn_C = dt_w.DefaultView[i + 19][61].ToString();
            wvm.W_Zn_R = dt_w.DefaultView[i + 19][62].ToString();
            wvm.W_Zn_LLD = dt_w.DefaultView[i + 19][63].ToString();
            wvm.W_Sb_E10 = dt_w.DefaultView[i + 19][64].ToString();
            wvm.W_Sb_C = dt_w.DefaultView[i + 19][65].ToString();
            wvm.W_Sb_R = dt_w.DefaultView[i + 19][66].ToString();
            wvm.W_Sb_LLD = dt_w.DefaultView[i + 19][67].ToString();
            wvm.W_Te_E10 = dt_w.DefaultView[i + 19][68].ToString();
            wvm.W_Te_C = dt_w.DefaultView[i + 19][69].ToString();
            wvm.W_Te_R = dt_w.DefaultView[i + 19][70].ToString();
            wvm.W_Te_LLD = dt_w.DefaultView[i + 19][71].ToString();
            wvm.W_Na_E10 = dt_w.DefaultView[i + 19][72].ToString();
            wvm.W_Na_C = dt_w.DefaultView[i + 19][73].ToString();
            wvm.W_Na_R = dt_w.DefaultView[i + 19][74].ToString();
            wvm.W_Na_LLD = dt_w.DefaultView[i + 19][75].ToString();
            wvm.W_Mg_E10 = dt_w.DefaultView[i + 19][76].ToString();
            wvm.W_Mg_C = dt_w.DefaultView[i + 19][77].ToString();
            wvm.W_Mg_R = dt_w.DefaultView[i + 19][78].ToString();
            wvm.W_Mg_LLD = dt_w.DefaultView[i + 19][79].ToString();
            wvm.W_Al_E10 = dt_w.DefaultView[i + 19][80].ToString();
            wvm.W_Al_C = dt_w.DefaultView[i + 19][81].ToString();
            wvm.W_Al_R = dt_w.DefaultView[i + 19][82].ToString();
            wvm.W_Al_LLD = dt_w.DefaultView[i + 19][83].ToString();
            return wvm;
        }

        private bool CheckIsSampleCommentExist(ImportDGViewModel dGViewModel)
        {
            bool isexist = false;
            SqlHelper sqlhelper = new SqlHelper();
            string sql = "select UID,Ag,W from MCA_Pool where SampleComment='" + dGViewModel.SampleComment + "' and FileDir='"+dGViewModel.Csv_VM.FileDir+"'";
            sqlhelper.getSomeDate(sql);
            if (sqlhelper.dt.DefaultView.Count > 0)
            {
                isexist = true;
                dGViewModel.Csv_VM.GUID = sqlhelper.dt.DefaultView[0][0].ToString();
                dGViewModel.Csv_VM.Ag = (bool)sqlhelper.dt.DefaultView[0][1];
                dGViewModel.Csv_VM.W = (bool)sqlhelper.dt.DefaultView[0][2];
            }
            return isexist;
        }
        //数据库Insert操作
        private void ImportMCAPoolDBTable(CSVValueViewModel csv)
        {
            SqlHelper sqlhelper = new SqlHelper();
            string sql = "insert into MCA_Pool (UID,SampleComment,Sample,UpdateDateTime,UserName,Recipe,Version,EQP,FileDir,FileName) values ('"+csv.GUID+"','" + csv.SampleComment + "','" + csv.Sample + "','" + csv.DataTime + "','" + csv.User + "','" + csv.Recipe + "','" + csv.Version + "','" + csv.SampleComment.Split(' ')[0] +"','"+csv.FileDir+"','"+csv.FileName +"')"; sqlhelper.getSomeDate(sql);
        }

        private void ImportMACAgDBTable(CSVValueViewModel csv)
        {
            SqlHelper sqlhelper = new SqlHelper();
            ObservableCollection<AgSeriesViewModel> ag_list = csv.agvm_list;

            foreach (AgSeriesViewModel agvm in ag_list)
            {
                string sql = "insert into MCA_Ag (SampleComment,IndexNo,X,Y,R,T,IncidentAngle,Z,Phi,Si_E10,Si_IntenC,Si_IntenR,Si_LLD,Ge_E10,Ge_IntenC,Ge_IntenR,Ge_LLD,As_E10,As_IntenC,As_IntenR,As_LLD,Na_E10,Na_IntenC,Na_IntenR,Na_LLD,Mg_E10,Mg_IntenC,Mg_IntenR,Mg_LLD,Al_E10,Al_IntenC,Al_IntenR,Al_LLD,POOL_UID) values(@sampleComment,@indexNo,@x,@y,@r,@t,@incidentAngle,@z,@phi,@si_E10,@si_IntenC,@si_IntenR,@si_LLD,@ge_E10,@ge_IntenC,@ge_IntenR,@ge_LLD,@as_E10,@as_IntenC,@as_IntenR,@as_LLD,@na_E10,@na_IntenC,@na_IntenR,@na_LLD,@mg_E10,@mg_IntenC,@mg_IntenR,@mg_LLD,@al_E10,@al_IntenC,@al_IntenR,@al_LLD,@pool_uid)";
                SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@sampleComment",csv.SampleComment),
                    new SqlParameter("@indexNO",agvm.Index),
                    new SqlParameter("@x",ConvertString2Float( agvm.Ag_X)),
                    new SqlParameter("@y",ConvertString2Float(agvm.Ag_Y)),
                    new SqlParameter("@r",ConvertString2Float(agvm.Ag_Al_R)),
                    new SqlParameter("@t",ConvertString2Float(agvm.Ag_T)),
                    new SqlParameter("@incidentAngle",ConvertString2Float(agvm.Ag_IncidentAngle)),
                    new SqlParameter("@z",ConvertString2Float(agvm.Ag_Z)),
                    new SqlParameter("@phi",ConvertString2Float(agvm.Ag_Phi)),
                    new SqlParameter("@si_E10",ConvertString2Float(agvm.Ag_Si_E10)),
                    new SqlParameter("@si_IntenC",ConvertString2Float(agvm.Ag_Si_C)),
                    new SqlParameter("@si_IntenR",ConvertString2Float(agvm.Ag_Si_R)),
                    new SqlParameter("@si_LLD",ConvertString2Float(agvm.Ag_Si_LLD)),
                    new SqlParameter("@ge_E10",ConvertString2Float(agvm.Ag_Ge_E10)),
                    new SqlParameter("@ge_IntenC",ConvertString2Float(agvm.Ag_Ge_C)),
                    new SqlParameter("@ge_IntenR",ConvertString2Float(agvm.Ag_Ge_R)),
                    new SqlParameter("@ge_LLD",ConvertString2Float(agvm.Ag_Ge_LLD)),
                    new SqlParameter("@as_E10",ConvertString2Float(agvm.Ag_As_E10)),
                    new SqlParameter("@as_IntenC",ConvertString2Float(agvm.Ag_As_C)),
                    new SqlParameter("@as_IntenR",ConvertString2Float(agvm.Ag_As_R)),
                    new SqlParameter("@as_LLD",ConvertString2Float(agvm.Ag_As_LLD)),
                    new SqlParameter("@na_E10",ConvertString2Float(agvm.Ag_Na_E10)),
                    new SqlParameter("@na_IntenC",ConvertString2Float(agvm.Ag_Na_C)),
                    new SqlParameter("@na_IntenR",ConvertString2Float(agvm.Ag_Na_R)),
                    new SqlParameter("@na_LLD",ConvertString2Float(agvm.Ag_Na_LLD)),
                    new SqlParameter("@mg_E10",ConvertString2Float(agvm.Ag_Mg_E10)),
                    new SqlParameter("@mg_IntenC",ConvertString2Float(agvm.Ag_Mg_C)),
                    new SqlParameter("@mg_IntenR",ConvertString2Float(agvm.Ag_Mg_R)),
                    new SqlParameter("@mg_LLD",ConvertString2Float(agvm.Ag_Mg_LLD)),
                    new SqlParameter("@al_E10",ConvertString2Float(agvm.Ag_Al_E10)),
                    new SqlParameter("@al_IntenC",ConvertString2Float(agvm.Ag_Al_C)),
                    new SqlParameter("@al_IntenR",ConvertString2Float(agvm.Ag_Al_R)),
                    new SqlParameter("@al_LLD",ConvertString2Float(agvm.Ag_Al_LLD)),
                     new SqlParameter("@pool_uid",csv.GUID)
                };
                sqlhelper.getSomeData2(sql, para);

            }

        }

        private void ImportMCAWDBTable(CSVValueViewModel csv)
        {
            SqlHelper sqlhelper = new SqlHelper();
            ObservableCollection<WSeriesViewModel> w_list = csv.wvm_list;
            foreach (WSeriesViewModel wvm in w_list)
            {
                string sql = "insert into MCA_W (SampleComment,IndexNo,X,Y,R,T,IncidentAngle,Z,Phi,Si_E10,Si_IntenC,Si_IntenR,Si_LLD,S_E10,S_IntenC,S_IntenR,S_LLD,Cl_E10,Cl_IntenC,Cl_IntenR,Cl_LLD,K_E10,K_IntenC,K_IntenR,K_LLD,Ca_E10,Ca_IntenC,Ca_IntenR,Ca_LLD,Ti_E10,Ti_IntenC,Ti_IntenR,Ti_LLD,V_E10,V_IntenC,V_IntenR,V_LLD,Cr_E10,Cr_IntenC,Cr_IntenR,Cr_LLD,Mn_E10,Mn_IntenC,Mn_IntenR,Mn_LLD,Fe_E10,Fe_IntenC,Fe_IntenR,Fe_LLD,Co_E10,Co_IntenC,Co_IntenR,Co_LLD,Ni_E10,Ni_IntenC,Ni_IntenR,Ni_LLD,Cu_E10,Cu_IntenC,Cu_IntenR,Cu_LLD,Zn_E10,Zn_IntenC,Zn_IntenR,Zn_LLD,Sb_E10,Sb_IntenC,Sb_IntenR,Sb_LLD,Te_E10,Te_IntenC,Te_IntenR,Te_LLD,Na_E10,Na_IntenC,Na_IntenR,Na_LLD,Mg_E10,Mg_IntenC,Mg_IntenR,Mg_LLD,Al_E10,Al_IntenC,Al_IntenR,Al_LLD,POOL_UID) values (@sampleComment,@indexNo,@x,@y,@r,@t,@incidentAngle,@z,@phi,@si_E10,@si_IntenC,@si_IntenR,@si_LLD,@s_E10,@s_IntenC,@s_IntenR,@s_LLD,@cl_E10,@cl_IntenC,@cl_IntenR,@cl_LLD,@k_E10,@k_IntenC,@k_IntenR,@k_LLD,@ca_E10,@ca_IntenC,@ca_IntenR,@ca_LLD,@ti_E10,@ti_IntenC,@ti_IntenR,@ti_LLD,@v_E10,@v_IntenC,@v_IntenR,@v_LLD,@cr_E10,@cr_IntenC,@cr_IntenR,@cr_LLD,@mn_E10,@mn_IntenC,@mn_IntenR,@mn_LLD,@fe_E10,@fe_IntenC,@fe_IntenR,@fe_LLD,@co_E10,@co_IntenC,@co_IntenR,@co_LLD,@ni_E10,@ni_IntenC,@ni_IntenR,@ni_LLD,@cu_E10,@cu_IntenC,@cu_IntenR,@cu_LLD,@zn_E10,@zn_IntenC,@zn_IntenR,@zn_LLD,@sb_E10,@sb_IntenC,@sb_IntenR,@sb_LLD,@te_E10,@te_IntenC,@te_IntenR,@te_LLD,@na_E10,@na_IntenC,@na_IntenR,@na_LLD,@mg_E10,@mg_IntenC,@mg_IntenR,@mg_LLD,@al_E10,@al_IntenC,@al_IntenR,@al_LLD,@pool_uid)";
                SqlParameter[] para = new SqlParameter[]
                 {
                    new SqlParameter("@sampleComment",csv.SampleComment),
                    new SqlParameter("@indexNO",wvm.Index),
                    new SqlParameter("@x",ConvertString2Float( wvm.W_X)),
                    new SqlParameter("@y",ConvertString2Float(wvm.W_Y)),
                    new SqlParameter("@r",ConvertString2Float(wvm.W_Al_R)),
                    new SqlParameter("@t",ConvertString2Float(wvm.W_T)),
                    new SqlParameter("@incidentAngle",ConvertString2Float(wvm.W_IncidentAngle)),
                    new SqlParameter("@z",ConvertString2Float(wvm.W_Z)),
                    new SqlParameter("@phi",ConvertString2Float(wvm.W_Phi)),
                    new SqlParameter("@si_E10",ConvertString2Float(wvm.W_Si_E10)),
                    new SqlParameter("@si_IntenC",ConvertString2Float(wvm.W_Si_C)),
                    new SqlParameter("@si_IntenR",ConvertString2Float(wvm.W_Si_R)),
                    new SqlParameter("@si_LLD",ConvertString2Float(wvm.W_S_LLD)),
                    new SqlParameter("@s_E10",ConvertString2Float(wvm.W_S_E10)),
                    new SqlParameter("@s_IntenC",ConvertString2Float(wvm.W_S_C)),
                    new SqlParameter("@s_IntenR",ConvertString2Float(wvm.W_S_R)),
                    new SqlParameter("@s_LLD",ConvertString2Float(wvm.W_S_LLD)),
                    new SqlParameter("@cl_E10",ConvertString2Float(wvm.W_Cl_E10)),
                    new SqlParameter("@cl_IntenC",ConvertString2Float(wvm.W_Cl_C)),
                    new SqlParameter("@cl_IntenR",ConvertString2Float(wvm.W_Cl_R)),
                    new SqlParameter("@cl_LLD",ConvertString2Float(wvm.W_Cl_LLD)),
                    new SqlParameter("@k_E10",ConvertString2Float(wvm.W_K_E10)),
                    new SqlParameter("@k_IntenC",ConvertString2Float(wvm.W_K_C)),
                    new SqlParameter("@k_IntenR",ConvertString2Float(wvm.W_K_R)),
                    new SqlParameter("@k_LLD",ConvertString2Float(wvm.W_K_LLD)),
                    new SqlParameter("@ca_E10",ConvertString2Float(wvm.W_Ca_E10)),
                    new SqlParameter("@ca_IntenC",ConvertString2Float(wvm.W_Ca_C)),
                    new SqlParameter("@ca_IntenR",ConvertString2Float(wvm.W_Ca_R)),
                    new SqlParameter("@ca_LLD",ConvertString2Float(wvm.W_Ca_LLD)),
                    new SqlParameter("@ti_E10",ConvertString2Float(wvm.W_Ti_E10)),
                    new SqlParameter("@ti_IntenC",ConvertString2Float(wvm.W_Ti_C)),
                    new SqlParameter("@ti_IntenR",ConvertString2Float(wvm.W_Ti_R)),
                    new SqlParameter("@ti_LLD",ConvertString2Float(wvm.W_Ti_LLD)),
                    new SqlParameter("@v_E10",ConvertString2Float(wvm.W_V_E10)),
                    new SqlParameter("@v_IntenC",ConvertString2Float(wvm.W_V_C)),
                    new SqlParameter("@v_IntenR",ConvertString2Float(wvm.W_V_R)),
                    new SqlParameter("@v_LLD",ConvertString2Float(wvm.W_V_LLD)),
                     new SqlParameter("@cr_E10",ConvertString2Float(wvm.W_Cr_E10)),
                    new SqlParameter("@cr_IntenC",ConvertString2Float(wvm.W_Cr_C)),
                    new SqlParameter("@cr_IntenR",ConvertString2Float(wvm.W_Cr_R)),
                    new SqlParameter("@cr_LLD",ConvertString2Float(wvm.W_Cr_LLD)),
                     new SqlParameter("@mn_E10",ConvertString2Float(wvm.W_Mn_E10)),
                    new SqlParameter("@mn_IntenC",ConvertString2Float(wvm.W_Mn_C)),
                    new SqlParameter("@mn_IntenR",ConvertString2Float(wvm.W_Mn_R)),
                    new SqlParameter("@mn_LLD",ConvertString2Float(wvm.W_Mn_LLD)),
                     new SqlParameter("@fe_E10",ConvertString2Float(wvm.W_Fe_E10)),
                    new SqlParameter("@fe_IntenC",ConvertString2Float(wvm.W_Fe_C)),
                    new SqlParameter("@fe_IntenR",ConvertString2Float(wvm.W_Fe_R)),
                    new SqlParameter("@fe_LLD",ConvertString2Float(wvm.W_Fe_LLD)),
                     new SqlParameter("@co_E10",ConvertString2Float(wvm.W_Co_E10)),
                    new SqlParameter("@co_IntenC",ConvertString2Float(wvm.W_Co_C)),
                    new SqlParameter("@co_IntenR",ConvertString2Float(wvm.W_Co_R)),
                    new SqlParameter("@co_LLD",ConvertString2Float(wvm.W_Co_LLD)),
                     new SqlParameter("@ni_E10",ConvertString2Float(wvm.W_Ni_E10)),
                    new SqlParameter("@ni_IntenC",ConvertString2Float(wvm.W_Ni_C)),
                    new SqlParameter("@ni_IntenR",ConvertString2Float(wvm.W_Ni_R)),
                    new SqlParameter("@ni_LLD",ConvertString2Float(wvm.W_Ni_LLD)),
                     new SqlParameter("@cu_E10",ConvertString2Float(wvm.W_Cu_E10)),
                    new SqlParameter("@cu_IntenC",ConvertString2Float(wvm.W_Cu_C)),
                    new SqlParameter("@cu_IntenR",ConvertString2Float(wvm.W_Cu_R)),
                    new SqlParameter("@cu_LLD",ConvertString2Float(wvm.W_Cu_LLD)),
                     new SqlParameter("@zn_E10",ConvertString2Float(wvm.W_Zn_E10)),
                    new SqlParameter("@zn_IntenC",ConvertString2Float(wvm.W_Zn_C)),
                    new SqlParameter("@zn_IntenR",ConvertString2Float(wvm.W_Zn_R)),
                    new SqlParameter("@zn_LLD",ConvertString2Float(wvm.W_Zn_LLD)),
                     new SqlParameter("@sb_E10",ConvertString2Float(wvm.W_Sb_E10)),
                    new SqlParameter("@sb_IntenC",ConvertString2Float(wvm.W_Sb_C)),
                    new SqlParameter("@sb_IntenR",ConvertString2Float(wvm.W_Sb_R)),
                    new SqlParameter("@sb_LLD",ConvertString2Float(wvm.W_Sb_LLD)),
                     new SqlParameter("@te_E10",ConvertString2Float(wvm.W_Te_E10)),
                    new SqlParameter("@te_IntenC",ConvertString2Float(wvm.W_Te_C)),
                    new SqlParameter("@te_IntenR",ConvertString2Float(wvm.W_Te_R)),
                    new SqlParameter("@te_LLD",ConvertString2Float(wvm.W_Te_LLD)),
                     new SqlParameter("@na_E10",ConvertString2Float(wvm.W_Na_E10)),
                    new SqlParameter("@na_IntenC",ConvertString2Float(wvm.W_Na_C)),
                    new SqlParameter("@na_IntenR",ConvertString2Float(wvm.W_Na_R)),
                    new SqlParameter("@na_LLD",ConvertString2Float(wvm.W_Na_LLD)),
                     new SqlParameter("@mg_E10",ConvertString2Float(wvm.W_Mg_E10)),
                    new SqlParameter("@mg_IntenC",ConvertString2Float(wvm.W_Mg_C)),
                    new SqlParameter("@mg_IntenR",ConvertString2Float(wvm.W_Mg_R)),
                    new SqlParameter("@mg_LLD",ConvertString2Float(wvm.W_Mg_LLD)),
                     new SqlParameter("@al_E10",ConvertString2Float(wvm.W_Al_E10)),
                    new SqlParameter("@al_IntenC",ConvertString2Float(wvm.W_Al_C)),
                    new SqlParameter("@al_IntenR",ConvertString2Float(wvm.W_Al_R)),
                    new SqlParameter("@al_LLD",ConvertString2Float(wvm.W_Al_LLD)),
                    new SqlParameter("@pool_uid",csv.GUID),
                 };

                sqlhelper.getSomeData2(sql, para);
            }
        }

        private void UpdateMCAPoolDBTable(ImportDGViewModel dGViewModel)
        {
          //  string sql = "update MCA_Pool set "+dGViewModel.CsvType+"=1 where SampleComment ='"+dGViewModel.SampleComment+"'";
            string sql = string.Format("update MCA_POOL set {0}=1 where UID='{1}'",dGViewModel.CsvType,dGViewModel.Csv_VM.GUID);  //2019-3-5修改
            SqlHelper sqlHelper = new SqlHelper();
            sqlHelper.getSomeDate(sql);
        }

        private float ConvertString2Float(string str)
        {
            float f = 0;
            f = string.IsNullOrEmpty(str.Trim()) ? 0 : float.Parse(str.Trim());

            return f;
        }
    }
}
