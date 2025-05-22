using System;
using System.Linq;
using System.Text;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nОберіть завдання (1–9) або 0 для виходу:");
            Console.WriteLine("1 – Масиви A і B");
            Console.WriteLine("2 – Сума між мінімумом і максимумом у 5×5 масиві");
            Console.WriteLine("3 – Шифр Цезаря (зашифрування і розшифрування)");
            Console.WriteLine("4 – Операції з матрицями");
            Console.WriteLine("5 – Арифметичний вираз (+ і -)");
            Console.WriteLine("6 – Перша літера речення з великої букви");
            Console.WriteLine("7 – Цензура в тексті та статистика");
            Console.WriteLine("8 – Caesar шифрування з аргументів командного рядка");
            Console.WriteLine("9 – Множення матриці з параметрів командного рядка");
            Console.WriteLine("0 – Вихід");

            Console.Write("Ваш вибір: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": Task1(); break;
                case "2": Task2(); break;
                case "3": Task3(); break;
                case "4": Task4(); break;
                case "5": Task5(); break;
                case "6": Task6(); break;
                case "7": Task7(); break;
                case "8": Task8("Hello Caesar!", 3); break; // Приклад
                case "9": Task9(3, 3, 2); break; // Приклад
                case "0": return;
                default: Console.WriteLine("Невірний вибір."); break;
            }
        }
    }

    static void Task1()
    {
        int[] A = new int[5];
        double[,] B = new double[3, 4];
        Console.WriteLine("Введіть 5 чисел для масиву A:");
        for (int i = 0; i < A.Length; i++)
        {
            Console.Write($"A[{i}]: ");
            A[i] = int.Parse(Console.ReadLine());
        }

        Random rand = new Random();
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 4; j++)
                B[i, j] = rand.NextDouble() * 100;

        Console.WriteLine("Масив A:");
        Console.WriteLine(string.Join(" ", A));

        Console.WriteLine("Матриця B:");
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 4; j++)
                Console.Write($"{B[i, j]:F2}\t");
            Console.WriteLine();
        }

        double max = Math.Max(A.Max(), B.Cast<double>().Max());
        double min = Math.Min(A.Min(), B.Cast<double>().Min());
        double sum = A.Sum() + B.Cast<double>().Sum();
        double product = A.Aggregate(1.0, (acc, val) => acc * val) *
                         B.Cast<double>().Aggregate(1.0, (acc, val) => acc * val);
        int evenSum = A.Where(x => x % 2 == 0).Sum();

        double oddColSum = 0;
        for (int j = 0; j < 4; j += 2)
            for (int i = 0; i < 3; i++)
                oddColSum += B[i, j];

        Console.WriteLine($"Макс: {max:F2}, Мін: {min:F2}, Сума: {sum:F2}, Добуток: {product:F2}");
        Console.WriteLine($"Сума парних елементів A: {evenSum}");
        Console.WriteLine($"Сума непарних стовпців B: {oddColSum:F2}");
    }

    static void Task2()
    {
        int[,] arr = new int[5, 5];
        Random rand = new Random();
        int min = 101, max = -101, minPos = 0, maxPos = 0;

        Console.WriteLine("Масив:");
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                arr[i, j] = rand.Next(-100, 101);
                Console.Write($"{arr[i, j],5}");
                int flatIndex = i * 5 + j;
                if (arr[i, j] < min) { min = arr[i, j]; minPos = flatIndex; }
                if (arr[i, j] > max) { max = arr[i, j]; maxPos = flatIndex; }
            }
            Console.WriteLine();
        }

        int start = Math.Min(minPos, maxPos) + 1;
        int end = Math.Max(minPos, maxPos);
        int sum = 0;

        for (int k = start; k < end; k++)
            sum += arr[k / 5, k % 5];

        Console.WriteLine($"Сума елементів між мінімумом і максимумом: {sum}");
    }

    static void Task3()
    {
        Console.Write("Введіть рядок: ");
        string input = Console.ReadLine();
        Console.Write("Введіть зсув: ");
        int shift = int.Parse(Console.ReadLine());

        string encrypted = CaesarEncrypt(input, shift);
        string decrypted = CaesarEncrypt(encrypted, -shift);

        Console.WriteLine($"Зашифровано: {encrypted}");
        Console.WriteLine($"Розшифровано: {decrypted}");
    }

    static string CaesarEncrypt(string text, int shift)
    {
        StringBuilder result = new StringBuilder();
        foreach (char ch in text)
        {
            if (char.IsLetter(ch))
            {
                char baseChar = char.IsUpper(ch) ? 'A' : 'a';
                char enc = (char)((ch - baseChar + shift + 26) % 26 + baseChar);
                result.Append(enc);
            }
            else
                result.Append(ch);
        }
        return result.ToString();
    }

    static void Task4()
    {
        int[,] A = { { 1, 2 }, { 3, 4 } };
        int[,] B = { { 5, 6 }, { 7, 8 } };
        int scalar = 2;

        Console.WriteLine("Множення матриці A на число 2:");
        PrintMatrix(ScalarMultiply(A, scalar));

        Console.WriteLine("Додавання A + B:");
        PrintMatrix(MatrixAdd(A, B));

        Console.WriteLine("Добуток A * B:");
        PrintMatrix(MatrixMultiply(A, B));
    }

    static int[,] ScalarMultiply(int[,] matrix, int scalar)
    {
        int rows = matrix.GetLength(0), cols = matrix.GetLength(1);
        int[,] result = new int[rows, cols];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                result[i, j] = matrix[i, j] * scalar;
        return result;
    }

    static int[,] MatrixAdd(int[,] A, int[,] B)
    {
        int rows = A.GetLength(0), cols = A.GetLength(1);
        int[,] result = new int[rows, cols];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                result[i, j] = A[i, j] + B[i, j];
        return result;
    }

    static int[,] MatrixMultiply(int[,] A, int[,] B)
    {
        int rows = A.GetLength(0), cols = B.GetLength(1), n = A.GetLength(1);
        int[,] result = new int[rows, cols];
        for (int i = 0; i < rows; i++)
            for (int j = 0; j < cols; j++)
                for (int k = 0; k < n; k++)
                    result[i, j] += A[i, k] * B[k, j];
        return result;
    }

    static void PrintMatrix(int[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
                Console.Write($"{matrix[i, j],5}");
            Console.WriteLine();
        }
    }

    static void Task5()
    {
        Console.Write("Введіть вираз (+ або -): ");
        string input = Console.ReadLine();

        string[] parts = input.Split(new[] { '+', '-' });
        int[] numbers = parts.Select(int.Parse).ToArray();
        int result = numbers[0];
        int idx = 1;

        foreach (char op in input.Where(c => c == '+' || c == '-'))
        {
            if (op == '+') result += numbers[idx];
            else result -= numbers[idx];
            idx++;
        }

        Console.WriteLine($"Результат: {result}");
    }

    static void Task6()
    {
        Console.Write("Введіть текст: ");
        string text = Console.ReadLine();

        string[] sentences = text.Split(new[] { ". " }, StringSplitOptions.None);
        for (int i = 0; i < sentences.Length; i++)
            if (sentences[i].Length > 0)
                sentences[i] = char.ToUpper(sentences[i][0]) + sentences[i][1..];

        string result = string.Join(". ", sentences);
        Console.WriteLine(result);
    }

    static void Task7()
    {
        Console.Write("Введіть текст: ");
        string text = Console.ReadLine();
        Console.Write("Введіть заборонене слово: ");
        string bad = Console.ReadLine();

        int count = 0;
        string replaced = System.Text.RegularExpressions.Regex.Replace(text, $@"\b{bad}\b", m =>
        {
            count++;
            return new string('*', bad.Length);
        }, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        Console.WriteLine("\nРезультат:");
        Console.WriteLine(replaced);
        Console.WriteLine($"\nСтатистика: {count} замін слова {bad}.");
    }

    static void Task8(string text, int key)
    {
        string encrypted = CaesarEncrypt(text, key);
        Console.WriteLine($"Вхідний рядок: {text}");
        Console.WriteLine($"Зашифровано: {encrypted}");
    }

    static void Task9(int rows, int cols, int multiplier)
    {
        int[,] matrix = new int[rows, cols];
        Random rand = new Random();

        Console.WriteLine("Матриця до множення:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = rand.Next(1, 10);
                Console.Write($"{matrix[i, j],4}");
            }
            Console.WriteLine();
        }

        Console.WriteLine($"\nПісля множення на {multiplier}:");
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
                Console.Write($"{matrix[i, j] * multiplier,4}");
            Console.WriteLine();
        }
    }
}