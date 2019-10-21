using MainProgram.Utils;
using System.Linq;
using System.Management;
using System.Windows;

namespace AssemblyProject
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetTextBoxesData();
        }

        private void SetTextBoxesData()
        {
            var asm = AssemblyDll.AsmVal();
            
            coreNumber.Text = "Cores: " + SystemInfo.GetNumberOfCores();

            logicalProcessorsNumber.Text = "Logical processors: " + SystemInfo.GetNumberOfLogicalProcessors();
        }
    }
}
