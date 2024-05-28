public class TimedQuestion : Question
{
    public int TimeLimit { get; set; }

    public TimedQuestion(string text, string optionA, string optionB, string optionC, string optionD, char correctAnswer, string difficulty, int timeLimit)
        : base(text, optionA, optionB, optionC, optionD, correctAnswer, difficulty)
    {
        TimeLimit = timeLimit;
    }

    public bool? AskWithTimer(Lifelines lifelines, ref int balance)
    {
        Console.WriteLine(Text);
        Console.WriteLine("a. " + OptionA);
        Console.WriteLine("b. " + OptionB);
        Console.WriteLine("c. " + OptionC);
        Console.WriteLine("d. " + OptionD);
        Console.WriteLine($"Introdu raspunsul (a, b, c sau d), tasta 'l' pentru ajutor, sau tasta 'q' pentru a parasi jocul cu {balance} RON");
        Console.WriteLine($"Ai {TimeLimit} secunde pentru a raspunde.");

        var startTime = DateTime.Now;

        while ((DateTime.Now - startTime).TotalSeconds < TimeLimit)
        {
            if (Console.KeyAvailable)
            {
                string input = Console.ReadLine().ToLower();
                if (input == "q")
                {
                    return null;
                }
                if (input == "l")
                {
                    lifelines.UseLifeline(this);
                    continue;
                }
                if (input.Length == 1 && "abcd".Contains(input))
                {
                    return input[0] == CorrectAnswer;
                }
                else
                {
                    Console.WriteLine("Raspuns invalid. Te rog sa introduci a, b, c sau d.");
                }
            }
        }

        Console.WriteLine("Timpul a expirat!");
        return false;
    }
}