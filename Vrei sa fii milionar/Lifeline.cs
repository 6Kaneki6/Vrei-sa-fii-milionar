using System;
using System.Collections.Generic;

public class Lifelines
{
    private int fiftyFiftyCount = 2;
    private int phoneAFriendCount = 2;
    private int askTheAudienceCount = 2;

    private HashSet<string> usedLifelinesForCurrentQuestion;

    public Lifelines()
    {
        usedLifelinesForCurrentQuestion = new HashSet<string>();
    }

    public void UseLifeline(Question question)
    {
        Console.WriteLine("Alege un lifeline:");
        Console.WriteLine("1. 50-50");
        Console.WriteLine("2. Phone a Friend");
        Console.WriteLine("3. Ask the Audience");

        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                if (fiftyFiftyCount > 0 && !usedLifelinesForCurrentQuestion.Contains("50-50"))
                {
                    UseFiftyFifty(question);
                    fiftyFiftyCount--;
                    usedLifelinesForCurrentQuestion.Add("50-50");
                }
                else
                {
                    Console.WriteLine("Nu mai ai lifeline-uri 50-50 sau ai folosit deja acest lifeline pentru aceasta intrebare.");
                }
                break;
            case "2":
                if (phoneAFriendCount > 0 && !usedLifelinesForCurrentQuestion.Contains("Phone a Friend"))
                {
                    UsePhoneAFriend(question);
                    phoneAFriendCount--;
                    usedLifelinesForCurrentQuestion.Add("Phone a Friend");
                }
                else
                {
                    Console.WriteLine("Nu mai ai lifeline-uri Phone a Friend sau ai folosit deja acest lifeline pentru aceasta intrebare.");
                }
                break;
            case "3":
                if (askTheAudienceCount > 0 && !usedLifelinesForCurrentQuestion.Contains("Ask the Audience"))
                {
                    UseAskTheAudience(question);
                    askTheAudienceCount--;
                    usedLifelinesForCurrentQuestion.Add("Ask the Audience");
                }
                else
                {
                    Console.WriteLine("Nu mai ai lifeline-uri Ask the Audience sau ai folosit deja acest lifeline pentru aceasta intrebare.");
                }
                break;
            default:
                Console.WriteLine("Optiune invalida.");
                break;
        }
    }

    public void AddLifeline(string lifeline)
    {
        switch (lifeline)
        {
            case "50-50":
                fiftyFiftyCount++;
                break;
            case "PhoneAFriend":
                phoneAFriendCount++;
                break;
            case "AskTheAudience":
                askTheAudienceCount++;
                break;
        }
    }

    public void ResetLifelinesForNewQuestion()
    {
        usedLifelinesForCurrentQuestion.Clear();
    }

    private void UseFiftyFifty(Question question)
    {
        Console.WriteLine("Ai folosit 50-50.");
        Random random = new Random();
        char correctAnswer = question.CorrectAnswer;
        List<char> options = new List<char> { 'a', 'b', 'c', 'd' };
        options.Remove(correctAnswer);

        // Eliminate two incorrect options
        for (int i = 0; i < 2; i++)
        {
            int index = random.Next(options.Count);
            options.RemoveAt(index);
        }
        Console.WriteLine(question.Text);
        // Show the remaining options
        foreach (var option in options)
        {
            Console.WriteLine($"{option}. {GetOptionText(question, option)}");
        }
        Console.WriteLine($"{correctAnswer}. {GetOptionText(question, correctAnswer)}");
    }

    private void UsePhoneAFriend(Question question)
    {
        Console.WriteLine("Ai folosit Phone a Friend.");
        Console.WriteLine($"Prietena ta crede ca raspunsul corect este {question.CorrectAnswer}.");
    }

    private void UseAskTheAudience(Question question)
    {
        Console.WriteLine("Ai folosit Ask the Audience.");
        Random random = new Random();
        int correctPercentage = random.Next(60, 90);
        int remainingPercentage = 100 - correctPercentage;
        List<char> options = new List<char> { 'a', 'b', 'c', 'd' };
        options.Remove(question.CorrectAnswer);

        int incorrectPercentage1 = random.Next(0, remainingPercentage);
        remainingPercentage -= incorrectPercentage1;

        int incorrectPercentage2 = random.Next(0, remainingPercentage);
        remainingPercentage -= incorrectPercentage2;

        int incorrectPercentage3 = remainingPercentage;

        // Prepare the audience poll result in a dictionary
        Dictionary<char, int> audiencePoll = new Dictionary<char, int>
        {
            { question.CorrectAnswer, correctPercentage },
            { options[0], incorrectPercentage1 },
            { options[1], incorrectPercentage2 },
            { options[2], incorrectPercentage3 }
        };

        // Sort the dictionary by keys (alphabetically)
        var sortedAudiencePoll = audiencePoll.OrderBy(kvp => kvp.Key);

        Console.WriteLine($"Publicul crede:");
        foreach (var kvp in sortedAudiencePoll)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}%");
        }
    }

    private string GetOptionText(Question question, char option)
    {
        return option switch
        {
            'a' => question.OptionA,
            'b' => question.OptionB,
            'c' => question.OptionC,
            'd' => question.OptionD,
            _ => ""
        };
    }
}