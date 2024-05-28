using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class QuestionManager
{
    private List<Question> questions;

    public QuestionManager(string filePath)
    {
        questions = LoadQuestions(filePath);
    }

    private List<Question> LoadQuestions(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<Question>>(json);
    }

    public List<Question> GetQuestionsByDifficulty(string difficulty, int numberOfQuestions)
    {
        List<Question> filteredQuestions = questions.FindAll(q => q.Difficulty == difficulty);
        return GetRandomQuestions(filteredQuestions, numberOfQuestions);
    }

    private List<Question> GetRandomQuestions(List<Question> questions, int numberOfQuestions)
    {
        Random random = new Random();
        List<Question> randomQuestions = new List<Question>(questions);

        for (int i = 0; i < randomQuestions.Count; i++)
        {
            int j = random.Next(i, randomQuestions.Count);
            Question temp = randomQuestions[i];
            randomQuestions[i] = randomQuestions[j];
            randomQuestions[j] = temp;
        }

        if (numberOfQuestions > randomQuestions.Count)
        {
            numberOfQuestions = randomQuestions.Count;
        }

        return randomQuestions.GetRange(0, numberOfQuestions);
    }
}