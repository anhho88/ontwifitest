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


        List<txdataFields> txListTestCase = new List<txdataFields>();
        List<rxdataFields> rxListTestCase = new List<rxdataFields>();
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

        Dictionary<double, string> waveFormN40 = new Dictionary<double, string>() {
            {0, "001122334455_H4M0.wfm"},
            {1, "001122334455_H4M1.wfm"},
            {2, "001122334455_H4M2.wfm"},
            {3, "001122334455_H4M3.wfm"},
            {4, "001122334455_H4M4.wfm"},
            {5, "001122334455_H4M5.wfm"},
            {6, "001122334455_H4M6.wfm"},
            {7, "001122334455_H4M7.wfm"}
        };

        Dictionary<double, string> waveFormN20 = new Dictionary<double, string>() {
            {0, "001122334455_H2M0.wfm"},
            {1, "001122334455_H2M1.wfm"},
            {2, "001122334455_H2M2.wfm"},
            {3, "001122334455_H2M3.wfm"},
            {4, "001122334455_H2M4.wfm"},
            {5, "001122334455_H2M5.wfm"},
            {6, "001122334455_H2M6.wfm"},
            {7, "001122334455_H2M7.wfm"}
        };

        Dictionary<double, string> waveFormG = new Dictionary<double, string>() {
            {6, "001122334455_6M.wfm"},
            {9, "001122334455_9M.wfm"},
            {12, "001122334455_12M.wfm"},
            {18, "001122334455_18M.wfm"},
            {24, "001122334455_24M.wfm"},
            {36, "001122334455_36M.wfm"},
            {48, "001122334455_48M.wfm"},
            {54, "001122334455_54M.wfm"}
        };

        Dictionary<double, string> waveFormB = new Dictionary<double, string>() {
            {1, "001122334455_1ML.wfm"},
            {2, "001122334455_2ML.wfm"},
            {5.5, "001122334455_5ML.wfm"},
            {11, "001122334455_11ML.wfm"}
        };

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
            initialControlContext = true;
            txGridDataContext.Clear();
            dgTXGrid.DataSource = null;
            rxGridDataContext.Clear();
            dgRXGrid.DataSource = null;
        }

        /// <summary>
        /// LOAD FORM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainForm_Load(object sender, EventArgs e) {
            listControls = defaultSettings;
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

        private void rXToolStripMenuItem_Click(object sender, EventArgs e) {
            LoadSaveFile.rxConfig rfrm = new LoadSaveFile.rxConfig();
            rfrm.ShowDialog();
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
                    MessageBox.Show("File test case không hợp lệ.\nTên file phải bắt đầu bằng kí tự 'TX_' hoặc 'RX_'.\n----------------------------------------------\nVui lòng kiểm tra lại.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblType.Text = "--";
                    lbltestcasefilePath.Text = "--";
                    return;
                }
                lbltestcasefilePath.Text = tmpStr;
                lblType.Text = tmpStr.ToUpper().Contains("TX_") == true ? "TX" : (tmpStr.ToUpper().Contains("RX_") == true ? "RX" : "--");
                selectTabPage(lblType.Text);
            }
        }


        private void btnExportExcel_Click(object sender, EventArgs e) {
            SaveFileDialog saveFile = new SaveFileDialog();
            string fileType = "";
            if (tabControl1.SelectedTab == tabPage1) fileType = "TX";
            else fileType = "RX";
            saveFile.Title = string.Format("Save As {0} log file", fileType);
            saveFile.FileName = fileType;
            saveFile.Filter = "*.csv|*.csv";
            if (saveFile.ShowDialog() == DialogResult.OK) {
                switch (fileType) {
                    case "TX": {
                            if (txGridDataContext.Count > 0) {
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.AppendLine("WifiStandard,Anten,Bandwidth,Channel,Frequency,Rate,Power_Lower_Limit,Power_Actual,Power_Upper_Limit,FreqErr_Lower_Limit,FreqErr_Actual,FreqErr_Upper_Limit,SymClock_Actual,SymClock_Upper_Limit,EVM_Actual,EVM_Upper_Limit");
                                foreach (var item in txGridDataContext) {
                                    sb.AppendLine(item.ToString());
                                }
                                System.IO.File.WriteAllText(string.Format("{0}", saveFile.FileName), sb.ToString());
                                MessageBox.Show("Lưu file OK.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Process.Start(string.Format("{0}", saveFile.FileName));
                            }
                            else MessageBox.Show("Không có dữ liệu để lưu.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case "RX": {
                            if (rxGridDataContext.Count > 0) {
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                                sb.AppendLine("WifiStandard,Anten,Channel,Rate,Power_Actual,PacketSent,PacketGet,PER");
                                foreach (var item in rxGridDataContext) {
                                    sb.AppendLine(item.ToString());
                                }
                                System.IO.File.WriteAllText(string.Format("{0}", saveFile.FileName), sb.ToString());
                                MessageBox.Show("Lưu file OK.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Process.Start(string.Format("{0}", saveFile.FileName));
                            }
                            else MessageBox.Show("Không có dữ liệu để lưu.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    default: break;
                }
                

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
            catch (Exception ex) { errCode = 1; errContents.Add(ex.ToString()); goto NG; }
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

        private bool txconvertInputData(string datainput, ref List<txdataFields> list) {
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
                                txdataFields data = new txdataFields() { wifi = _wifi, bandwidth = _bandwidth, anten = atitem, channel = chitem, rate = ritem, power = pitem };
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
                txListTestCase.Clear();
                dataLines = File.ReadAllLines(lbltestcasefilePath.Text);
                if (dataLines.Length > 0) {
                    bool ret = true;
                    foreach (var item in dataLines) {
                        bool result = txconvertInputData(item, ref txListTestCase);
                        if (!result) ret = false;
                    }
                    if (!ret) { errCode = 0; goto NG; }
                    else goto OK;
                }
                else { errCode = 1; goto NG; }
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

        #region Send TX Command To DUT

        bool txGenerateCommand(ref S80211 s, txdataFields data) {
            try {
                switch (data.wifi) {
                    case "802.11b": {
                            s = new S80211b(int.Parse(data.bandwidth), int.Parse(data.channel), double.Parse(data.rate), int.Parse(data.power));
                            break;
                        }
                    case "802.11g": {
                            s = new S80211g(int.Parse(data.bandwidth), int.Parse(data.channel), double.Parse(data.rate), int.Parse(data.power), int.Parse(data.anten));
                            break;
                        }
                    case "802.11nHT20": {
                            s = new S80211n(2, int.Parse(data.channel), double.Parse(data.rate), int.Parse(data.power), int.Parse(data.anten));
                            break;
                        }
                    case "802.11nHT40": {
                            s = new S80211n(4, int.Parse(data.channel), double.Parse(data.rate), int.Parse(data.power), int.Parse(data.anten));
                            break;
                        }
                    default: return false;
                }
                return true;
            }
            catch {
                return false;
            }
        }

        bool sendtxCommandToDUT(txdataFields data) {
            int errCode = 0;
            List<string> errContents = new List<string>() {
                "Không thể tạo list tx command cho DUT.", //errCode = 0
                "List tx command của DUT = 0.", //errCode = 1
            };
            try {
                debugWriteLine("> Điều khiển DUT phát wifi...");
                S80211 wifiUnit = null;
                string msg;
                if (!txGenerateCommand(ref wifiUnit, data)) { errCode = 0; goto NG; }
                if (wifiUnit.txCommandList.Count > 0) {
                    ontDevice.sendListCommand(wifiUnit.txCommandList, out msg);
                    debugWriteLine(msg);
                }
                else { errCode = 1; goto NG; }
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

        #endregion

        #region Send TX Command To Instrument

        private bool convertSettingDUTtoInstrument(txdataFields dataIn, ref txdataFields dataOut) {
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
                if (dataIn.wifi == "802.11nHT40") {
                    switch (dataIn.channel) {
                        case "5": { dataOut.channel = "2422"; break; }
                        case "8": { dataOut.channel = "2437"; break; }
                        case "13": { dataOut.channel = "2462"; break; }
                        default: break;
                    }
                }
                else {
                    dataOut.channel = ((int.Parse(dataIn.channel) * 5) + 2407).ToString();
                }
                return true;
            }
            catch {
                return false;
            }
        }

        private bool sendtxCommandToInstrument(txdataFields data) {
            int errCode = 0;
            List<string> errContents = new List<string>() {
                "Không thể chuyển đổi dữ liệu wifi, channel từ DUT sang Instrument.", //errCode = 0
            };
            try {
                debugWriteLine("> Cấu hình máy đo EXM6640A...");
                txdataFields df = new txdataFields();
                if (!convertSettingDUTtoInstrument(data, ref df)) { errCode = 0; goto NG; }
                Instrument.config_HT20_RxTest_Transmitter(df.channel, df.wifi, "25", "RFB");
                goto OK;
            }
            catch (Exception ex) { errCode = 1; errContents.Add(ex.ToString()); goto NG; }
            OK:
            debugWriteLine("# Thành công!");
            return true;
            NG:
            debugWriteLine("# Thất bại!");
            debugWriteLine(errContents[errCode]);
            return false;
        }

        #endregion

        #region Get TX Result

        string convertNR3ToDecimal(string nr3, int div) {
            string[] buffer = nr3.Split('E');
            double n = double.Parse(buffer[0]);
            double p = Math.Pow(10, double.Parse(buffer[1]));
            double result = n * p;
            return Math.Round(result / div, 2).ToString();
        }

        bool getResult(txdataFields dataIn, ref txdataMeasures dataOut) {
            try {
                string result = Instrument.HienThi();
                string[] buffer = result.Split(',');
                dataOut.power = convertNR3ToDecimal(buffer[dataIn.wifi == "802.11b" ? 35 : 19], 1);
                dataOut.freqErr = convertNR3ToDecimal(buffer[7], 1000);
                dataOut.sym = convertNR3ToDecimal(buffer[11], 1);
                dataOut.evm = convertNR3ToDecimal(buffer[1], 1);
                return true;
            }
            catch {
                return false;
            }
        }

        #endregion

        #region SubFunction

        void delay(int miliseconds, int step) {
            debugWriteLine(string.Format("Wait {0}ms ...", miliseconds));
            int count = (int)(miliseconds / step);
            for (int i = 0; i < count; i++) {
                Thread.Sleep(step);
            }
        }

        void DELAY(int seconds) {
            for (int i = 0; i < seconds; i++) {
                debugWriteLine(string.Format("Wait {0}ms ...", seconds - i));
                Thread.Sleep(1000);
            }
        }

        string convertTime(int time) {
            if (time < 10) return string.Format("0{0}", time);
            else return time.ToString();
        }

        string convertms(int time) {
            if (time < 10) return string.Format("00{0}", time);
            else if (time >=10 && time <100) return string.Format("0{0}", time);
            else return time.ToString();
        }

        void startCalculateElapsedTime() {
            _flag = false;
            stTimeCount = new Stopwatch();
            stTimeCount.Start();
            MethodInvoker invoker_Ok = delegate {
                int miliseconds = (int)stTimeCount.ElapsedMilliseconds;
                int seconds = miliseconds / 1000;
                int h = seconds / 3600;
                int m = (seconds - (h * 3600)) / 60;
                int s = seconds - (h * 3600) - (m * 60);
                lblTimeElapsed.Text = string.Format("{0}:{1}:{2}", convertTime(h), convertTime(m), convertTime(s));
                lblTimeElapsed.Refresh();
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
            }
            catch { }
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
            progressBarTotal.Refresh();
            lblProgress.Refresh();
        }

        void InitControls() {
            //Count time
            startCalculateElapsedTime();
            btnStart.Enabled = false;
            btnStart.Text = "STOP";
            initialControlContext = true;
            if (lblType.Text=="TX") {
                txGridDataContext.Clear();
                dgTXGrid.DataSource = null;
            } else {
                rxGridDataContext.Clear();
                dgRXGrid.DataSource = null;
            }
            debugWriteLine("Starting...\n");
            this.Refresh();
            lblStatus.Text = "testing...";
        }

        void FinishControls() {
            stopCalculateElapsedTime();
            debugWriteLine("\n...\nThe End >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            btnStart.Enabled = true;
            btnStart.Text = "START";
            lblStatus.Text = "completed!";
        }

        /// <summary>
        /// Day du lieu vao txGridDataContext
        /// </summary>
        /// <param name="data1"></param>
        /// <param name="data2"></param>
        void displaytxResultToGrid(txdataFields data1, txdataMeasures data2) {
            txGridDataRow data = new txGridDataRow();
            //
            data.Wifi = data1.wifi;
            data.ANT = data1.anten;
            data.Bandwidth = data1.bandwidth;
            data.Channel = int.Parse(data1.channel);
            if (data1.wifi == "802.11nHT40") {
                switch (data1.channel) {
                    case "5": { data.Freq = 2422; break; }
                    case "8": { data.Freq = 2437; break; }
                    case "13": { data.Freq = 2462; break; }
                    default: break;
                }
            }
            else data.Freq = (int.Parse(data1.channel) * 5) + 2407;

            data.Rate = double.Parse(data1.rate);
            data.Pwr = data2.power;
            data.FreqErr = data2.freqErr;
            data.SymClock = data2.sym;
            data.EVM = data2.evm;

            dgTXGrid.DataSource = null;
            txGridDataContext.Add(data);
            dgTXGrid.DataSource = txGridDataContext;
            dgTXGrid.ClearSelection();
            dgTXGrid.FirstDisplayedScrollingRowIndex = dgTXGrid.RowCount - 1;
            dgTXGrid.Rows[dgTXGrid.RowCount - 1].Selected = true;
            dgTXGrid.Refresh();
        }

        void displayrxResultToGrid(rxdataFields data1,ref rxdataMeasures data2) {
            rxGridDataRow data = new rxGridDataRow();
            //
            data.Wifi = data1.wifi;
            data.ANT = data1.anten;
            data.Channel = int.Parse(data1.channel);
            data.Rate = double.Parse(data1.rate);
            data.Pwr = data1.power;
            data.packetSent = int.Parse(data1.packetSent);
            data.packetGet = int.Parse(data2.packetGet);
            data2.PER = data.PER;

            dgRXGrid.DataSource = null;
            rxGridDataContext.Add(data);
            dgRXGrid.DataSource = rxGridDataContext;
            dgRXGrid.ClearSelection();
            dgRXGrid.FirstDisplayedScrollingRowIndex = dgRXGrid.RowCount - 1;
            dgRXGrid.Rows[dgRXGrid.RowCount - 1].Selected = true;
            dgRXGrid.Refresh();
        }

        bool rxGetWLCounter(ref double count, byte time) {
            try {
                string data = ontDevice.get_WL_Counter_MAC(time);
                if (!(data.Trim().Contains("pktengrxducast") && data.Trim().Contains("pktengrxdmcast"))) return false;
                int fistmmcast_index = data.IndexOf("pktengrxducast");
                int lastmmcast_index = data.IndexOf("pktengrxdmcast");
                string numb_Counter = data.Substring(fistmmcast_index + 15, lastmmcast_index - 16);
                count = double.Parse(numb_Counter);
                return true;
            } catch {
                return false;
            }
        }

        void txTest() {
            Thread t = new Thread(new ThreadStart(() => {
                this.Invoke(new MethodInvoker(delegate () {
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
                        if (!sendtxCommandToDUT(data)) goto END;
                        //Wait ....
                        delay(200, 20);
                        //Send Instrument command
                        if (!sendtxCommandToInstrument(data)) goto END;
                        //Wait DUT tx stable
                        delay(3000, 100);
                        //Get result
                        txdataMeasures dm = new txdataMeasures();
                        int count = 0;
                        REPEAT:
                        {
                            count++;
                            if (!getResult(data, ref dm)) {
                                if (count <= 3) goto REPEAT;
                            }
                        }
                        //Hien thi ket qua do len Grid
                        displaytxResultToGrid(data, dm);
                        debugWriteLine(string.Format("> Result: {0}\n", dm.ToString()));
                    }
                    END: FinishControls();
                }));
            }));
            t.IsBackground = true;
            t.Start();
        }

        void rxTest() {
            Thread t = new Thread(new ThreadStart(() => {
                this.Invoke(new MethodInvoker(delegate () {
                    //Khoi tao controls
                    InitControls();
                    //Ket noi toi ONT
                    if (!connectONT()) goto END;
                    //Ket noi toi Instrument
                    if (!connectInstrument()) goto END;
                    //Load settings vao List....
                    if (!load_AllRXTestCase()) goto END;
                    //Select tab
                    selectTabPage(lblType.Text);
                    //start show progress
                    int totaltestCount = rxListTestCase.Count;
                    testCount = 0;
                    startProgress(totaltestCount);
                    //Start LOOP
                    foreach (var data in rxListTestCase) {
                        double sCounter = 0, eCounter = 0;
                        //Update progress
                        testCount++;
                        updateProgress(testCount, totaltestCount);
                        //Set ONT receive packets
                        if (!sendrxCommandToDUT(data)) goto END;
                        //Wait ....
                        delay(200, 20);
                        //Read current value of packets counter
                        int count = 0;
                        LOOP:
                        {
                            count++;
                            if (!rxGetWLCounter(ref sCounter, 0)) {
                                if (count <= 3) goto LOOP;
                            }
                        }

                        //Set Instrument transmit packets
                        if (!sendrxCommandToInstrument(data)) goto END;

                        //Wait packets transmit completed
                        DELAY(int.Parse(data.waitSent) / 1000);

                        //Read current value of packets counter
                        rxdataMeasures dm = new rxdataMeasures();
                        count = 0;
                        REPEAT:
                        {
                            count++;
                            if (!rxGetWLCounter(ref eCounter, 0)) {
                                if (count <= 3) goto REPEAT;
                            }
                        }
                        dm.packetGet = ((int)(eCounter - sCounter)).ToString();

                        //Hien thi ket qua do len Grid
                        dm.StartValue = sCounter.ToString();
                        dm.EndValue = eCounter.ToString();
                        displayrxResultToGrid(data, ref dm);
                        debugWriteLine(string.Format("> Result: {0}\n", dm.ToString()));
                    }

                    END: FinishControls();
                }));
            }));
            t.IsBackground = true;
            t.Start();
        }

        #endregion

        #region Load_RX_TestCase

        private bool rxconvertInputData(string datainput, ref List<rxdataFields> list) {
            try {
                //transfer data input to variables
                string[] buffer = datainput.Split(';');
                string _wifi = buffer[0].Split('=')[1];
                string _anten = buffer[1].Split('=')[1];
                string _channel = buffer[2].Split('=')[1];
                string _rate = buffer[3].Split('=')[1];
                string _power = buffer[4].Split('=')[1];
                string _packetSent = buffer[5].Split('=')[1];
                string _waitSent = buffer[6].Split('=')[1];

                //transfer variables to ienumerator
                bool ret;
                List<string> antenlist = new List<string>();
                List<string> channellist = new List<string>();
                List<string> ratelist = new List<string>();
                List<string> powerlist = new List<string>();

                ret = convertStringtoList(_anten, ref antenlist);
                ret = convertStringtoList(_channel, ref channellist);
                ret = convertStringtoList(_rate, ref ratelist);
                ret = convertStringtoList(_power, ref powerlist);

                //transfer data from ienumerator to list
                foreach (var atitem in antenlist) {
                    foreach (var chitem in channellist) {
                        foreach (var ritem in ratelist) {
                            foreach (var pitem in powerlist) {
                                rxdataFields data = new rxdataFields() { wifi = _wifi, anten = atitem, channel = chitem, rate = ritem, power = pitem, packetSent = _packetSent, waitSent = _waitSent };
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

        bool load_AllRXTestCase() {
            int errCode = 0;
            List<string> errContents = new List<string>() {
                "Định dạng file test case không hợp lệ.", //errCode = 0
                "Chưa load file test case vào phần mềm.", //errCode = 1
            };
            try {
                debugWriteLine("> Tải dữ liệu từ test case file vào phần mềm...");
                dataLines = null;
                rxListTestCase.Clear();
                dataLines = File.ReadAllLines(lbltestcasefilePath.Text);
                if (dataLines.Length > 0) {
                    bool ret = true;
                    foreach (var item in dataLines) {
                        bool result = rxconvertInputData(item, ref rxListTestCase);
                        if (!result) ret = false;
                    }
                    if (!ret) { errCode = 0; goto NG; }
                    else goto OK;
                }
                else { errCode = 1; goto NG; }
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

        #endregion

        #region Send RX Command To DUT

        bool rxGenerateCommand(ref S80211 s, rxdataFields data) {
            try {
                switch (data.wifi) {
                    case "802.11b": {
                            s = new S80211b(int.Parse(data.channel), double.Parse(data.rate), int.Parse(data.anten));
                            break;
                        }
                    case "802.11g": {
                            s = new S80211g(int.Parse(data.channel), double.Parse(data.rate), int.Parse(data.anten));
                            break;
                        }
                    case "802.11nHT20": {
                            s = new S80211n(int.Parse(data.channel), double.Parse(data.rate), int.Parse(data.anten));
                            break;
                        }
                    case "802.11nHT40": {
                            s = new S80211n(int.Parse(data.channel), double.Parse(data.rate), int.Parse(data.anten));
                            break;
                        }
                    default: return false;
                }
                return true;
            }
            catch {
                return false;
            }
        }

        bool sendrxCommandToDUT(rxdataFields data) {
            int errCode = 0;
            List<string> errContents = new List<string>() {
                "Không thể tạo list rx command cho DUT.", //errCode = 0
                "List rx command của DUT = 0.", //errCode = 1
            };
            try {
                debugWriteLine("> Điều khiển DUT nhận gói tin...");
                S80211 wifiUnit = null;
                string msg;
                if (!rxGenerateCommand(ref wifiUnit, data)) { errCode = 0; goto NG; }
                if (wifiUnit.rxCommandList.Count > 0) {
                    ontDevice.sendListCommand(wifiUnit.rxCommandList, out msg);
                    debugWriteLine(msg);
                }
                else { errCode = 1; goto NG; }
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

        #endregion

        #region Send RX Command To Instrument

        bool getWaveForm(string wifi, string rate, ref string waveform) {
            try {
                Dictionary<double, string> dictwf = new Dictionary<double, string>();
                switch (wifi) {
                    case "802.11nHT20": {
                            dictwf = waveFormN20;
                            break;
                        }
                    case "802.11nHT40": {
                            dictwf = waveFormN40;
                            break;
                        }
                    case "802.11b": {
                            dictwf = waveFormB;
                            break;
                        }
                    case "802.11g": {
                            dictwf = waveFormG;
                            break;
                        }
                    default: break;
                }
                //
                bool ret = dictwf.TryGetValue(double.Parse(rate), out waveform);
                debugWriteLine(string.Format("wave file: {0}", waveform));
                return waveform.Trim().Length == 0 ? false : true;
            }
            catch {
                return false;
            }
        }

        private bool sendrxCommandToInstrument(rxdataFields data) {
            int errCode = 0;
            
            List<string> errContents = new List<string>() {
                "Không cấu hình được máy đo.", //err 0
                "Không tìm được wave form.", //err 1
            };
            try {
                debugWriteLine("> Cấu hình máy đo EXM6640A...");
                string wf="";
                if (!getWaveForm(data.wifi, data.rate, ref wf)) { errCode = 1; goto NG; }
                if(Instrument.config_HT20_RxTest_MAC(data.channel, data.power, data.packetSent, wf)) goto NG;
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

        #endregion

        private void btnStart_Click(object sender, EventArgs e) {
            switch (lblType.Text) {
                case "TX": {
                        txTest();
                        break;
                    }
                case "RX": {
                        rxTest();
                        break;
                    }
                default: {
                        MessageBox.Show("Chưa load file test case vào phần mềm!\nVui lòng kiểm tra lại.\nClick 'File => Load test case'.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
            }
        }

        #endregion

    }
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    #endregion

    #region CUSTOM USER
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//

    public class txdataMeasures {

        public string power { get; set; }
        public string freqErr { get; set; }
        public string sym { get; set; }
        public string evm { get; set; }

        public override string ToString() {
            return string.Format("\npower={0},\nfreqErr={1},\nSYM={2},\nEVM={3}", power, freqErr, sym, evm);
        }
    }

    public class rxdataMeasures {
        
        public string packetGet { get; set; }
        public string PER { get; set; }
        public string StartValue { get; set; }
        public string EndValue { get; set; }

        public override string ToString() {
            return string.Format("\nStartValue={0}\nEndValue={1}\npacketGet={2},\nPER={3}", StartValue, EndValue, packetGet, PER);
        }
    }

    public class txdataFields {

        public string wifi { get; set; }
        public string bandwidth { get; set; }
        public string anten { get; set; }
        public string channel { get; set; }
        public string rate { get; set; }
        public string power { get; set; }

        public override string ToString() {
            return string.Format("wifi={0}, bandwidth={1}, anten={2}, channnel={3}, rate={4}, power={5}", wifi, bandwidth, anten, channel, rate, power);
        }
    }

    public class rxdataFields {

        public string wifi { get; set; }
        public string anten { get; set; }
        public string channel { get; set; }
        public string rate { get; set; }
        public string power { get; set; }
        public string packetSent { get; set; }
        public string waitSent { get; set; }

        public override string ToString() {
            return string.Format("wifi={0}, anten={1}, channel={2}, rate={3}, power={4}, packetSent={5}, waitSent={6}", wifi, anten, channel, rate, power, packetSent, waitSent);
        }
    }

    /// <summary>
    /// CLASS ĐỊNH NGHĨA KIỂU DỮ LIỆU CỦA CHO TXGRID
    /// NaN = Not a Number
    /// </summary>
    public class txGridDataRow {
        private int _anten;
        private double _evm;
        private double _pwr;
        private double _freqerr;
        private double _symclock;
        private int _bandwidth;

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
        public string PL_Limit { get; set; }
        public string Pwr {
            get { return string.Format("{0} dBm", _pwr == 0 ? "NaN" : _pwr.ToString()); }
            set {
                try {
                    _pwr = double.Parse(value);
                }
                catch { _pwr = 0; }
            }
        }
        public string PU_Limit { get; set; }

        public string FEL_Limit { get; set; }
        public string FreqErr {
            get { return string.Format("{0} kHz", _freqerr == 0 ? "NaN" : _freqerr.ToString()); }
            set {
                try {
                    _freqerr = double.Parse(value);
                }
                catch { _freqerr = 0; }

            }
        }
        public string FEU_Limit { get; set; }


        public string SymClock {
            get { return string.Format("{0} ppm", _symclock == 0 ? "NaN" : _symclock.ToString()); }
            set {
                try {
                    _symclock = double.Parse(value);
                }
                catch { _symclock = 0; }
            }
        }
        public string SCU_Limit { get; set; }

        public string EVM {
            get { return string.Format("{0} {1}", _evm == 0 ? "NaN" : _evm.ToString(), this.Wifi.ToUpper().Contains("B") == true ? "%" : "dBm"); }
            set {
                try {
                    _evm = double.Parse(value);
                }
                catch { _evm = 0; }

            }
        }
        public string EVMU_Limit { get; set; }

        public override string ToString() {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15}",
                                  Wifi,
                                  ANT,
                                  Bandwidth,
                                  Channel,
                                  Freq,
                                  Rate,
                                  PL_Limit,
                                  Pwr,
                                  PU_Limit,
                                  FEL_Limit,
                                  FreqErr,
                                  FEU_Limit,
                                  SymClock,
                                  SCU_Limit,
                                  EVM,
                                  EVMU_Limit);
        }
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
                return (((packetSent - packetGet) * 1.0) / (packetSent * 1.0)).ToString("00.00%");
            }
        }

        public override string ToString() {
            return string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", Wifi, ANT, Channel, Rate, Pwr, packetSent, packetGet, PER);
        }
    }
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~//
    #endregion
}
