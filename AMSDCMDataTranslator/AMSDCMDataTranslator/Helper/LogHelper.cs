﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Helper
{
   public class LogHelper
    {
        public static void ErrorLog(object msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("logerror");
            Task.Run(() => log.Error(msg));   //异步
        }

        public static void ErrorLog(Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("logerror");
            Task.Run(() => log.Error(ex.Message.ToString() + "/r/n" + ex.Source.ToString() + "/r/n" + ex.TargetSite.ToString() + "/r/n" + ex.StackTrace.ToString()));
        }

        public static void ErrorLog(object msg,Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("logerror");
            Task.Run(() => log.Error( msg.ToString()+"/r/n" + ex.Message.ToString() + "/r/n" + ex.Source.ToString() + "/r/n" + ex.TargetSite.ToString() + "/r/n" + ex.StackTrace.ToString()));

        }

        public static void InfoLog(object msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("loginfo");
            log.Info(msg);
        }

        public static void WATInfoLog(object msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("watloginfo");
            log.Info(msg);
        }


    }
}
