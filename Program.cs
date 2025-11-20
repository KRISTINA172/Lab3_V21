using System;
using System.IO;   // Дозволяє працювати з файлами
using System.Text;

namespace Lab3_V_21
{
    class Program
    {
        static void Main(string[] args)  // Головний метод
        {
            Console.OutputEncoding = Encoding.UTF8; // Підтримка українських символів 
            Console.InputEncoding = Encoding.UTF8;

            bool running = true; // Змінна, яка керує роботою меню (поки true — меню працює)

            while (running)   // Початок циклу меню
            {
                Console.Clear();
                Console.WriteLine("   Практична робота 3 (Варіант 21)");
                Console.WriteLine("1. Завдання 1: слово з найбільшою кількістю голосних + видалення пробілів");
                Console.WriteLine("2. Завдання 2: перевірка трикутника");
                Console.WriteLine("3. Завдання 3: найдовше слово");
                Console.WriteLine("0. Вихід");
                Console.Write("Що обираємо? ");

                string choice = Console.ReadLine(); // Зчитування вибору користувача

                switch (choice) // Вибір дії залежно від введеного пункту
                {
                    case "1": // Якщо вибрали 1
                        ExecuteTask1(); // Виклик методу Завдання 1
                        break;
                    case "2":
                        ExecuteTask2();
                        break;
                    case "3":
                        ExecuteTask3();
                        break;
                    case "0": // Якщо вибрали 0
                        Console.WriteLine("\nЗавершення програми :)"); // Повідомлення про вихід
                        running = false; // Міняємо running на false, меню зупиниться
                        break;
                    default: // Якщо введено щось інше
                        Console.WriteLine("\nПомилка: невірний вибір.");
                        break;
                }

                if (running) // Якщо меню ще працює
                {
                    Console.WriteLine("\nНатисніть Enter, щоб повернутися до вибору...");
                    Console.ReadLine();
                }
            }
        }

        //ЗАВДАННЯ 1 
        static void ExecuteTask1() // Метод, що виконує завдання 1
        {
            Console.Clear();
            Console.WriteLine(" Завдання 1: слово з найбільшою кількістю голосних + видалення пробілів\n");

            Console.Write("Напишіть щось: ");
            string text = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(text)) // Перевірка на пустий рядок
            {
                Console.WriteLine("Помилка: рядок не може бути порожнім.");
                return;
            }

            string cleaned = RemoveExtraSpaces(text); // Метод для видалення зайвих пробілів
            string best = FindMostVowels(cleaned);  // Метод для пошуку слова з найбільшою кількістю голосних

            Console.WriteLine("\n--- Результати ---");
            Console.WriteLine("Очищений текст: " + cleaned);
            Console.WriteLine("Слово з найбільшою кількістю голосних: " + best);
        }

        static string RemoveExtraSpaces(string text) // Метод видалення зайвих пробілів
        {
            StringBuilder result = new StringBuilder(); // Створення динамічного рядка
            bool prevSpace = false; // Прапорець чи попередній символ був пробілом

            foreach (char c in text) // Перебираємо усі символи рядка
            {
                if (char.IsWhiteSpace(c)) // Якщо символ — пробіл
                {
                    if (!prevSpace) result.Append(' '); // Додаємо пробіл тільки якщо попереднього пробілу не було
                    prevSpace = true; // Встановлюємо прапорець
                }
                else
                {
                    result.Append(c); // Додаємо звичайний символ
                    prevSpace = false; // Скидаємо прапорець
                }
            }

            return result.ToString().Trim(); // Повертаємо результат без пробілів по краях
        }

