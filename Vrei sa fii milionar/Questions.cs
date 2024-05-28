public class Question
{
    public string Text { get; set; }
    public string OptionA { get; set; }
    public string OptionB { get; set; }
    public string OptionC { get; set; }
    public string OptionD { get; set; }
    public char CorrectAnswer { get; set; }
    public string Difficulty { get; set; }  // Adaugam dificultatea

    public Question() { }

    public Question(string text, string optionA, string optionB, string optionC, string optionD, char correctAnswer, string difficulty)
    {
        Text = text;
        OptionA = optionA;
        OptionB = optionB;
        OptionC = optionC;
        OptionD = optionD;
        CorrectAnswer = correctAnswer;
        Difficulty = difficulty;
    }
}
