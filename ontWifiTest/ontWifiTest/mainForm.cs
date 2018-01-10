using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using DUT;
using TESTER;
using WIFI;
using System.IO;
using System.Diagnostics;

namespace ontWifiTest {

    #region MAINFORM
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    public partial class mainForm : Form {

        #region User Interface

        //KHAI BÁO BIẾN TOÀN CỤC
        //---------------------------------//
        GW ontDevice = null;
        EXM6640A Instrument = null;


        List<dataFields> txListTestCase = new List<dataFields>();
        string[] dataLines;
        Stopwatch stTimeCount = null;
        Thread threadTimeCount = null;
        bool _flag = false;
        int testCount = 0;

        List<txGridDataRow> txGridDataContext = new List<txGridDataRow>();
        List<rxGridDataRow> rxGridDataContext = new List<rxGridDataRow>();

        List<TextBox> listControls = null;
        List<TextBox> defaultSettings {
            get {
                List<TextBox> list = new List<TextBox>() { txtInstrAddress, txtPackets, txtWaitSent, txtDUTAddr, txtDUTUser, txtDUTPassword };
                var Settings = Properties.Settings.Default;
                txtInstrAddress.Text = Settings.Instrument;
                txtPackets.Text = Settings.Packets;
                txtWaitSent.Text = Settings.WaitSent;
                txtDUTAddr.Text = Settings.DUT;
                txtDUTUser.Text = Settings.User;
                txtDUTPassword.Text = Settings.Password;
                return list;
            }
            set {
                var Settings = Properties.Settings.Default;
                Settings.Instrument = value[0].Text;
                Settings.Packets = value[1].Text;
                Settings.WaitSent = value[2].Text;
                Settings.DUT = value[3].Text;
                Settings.User = value[4].Text;
                Settings.Password = value[5].Text;
                Settings.Save();
            }
        }
        /// <summary>
        /// INIT CONTROL DATACONTENT
        /// </summary>
        bool initialControlContext {
            set {
                lblProgress.Text = "0 / 0";
                lblTimeElapsed.Text = "00:00:00";
                lblStatus.Text = "Ready!";
                lblProjectName.Text = ProductName.ToString();
                lblProjectVer.Text = string.Format("Verion: {0}", ProductVersion);
                rtbDetails.Clear();
                progressBarTotal.Value = 0;
                dgTXGrid.DataSource = null;
                dgTXGrid.DataSource = txGridDataContext;
                dgRXGrid.DataSource = null;
                dgRXGrid.DataSource = rxGridDataContext;
                testCount = 0;
            }
        }

        //KHỞI TẠO, SAVE SETTING
        //---------------------------------//
        /// <summary>
        ///INITIAL FORM
        /// </summary>
        public mainForm() {
            InitializeComponent();
            listControls = new List<TextBox>() { txtInstrAddress, txtPackets, txtWaitSent, txtDUTAddr, txtDUTUser, txtDUTPassword };
        }

        /// <summary>
        /// LOAD FORM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainForm_Load(object sender, EventArgs e) {
            listControls = defaultSettings;
            initialControlContext = true;
            btnStart.Focus();
        }

        /// <summary>
        /// EXIT APPLICATION
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ckChangeSettings_CheckedChanged(object sender, EventArgs e) {
            if (ckChangeSettings.Checked == true) {
                foreach (var item in listControls) { //Enable textboxs for changing content
                    item.Enabled = true;
                }
            }
            else {
                foreach (var item in listControls) { //Disable textboxs for saving content
                    item.Enabled = false;
                }
                defaultSettings = listControls;
            }
        }

        private void tXToolStripMenuItem_Click(object sender, EventArgs e) {
            LoadSaveFile.txConfig tfrm = new LoadSaveFile.txConfig();
            tfrm.ShowDialog();
        }

        private void exitToolStripMenuItem_Click_1(object sender, EventArgs e) {
            this.Close();
        }

