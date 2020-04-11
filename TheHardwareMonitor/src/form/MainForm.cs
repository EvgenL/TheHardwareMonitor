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

        }

        private void tabPageMemoryInit(object sender)
        {

        }

        private void tabPageSpdInit(object sender)
        {

        }

        private void tabPageGraphicsInit(object sender)
        {

        }

        private void tabPageBenchInit(object sender)
        {

        }
    }
}
