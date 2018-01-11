using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DUT
{
    public class GW : Telnet
    {
        public GW() : base() { }

        public GW(string _host, int _port) : base(_host, _port) { }

        /// <summary>
        /// Login to TCP Server
        /// </summary>
        /// <param name="_user"></param>
        /// <param name="_pass"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public new bool Login(string _user, string _pass, out string msg)
        {
            if (!base.IsConnected) { msg = "Disconnected"; return false; }
            try {
                 bool ret = base.Login(_user, _pass, out msg);
                if (msg.ToLower().Contains("login incorrect")==true || ret==false) return false;
                string str = msg;
                str = str.TrimEnd();
                str = str.Substring(str.Length - 1, 1);
                return ((str == "$") || (str == ">")) == true ? true : false;
            }
            catch (Exception ex) {
                msg = ex.Message.ToString();
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numb"></param>
        /// <returns></returns>
        public string get_WL_Counter_MAC(byte numb) {
            string str_rxdcast = "";

            if (base.IsConnected) {
                base.WriteLine("wlctl counters");
                Thread.Sleep(200);
                string read_WLcounter = base.Read();

                for (int i = 0; i < read_WLcounter.Split('\n').Length; i++) {
                    if (read_WLcounter.Split('\n')[i].Contains("pktengrxducast")) {
                        str_rxdcast = read_WLcounter.Split('\n')[i].Trim();
                    }
                }
                if (numb == 1) {
                    base.WriteLine("exit");
                    Thread.Sleep(200);
                }
            }
            return str_rxdcast;
        }
    }
}
