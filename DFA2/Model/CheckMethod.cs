using System;
using System.Linq;

namespace DFA2.Model
{
    class CheckMethod // проверка семантики
    {
        public Tuple<bool, OutputUnit> CheckChar(OutputUnit ou, string str)
        {
           bool isChar = false;
            if (ou.Haystack.Length == 0) return new Tuple<bool, OutputUnit>(isChar, ou);
            switch (str)
            {
                case "l":// если символ должен быть буквой и это так, добавляем его в выходную строку и удаляем из разбираемой
                    {
                        if (Char.IsLetter(ou.Haystack.First()) || ou.Haystack.First() == '_')
                            isChar = AddChar(ou);
                        else ou.Result = "ожидалась буква или нижнее подчёркивание";
                        break;
                    }
                case "d":// если символ должен быть цифрой и это так, добавляем его в выходную строку и удаляем из разбираемой
                    {
                        if (Char.IsDigit(ou.Haystack.First()))
                            isChar = AddChar(ou);
                        else ou.Result = "ожидалась цифра";
                        break;
                    }
                default:// если строка должна быть другой лексемой или символом из алфавита и это так, добавляем её в выходную строку и удаляем из разбираемой
                    {
                        if (ou.Haystack.StartsWith(str))  
                        {
                            ou.VarTypeName += str;
                            ou.Haystack = ou.Haystack.Substring(str.Length);
                            isChar = true;
                        }
                        else ou.Result = "ожидался символ "+ Char.GetUnicodeCategory(str.ToCharArray()[0]);
                        break;
                    }
            }            
            return new Tuple<bool, OutputUnit>(isChar, ou);
        }
        private bool AddChar(OutputUnit ou)
        {
            ou.VarTypeName += ou.Haystack.First();   // записываем подтверждённый символ в результирующую строку
            ou.Haystack = ou.Haystack.Substring(1);  // убираем из цепочки проверенный символ
            return true;
        }
    }
}
