using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI
{
    public class S80211n : S80211
    {
        public int Anten { get; set; }
        public override List<string> commandList { get; set; }

        public S80211n(int _bandWidth, int _channel, int _rate, int _power, int _anten) : base(_bandWidth, _channel, _rate, _power)
        {
            this.Anten = _anten;
            string str = "";
            commandList = new List<string>();

            //add one by one command into commandlist
            //commandList.Add("sh");
            if (_bandWidth == 4) commandList.Add("wl down");
            commandList.Add("wl pkteng_stop rx");
            commandList.Add("wl pkteng_stop tx");
            commandList.Add("wl stbc_rx 0");
            commandList.Add("wl mpc 0");
            commandList.Add("wl interference_override 0");
            commandList.Add("wl ssid \"\"");
            commandList.Add("wl down");
            if (_bandWidth == 2) commandList.Add("wl country ALL");
            commandList.Add("wl frameburst 1");
            commandList.Add("wl ampdu 1");
            commandList.Add("wl mimo_bw_cap 1");
            commandList.Add("wl bi 65535");
            commandList.Add("wl tempsense_disable 1");
            commandList.Add("wl up");
            commandList.Add("wl down");
            commandList.Add(string.Format("wl mimo_txbw {0}", _bandWidth));
            commandList.Add("wl band b");
            commandList.Add("wl up");
            commandList.Add("wl isup");
            commandList.Add("wl down");
            commandList.Add(str = bandWidth == 2 ? string.Format("wl chanspec {0}", _channel) : string.Format("wl chanspec {0}/40u", _channel));
            commandList.Add("wl up");
            if (_bandWidth==2) commandList.Add(string.Format("wl nrate -m {0} -s 0", _rate));
            else commandList.Add(string.Format("wl nrate -m {0} -b 2 -w 40 -s 0", _rate));
            commandList.Add("wl phy_watchdog 0");
            commandList.Add("wl txchain 3");
            commandList.Add("wl down");
            commandList.Add(string.Format("wl rxchain {0}", this.Anten));
            commandList.Add("wl up");
            commandList.Add(string.Format("wl txchain {0}", this.Anten));
            commandList.Add(string.Format("wl txpwr1 -o -q {0}", _power));
            commandList.Add("wl ssid txtest");
            commandList.Add("wl ssid txtest");
            commandList.Add("wl isup");
            commandList.Add("wl ssid \"\"");
            commandList.Add("wl pkteng_start aa:bb:cc:dd:ee:ff tx 50 1500 0");
            if (_bandWidth == 4) commandList.Add("wl up");
            //commandList.Add("exit");
        }
    }
}
