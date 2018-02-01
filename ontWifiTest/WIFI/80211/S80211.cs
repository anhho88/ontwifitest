using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WIFI
{
    ///// <summary>
    ///// List of bandwidths
    ///// </summary>
    //public static class bandWidths
    //{
    //    public enum bw11b { Twenty = 20 };
    //    public enum bw11g { Twenty = 20 };
    //    public enum bw11n { Twenty = 20, Forty = 40 };
    //}

    ///// <summary>
    ///// List of channels
    ///// </summary>
    //public static class channels
    //{
    //    public enum ch11b { One = 1, Six = 6, Eleven = 11 };
    //    public enum ch11g { One = 1, Six = 6, Eleven = 11 };
    //    public enum ch11nx20 { One = 1, Six = 6, Eleven = 11 };
    //    public enum ch11nx40 { Three = 3, Six = 6, Eleven = 11 };
    //}

    ///// <summary>
    ///// List of rates
    ///// </summary>
    //public static class rates
    //{
    //    public enum r11b { One = 1, Two = 2, Five = 5, Eleven = 11 };
    //    public enum r11g { Six = 6, Nine = 9, Twelf = 12, Eighteen = 18, TwentyFour = 24, ThirtySix = 36, FortyEight = 48, FiftyFour = 54 };
    //    public enum r11n { Zero = 0, One = 1, Two = 2, Three = 3, Four = 4, Five = 5, Six = 6, Seven = 7 };
    //}

    //public enum Antens { One = 1, Two = 2 };

    //public enum Powers { SixtyEight = 68 };

    public abstract class S80211
    {
        public int bandWidth { get; set; }
        public int channel { get; set; }
        public double rate { get; set; }
        public int power { get; set; }

        public abstract List<string> txCommandList { get; set; }
        public abstract List<string> rxCommandList { get; set; }

        public S80211(int _bandWidth, int _channel, double _rate, int _power)
        {
            this.bandWidth = _bandWidth;
            this.channel = _channel;
            this.rate = _rate;
            this.power = _power;
        }

        public S80211(string _bandWidth,int _channel, double _rate) {
            this.channel = _channel;
            this.rate = _rate;
            this.bandWidth = int.Parse(_bandWidth);
        }
    }
}
