using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
