using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DUT;

namespace TESTER
{
    public class EXM : Telnet
    {

        public EXM(string _host, int _port) : base(_host, _port) { }

    }
}
