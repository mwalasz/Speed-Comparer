using MainProgram.Extensions;
using MainProgram.Files;
using MainProgram.Libraries;
using MainProgram.Utils;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AssemblyProject
{
    public partial class MainWindow : Window
    {
        private const string DefaultSearchBoxContent = @"C:\Users\Mateusz\Desktop\arr_100x100.txt";
        
        private LoadedData data;
        private Executer executer;

        private bool averageStatistics;

        public MainWindow()
        {
            InitializeComponent();
            SetSystemInfoTextBoxesContent();
            SetThreadsTextBlockContent();
            SetDataInputTextBlockContent();
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
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
            if (CheckIfAllConditionsAreMet())
            {
                RunApplicationMethod();
                SaveOperationResultAndUpdateGui();
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

        private void runStatisticsButton_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIfDataIsLoaded())
            {
                var threadsToRunOn = new int[] { 1, 2, 4, 8, 16, 32, 64 };
                var description = RunStatisticsAndGetInfo(threadsToRunOn, false);

                FileSaver.SaveAndOpen(description);
            }
            else
            {
                MessageBox.Show("Load any data to start statistics!");
            }
        }

        private string RunStatisticsAndGetInfo(int[] threads, bool runAssembly)
        {
            languageComboBox.SelectedIndex = (int)LibraryLanguage.CPlusPlus;

            string description = "Language: " + GetSelectedLanguage().GetName() + ":\n";
            RunStatistics(threads, ref description);

            if (runAssembly)
            {
                languageComboBox.SelectedIndex = (int)LibraryLanguage.Assembly;

                description += "\nLanguage: " + GetSelectedLanguage().GetName() + ":\n";
                RunStatistics(threads, ref description);
            }
            
            return description;
        }

        private void RunStatistics(int[] threads, ref string description)
        {
            if (averageStatistics)
            {
                foreach (var t in threads)
                {
                    threadsSlider.Value = t;

                    executer = new Executer(GetSelectedLanguage(), t, data);
                    description += executer.Execute(5);
                }
            }
            else
            {
                foreach (var t in threads)
                {
                    threadsSlider.Value = t;

                    RunApplicationMethod();
                    description += executer.RetrieveStatisticsInfo();
                }
            }
        }

        #endregion

        #region TextBoxes

        private void SetInputDataTextBoxesContent()
        {
            if (data.Matrix.Columns <= 20 || data.Matrix.Rows <= 20)
            {
                matrixInputBox.Text = data.Matrix.ToString();
                scalarInputBox.Text = data.Scalar.Value.ToString();

                multiplySign.Visibility = Visibility.Visible;
            }
            else
            {
                multiplySign.Visibility = Visibility.Hidden;
                scalarInputBox.Text = string.Empty;
                matrixInputBox.Text = $"Data loaded, but matrix is too big to be displayed.\n ({data.Matrix.Rows}x{data.Matrix.Columns})";
            }
        }

        private void SetSystemInfoTextBoxesContent()
        {
            coreNumber.Text = SystemInfo.GetNumberOfCores();
            logicalProcessorsNumber.Text = SystemInfo.GetNumberOfLogicalProcessors();
        }

        private void threadsTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                if (!threadsTextBox.Text.Any(Char.IsLetter))
                {
                    double value = Convert.ToDouble(threadsTextBox.Text);

                    if (value <= ThreadsNumber.Max && value >= ThreadsNumber.Min)
                    {
                        threadsSlider.Value = value;
                    }
                }
                else
                {
                    WrongThreadNumberInputMessage();
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

        private void SetDataInputTextBlockContent()
        {
            searchBox.Text = DefaultSearchBoxContent;
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
        }

        private void SaveOperationResultAndUpdateGui()
        {
            var operationInfo = executer.RetrieveExectionInfo();
            
            if (data.Matrix.Columns <= 20 || data.Matrix.Rows <= 20)
                finalMatrix.Text = executer.Result.ToString(data.Matrix.Rows, data.Matrix.Columns);
            else finalMatrix.Text = $"Data successfully processed, but matrix is too big to be displayed.\n ({data.Matrix.Rows}x{data.Matrix.Columns})";

            SetOutputTextBlockContent(operationInfo);
        }

        private void ResetSliderAndTextBox(int value = 0)
        {
            threadsTextBox.Text = value.ToString();
            threadsSlider.Value = value;
        }

        private void WrongThreadNumberInputMessage()
        {
            MessageBox.Show("Number of threads must be between 0 and 64.\n It cannot contain any letter!");
        }

        private bool CheckIfAllConditionsAreMet()
        {
            if (CheckIfAnyLanguageIsSelected())
            {
                if (CheckIfDataIsLoaded())
                {
                    if (CheckIfThreadsAreChoosed())
                    {
                        return true;
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

            return false;
        }

        private void averageCheckbox_Click(object sender, RoutedEventArgs e)
        {
            averageStatistics = averageCheckbox.IsChecked ?? false;
        }
    }
}
