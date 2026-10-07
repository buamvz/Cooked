
[System.Serializable]
public class PlayerProfile
{
    public string playerName;
    private int completedLevels;
    private int totalScore;

    public int getCompletedLevels()
    {
        return completedLevels;
    }
    public int setCompletedLevels(int levels)
    {
        completedLevels = levels;
        return completedLevels;
    }
    public int getTotalScore()
    {
        return totalScore;
    }
    public int setTotalScore(int score)
    {
        totalScore = score;
        return totalScore;
    }

    // methods for completing levels and adding score
    public void addCompletedLevel()
    {
        completedLevels++;
    }

    public void addScore(int score)
    {
        totalScore += score;
    }

}
