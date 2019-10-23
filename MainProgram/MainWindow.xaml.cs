using MainProgram.Files;
using MainProgram.Maths;
using MainProgram.Utils;
using System;
using System.Windows;

namespace AssemblyProject
{
    public partial class MainWindow : Window
    {
        private LoadedData data;

        public MainWindow()
        {
            InitializeComponent();
            SetSystemInfoTextBoxesContent();
        }

        private void searchFileButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            dlg.DefaultExt = ".txt";
            dlg.Filter = "Text files (*.txt)|*.txt";

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                searchBox.Text = filename;
            }
        }

        private void runAppButton_Click(object sender, RoutedEventArgs e)
        {
            if (data != null)
            {
                var result = data.Matrix.MultiplyByScalar(data.Scalar);
                SetOutputDataTextBoxesContent(result);
            }
            else
            {
                MessageBox.Show("Cannot run program, because there is no data to process!");
            }
        }

        private void loadDataButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(searchBox.Text))
            {
                var fileReader = new FileReader(searchBox.Text);
                data = fileReader.GetLoadedData();

                SetInputDataTextBoxesContent();
            }
            else
            {
                MessageBox.Show("Choose .txt file in searchbox!");
            }
        }

        private void SetInputDataTextBoxesContent()
        {
            matrixInputBox.Text = data.Matrix.ToString();
            scalarInputBox.Text = data.Scalar.Value.ToString();

            multiplySign.Visibility = Visibility.Visible;
        }

        private void SetSystemInfoTextBoxesContent()
        {
            coreNumber.Text = SystemInfo.GetNumberOfCores();
            logicalProcessorsNumber.Text = SystemInfo.GetNumberOfLogicalProcessors();
        }

        private void SetOutputDataTextBoxesContent(Matrix resultMatrix)
        {
            finalMatrix.Text = resultMatrix.ToString();
        }
    }
}
