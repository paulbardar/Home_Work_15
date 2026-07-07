using System.Text.RegularExpressions;

namespace search_word
{

    // Користувач вводить з клавіатури слово для пошуку у файлі та слово для заміни.
    // Додаток повинен змінити всі входження шуканого слова на слово для заміни.
    // Статистику роботи додатку потрібно відобразити на екрані.

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, my word!");

            
            string textToWrite = "Life is a constant journey of change that shapes who we are. " +
                "Sometimes, a small change in our daily routine can lead to great success. " +
                "If you want to see a change in the world, you must start with yourself. " +
                "Embracing change allows us to grow, learn, and discover new opportunities. " +
                "Therefore, we should never fear change but welcome it with an open heart.";

            string filePath = "output.txt";

            try
            {
                // Create file
                File.WriteAllText(filePath, textToWrite);
                Console.WriteLine($"Successful! File saved here: {Path.GetFullPath(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\n=== Find and replace words in a file ===");

            Console.Write("Enter a search word (example, change): ");
            string searchWord = Console.ReadLine();

            Console.Write("Enter word for replace: ");
            string replaceWord = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(searchWord))
            {
                Console.WriteLine("\nError: Word for search can`t be empty!");
                return;
            }
            try
            {
                // Read text from file
                string content = File.ReadAllText(filePath);

                // IgnoreCase
                string pattern = $@"\b{Regex.Escape(searchWord)}\b";
                RegexOptions options = RegexOptions.IgnoreCase;

                // Count of changes
                MatchCollection matches = Regex.Matches(content, pattern, options);
                int count = matches.Count;

                if (count > 0)
                {
                    // do changes
                    string newContent = Regex.Replace(content, pattern, replaceWord, options);

                    // Rewrite text in file
                    File.WriteAllText(filePath, newContent);
                }

                // Write Statistics
                Console.WriteLine("\n--- Statistics of work ---");
                Console.WriteLine($"Finded and changed : {count}");
                Console.WriteLine(count > 0 ? "File renewed succefully." : "No changes done, no matches found.");

                // Read new text
                Console.WriteLine("\n--- Content on renewed file ---");
                string finalContent = File.ReadAllText(filePath);
                Console.WriteLine(finalContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

        }
    }
}
