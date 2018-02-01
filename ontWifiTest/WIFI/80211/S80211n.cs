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
        public override List<string> txCommandList { get; set; }
        public override List<string> rxCommandList { get; set; }

        public S80211n(int _bandWidth, int _channel, double _rate, int _power, int _anten) : base(_bandWidth, _channel, _rate, _power)
        {
            this.Anten = _anten;
            string str = "";
            txCommandList = new List<string>();

            //add one by one command into commandlist
            if (_bandWidth == 4) txCommandList.Add("wl down");
            txCommandList.Add("wl pkteng_stop rx");
            txCommandList.Add("wl pkteng_stop tx");
            txCommandList.Add("wl stbc_rx 0");
            txCommandList.Add("wl mpc 0");
            txCommandList.Add("wl interference_override 0");
            txCommandList.Add("wl ssid \"\"");
            txCommandList.Add("wl down");
            if (_bandWidth == 2) txCommandList.Add("wl country ALL");
            txCommandList.Add("wl frameburst 1");
            txCommandList.Add("wl ampdu 1");
            txCommandList.Add("wl mimo_bw_cap 1");
            txCommandList.Add("wl bi 65535");
            txCommandList.Add("wl tempsense_disable 1");
            txCommandList.Add("wl up");
            txCommandList.Add("wl down");
            txCommandList.Add(string.Format("wl mimo_txbw {0}", _bandWidth));
            txCommandList.Add("wl band b");
            txCommandList.Add("wl up");
            txCommandList.Add("wl isup");
            txCommandList.Add("wl down");
            txCommandList.Add(str = bandWidth == 2 ? string.Format("wl chanspec {0}", _channel) : string.Format("wl chanspec {0}/40u", _channel));
            txCommandList.Add("wl up");
            if (_bandWidth==2) txCommandList.Add(string.Format("wl nrate -m {0} -s 0", _rate));
            else txCommandList.Add(string.Format("wl nrate -m {0} -b 2 -w 40 -s 0", _rate));
            txCommandList.Add("wl phy_watchdog 0");
            txCommandList.Add("wl txchain 3");
            txCommandList.Add("wl down");
            txCommandList.Add(string.Format("wl rxchain {0}", this.Anten));
            txCommandList.Add("wl up");
            txCommandList.Add(string.Format("wl txchain {0}", this.Anten));
            txCommandList.Add(string.Format("wl txpwr1 -o -q {0}", _power));
            txCommandList.Add("wl ssid txtest");
            txCommandList.Add("wl ssid txtest");
            txCommandList.Add("wl isup");
            txCommandList.Add("wl ssid \"\"");
            txCommandList.Add("wl pkteng_start aa:bb:cc:dd:ee:ff tx 50 1500 0");
            if (_bandWidth == 4) txCommandList.Add("wl up");
        }


        public S80211n(string _bandwidth,int _channel, double _rate, int _anten) : base(_bandwidth, _channel, _rate) {

            this.Anten = _anten;
            rxCommandList = new List<string>();
            //add one by one command into rxCommandList
            rxCommandList.Add("wlctl mpc 0");
            rxCommandList.Add("wlctl interference 0");
            rxCommandList.Add("wlctl phy_watchdog 0");
            rxCommandList.Add("wlctl scansuppress 1");
            rxCommandList.Add("wlctl down");
            rxCommandList.Add("wlctl country ALL");
            rxCommandList.Add("wlctl frameburst 0");
            rxCommandList.Add("wlctl ampdu 0");
            rxCommandList.Add("wlctl mimo_bw_cap 1");
            rxCommandList.Add("wlctl up");
            rxCommandList.Add("wlctl down");
            //rxCommandList.Add("wlctl mimo_tx_bw 2");
            rxCommandList.Add(string.Format("wlctl mimo_tx_bw {0}", _bandwidth));
            rxCommandList.Add(string.Format("wlctl chanspec {0}", _channel));
            rxCommandList.Add("wlctl up");
            rxCommandList.Add(string.Format("wlctl nrate -m {0} -s 0", _rate));
            rxCommandList.Add("wlctl down");
            rxCommandList.Add(string.Format("wlctl rxchain {0}", _anten));
            rxCommandList.Add("wlctl up");
            rxCommandList.Add("wlctl ssid \"\"");
            rxCommandList.Add("wlctl pkteng_start 00:11:22:33:44:55 rx");
        }
    }
}
