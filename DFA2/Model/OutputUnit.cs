
namespace DFA2.Model
{
    public class OutputUnit // объект для хранения разбираемой и записи выходной строк
    {
        public string Haystack { set; get; } = ""; // для временного хранения разбираемой строки
        public string VarTypeName { set; get; } = ""; // для определения переменной 
        public string Result { set; get; } = "ошибка"; // для примечания о ходе разбора
        public OutputUnit(string haystack, string vtn)
        {
            Haystack = haystack;
            VarTypeName = vtn;
        }
    }
}
