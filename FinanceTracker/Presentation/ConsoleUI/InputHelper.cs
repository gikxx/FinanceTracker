using System;
using System.Globalization;

namespace FinanceTracker.Presentation.Helpers
{
    public static class InputHelper
    {
        // Чтение целого числа с проверкой
        public static int ReadInt(string prompt, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int result) && result >= minValue && result <= maxValue)
                    return result;
                Console.WriteLine($"Ошибка: введите целое число от {minValue} до {maxValue}.");
            }
        }

        // Чтение десятичного числа с проверкой диапазона
        public static decimal ReadDecimal(string prompt, decimal minValue = decimal.MinValue, decimal maxValue = decimal.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result)
                    && result >= minValue
                    && result <= maxValue)
                    return result;
                Console.WriteLine($"Ошибка: введите число от {minValue} до {maxValue}.");
            }
        }

        // Чтение даты в формате ГГГГ-ММ-ДД
        public static DateTime ReadDate(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                if (DateTime.TryParseExact(
                    Console.ReadLine(),
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime result))
                    return result;
                Console.WriteLine("Ошибка: введите дату в формате ГГГГ-ММ-ДД.");
            }
        }

        // Выбор значения из перечисления
        public static T ReadEnum<T>(string prompt) where T : Enum
        {
            var values = Enum.GetValues(typeof(T)).Cast<T>().ToList();
            Console.WriteLine(prompt);
            for (int i = 0; i < values.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {values[i]}");
            }

            while (true)
            {
                Console.Write("Выберите пункт: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= values.Count)
                    return values[choice - 1];
                Console.WriteLine("Ошибка: введите номер из списка.");
            }
        }

        // Чтение строки с проверкой на пустоту
        public static string ReadString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(input))
                    return input;
                Console.WriteLine("Ошибка: поле не может быть пустым.");
            }
        }
    }
}