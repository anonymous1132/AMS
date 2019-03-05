using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;

namespace AMSDCMDataTranslator
{
    public class SshOper
    {
        public SshOper(string ip,string username,string password)
        {
            IP = ip;
            UserName = username;
            Password = password;
            TrySshConnect();
        }

        public SshOper()
        {
            TrySshConnect();
        }

        public string IP
        {
            get;
            set;
        } = "10.132.0.38";

        public string UserName
        {
            get;
            set;
        } = "ace";

        public string Password
        {
            get;
            set;
        } = "AcElic";

        private SshClient client ;

        private void TrySshConnect()
        {
            client = new SshClient(IP,UserName,Password);
            client.Connect();
        }

        public string GetResault(string command)
        {
            var cmd = client.CreateCommand(command);
            cmd.Execute();
            client.Disconnect();
            return cmd.Result;
        }
    }
}
