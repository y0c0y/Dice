using System;

public class DicePlay
{
    public CharacterInfo Player;
    public CharacterInfo Npc;

    public string ScoreString(Enums.Score score)
    {
        switch (score)
        {
            case Enums.Score.Win:
                return TextData.WinString;
            case Enums.Score.Loss:
                return TextData.LoseString;
            case Enums.Score.Draw:
                return TextData.DrawString;
            default:
                return TextData.WrongString;
        }
    }
    
    public void ScoreUpdate(Enums.Score score)
    {
        switch (score)
        {
            case Enums.Score.Win:
                Player.Scores[(int)Enums.Score.Win]++;
                // Npc.Scores[(int)Enums.Score.Loss]++;
                break;
            case Enums.Score.Loss:
                Player.Scores[(int)Enums.Score.Loss]++;
                // Npc.Scores[(int)Enums.Score.Win]++;
                break;
            case Enums.Score.Draw:
                Player.Scores[(int)Enums.Score.Draw]++;
                // Npc.Scores[(int)Enums.Score.Draw]++;
                break;
            case Enums.Score.WrongAnswer:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(score), score, null);
        }
    }
    
    public Enums.Score CheckResult()
    {
        if(Player.State == Enums.DiceState.WrongAnswer) return Enums.Score.WrongAnswer;
        return Player.State == Npc.State ? Enums.Score.Win : Enums.Score.Loss;
    }
}