        /// <summary>
        /// Load file test case vao phan mem
        /// ten file test case co ki tu :'TX_', 'RX_' la hop le
        /// Neu ko se bao loi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadTestCaseToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "file *.txt|*.txt";
            if (op.ShowDialog() == DialogResult.OK) {
                string tmpStr = op.FileName;
                if ((!tmpStr.ToUpper().Contains("RX_")) && (!tmpStr.ToUpper().Contains("TX_"))) {
                    MessageBox.Show("File test case không hợp lệ.\nTên file phải bắt đầu bằng kí tự 'TX_' hoặc 'RX_'.\n----------------------------------------------\nVui lòng kiểm tra lại.","ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblType.Text = "--";
                    lbltestcasefilePath.Text = "--";
                    return;
                }
                lbltestcasefilePath.Text = tmpStr;
                lblType.Text = tmpStr.ToUpper().Contains("TX") == true ? "TX" : "RX";
                selectTabPage(lblType.Text);
            }
        }

        #endregion

        #region Main Program

        #region WriteDebug 

        void debugWrite(string data) {
            rtbDetails.AppendText(data);
            rtbDetails.ScrollToCaret();
        }

        void debugWriteLine(params string[] data) {
            if (data.Length > 1) {
                debugWrite(string.Format("{0}, {1}\n", DateTime.Now.ToString("HH:mm:ss ffff"), data[1]));
            }
            else {
                debugWrite(string.Format("{0}\n", data));
            }
        }
        #endregion

        #region Connect
        //Connect to ONT
        bool connectONT() {
            int errCode = 0;
            List<string> errContents = new List<string>() {
                "Không thể mở luồng telnet tới DUT.", //errCode = 0
                "Không thể đăng nhập được vào DUT.", //errCode = 1
            };
            try {
                bool result;
                string message;
                var Settings = Properties.Settings.Default;
                debugWriteLine("> Thiết lập kết nối tới DUT...");
                ontDevice = new GW020(Settings.DUT, 23);
                result = ontDevice.Connection();
                if (!result) { errCode = 0; goto NG; }
                result = ontDevice.Login(Settings.User, Settings.Password, out message);
                if (!result) { errCode = 1; goto NG; }
                ontDevice.WriteLine("sh");
                message += ontDevice.Read();
                goto OK;
            }
            catch (Exception ex) { errCode = 2; errContents.Add(ex.ToString()); goto NG; }
            OK:
            debugWriteLine("# Thành công!");
            return true;
            NG:
            debugWriteLine("# Thất bại!");
            debugWriteLine(errContents[errCode]);
            return false;
        }

        //Connect to Instrument
        bool connectInstrument() {
            int errCode = 0;
            List<string> errContents = new List<string>();
            try {
                var Settings = Properties.Settings.Default;
                debugWriteLine("> Thiết lập kết nối tới máy đo...");
                Instrument = new EXM6640A(Settings.Instrument);
                string msg = "";
                bool ret = Instrument.Connection(ref msg);
                if (ret) goto OK;
                else { errCode = 0; errContents.Add(msg); goto NG; }
            }
            catch(Exception ex) { errCode = 1; errContents.Add(ex.ToString()); goto NG; }
            OK:
            debugWriteLine("# Thành công!");
            return true;
            NG:
            debugWriteLine("# Thất bại!");
            debugWriteLine(errContents[errCode]);
            return false;
        }
        #endregion

        #region Load_TX_TestCase

        private bool convertStringtoList(string data, ref List<string> list) {
            try {
                if (!data.Contains(",")) list.Add(data);
                else {
                    string[] buffer = data.Split(',');
                    for (int i = 0; i < buffer.Length; i++) {
                        list.Add(buffer[i]);
                    }
                }
                return true;
            }
            catch {
                return false;
            }
        }

        private bool convertInputData(string datainput, ref List<dataFields> list) {
            try {
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
                foreach (var atitem in antenlist) {
                    foreach (var chitem in channellist) {
                        foreach (var ritem in ratelist) {
                            foreach (var pitem in powerlist) {
                                dataFields data = new dataFields() { wifi = _wifi, bandwidth = _bandwidth, anten = atitem, channel = chitem, rate = ritem, power = pitem };
                                list.Add(data);
                            }
                        }
                    }
                }
                return true;
            }
            catch {
                return false;
            }
        }

