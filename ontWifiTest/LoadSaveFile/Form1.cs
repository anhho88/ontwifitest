using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DUT;
using WIFI;
using System.Threading;
using E6640A_VNPT;

namespace LoadSaveFile
{
    
    public partial class Form1 : Form
    {
        List<string> DUTsss = new List<string>() { "GW020", "GW040" };
        List<singleMode> listtest = new List<singleMode>();
        GW ontDevice = null;
        E6640A_VISA E6640AVISA;
        public Form1()
        {
            InitializeComponent();
            cbONT.DataSource = DUTsss;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            txConfig form = new txConfig();
            form.Show();
        }
        //
        private bool convertStringtoList(string data, ref List<string> list)
        {
            try
            {
                if (!data.Contains(",")) list.Add(data);
                else
                {
                    string[] buffer = data.Split(',');
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        list.Add(buffer[i]);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private bool convertInputData(string datainput, ref List<singleMode> list)
        {
            try
            {
                //transfer data input to variables
                string[] buffer = datainput.Split(';');
                string _wifi = buffer[0].Split('=')[1];
                string _bandwidth = "";
                if (_wifi.Contains("HT20")) _bandwidth = "2";
                else if (_wifi.Contains("HT40")) _bandwidth = "4";
                else _bandwidth = "2";

                string anten = buffer[1].Split('=')[1];
                string channel = buffer[2].Split('=')[1];
                string rate = buffer[3].Split('=')[1];
                string power = buffer[4].Split('=')[1];

                //transfer variables to ienumerator
                bool ret;
                List<string> antenlist = new List<string>();
                List<string> channellist = new List<string>();
                List<string> ratelist = new List<string>();
                List<string> powerlist = new List<string>();
                ret = convertStringtoList(anten, ref antenlist);
                ret = convertStringtoList(channel, ref channellist);
                ret = convertStringtoList(rate, ref ratelist);
                ret = convertStringtoList(power, ref powerlist);

                //transfer data from ienumerator to list
                foreach (var atitem in antenlist)
                {
                    foreach (var chitem in channellist)
                    {
                        foreach (var ritem in ratelist)
                        {
                            foreach (var pitem in powerlist)
                            {
                                singleMode single = new singleMode() { wifi = _wifi, bandwidth = _bandwidth, anten = atitem, channel = chitem, rate = ritem, power = pitem };
                                list.Add(single);
                            }
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        //
        /// <summary>
        ///     
        /// </summary>
        /// 
        void GenerateCommand(ref S80211 s)
        {
            switch (_wifi)
            {
                case "802.11b":
                    {
                        s = new S80211b(int.Parse(_bandwith), int.Parse(_channel), (int)double.Parse(_rate), int.Parse(_power));
                        break;
                    }
                case "802.11g":
                    {
                        s = new S80211g(int.Parse(_bandwith), int.Parse(_channel), int.Parse(_rate), int.Parse(_power), int.Parse(_anten));
                        break;
                    }
                case "802.11nHT20":
                    {
                        s = new S80211n(2, int.Parse(_channel), int.Parse(_rate), int.Parse(_power), int.Parse(_anten));
                        break;
                    }
                case "802.11nHT40":
                    {
                        s = new S80211n(4, int.Parse(_channel), int.Parse(_rate), int.Parse(_power), int.Parse(_anten));
                        break;
                    }
                default: break;
            }
        }
        void Clear()
        {
            rtxbConsole.Clear();
        }
        void WriteCode(string _str)
        {
            rtxbConsole.AppendText(string.Format("{0}\n", _str));
        }
        void WriteLine(string _str)
        {
            rtxbConsole.AppendText(string.Format("{0}, {1}\n", DateTime.Now.ToString("HH:mm:ss ffff"), _str));
        }

        /// <summary>
        /// 
        /// </summary>
        string[] lines;
        string _wifi;
        string _bandwith;
        string _anten;
        string _channel;
        string _rate;
        string _power;
        string wifi1, channel1, range1, trigger, SUM;
        //int Channel(int channel)
        //{
        //    for (int i = 1; i <=11; i++)
        //    {
        //        if (channel == i)
        //        {
        //            channel1 = 2407 + 5*i;
        //        }
        //    }
        //    return channel1;
        //}       
        private void btnstart_Click(object sender, EventArgs e)
        {
            //Control.CheckForIllegalCrossThreadCalls = false;
            try
            {
                int i = 1;
                ListDataTest.Items.Clear();
                listtest.Clear();

                //
                foreach (var item in lines)
                {
                    //MessageBox.Show(item);
                    bool ret = convertInputData(item, ref listtest);
                    //MessageBox.Show();
                }
                int j = 0;
                foreach (var ditem in listtest)
                {
                    _wifi = ditem.wifi;
                    _bandwith = ditem.bandwidth;
                    _anten = ditem.anten;
                    _channel = ditem.channel;
                    _rate = ditem.rate;
                    _power = ditem.power;
                    ListDataTest.Items.Add("wifi=" + _wifi + ";anten=" + _anten + ";Channel=" + _channel + ";rate=" + _rate + ";Power=" + _power + "\n");
                    lbdisplay.Text = "--";
                    //Check data inputed is ok/ng
                    //if (!InputedDataValid()) return;
                    //Generate command
                    S80211 wifiUnit = null;
                    GenerateCommand(ref wifiUnit);
                    //Write command into DUT
                    Clear();
                    string msg = "";
                    MethodInvoker invoker_Ok = delegate
                    {
                        WriteCode(msg);
                        lbdisplay.Text = "OK";
                    };
                    MethodInvoker invoker_Ng = delegate
                    {
                        lbdisplay.Text = "NG";
                        WriteCode("FAIL.");
                    };

                    //Thread t = new Thread(() =>
                    //{
                    #region wifi
                    if (_wifi == "802.11nHT20")
                    {
                        wifi1 = "N20";
                    }
                    if (_wifi == "802.11nHT40")
                    {
                        wifi1 = "N40";
                    }
                    if (_wifi == "802.11b")
                    {
                        wifi1 = "BG";
                    }
                    if (_wifi == "802.11g")
                    {
                        wifi1 = "GDO";
                    }
                    #endregion
                    #region Channel
                   
                    if (_channel == "1")
                    {
                        channel1 = "2412";
                    }
                    if (_channel == "2")
                    {
                        channel1 = "2417";
                    }
                    if (_channel == "3")
                    {
                        channel1 = "2422";
                    }
                    if (_channel == "4")
                    {
                        channel1 = "2427";
                    }
                    if (_channel == "5")
                    {
                        channel1 = "2432";
                    }
                    if (_channel == "6")
                    {
                        channel1 = "2437";
                    }
                    if (_channel == "7")
                    {
                        channel1 = "2442";
                    }
                    if (_channel == "8")
                    {
                        channel1 = "2447";
                    }
                    if (_channel == "9")
                    {
                        channel1 = "2452";
                    }
                    if (_channel == "10")
                    {
                        channel1 = "2457";
                    }
                    if (_channel == "11")
                    {
                        channel1 = "2462";
                    }
                    #endregion
                    if (wifiUnit.txCommandList.Count > 0)
                        {
                            ontDevice.sendListCommand(wifiUnit.txCommandList, out msg);
                            this.Invoke(invoker_Ok);
                            Application.DoEvents();
                            
                        }
                        else
                        {
                            this.Invoke(invoker_Ng);
                        }
                    //});
                    //t.Start();
                    E6640AVISA = new E6640A_VISA("TCPIP0::192.168.1.2::inst0::INSTR");

                    E6640AVISA.config_HT20_RxTest_Transmitter(channel1, wifi1, "25", "RFB");
                    if (_wifi == "802.11nHT20" || _wifi == "802.11nHT40" || _wifi == "802.11g")
                    {
                        SUM = E6640AVISA.HienThi();
                        string[] array = SUM.Split(',');
                        string Power = Convert.ToDouble(array[19]).ToString("00.00");
                        string Ferqerror = Convert.ToDouble(array[7]).ToString("00.00");
                        string sym = Convert.ToDouble(array[11]).ToString("00.00");
                        string EVM = Convert.ToDouble(array[1]).ToString("00.00");
                        List<string> lData = new List<string>();
                        lData.Add(Power);
                        lData.Add(Ferqerror);
                        lData.Add(sym);
                        lData.Add(EVM);
                        dgv.DataSource = lData;
                        //string[] array1 = { Power, Ferqerror, sym, EVM + "\n" };
                        //dgv.DataSource = array1;
                        ////dgv.ColumnCount = 4;
                        //dgv.Rows.Add();
                        ////dgv.Rows[j].Cells[0].Value = j;
                        //dgv.Rows[j].Cells[1].Value = Power;
                        //dgv.Rows[j].Cells[2].Value = Ferqerror;
                        //dgv.Rows[j].Cells[3].Value = sym;
                        //dgv.Rows[j].Cells[4].Value = EVM;
                        j++;
                        ListDataTest.Items.Add("Power: " + Convert.ToDouble(array[19]).ToString("00.00") + ";EVM: " + Convert.ToDouble(array[1]).ToString("00.00"));
                        //ListDataTest.Items.Add(SUM + "\n");
                    }
                    else if (_wifi == "802.11b")
                    {
                        SUM = E6640AVISA.HienThi();
                        string[] array = SUM.Split(',');
                        string Power = Convert.ToDouble(array[35]).ToString("00.00");
                        string Ferqerror = Convert.ToDouble(array[7]).ToString("00.00");
                        string chip = Convert.ToDouble(array[11]).ToString("00.00");
                        string EVM = Convert.ToDouble(array[1]).ToString("00.00") + "%";

                        List<string> lData = new List<string>();
                        lData.Add(Power);
                        lData.Add(Ferqerror);
                        lData.Add(chip);
                        lData.Add(EVM);
                        dgv.DataSource = lData;
                        //string[] array1 = { Power, Ferqerror, sym, EVM + "\n" };
                        //dgv.ColumnCount = 4;
                        //dgv.Rows.Add();
                        ////dgv.Rows[j].Cells[0].Value = j;
                        //dgv.Rows[j].Cells[1].Value = Power;
                        //dgv.Rows[j].Cells[2].Value = Ferqerror;
                        //dgv.Rows[j].Cells[3].Value = sym;
                        //dgv.Rows[j].Cells[4].Value = EVM;
                        j++;
                        //dgv.DataSource = array1;
                        ListDataTest.Items.Add("Power: " + Power + ";EVM: " +  EVM + ";Fer Error: " + Ferqerror + ";Chip Clock Error: "+ chip + "\n");
                    }


                    for (int k = 0; k < 50; k++)
                    {
                        Thread.Sleep(100);
                        Application.DoEvents();
                    }
                    ListDataTest.Items.Add("Hoan thanh bai thu " + i);
                    i++;

                    //while (t.IsAlive) ;
                }
                //dgv.DataSource = listtest;

                //dgv.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //throw;
            }

        }


        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "txt file|*.txt";
            op.ShowDialog();
            try
            {
                lines = File.ReadAllLines(op.FileName);
                //ListData.Items.AddRange(line);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //throw;
            }
        }
        private void btnlogin_Click(object sender, EventArgs e)
        {
            string msg = ""; bool result = false;
            this.Clear();
            //Initial DUT
            switch (cbONT.Text.ToUpper().Trim())
            {
                case "GW020": { ontDevice = new GW020("192.168.1.1", 23); break; }
                case "GW040": { ontDevice = new GW040("192.168.1.1", 23); break; }
                default: { ontDevice = null; break; }
            }
            if (ontDevice == null) { MessageBox.Show("DUT's Name is not valid!\nPlease check again.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            //Connect to DUT
            result = ontDevice.Connection();
            WriteLine(string.Format("Connecting to {0}(host={1},port={2}) = {3}", cbONT.Text, ontDevice.host, ontDevice.port, result));
            ////Login to DUT
            result = ontDevice.Login("admin", "ttcn@77CN", out msg);
            WriteLine(string.Format("Login = {0}", result));
            WriteLine(msg);
            //cbChuan.DataSource = WifiTypesss;
            //cbChuan.Text = "";
            //ClearCbbValue();
            //grbDUT.Enabled = false;
            //grbSettings.Enabled = true;
            ontDevice.WriteLine("sh"); //open Busybox to write command
            msg = ontDevice.Read();
            WriteLine(msg);
            if (result)
            {
                //cbChuan.DataSource = WifiTypesss;
                //cbChuan.Text = "";
                //ClearCbbValue();
                //grbDUT.Enabled = false;
                //grbSettings.Enabled = true;
                ontDevice.WriteLine("sh"); //open Busybox to write command
                msg = ontDevice.Read();
                WriteLine(msg);
            }
            else MessageBox.Show("Not Connet to ONT!!!");

        }
        public class singleMode
        {
            public string wifi { get; set; }
            public string bandwidth { get; set; }
            public string anten { get; set; }
            public string channel { get; set; }
            public string rate { get; set; }
            public string power { get; set; }
        }


    }
}
