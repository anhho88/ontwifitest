using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI
{
    public class S80211b : S80211
    {
        public int Anten { get; set; }
        public override List<string> txCommandList { get; set; }
        public override List<string> rxCommandList { get; set; }

        public S80211b(int _bandWidth, int _channel, double _rate, int _power) : base(_bandWidth, _channel, _rate, _power)
        {
            //double _locRate = _rate == 5 ? 5.5 : _rate;
            txCommandList = new List<string>();
            
            //add one by one command into txCommandList
            txCommandList.Add("wl down");
            txCommandList.Add("wl mpc 0");
            txCommandList.Add("wl legacylink 1");
            txCommandList.Add("wl country ALL");
            txCommandList.Add("wl wsec 0");
            txCommandList.Add("wl scansuppress 1");
            txCommandList.Add("wl interference 0");
            txCommandList.Add("wl band auto");
            txCommandList.Add("wl spect 0");
            txCommandList.Add("wl ibss_gmode -1");
            txCommandList.Add("wl mimo_bw_cap 1");
            txCommandList.Add("wl ampdu 0");
            txCommandList.Add("wl gmode auto");
            txCommandList.Add("wl up");
            txCommandList.Add("wl PM 0");
            txCommandList.Add("wl down");
            txCommandList.Add("wl phy_watchdog 0");
            txCommandList.Add("wl band b");
            txCommandList.Add("wl mimo_txbw 2");
            txCommandList.Add("wl txant 3");
            txCommandList.Add(string.Format("wl chanspec -c {0} -b 2 -w 20 -s 0", _channel));
            txCommandList.Add(string.Format("wl nrate -r {0}", _rate));
            txCommandList.Add(string.Format("wl rateset {0}b", _rate));
            txCommandList.Add("wl up");
            txCommandList.Add(string.Format("wl txpwr1 -o -q {0}", _power));
            txCommandList.Add("wl join BCMTest11G imode adhoc");
            txCommandList.Add("wl pkteng_start 00:90:4C:21:00:8e tx 100 1024 0");
        }

        public S80211b(int _channel, double _rate, int _anten) : base(_channel, _rate) {

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
            rxCommandList.Add("wlctl mimo_tx_bw 2");
            rxCommandList.Add(string.Format("wlctl chanspec {0}", _channel));
            rxCommandList.Add("wlctl up");
            rxCommandList.Add(string.Format("wlctl nrate -r {0}", _rate));
            rxCommandList.Add("wlctl down");
            rxCommandList.Add(string.Format("wlctl rxchain {0}", _anten));
            rxCommandList.Add("wlctl up");
            rxCommandList.Add("wlctl ssid \"\"");
            rxCommandList.Add("wlctl pkteng_start 00:11:22:33:44:55 rx");
        }

    }
}