        bool load_AllTXTestCase() {
            int errCode = 0;
            List<string> errContents = new List<string>() {
                "Định dạng file test case không hợp lệ.", //errCode = 0
                "Chưa load file test case vào phần mềm.", //errCode = 1
            };
            try {
                debugWriteLine("> Tải dữ liệu từ test case file vào phần mềm...");
                dataLines = null;
                dataLines = File.ReadAllLines(lbltestcasefilePath.Text);
                if (dataLines.Length > 0) {
                    bool ret = true;
                    foreach (var item in dataLines) {
                        bool result = convertInputData(item, ref txListTestCase);
                        if (!result) ret = false;
                    }
                    if (!ret) { errCode = 0; goto NG; }
                    else goto OK;
                } else { errCode = 1; goto NG; }
            }
            catch(Exception ex) { errCode = 2; errContents.Add(ex.ToString()); goto NG;}
            OK:
            debugWriteLine("# Thành công!");
            return true;
            NG:
            debugWriteLine("# Thất bại!");
            debugWriteLine(errContents[errCode]);
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tabtitle">TX,RX</param>
        private bool selectTabPage(string tabtitle) {
            switch (tabtitle) {
                case "TX": {
                        tabControl1.SelectedTab = tabPage1;
                        break;
                    }
                case "RX": {
                        tabControl1.SelectedTab = tabPage2;
                        break;
                    }
                default: break;
            }
            return true;
        }

        #endregion

        #region SendCommandToDUT

        bool GenerateCommand(ref S80211 s, dataFields data) {
            try {
                switch (data.wifi) {
                    case "802.11b": {
                            s = new S80211b(int.Parse(data.bandwidth), int.Parse(data.channel), (int)double.Parse(data.rate), int.Parse(data.power));
                            break;
                        }
                    case "802.11g": {
                            s = new S80211g(int.Parse(data.bandwidth), int.Parse(data.channel), int.Parse(data.rate), int.Parse(data.power), int.Parse(data.anten));
                            break;
                        }
                    case "802.11nHT20": {
                            s = new S80211n(2, int.Parse(data.channel), int.Parse(data.rate), int.Parse(data.power), int.Parse(data.anten));
                            break;
                        }
                    case "802.11nHT40": {
                            s = new S80211n(4, int.Parse(data.channel), int.Parse(data.rate), int.Parse(data.power), int.Parse(data.anten));
                            break;
                        }
                    default: return false;
                }
                return true;
            } catch {
                return false;
            }
        }

        bool sendCommandToDUT(dataFields data) {
            int errCode = 0;
            List<string> errContents = new List<string>() {
                "Không thể tạo list command cho DUT.", //errCode = 0
                "List command của DUT = 0.", //errCode = 1
            };
            try {
                debugWriteLine("> Điều khiển DUT phát wifi...");
                S80211 wifiUnit = null;
                string msg;
                if (!GenerateCommand(ref wifiUnit, data)) { errCode = 0; goto NG; }
                if (wifiUnit.commandList.Count > 0) {
                    ontDevice.sendListCommand(wifiUnit.commandList, out msg);
                    debugWriteLine(msg);
                }
                else { errCode = 1; goto NG; }
                goto OK;
            } catch (Exception ex) { errCode = 2; errContents.Add(ex.ToString()); goto NG; }
            OK:
            debugWriteLine("# Thành công!");
            return true;
            NG:
            debugWriteLine("# Thất bại!");
            debugWriteLine(errContents[errCode]);
            return false;
        }

        #endregion

        #region SendCommandToInstrument

        private bool convertSettingDUTtoInstrument(dataFields dataIn, ref dataFields dataOut) {
            try {
                //Wifi
                switch (dataIn.wifi) {
                    case "802.11nHT20": { dataOut.wifi = "N20"; break; }
                    case "802.11nHT40": { dataOut.wifi = "N40"; break; }
                    case "802.11b": { dataOut.wifi = "BG"; break; }
                    case "802.11g": { dataOut.wifi = "GDO"; break; }
                    default: return false;
                }
                //Channel
                dataOut.channel = ((int.Parse(dataIn.channel) * 5) + 2407).ToString();
                return true;
            } catch {
                return false;
            }
        }

        private bool sendCommandToInstrument(dataFields data) {
            int errCode = 0;
            List<string> errContents = new List<string>() {
                "Không thể chuyển đổi dữ liệu wifi, channel từ DUT sang Instrument.", //errCode = 0
            };
            try {
                debugWriteLine("> Cấu hình máy đo EXM6640A...");
                dataFields df = new dataFields();
                if (!convertSettingDUTtoInstrument(data, ref df)) { errCode = 0; goto NG; }
                Instrument.config_HT20_RxTest_Transmitter(df.channel, df.wifi, "25", "RFB");
                goto OK;
            } catch (Exception ex) { errCode = 1; errContents.Add(ex.ToString()); goto NG; }
            OK:
            debugWriteLine("# Thành công!");
            return true;
            NG:
            debugWriteLine("# Thất bại!");
            debugWriteLine(errContents[errCode]);
            return false;
        }

        #endregion

        #region GetResult

        bool getResult(dataFields dataIn,ref dataMeasures dataOut) {
            try {
                string result = Instrument.HienThi();
                string[] buffer = result.Split(',');
                dataOut.power = Convert.ToDouble(buffer[dataIn.wifi == "802.11b" ? 35 : 19]).ToString("00.00");
                dataOut.freqErr = Convert.ToDouble(buffer[7]).ToString("00.00");
                dataOut.sym = Convert.ToDouble(buffer[11]).ToString("00.00");
                dataOut.evm = Convert.ToDouble(buffer[1]).ToString("00.00");
                return true;
            } catch {
                return false;
            }
        }

        #endregion

        void delay(int miliseconds, int step) {
            int count = (int)(miliseconds / step);
            for (int i = 0; i < count; i++) {
                Thread.Sleep(step);
                Application.DoEvents();
            }
        }

        string convertTime(int time) {
            if (time < 10) return string.Format("0{0}", time);
            else return time.ToString();
        }

        void startCalculateElapsedTime() {
            _flag = false;
            stTimeCount = new Stopwatch();
            stTimeCount.Start();
            MethodInvoker invoker_Ok = delegate {
                int seconds =(int)stTimeCount.ElapsedMilliseconds / 1000;
                int h = seconds / 3600;
                int m = (seconds -  (h * 3600)) / 60;
                int s = seconds - (h * 3600) - (m * 60);
                lblTimeElapsed.Text = string.Format("{0}:{1}:{2}", convertTime(h), convertTime(m), convertTime(s));
            };
            threadTimeCount = new Thread(new ThreadStart(() => {
                while (!_flag) {
                    this.Invoke(invoker_Ok);
                    Thread.Sleep(100);
                }
            }));
            threadTimeCount.IsBackground = true;
            threadTimeCount.Start();
        }

        void stopCalculateElapsedTime() {
            try {
                _flag = true;
                threadTimeCount.Join(1);
                threadTimeCount.Abort();
            } catch { }
        }

        void startProgress(int total) {
            lblProgress.Text = string.Format("{0} / {1}", 0, total);
            progressBarTotal.Maximum = total;
            progressBarTotal.Minimum = 0;
            progressBarTotal.Value = 0;
        }

        void updateProgress(int value, int total) {
            lblProgress.Text = string.Format("{0} / {1}", value, total);
            progressBarTotal.Value = value;
            Application.DoEvents();
        }

        void InitControls() {
            //Count time
            startCalculateElapsedTime();
            btnStart.Enabled = false;
            btnStart.Text = "STOP";
            initialControlContext = true;
            debugWriteLine("Starting...\n");
            Application.DoEvents();
        }

        void FinishControls() {
            stopCalculateElapsedTime();
            debugWriteLine("\n...\nThe End >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            btnStart.Enabled = true;
            btnStart.Text = "START";
            Application.DoEvents();
        }

        private void btnStart_Click(object sender, EventArgs e) {
            //Khoi tao controls
            InitControls();
            //Ket noi toi ONT
            if (!connectONT()) goto END;
            //Ket noi toi Instrument
            if (!connectInstrument()) goto END;
            //Load settings vao List....
            if (!load_AllTXTestCase()) goto END;
            //Select tab
            selectTabPage(lblType.Text);
            //start show progress
            int totaltestCount = txListTestCase.Count;
            testCount = 0;
            startProgress(totaltestCount);
            //Start LOOP
            foreach (var data in txListTestCase) {
                //Update progress
                testCount++;
                updateProgress(testCount, totaltestCount);
                //Send ONT command
                if (!sendCommandToDUT(data)) goto END;
                //Wait ....
                delay(200, 20);
                //Send Instrument command
                if (!sendCommandToInstrument(data)) goto END;
                //Wait ....
                delay(200, 20);
                //Get result
                dataMeasures dm = new dataMeasures();
                getResult(data, ref dm);
                debugWriteLine(string.Format("> Result: {0}", dm.ToString()));
                delay(3000, 100);
            }
            ////Send ONT command
            //if (!sendCommandToDUT(txListTestCase[0])) goto END;
            ////Wait ....
            //delay(200, 20);
            ////Send Instrument command
            //if (!sendCommandToInstrument(txListTestCase[0])) goto END;
            ////Wait ....
            //delay(200, 20);
            ////Get result
            //dataMeasures dm = new dataMeasures();
            //getResult(txListTestCase[0], ref dm);
            //debugWriteLine(string.Format("> Result: {0}", dm.ToString()));
            //
            END: FinishControls();
        }

        #endregion
    }
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    #endregion

    #region CUSTOM USER
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

    public class dataMeasures {

        public string power { get; set; }
        public string freqErr { get; set; }
        public string sym { get; set; }
        public string evm { get; set; }

        public override string ToString() {
            return string.Format("\npower={0},\nfreqErr={1},\nSYM={2},\nEVM={3}", power, freqErr, sym, evm);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class dataFields {

        public string wifi { get; set; }
        public string bandwidth { get; set; }
        public string anten { get; set; }
        public string channel { get; set; }
        public string rate { get; set; }
        public string power { get; set; }
    }

    /// <summary>
    /// CLASS ĐỊNH NGHĨA KIỂU DỮ LIỆU CỦA CHO TXGRID
    /// </summary>
    public class txGridDataRow {
        private int _anten;
        private double _evm;
        private double _pwr;
        private double _freqerr;
        private double _symclock;
        private int _bandwidth;

        //public int Order { get; set; }
        public string Wifi { get; set; }
        public string ANT {
            get { return string.Format("ANT {0}", _anten); }
            set { _anten = int.Parse(value); }
        }
        public string Bandwidth {
            get { return string.Format("{0}", _bandwidth * 10); }
            set { _bandwidth = int.Parse(value); }
        }
        public int Channel { get; set; }
        public int Freq { get; set; }
        public double Rate { get; set; }
        //public double Power { get; set; }

        public string PL_Limit { get; set; }
        public string Pwr {
            get { return string.Format("{0} dBm", _pwr); }
            set { _pwr = double.Parse(value); }
        }
        public string PU_Limit { get; set; }

        public string FEL_Limit { get; set; }
        public string FreqErr {
            get { return string.Format("{0} kHz", _freqerr); }
            set { _freqerr = double.Parse(value); }
        }
        public string FEU_Limit { get; set; }


        public string SymClock {
            get { return string.Format("{0} ppm", _symclock); }
            set { _symclock = double.Parse(value); }
        }
        public string SCU_Limit { get; set; }

        public string EVM {
            get { return string.Format("{0} {1}", _evm, this.Wifi.ToUpper().Contains("B") == true ? "%" : "dBm"); }
            set { _evm = double.Parse(value); }
        }
        public string EVMU_Limit { get; set; }
    }


    /// <summary>
    /// CLASS ĐỊNH NGHĨA KIỂU DỮ LIỆU CỦA CHO RXGRID
    /// </summary>
    public class rxGridDataRow {
        private int _anten;
        private double _pwr;

        public string Wifi { get; set; }
        public string ANT {
            get { return string.Format("ANT {0}", _anten); }
            set { _anten = int.Parse(value); }
        }
        public int Channel { get; set; }
        public double Rate { get; set; }
        public string Pwr {
            get { return string.Format("{0} dBm", _pwr); }
            set { _pwr = double.Parse(value); }
        }
        public int packetSent { get; set; }
        public int packetGet { get; set; }
        public string PER {
            get {
                return ((packetGet * 1.0) / (packetSent * 1.0)).ToString("00.00%");
            }
        }
    }
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    #endregion
}
