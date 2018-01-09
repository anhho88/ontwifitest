using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI
{
    public class S80211g : S80211
    {
        public int Anten { get; set; }
        public override List<string> commandList { get; set; }

        public S80211g(int _bandWidth, int _channel, int _rate, int _power, int _anten) : base(_bandWidth, _channel, _rate, _power)
        {
            this.Anten = _anten;
            commandList = new List<string>();

            //add one by one command into commandlist
            //commandList.Add("sh");
            commandList.Add("wl down");
            commandList.Add("wl mpc 0");
            commandList.Add("wl legacylink 1");
            commandList.Add("wl country ALL");
            commandList.Add("wl wsec 0");
            commandList.Add("wl scansuppress 1");
            commandList.Add("wl interference 0");
            commandList.Add("wl band auto");
            commandList.Add("wl spect 0");
            commandList.Add("wl ibss_gmode -1");
            commandList.Add("wl mimo_bw_cap 1");
            commandList.Add("wl ampdu 0");
            commandList.Add("wl gmode auto");
            commandList.Add("wl up");
            commandList.Add("wl PM 0");
            commandList.Add("wl down");
            commandList.Add("wl phy_watchdog 0");
            commandList.Add("wl band b");
            commandList.Add("wl mimo_txbw 2");
            commandList.Add("wl txant 3");
            commandList.Add(string.Format("wl chanspec -c {0} -b 2 -w 20 -s 0", _channel));
            commandList.Add(string.Format("wl nrate -r {0}", _rate));
            commandList.Add(string.Format("wl rateset {0}b", _rate));
            commandList.Add("wl up");
            commandList.Add("wl txant 0");
            commandList.Add(string.Format("wl txchain {0}", this.Anten));
            commandList.Add(string.Format("wl txpwr1 -o -q {0}", _power));
            commandList.Add("wl join BCMTest11G imode adhoc");
            commandList.Add("wl pkteng_start 00:90:4C:21:00:8e tx 100 1024 0");
            //commandList.Add("exit");
        }
    }
}
