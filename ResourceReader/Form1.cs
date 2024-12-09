using Common;
using Common.Display;
using Common.DisplayClasses;
using FormsUtils;
using ResourceReader.SUtils;
using System.Text.Json;

namespace ResourceReader
{
    public partial class Form1 : Form
    {
        private readonly SimpleHttpClient _httpClient;
        private Linked<IMachineData> _linkedMachineData;

        public Form1()
        {
            InitializeUi();

            var json = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("appsettings.json"));

            if (json == null)
                throw new Exception($"Could not read configuration");

            _httpClient = new SimpleHttpClient(json["Address"]);

            tbMachineName.Text = Environment.MachineName;
        }

        #region INIT
        private void InitializeUi()
        {
            InitializeComponent();
            this.BackColor = CustomColors.BACKGROUND_COLOR;
            var controls = Utils.GetAllControlsInForm(this);
            foreach (Control c in controls)
            {
                if (c.Name.StartsWith("lbl"))
                {
                    c.BackColor = CustomColors.BACKGROUND_COLOR;
                    c.ForeColor = Color.White;
                }
                if (c.Name.StartsWith("chb"))
                    c.ForeColor = Color.White;
            }

            ColorOptions();

        }
        #endregion

        #region INTERACTIVE
        private void OnDisplaySelected_Click(object sender, EventArgs e)
        {
            if (_linkedMachineData == null)
                return;

            rtbOutput.Clear();

            var result = GetDataFromTargetMachine(tbMachineName.Text).ToArray();

            foreach (var item in result)
                rtbOutput.Text += item.ToStringAndFormat();
        }

        private void OnDisplayAll_Click(object sender, EventArgs e)
        {
            if (_linkedMachineData == null)
                return;

            rtbOutput.Clear();

            foreach (var item in _linkedMachineData)
                rtbOutput.Text += item.Value.ToStringAndFormat();
        }

        private async void OnFetchData(object sender, EventArgs e)
        {
            var json = await _httpClient.ExecuteAsync();
            GenerateLinkedMachineData(json.GenerateArrayFromJson().ToArray());
            ColorOptions();

            if (_linkedMachineData.Any())
                rtbOutput.Text = "Data Collected";
            else
                rtbOutput.Text = "No data recieved from endpoint";
        }
        #endregion

        #region Auxiliary
        private void GenerateLinkedMachineData(string[] list)
        {
            _linkedMachineData = new Linked<IMachineData>();

            foreach (var item in list)
            {
                var local = item.Split("||", StringSplitOptions.RemoveEmptyEntries);

                if (local[3].Equals("CPU"))
                    _linkedMachineData.Add(new CPUData(local[0], local[2], local[3], local[4]));
                else if (local[3].Equals("RAM"))
                    _linkedMachineData.Add(new MemoryData(local[0], local[2], local[3], local[4], local[5]));
                else if (local[3].Equals("DISK"))
                {
                    for (var i = 4; i < local.Length; i += 3)
                        _linkedMachineData.Add(new DiskData(local[0], local[2], local[3], local[i], local[i + 1], local[i + 2]));
                }
            }
        }

        private IEnumerable<IMachineData> GetDataFromTargetMachine(string machineName)
        {
            foreach (var item in _linkedMachineData)
            {
                if (item.Value.GetMachineName().CompareTo(machineName) == 0)
                    yield return item.Value;
            }
        }

        private void ColorOptions()
        {
            if (_linkedMachineData == null)
            {
                btnDisplayAll.BackColor = Color.Red;
                btnDisplaySelected.BackColor = Color.Red;
            }
            else
            {
                btnDisplayAll.BackColor = btnFetchData.BackColor;
                btnDisplaySelected.BackColor = btnFetchData.BackColor;
            }
        }


        private void RtbOutput_VScroll(object sender, EventArgs e)
        {
            rtbOutput.Refresh();
        }

        #endregion
    }
}
