using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Core;

namespace TheHardwareMonitor
{
    public partial class MainForm : Form
    {
        private Dictionary<string, Tuple<int, int>> cpuBenches = new Dictionary<string, Tuple<int, int>>()
        {
            { "intel core i3-2120", Tuple.Create<int, int>(346, 288) },
        };

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
            label10.Text = Cpu.voltage;
            label12.Text = Cpu.specification;
            label14.Text = Cpu.familyNumber;
            label16.Text = Cpu.model;
            label18.Text = Cpu.internalVersion;
            label20.Text = Cpu.extFamily;
            label22.Text = Cpu.extModel;
            label24.Text = Cpu.coreRevision;

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
                if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
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
        }

        private void tabPageMemoryInit(object sender)
        {

            // dropdown
            if (sender == this)
            {
                comboBox2.Items.Clear();
                foreach (var ram in Ram.ramsNames)
                {
                    comboBox2.Items.Add(ram);
                }
                comboBox2.Click += onCombo2Click();
                if (comboBox2.Items.Count > 0) comboBox2.SelectedIndex = 0;
            }
            label110.Text = Ram.memType;
            label112.Text = Ram.memSize;
            label136.Text = Ram.manufacturer;
            label114.Text = Ram.number;
            label120.Text = Ram.controllerClock;
            label128.Text = Ram.ramClock;
            label122.Text = Ram.wordWidth;
            label124.Text = Ram.formFactor;

        }
        private EventHandler onCombo2Click()
        {
            Ram.selectedDevice = Math.Max(0, comboBox2.SelectedIndex);
            tabPageMemoryInit(comboBox2);

            return null;
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
                if (comboBox3.Items.Count > 0) comboBox3.SelectedIndex = 0;
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

            int cores = int.Parse(Cpu.logicalCoreCount);
            comboBox7.Items.Clear();
            for (int i = 0; i < cores; i++)
            {
                comboBox7.Items.Add(i + 1);
            }
            comboBox7.SelectedIndex = cores - 1;

            comboBox6.Items.Clear();
            var it = cpuBenches.GetEnumerator();
            while (it.MoveNext())
            {
                comboBox6.Items.Add(it.Current.Key);
            }
            comboBox6.SelectedIndex = 0;
        }

        private bool testIsRunning = false;

        private void onRunTestClick(object sender, EventArgs e)
        {
            label241.Text = label244.Text = "~";
            singlethreadBar.Value = multithreadBar.Value = 0;
            if (testIsRunning) return;
            testIsRunning = true;
            button1.Enabled = false;

            var bench = new Benchmark();
            int coresCount = comboBox7.SelectedIndex;
            if (!checkBox3.Checked) coresCount = 1;
            bench.RunBenchmark(1, (result) => {
            OnProgressUpdate(result, singlethreadBar, singlethreadBarReference, label241);
            },
            (result) =>
            {
                OnProgressUpdate(result, singlethreadBar, singlethreadBarReference, label241);
                bench.RunBenchmark(coresCount, (finalResult) =>
                {
                    OnProgressUpdate(finalResult, multithreadBar, multithreadBarReference, label244);
                }, onBenchEnd);
            });
        }

        private void OnProgressUpdate(int result, ColorProgressBar.ColorProgressBar bar, ColorProgressBar.ColorProgressBar referenceBar, Label label)
        {
            BenchBarValueSet(result, bar, referenceBar);
            label.Invoke(new Action(() => label.Text = result + " мс"));
            
        }

        private int barMax = 0;

        private void BenchBarValueSet(int value, ColorProgressBar.ColorProgressBar bar, ColorProgressBar.ColorProgressBar barPaired)
        {
            while (barMax < value)
            {
                barMax += 500;
            }

            if (barMax > bar.Maximum)
            {
                bar.Maximum = barMax;
                barPaired.Maximum = barMax;
            }
            bar.Value = barMax - value;
        }
        private void onBenchEnd(int result)
        {
            OnProgressUpdate(result, multithreadBar, multithreadBarReference, label244);
            testIsRunning = false;
            button1.Invoke(new Action(() => button1.Enabled = true));
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = comboBox6.SelectedIndex;
            string cpu = comboBox6.Items[i].ToString();
            var values = cpuBenches[cpu];

            BenchBarValueSet(values.Item1, singlethreadBarReference, singlethreadBar);
            label242.Text = values.Item1 + " мс";
            BenchBarValueSet(values.Item2, multithreadBarReference, multithreadBar);
            label243.Text = values.Item1 + " мс";
        }
    }
}
