using System.Text.RegularExpressions;

namespace Censored_text
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, Censored text!");

            // Create file with text
            string defaultTextFile = "censored.txt";
            string initialText = "test best rest car\nman  telephone";
            File.WriteAllText(defaultTextFile, initialText);

            Console.WriteLine($"[System]: Created file '{defaultTextFile}' with initial text.");
            Console.WriteLine("----------------------------------------------------------------");

            // Reading replacement words from the keyboard
            List<string> wordsToModerate = new List<string>();
            Console.WriteLine("Enter words to replace with asterisks (each word on a new line).");
            Console.WriteLine("To finish, press Enter on an empty line.:");

            while (true)
            {
                string inputWord = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(inputWord))
                {
                    break;
                }

                if (!wordsToModerate.Contains(inputWord))
                {
                    wordsToModerate.Add(inputWord);
                }
            }
            if (wordsToModerate.Count == 0)
            {
                Console.WriteLine("You have not entered any replacement words. The job is complete.");
                return;
            }

            Console.WriteLine("\nEnter the name of the file to be moderated (e.g. censored.txt):");
            string textFilePath = Console.ReadLine()?.Trim('\"');

            if (!File.Exists(textFilePath))
            {
                Console.WriteLine($"Error: File '{textFilePath}' not found.");
                return;
            }

            try
            {
                
                string text = File.ReadAllText(textFilePath);

                var sortedWords = wordsToModerate
                    .OrderByDescending(w => w.Length)
                    .ToList();

                foreach (string word in sortedWords)
                {
                    string escapedWord = Regex.Escape(word);
                    string pattern = $@"\b{escapedWord}\b";

                    text = Regex.Replace(text, pattern, new string('*', word.Length));
                }

                File.WriteAllText(textFilePath, text);

                Console.WriteLine("\nModeration successfully completed!");
                Console.WriteLine("New text of the file:\n");
                Console.WriteLine(text);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Runtime error: {ex.Message}");
            }

        }
    }
}
