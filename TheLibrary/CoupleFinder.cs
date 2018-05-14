using System;
using System.Collections.Generic;
using System.Linq;

namespace Couples
{
    public static class CoupleFinder
    {
        /// <summary>
        /// Находит все сочетания из двух элементов входной коллекции, такие чтобы сумма элементов была равна заданной величине.
        /// </summary>
        /// <param name="input">Входная коллекция</param>
        /// <param name="desireлаемая величина сочетания</param>
        /// <returns>Список сочетаний</returns>
        public static List<int[]> GetCouples(int [] input, int desiredCoupleSum)
        {
            Array.Sort(input);

            int sPos = 0;
            int ePos = input.Length - 1;
            List<int[]> output = new List<int[]>();

            while (sPos < ePos)
            {
                int curSum = input[sPos] + input[ePos];
                if (desiredCoupleSum == curSum)
                {
                    output.Add(new int[] { input[sPos], input[ePos] });
                    sPos++;
                    ePos--;
                }
                else if (desiredCoupleSum > curSum)
                {
                    sPos++;
                }
                else
                {
                    ePos--;
                }
            }

            return output;
        }

        /// <summary>
        /// Преобразует список пар чисел в строку
        /// </summary>
        /// <param name="data">Список пар чисел</param>
        /// <returns>Строка</returns>
        public static string CouplesToStr(List<int[]> data)
        {
            return string.Join(",", data.Select(x => "(" + x[0] + "," + x[1] + ")").ToArray());
        }
    }
}
