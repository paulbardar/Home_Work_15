namespace app_100_nums
{
    // Додаток генерує 100 цілих чисел. Потрібно зберегти в один файл усі прості числа,
    // в інший файл усі числа Фібоначчі.
    // Статистику роботи додатка потрібно відобразити на екран.
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, 100 numbers!");

            string sourceFile = "numbers.bin";
            string primeFile = "primes.txt";
            string fibFile = "fibonacci.txt";

            using FileStream fs = new FileStream(sourceFile, FileMode.Create, FileAccess.Write);
            using (BinaryWriter bn = new BinaryWriter(fs))
            {
                Random rand = new Random();
                for (int i = 0; i < 100; i++)
                {
                    bn.Write(rand.Next(1, 100));
                }
            }

            List<int> primes = new List<int>();
            List<int> fibonacciNumbers = new List<int>();

            using (FileStream fs1 = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
            using (BinaryReader br = new BinaryReader(fs1))
            {
                while (fs1.Position < fs1.Length)
                {
                    int number = br.ReadInt32();

                    if (IsPrime(number))
                        primes.Add(number);

                    if (IsFibonacci(number))
                        fibonacciNumbers.Add(number);
                }
            }
            File.WriteAllText(primeFile, string.Join(", ", primes));
            File.WriteAllText(fibFile, string.Join(", ", fibonacciNumbers));

            // Statistics
            Console.WriteLine("\n=== Work Statistics ===");
            Console.WriteLine($"Generated and recorded in {sourceFile}: 100 numbers.");
            Console.WriteLine($"Prime numbers found: {primes.Count} (saved in {primeFile})");
            Console.WriteLine($"Fibonacci numbers found: {fibonacciNumbers.Count} (saved in {fibFile})");
        }
        // check prime number
        static bool IsPrime(int n)
        {
            if (n < 2) return false;
            for (int i = 2; i <= Math.Sqrt(n); i++)
            {
                if (n % i == 0) return false;
            }
            return true;
        }

        // Check Fibonacci number
        static bool IsFibonacci(int n)
        {
            if (n < 0) return false;
            int sq1 = 5 * n * n + 4;
            int sq2 = 5 * n * n - 4;
            return IsPerfectSquare(sq1) || IsPerfectSquare(sq2);
        }

        static bool IsPerfectSquare(int x)
        {
            if (x < 0) return false;
            int s = (int)Math.Sqrt(x);
            return s * s == x;
        }
    }
}
