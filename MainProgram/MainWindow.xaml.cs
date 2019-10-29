using MainProgram.Files;
using MainProgram.Libraries;
using MainProgram.Maths;
using MainProgram.Utils;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AssemblyProject
{
    public partial class MainWindow : Window
    {
        private LoadedData data;
        private Executer executer;

        public MainWindow()
        {
            InitializeComponent();
            SetSystemInfoTextBoxesContent();
            SetThreadsTextBlockContent();
        }


        private void Slider_ValueChanged(object sender,
            RoutedPropertyChangedEventArgs<double> e)
        {
            var slider = sender as Slider;
            SetThreadsTextBlockContent(slider.Value);
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
            if (CheckIfAnyLanguageIsSelected())
            {
                if (CheckIfDataIsLoaded())
                {
                    if (CheckIfThreadsAreChoosed())
                    {
                        RunApplicationMethod();
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

        private void threadsTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                double value = Convert.ToDouble(threadsTextBox.Text);

                if (value <= 64 && value >= 0)
                {
                    threadsSlider.Value = value;
                }
                else
                {
                    OutOfRangeMessageBox();
                    ResetSliderAndTextBox();
                }
            }
        }

        #endregion

        #region TextBlock

        private void SetOutputTextBlockContent(string content)
        {
            outputTextBlock.Text = content;
        }

        private void SetThreadsTextBlockContent(double value = 0.0)
        {
            threadsTextBox.Text = value.ToString();
        }

        #endregion

        #region ComboBox
        
        private bool CheckIfAnyLanguageIsSelected()
        {
            return languageComboBox.SelectedIndex > 0;
        }

        private LibraryLanguage GetSelectedLanguage()
        {
            return (LibraryLanguage)languageComboBox.SelectedIndex;
        }

        #endregion

        private bool CheckIfThreadsAreChoosed()
        {
            return (threadsSlider.Value != 0);
        }

        private bool CheckIfDataIsLoaded()
        {
            return data != null;
        }

        private void RunApplicationMethod()
        {
            executer = new Executer(GetSelectedLanguage(), Convert.ToInt32(threadsSlider.Value), data);

            executer.Execute();

            SaveOperationResultAndUpdateGui();
        }

        private void SaveOperationResultAndUpdateGui()
        {
            finalMatrix.Text = executer.Result.ToString();
            
            var operationInfo = executer.RetrieveExectionInfo();
            
            FileSaver.Save(operationInfo, "wyniki.txt");
            SetOutputTextBlockContent(operationInfo);
        }

        private void ResetSliderAndTextBox(int value = 0)
        {
            threadsTextBox.Text = value.ToString();
            threadsSlider.Value = value;
        }
        private void OutOfRangeMessageBox()
        {
            MessageBox.Show("Number of threads must be between 0 and 64!");
        }
    }
}
