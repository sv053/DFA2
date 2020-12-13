
namespace DFA2.Model
{
    public class Dfa  // анализатор
    {
        public OutputUnit Parse(OutputUnit ou, int q, int k) // получает входную цепочку и индекс ключевого слова в алфавите для начала разбора
        {
            for (int j = 0; j < Abc.Transitions[Abc.Transitions[q][k]].Length; j++) // проходит по строке, индексом которой является значение заданной ячейки
            {
                if (Abc.Transitions[Abc.Transitions[q][k]][j] > -1) // если переход для текущего элемента существует, проверяем его
                {
                    if (new CheckMethod().CheckChar(ou, Abc.Alphabet[j]).Item1) // если ожидаемый символ подтвердился, 
                    {
                        Parse(ou, Abc.Transitions[q][k], j); break; // значение подтверждённой ячейки матрицы передаём как номер следующего состояния (строки) функции переходов 
                    }
                } else if (Abc.Transitions[Abc.Transitions[q][k]][j] == -2) ou.Result = "определение корректно"; // если пришли сюда, значит всё хорошо.  
            }
            return ou;
        }
    }
}