        static string FindMostVowels(string text) // Метод пошуку слова з найбільшою кількістю голосних
        {
            string[] words = text.Split(' '); // Розбиваємо текст на слова
            string vowels = "аеєиіїоуюяАЕЄИІЇОУЮЯ"; // Всі голосні

            string best = ""; // Змінна для слова з найбільшою кількістю голосних
            int maxCount = 0; // Максимальна кількість голосних

            foreach (string word in words) // Перебір слів
            {
                int count = 0; // Лічильник голосних у слові

                foreach (char c in word) // Перебір символів слова
                {
                    if (vowels.IndexOf(c) != -1) // Якщо символ — голосна
                        count++; // Збільшуємо лічильник
                }

                if (count > maxCount) // Якщо знайдено слово з більшою кількістю голосних
                {
                    maxCount = count; // Оновлюємо максимальну кількість
                    best = word; // Зберігаємо слово
                }
            }

            return best; // Повертаємо результат
        }

        //ЗАВДАННЯ 2
        static void ExecuteTask2()
        {
            Console.Clear();
            Console.WriteLine(" Завдання 2: перевірка трикутника (файл)\n");

            string fileName = "triangle.txt"; // Назва файлу, куди записуємо числа

            Console.WriteLine("Введіть 3 числа:");
            Console.Write("a = ");
            string a = Console.ReadLine();
            Console.Write("b = ");
            string b = Console.ReadLine();
            Console.Write("c = ");
            string c = Console.ReadLine();

            // запис у файл
            using (StreamWriter writer = new StreamWriter(fileName, false, Encoding.UTF8)) // Відкриваємо файл для запису (перезаписуємо)
            {
                writer.WriteLine(a);
                writer.WriteLine(b);
                writer.WriteLine(c);
            }

            // зчитування
            double x, y, z;
            using (StreamReader reader = new StreamReader(fileName, Encoding.UTF8)) // Відкриваємо файл для читання
            {
                x = double.Parse(reader.ReadLine());// Зчитуємо перше число як число
                y = double.Parse(reader.ReadLine());  // 2
                z = double.Parse(reader.ReadLine());  // 3
            }

            string answer; // Змінна для збереження результату

            // проста перевірка трикутника
            if (x + y > z && x + z > y && y + z > x)
                answer = "Так, трикутник існує.";
            else
                answer = "Ні, трикутник НЕ існує.";

            // дописуємо у файл
            using (StreamWriter writer2 = new StreamWriter(fileName, true, Encoding.UTF8)) // Відкриваємо файл у режимі дозапису
            {
                writer2.WriteLine("Результат: " + answer);  // Записуємо відповідь у файл
            }

            Console.WriteLine("\nРезультат записано у файл: " + fileName);
        }

        //ЗАВДАННЯ 3
        static void ExecuteTask3()
        {
            Console.Clear();
            Console.WriteLine(" Завдання 3: найдовше слово (input → output)\n");

            string inputFile = "input.txt"; // Файл для читання тексту
            string outputFile = "output.txt"; // Файл для запису результату

            if (!File.Exists(inputFile)) // Якщо файл не існує
            {
                Console.WriteLine("Помилка: файл input.txt не знайдено.");
                return; // Вихід з методу
            }

            string text = File.ReadAllText(inputFile, Encoding.UTF8); // Зчитуємо весь текст з файлу
            string longest = FindLongestWord(text);  // Знаходимо найдовше слово

            File.WriteAllText(outputFile, "Найдовше слово: " + longest, Encoding.UTF8); // Записуємо результат у файл

            Console.WriteLine("Результат записано у файл output.txt");
        }

        static string FindLongestWord(string text) // Метод для пошуку найдовшого слова
        {
            string[] words = text.Split(new char[] { ' ', ',', '.', '!', '?', '\n', '\r', '\t' },
                                        StringSplitOptions.RemoveEmptyEntries);

            string best = ""; // Змінна для найдовшого слова

            foreach (string w in words) // Перебір усіх слів
            {
                if (w.Length > best.Length) // Якщо слово довше за попереднє
                    best = w; // Зберігаємо його
            }

            return best; // Повертаємо найдовше слово
        }
    }
}
