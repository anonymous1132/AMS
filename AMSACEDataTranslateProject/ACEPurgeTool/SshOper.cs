using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Renci.SshNet;

namespace ACEPurgeTool
{
    public class SshOper:IDisposable
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


        ~SshOper()
        {
            Dispose();
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

        public List<string> Commands = new List<string>();

        public string GetResault()
        {
            string output="";
            foreach (string comd in Commands)
            {
                var cmd = client.CreateCommand(comd);
                cmd.Execute();
                output+= cmd.Result;
            }
            return output;
        }

        public void Dispose()
        {
            client.Disconnect();
        }
    }
}
