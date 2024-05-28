using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        QuestionManager questionManager = new QuestionManager("questions.json");
        Lifelines lifelines = new Lifelines();
        Shop shop = new Shop();
        int balance = 0;

        while (true)
        {
            Console.WriteLine("Alege o optiune:");
            Console.WriteLine("1. Joaca jocul");
            Console.WriteLine("2. Intra in magazin (balance: " + balance + " RON)");
            Console.WriteLine("3. Iesire");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                PlayGame(questionManager, lifelines, ref balance);
            }
            else if (choice == "2")
            {
                EnterShop(shop, lifelines, ref balance);
            }
            else if (choice == "3")
            {
                break;
            }
            else
            {
                Console.WriteLine("Optiune invalida. Te rog sa incerci din nou.");
            }
        }
    }

    static void PlayGame(QuestionManager questionManager, Lifelines lifelines, ref int balance)
    {
        List<Question> easyQuestions = questionManager.GetQuestionsByDifficulty("easy", 5);
        List<Question> mediumQuestions = questionManager.GetQuestionsByDifficulty("medium", 5);
        List<Question> hardQuestions = questionManager.GetQuestionsByDifficulty("hard", 5);

        List<Question> allQuestions = new List<Question>();
        allQuestions.AddRange(ConvertToTimedQuestions(easyQuestions, 10)); // 10 seconds for easy questions
        allQuestions.AddRange(ConvertToTimedQuestions(mediumQuestions, 20)); // 20 seconds for medium questions
        allQuestions.AddRange(ConvertToTimedQuestions(hardQuestions, 30)); // 30 seconds for hard questions

        int score = 0;
        int[] prizes = { 100, 200, 300, 500, 1000, 2000, 4000, 8000, 16000, 32000, 64000, 125000, 250000, 500000, 1000000 };

        foreach (var question in allQuestions)
        {
            lifelines.ResetLifelinesForNewQuestion();

            bool? correct = false;
            if (question is TimedQuestion timedQuestion)
            {
                correct = timedQuestion.AskWithTimer(lifelines, ref balance);
            }
            else
            {
                Console.WriteLine(question.Text);
                Console.WriteLine("a. " + question.OptionA);
                Console.WriteLine("b. " + question.OptionB);
                Console.WriteLine("c. " + question.OptionC);
                Console.WriteLine("d. " + question.OptionD);

                bool validAnswer = false;
                char answer = ' ';

                while (!validAnswer)
                {
                    try
                    {
                        Console.WriteLine($"Introdu raspunsul (a, b, c sau d), tasta 'l' pentru ajutor, sau tasta 'q' pentru a parasi jocul cu {balance} RON");
                        string input = Console.ReadLine().ToLower();
                        if (input == "q")
                        {
                            Console.WriteLine($"Ai ales sa parasesti jocul cu {balance} RON.");
                            return;
                        }
                        if (input == "l")
                        {
                            lifelines.UseLifeline(question);
                            continue;
                        }
                        answer = char.Parse(input);
                        if (answer == 'a' || answer == 'b' || answer == 'c' || answer == 'd')
                        {
                            validAnswer = true;
                        }
                        else
                        {
                            Console.WriteLine("Raspuns invalid. Te rog sa introduci a, b, c sau d.");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Raspuns invalid. Te rog sa introduci a, b, c sau d.");
                    }
                }

                correct = answer == question.CorrectAnswer;
            }

            if (correct == null)
            {
                Console.WriteLine($"Ai ales sa parasesti jocul cu {balance} RON.");
                return;
            }

            if (correct == true)
            {
                Console.WriteLine("Raspuns corect!");
                score++;
                balance = prizes[score - 1];
            }
            else
            {
                Console.WriteLine("Raspuns gresit!");
                balance = score > 0 ? prizes[(score - 1) / 2] : 0;
                break;
            }
        }

        if (score == 15)
        {
            Console.WriteLine($"Felicitari! Ai castigat marele premiu de {balance} RON!");
        }
        else if (balance > 0)
        {
            Console.WriteLine($"Ai plecat cu {balance} RON.");
        }
        Console.WriteLine("Multumim ca ai jucat 'Vrei sa fii milionar'!");
    }

    static List<Question> ConvertToTimedQuestions(List<Question> questions, int timeLimit)
    {
        List<Question> timedQuestions = new List<Question>();
        foreach (var question in questions)
        {
            timedQuestions.Add(new TimedQuestion(question.Text, question.OptionA, question.OptionB, question.OptionC, question.OptionD, question.CorrectAnswer, question.Difficulty, timeLimit));
        }
        return timedQuestions;
    }

    static void EnterShop(Shop shop, Lifelines lifelines, ref int balance)
    {
        while (true)
        {
            shop.DisplayItems();
            Console.WriteLine("Alege un item sau 'exit' pentru a iesi:");
            string choice = Console.ReadLine();

            if (choice.ToLower() == "exit")
            {
                break;
            }

            shop.PurchaseItem(choice, ref balance, lifelines);
        }
    }

    static List<Question> ShuffleQuestions(List<Question> questions)
    {
        Random random = new Random();
        for (int i = 0; i < questions.Count; i++)
        {
            int j = random.Next(i, questions.Count);
            Question temp = questions[i];
            questions[i] = questions[j];
            questions[j] = temp;
        }
        return questions;
    }
}