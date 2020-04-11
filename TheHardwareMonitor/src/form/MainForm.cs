using System;
using System.Windows.Forms;
using Core;

namespace TheHardwareMonitor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            InitHardwareDisplay();
        }

        private void InitHardwareDisplay()
        {
            tabPageCpuInit(this);
            tabPageCachesInit(this);
            tabPageMainboardInit(this);
            tabPageMemoryInit(this);
            tabPageSpdInit(this);
            tabPageGraphicsInit(this);
            tabPageBenchInit(this);
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tabPageCpuInit(object sender)
        {
            labelValue.Text = Cpu.currentProcessorName;
            label2.Text = Cpu.codename;
            label4.Text = Cpu.heatDiss;
            label6.Text = Cpu.package;
            label8.Text = Cpu.thickness;
            label10.Text = Cpu.voltage;
            label12.Text = Cpu.specification;
            label14.Text = Cpu.familyNumber;
            label16.Text = Cpu.model;
            label18.Text = Cpu.internalVersion;
            label20.Text = Cpu.extFamily;
            label22.Text = Cpu.extModel;
            label24.Text = Cpu.coreRevision;
            label26.Text = Cpu.instructions;

            label30.Text = Cpu.clock;
            label28.Text = Cpu.multiplier;
            label32.Text = Cpu.outerFreq;
            label34.Text = Cpu.effectiveFreq;

            label36.Text = Cpu.level1cacheData;
            label38.Text = Cpu.level1cache;
            label40.Text = Cpu.level2cache;
            label42.Text = Cpu.level3cache;
            label43.Text = Cpu.level1cacheDataAssociativity;
            label44.Text = Cpu.level1cacheAssociativity;
            label45.Text = Cpu.level2cacheAssociativity;
            label46.Text = Cpu.level3cacheAssociativity;

            // dropdown
            if (sender == this)
            {
                comboBox1.Items.Clear();
                foreach (var cpu in Cpu.processors)
                {
                    comboBox1.Items.Add(cpu);
                }
                comboBox1.Click += onCombo1Click();
                comboBox1.SelectedIndex = 0;
            }

            label70.Text = Cpu.coreCount;
            label72.Text = Cpu.logicalCoreCount;

        }

        private EventHandler onCombo1Click()
        {
            Cpu.currCpuIndex = Math.Max(0, comboBox1.SelectedIndex);
            tabPageCpuInit(comboBox1);

            return null;
        }

        private void tabPageCachesInit(object sender)
        {
            label48.Text = Cpu.level1cacheData;
            label51.Text = Cpu.level1cacheDataAssociativity;
            label56.Text = Cpu.level1cacheInst;
            label54.Text = Cpu.level1cacheInstAssociativity;
            label61.Text = Cpu.level2cache;
            label59.Text = Cpu.level2cacheAssociativity;
            label66.Text = Cpu.level3cache;
            label64.Text = Cpu.level3cacheAssociativity;
        }

        private void tabPageMainboardInit(object sender)
        {
            label90.Text = Mainboard.manufacturer;
            label92.Text = Mainboard.model;
            label93.Text = Mainboard.modelRev;
            label96.Text = Mainboard.chipsetName;
            label94.Text = Mainboard.chipsetNumber;
            label118.Text = Mainboard.chipsetRev;
            label98.Text = Mainboard.southBridgeName;
            label99.Text = Mainboard.southBridgeNumber;
            label100.Text = Mainboard.southBridgeRev;
            label103.Text = Mainboard.lpcioBridgeName;
            label104.Text = Mainboard.lpcioBridgeNumber;
            label82.Text = Mainboard.biosBrand;
            label80.Text = Mainboard.biosVersion;
            label105.Text = Mainboard.biosVersion;
            label85.Text = Mainboard.gbusVersion;
            label87.Text = Mainboard.gbusMode;
            label88.Text = Mainboard.gbusMaxMode;
            label108.Text = Mainboard.gbusAgp;
        }

        private void tabPageMemoryInit(object sender)
        {

            
        }
        private void tabPageSpdInit(object sender)
        {

        }

        private void tabPageGraphicsInit(object sender)
        {
            // dropdown
            if (sender == this)
            {
                comboBox3.Items.Clear();
                foreach (var gpu in Gpu.cardsNames)
                {
                    comboBox3.Items.Add(gpu);
                }
                comboBox3.Click += onCombo3Click();
                comboBox3.SelectedIndex = 0;
            }

            label239.Text = Gpu.name;
            label218.Text = Gpu.manufacturer;
            label220.Text = Gpu.codename;
            label222.Text = Gpu.rev;
            label224.Text = Gpu.thicc;

            label226.Text = Gpu.coreClock;
            label228.Text = Gpu.shaders;
            label230.Text = Gpu.videoClock;

            label232.Text = Gpu.memSize;
            label234.Text = Gpu.memType;
            label236.Text = Gpu.memWidth;
        }

        private EventHandler onCombo3Click()
        {
            Gpu.currGpuIndex = Math.Max(0, comboBox3.SelectedIndex);
            tabPageGraphicsInit(comboBox3);

            return null;
        }

        private void tabPageBenchInit(object sender)
        {
            button1.Click += onRunTestClick;

            int cores = int.Parse(Cpu.coreCount);

            for (int i = 0; i < cores; i++)
            {
                comboBox7.Items.Add(i + 1);
            }
            comboBox7.SelectedIndex = cores;
        }

        private bool testIsRunning = false;

        private void onRunTestClick(object sender, EventArgs e)
        {
            if (testIsRunning) return;
            testIsRunning = true;
            button1.Enabled = false;

            var bench = new Benchmark();
            int coresCount = comboBox7.SelectedIndex;
            if (!checkBox3.Checked) coresCount = 1;
            bench.RunBenchmark(1, onProgressUpdate, (result) =>
            {
                onProgressUpdate(result);
                bench.RunBenchmark(coresCount, onProgressUpdateMultithread, onBenchEnd);
            });
        }

        private void onProgressUpdate(int result)
        {
            if (result > colorProgressBar1.Maximum) colorProgressBar1.Maximum = result;
            colorProgressBar1.Value = result;
            label241.Invoke(new Action(() => label241.Text = result.ToString()));
            
        }

        private void onProgressUpdateMultithread(int result)
        {
            if (result > colorProgressBar3.Maximum) colorProgressBar3.Maximum = result;
            colorProgressBar3.Value = result;
            label244.Invoke(new Action(() => label244.Text = result.ToString()));
        }

        private void onBenchEnd(int result)
        {
            onProgressUpdate(result);
            testIsRunning = false;
            button1.Invoke(new Action(() => button1.Enabled = true));
        }
    }
}
