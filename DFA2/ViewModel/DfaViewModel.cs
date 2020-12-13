using System;
using System.Collections.Generic;
using System.Linq;
using DFA2.Model;

namespace DFA2.ViewModel
{
    class DfaViewModel // получает входной текст, делит его по точке с запятой и отправляет их на разбор
    {
        int MinStringParsLength = 5;   // минимальная длина строки для разбора, в символах
        int MaxStringParsLength = 150; // максимально допустимая длина строки для разбора, в символах
        public List<OutputUnit> outputUnits = new List<OutputUnit>(); // список выходных объектов для датагрида
        public string Err = "";  

        public void StartParse(string txt)
        {// делим входящий текст на массив по точке с запятой, убираем из полученных строк те, длина которых меньше 5 символов
            List<string> listToParse = txt.Split(';').ToList().Where(st => st.Length > MinStringParsLength).ToList();

            if (listToParse.Count > 0)
            {
                foreach (string stp in listToParse)   
                {
                    if (stp.Length <= MaxStringParsLength) // если её длина не превышает установленную
                    {   // ищем первое вхождение слова из алфавита
                        var glue = stp.Split(' ').ToList().Where(word => Abc.Alphabet.Contains(word) && word.Length > 2).FirstOrDefault();

                        if (glue != null) // если в строке есть тип переменной,
                        {   // для каждой строки перед отправкой создаём объект, куда передаём оставшуюся цепочку, заменив табы на пробелы
                            string s = stp.Substring(stp.IndexOf(glue) + glue.Length).Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ");

                            try
                            {  var ou = new Dfa().Parse(new OutputUnit(s, glue), 0, Abc.Alphabet.IndexOf(glue)); // получили результат разбора строки

                                if (outputUnits.Count == 0) outputUnits.Add(ou); // если выходной список пуст, записываем туда результат разбора первой строки
                                else // если не пуст, проверяем объекты на полное совпадение определения
                                {
                                    if (outputUnits.Where(o => o.VarTypeName == ou.VarTypeName).FirstOrDefault() == null) outputUnits.Add(ou); // если определение новое
                                    else {  ou.Result = "дубликат"; outputUnits.Add(ou); } // если такое определение уже существует
                                }
                            }
                            catch (Exception e) { Err = e.Message; }
                        }
                    }
                    else { outputUnits.Add(new OutputUnit(stp, "максимальная длина определения - " + MaxStringParsLength + " символов")); }
                }
            }
        }
    }
}
