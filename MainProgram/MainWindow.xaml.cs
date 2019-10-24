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

        #region Buttons

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
            if (languageComboBox.SelectedIndex > 0)
            {
                if (data != null)
                {
                    if (CheckIfAnyTextBoxIsChecked())
                    {
                        switch (languageComboBox.SelectedIndex)
                        {
                            case (int)LibraryLanguage.CSharp:
                                var result = data.Matrix.MultiplyByScalar(data.Scalar);
                                SetOutputDataTextBoxesContent(result);
                                break;

                            case (int)LibraryLanguage.Assembly:
                                var res = AssemblyDll.AsmVal();
                                finalMatrix.Text = res.ToString();
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Choose number of threads to run program on!");
                    }
                }
                else
                {
                    MessageBox.Show("Load any data to process!");
                }
            }
            else
            {
                MessageBox.Show("Choose library language!");
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

        #endregion

        #region TextBoxes

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

        #endregion

        #region CheckBoxes
        
        private void threadCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (sender == eightThreads)
            {
                eightThreads.IsChecked = true;

                if (fourThreads.IsChecked == true)
                    fourThreads.IsChecked = !fourThreads.IsChecked;

                if (twoThreads.IsChecked == true)
                    twoThreads.IsChecked = !twoThreads.IsChecked;

                if (oneThread.IsChecked == true)
                    oneThread.IsChecked = !oneThread.IsChecked;
            }

            if (sender == fourThreads)
            {
                fourThreads.IsChecked = true;

                if (eightThreads.IsChecked == true)
                    eightThreads.IsChecked = !eightThreads.IsChecked;

                if (twoThreads.IsChecked == true)
                    twoThreads.IsChecked = !twoThreads.IsChecked;

                if (oneThread.IsChecked == true)
                    oneThread.IsChecked = !oneThread.IsChecked;
            }

            if (sender == twoThreads)
            {
                twoThreads.IsChecked = true;

                if (eightThreads.IsChecked == true)
                    eightThreads.IsChecked = !eightThreads.IsChecked;

                if (fourThreads.IsChecked == true)
                    fourThreads.IsChecked = !fourThreads.IsChecked;

                if (oneThread.IsChecked == true)
                    oneThread.IsChecked = !oneThread.IsChecked;
            }

            if (sender == oneThread)
            {
                oneThread.IsChecked = true;

                if (eightThreads.IsChecked == true)
                    eightThreads.IsChecked = !eightThreads.IsChecked;

                if (fourThreads.IsChecked == true)
                    fourThreads.IsChecked = !fourThreads.IsChecked;

                if (twoThreads.IsChecked == true)
                    twoThreads.IsChecked = !twoThreads.IsChecked;
            }
        }

        private bool CheckIfAnyTextBoxIsChecked()
        {
            if (oneThread.IsChecked != true)
            {
                if (twoThreads.IsChecked != true)
                {
                    if (fourThreads.IsChecked != true)
                    {
                        if (eightThreads.IsChecked != true)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        
        #endregion
    }
}
