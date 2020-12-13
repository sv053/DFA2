using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using DFA2.ViewModel;

namespace DFA2.View
{
    public partial class ParserWindow : Window
    {
        string Text = "";
        DirectoryInfo dir = new DirectoryInfo(@"..\..\..");
        public ParserWindow()
        {
            InitializeComponent();
            Loaded += ParserWindow_Loaded;
        }
        private void ParserWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //if (inputChar == Char.ConvertFromUtf32(65))
             //    MessageBox.Show("yeap!");
                //else MessageBox.Show(Char.ConvertFromUtf32(65));

                string path = dir.FullName + "\\parsData.txt";
            try
            {
               using (StreamReader streamReader = new StreamReader(path, System.Text.Encoding.Default))
                {
                    List<string> sl = new List<string>(); string s;
                    while ((s = streamReader.ReadLine()) != null)
                    {
                        sl.Add(s);
                    }
                    fileDataTb.ItemsSource = sl;
                    Text = string.Join("", sl);
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

            if (startParseAllBtn != null) startParseAllBtn.IsEnabled = false;

            if (startParseStringBtn != null) startParseStringBtn.IsEnabled = true;
        }

        private void parseAllRb_Checked(object sender, RoutedEventArgs e)
        {
            if (startParseAllBtn != null) startParseAllBtn.IsEnabled = true;

            if (startParseStringBtn != null) startParseStringBtn.IsEnabled = false;
        }
        private void startParseAllBtn_Click(object sender, RoutedEventArgs e)
        {
            DfaViewModel dfa = new DfaViewModel();
            Stopwatch timer = new Stopwatch();
            timer.Start();
            dfa.StartParse(Text);
            if (dfa.Err != "") MessageBox.Show(dfa.Err);
            outputDg.ItemsSource = dfa.outputUnits;
            timer.Stop();
            testRes.Text = (timer.ElapsedMilliseconds).ToString("E") + " Мс";
            timer.Reset();

            //if (dfa.outputUnits != null)
            //    SaveData(dfa.outputUnits, "AllText");
        }

        private void startParseStringBtn_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch timer = new Stopwatch();
            DfaViewModel dfa = new DfaViewModel();
            timer.Start();

            if (inputStringTb.Text.Length > 0)
            {
                dfa.StartParse(inputStringTb.Text);
                outputDg1.ItemsSource = dfa.outputUnits;
            }
            timer.Stop();
            testRes.Text = (timer.ElapsedMilliseconds).ToString("E") + " Мс";
            timer.Reset();

            //if (dFA.OutputUnits != null)
            //    SaveData(dFA.OutputUnits, "Line");
        }

        //private void SaveData(List<OutputUnit> results, string fileName)
        //{
        //    string uriSL = dir.FullName + "\\" + fileName + "ParsResult.xml";
        //    try
        //    {
        //        using (Stream fStream = new FileStream(uriSL, FileMode.Create, FileAccess.Write, FileShare.None))
        //        {
        //            XmlSerializer xmlFormat = new XmlSerializer(typeof(List<OutputUnit>));

        //            xmlFormat.Serialize(fStream, results);
        //            MessageBox.Show("результат разбора записан в файл по адресу " + uriSL);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message);
        //    }
        //}

    }
}
