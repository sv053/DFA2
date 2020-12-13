using System;
using System.Collections.Generic;
using System.Windows;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using DFA2.Model;
using DFA2.ViewModel;

namespace DFA2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> inputStrings = new List<string>();
        string Text = "";
        DirectoryInfo dir = new DirectoryInfo(@"..\..\..");
        public MainWindow()
        {
            InitializeComponent();

            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            string path = dir.FullName + "\\parsData.txt";

            try
            {
                using (StreamReader streamReader = new StreamReader(path, System.Text.Encoding.Default))
                {

                    string tmpStr = "";

                    while ((tmpStr = streamReader.ReadLine()) != null)
                    {

                        inputStrings.Add(tmpStr);
                    }

                    using (StreamReader streamReader1 = new StreamReader(path, System.Text.Encoding.Default))
                    {
                        Text += streamReader1.ReadToEnd();

                    }
                    fileDataTb.ItemsSource = inputStrings;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ошибка чтения файла, содержимое не загружено  " + ex.Message);
            }
        }

        private void parseStrRb_Checked(object sender, RoutedEventArgs e)
        {
            inputStringTb.IsEnabled = true;

            if (startParseAllBtn != null)
                startParseAllBtn.IsEnabled = false;

            if (startParseStringBtn != null)
                startParseStringBtn.IsEnabled = true;
        }

        private void parseAllRb_Checked(object sender, RoutedEventArgs e)
        {
            if (startParseAllBtn != null)
                startParseAllBtn.IsEnabled = true;

            if (startParseStringBtn != null)
                startParseStringBtn.IsEnabled = false;
        }

        private void parseStrRb_Unchecked(object sender, RoutedEventArgs e)
        { }

        private void startParseAllBtn_Click(object sender, RoutedEventArgs e)
        {
            DfaViewModel dfa = new DfaViewModel();
            if (dfa.Err != "")
                MessageBox.Show(dfa.Err);
           
            Stopwatch timer = new Stopwatch();
            timer.Start();

            dfa.StartParse(Text);
            outputDg.ItemsSource = dfa.outputUnits;
            timer.Stop();
            testRes.Text = "время выполнения - " + (timer.ElapsedMilliseconds).ToString("E") + " Мс";
            timer.Reset();

            //if (dfa.outputUnits != null)
            //    SaveData(dfa.outputUnits, "AllText");
        }

        private void startParseStringBtn_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            //Dfa dfa = new Dfa();

            //if (inputStringTb.Text.Length > 0)
            //{
            //    dFA.Start(inputStringTb.Text);
            //    outputDg1.ItemsSource = dFA.OutputUnits;
            //}
            //timer.Stop();
            //testRes.Text = "время выполнения - " + (timer.ElapsedMilliseconds).ToString("E") + " Мс";
            //timer.Reset();

            //if (dFA.OutputUnits != null)
            //    SaveData(dFA.OutputUnits, "Line");
        }

        private void SaveData(List<OutputUnit> results, string fileName)
        {
            string uriSL = dir.FullName + "\\" + fileName + "ParsResult.xml";
            try
            {
                using (Stream fStream = new FileStream(uriSL, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    XmlSerializer xmlFormat = new XmlSerializer(typeof(List<OutputUnit>));

                    xmlFormat.Serialize(fStream, results);
                    MessageBox.Show("результат разбора записан в файл по адресу " + uriSL);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}

