using Common;
using Common.DisplayClasses;
using FormsUtils;
using System.Text.Json;

namespace ResourceReader
{
    public partial class Form1 : Form
    {
        private readonly SimpleHttpClient _httpClient;
        private List<IMachineData> _machineData;

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
        }
        #endregion

        private async void btnGenerateSortData(object sender, EventArgs e)
        {
            var json = await _httpClient.ExecuteAsync();
            var list = json.GenerateArrayFromJson().ToArray();
            _machineData = GenerateMmachineData(list).ToList();

            foreach (var item in _machineData)
                rtbOutput.Text += $"{item.ToString()}{Environment.NewLine}{Environment.NewLine}";
        }

        private IEnumerable<IMachineData> GenerateMmachineData(string[] list)
        {

            foreach (var item in list)
            {
                var local = item.Split("||", StringSplitOptions.RemoveEmptyEntries);

                if (local[3].Equals("CPU"))
                    yield return new CPUData(local[0], local[2], local[3], local[4]);
                else if (local[3].Equals("RAM"))
                    yield return new MemoryData(local[0], local[2], local[3], local[4], local[5]);
                else if (local[3].Equals("DISK"))
                {
                    for (var i = 4; i < local.Length; i += 3)
                        yield return new DiskData(local[0], local[2], local[3], local[i], local[i + 1], local[i + 2]);
                }
            }
        }

        private void RtbOutput_VScroll(object sender, EventArgs e)
        {
            rtbOutput.Refresh();
        }
    }
}
