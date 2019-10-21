using MainProgram.Utils;
using System;
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
            
            coreNumber.Text = SystemInfo.GetNumberOfCores();

            logicalProcessorsNumber.Text = SystemInfo.GetNumberOfLogicalProcessors();
        }

        private void searchFileButton_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text files (*.txt)|*.txt";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName;
                searchBox.Text = filename;
            }
        }
    }
}
