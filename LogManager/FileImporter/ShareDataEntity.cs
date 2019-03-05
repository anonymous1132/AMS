using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections.ObjectModel;
using FirstFloor.ModernUI.App.Model;
using Caojin.Common;

namespace FirstFloor.ModernUI.App
{
    public class ShareDataEntity
    {
        //单例模式
        private static ShareDataEntity _singleton;
        public static ShareDataEntity GetSingleton()
        {
            if (_singleton == null)
            {
                _singleton = new ShareDataEntity();
            }
            return _singleton;
        }
        //构造函数
        public ShareDataEntity()
        {
            MailSmtpCollection = new ObservableCollection<MailSmtpModel>();
            MailQueueCollection = new ObservableCollection<MailQueueModel>();
            MailQueueReserverCollection = new ObservableCollection<MailQueueReserverEntity>();
        }
        private SqlHelper sqlo = new SqlHelper();

        #region MailSmtpLog
        /// <summary>
        /// 获取 MailSmtpModel数据集合，所有查询界面的数据从此获取
        /// </summary>
        public ObservableCollection<MailSmtpModel> MailSmtpCollection;


        public void AddNewModelToMailSmtpCollection(List<MailSmtpModel>smtpList)
        {
            int c = smtpList.Count / 5;
            for (int i = 0; i < c; i++)
            {
                StringBuilder value = new StringBuilder();
                for (int j = 0; j < 5; j++)
                {
                    var smtpLog = smtpList[i * 5 + j];
                    string temp = string.Format("('{0}','{1}','{2}',{3},N'{4}',N'{5}',N'{6}',{7},{8},N'{9}'),", smtpLog.Guid, DateTimeBuilder.CommonStrDateTime(smtpLog.StartTime), DateTimeBuilder.CommonStrDateTime(smtpLog.EndTime), smtpLog.Mask.ToString(), smtpLog.IPAddress, smtpLog.SendMailAddress, smtpLog.ReserveMailAddress, smtpLog.MaxSize.ToString(), smtpLog.ReserveSize.ToString(),smtpLog.Details);
                    value.Append(temp);
                }
                string sql = string.Format("insert into T_SmtpLog values {0}", value.ToString().TrimEnd(','));
                sqlo.getSomeDate(sql);
            }

            if (smtpList.Count - c * 5 > 0)
            {
                string value2 = "";
                for (int i = c * 5; i < smtpList.Count; i++)
                {
                    var smtpLog = smtpList[i];
                    string temp = string.Format("('{0}','{1}','{2}',{3},N'{4}',N'{5}',N'{6}',{7},{8},N'{9}'),", smtpLog.Guid, DateTimeBuilder.CommonStrDateTime(smtpLog.StartTime), DateTimeBuilder.CommonStrDateTime(smtpLog.EndTime), smtpLog.Mask.ToString(), smtpLog.IPAddress, smtpLog.SendMailAddress, smtpLog.ReserveMailAddress, smtpLog.MaxSize.ToString(), smtpLog.ReserveSize.ToString(), smtpLog.Details);
                    value2 += temp;
                }
                string sql2 = string.Format("insert into T_SmtpLog values {0}", value2.TrimEnd(','));
                sqlo.getSomeDate(sql2);
            }

        }


        #endregion

        #region MailQueueLog
        /// <summary>
        /// 获取 MailQueueModel数据集合，所有查询界面的数据从此获取
        /// </summary>
        public ObservableCollection<MailQueueModel> MailQueueCollection;


        public void AddNewModelToMailQueueCollection(List<MailQueueModel> queueList)
        {
            int c = queueList.Count / 50;
            for (int i = 0; i < c; i++)
            {
                StringBuilder value = new StringBuilder();
                for (int j = 0; j < 50; j++)
                {
                    var queueLog = queueList[i * 50 + j];
                    string temp = string.Format("('{0}','{1}','{2}',{3},N'{4}',{5},N'{6}',N'{7}'),", queueLog.Guid, DateTimeBuilder.CommonStrDateTime(queueLog.StartTime), DateTimeBuilder.CommonStrDateTime(queueLog.EndTime), queueLog.Mask.ToString(), queueLog.SendMailAddress, queueLog.SendSize.ToString(), queueLog.Details,queueLog.MailID);
                    value.Append(temp);
                }
                string sql = string.Format("insert into T_QueueLog values {0}", value.ToString().TrimEnd(','));
                sqlo.getSomeDate(sql);
            }

            if (queueList.Count - c * 50 > 0)
            {
                string value2 = "";
                for (int i = c * 50; i < queueList.Count; i++)
                {
                    var queueLog = queueList[i];
                    string temp = string.Format("('{0}','{1}','{2}',{3},N'{4}',{5},N'{6}',N'{7}'),", queueLog.Guid, DateTimeBuilder.CommonStrDateTime(queueLog.StartTime), DateTimeBuilder.CommonStrDateTime(queueLog.EndTime), queueLog.Mask.ToString(), queueLog.SendMailAddress, queueLog.SendSize.ToString(), queueLog.Details,queueLog.MailID);
                    value2 += temp;
                }
                string sql2 = string.Format("insert into T_QueueLog values {0}", value2.TrimEnd(','));
                sqlo.getSomeDate(sql2);
            }
        }


        #endregion

        #region MailQueueReserver
        /// <summary>
        /// 获取 MailQueueModel的Reserver数据集合，所有查询界面的数据从此获取
        /// </summary>
        public ObservableCollection<MailQueueReserverEntity> MailQueueReserverCollection;
       

        public void AddNewModelToMailQueueReserverCollection(List<MailQueueReserverEntity> reserverEntities)
        {
            int c = reserverEntities.Count / 50;
            for (int i = 0; i < c; i++)
            {
                StringBuilder value = new StringBuilder();
                for (int j = 0; j < 50; j++)
                {
                    var reserverEntity = reserverEntities[i * 50 + j];
                    string temp = string.Format("('{0}','{1}','{2}','{3}','{4}','{5}'),", reserverEntity.Guid, reserverEntity.ReserverAddress, reserverEntity.IsLocalServer, reserverEntity.IsSuccessful, reserverEntity.OutSideServerAddress,reserverEntity.MailGuid);
                    value.Append(temp);
                }
                string sql = string.Format("insert into T_QueueLog_ReserverDetail values {0}", value.ToString().TrimEnd(','));
                sqlo.getSomeDate(sql);
            }

            if (reserverEntities.Count - c * 50 > 0)
            {
                string value2 = "";
                for (int i = c * 50; i < reserverEntities.Count; i++)
                {
                    var reserverEntity = reserverEntities[i];
                    string temp = string.Format("('{0}','{1}','{2}','{3}','{4}','{5}'),", reserverEntity.Guid, reserverEntity.ReserverAddress, reserverEntity.IsLocalServer, reserverEntity.IsSuccessful, reserverEntity.OutSideServerAddress,reserverEntity.MailGuid);
                    value2 += temp;
                }
                string sql2 = string.Format("insert into T_QueueLog_ReserverDetail values {0}", value2.TrimEnd(','));
                sqlo.getSomeDate(sql2);
            }
   
        }
        #endregion


    }
}
