
using System.Collections.Generic;

[System.Serializable]
public class PlayerProfile
{
    public string playerName;
    private int completedLevels;
    private int totalScore;
    private Dictionary<int, int> bestStarsPerLevel = new Dictionary<int, int>();

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

    // Backwards-compatible method (keeps previous behaviour)
    public void addScore(int score)
    {
        totalScore += score;
    }

    // Update the best stars for a given level. Returns the additional stars added to totalScore (0 if not improved).
    public int UpdateBestStarsForLevel(int levelNumber, int stars)
    {
        if (levelNumber <= 0)
            return 0;

        bestStarsPerLevel.TryGetValue(levelNumber, out var previousBest);
        if (stars > previousBest)
        {
            var delta = stars - previousBest;
            bestStarsPerLevel[levelNumber] = stars;
            totalScore += delta;
            return delta;
        }

        return 0;
    }

    public int GetBestStarsForLevel(int levelNumber)
    {
        if (levelNumber <= 0)
            return 0;
        bestStarsPerLevel.TryGetValue(levelNumber, out var v);
        return v;
    }

    // For saving to Cloud Save: string keys are easier to serialize
    public Dictionary<string, int> GetBestStarsForSave()
    {
        var dict = new Dictionary<string, int>();
        foreach (var kvp in bestStarsPerLevel)
        {
            dict[kvp.Key.ToString()] = kvp.Value;
        }
        return dict;
    }

    public void SetBestStarsFromSave(Dictionary<string, int> dict)
    {
        bestStarsPerLevel.Clear();
        if (dict == null)
            return;
        foreach (var kvp in dict)
        {
            if (int.TryParse(kvp.Key, out var levelNum))
            {
                bestStarsPerLevel[levelNum] = kvp.Value;
            }
        }
        // Recompute totalScore from bestStars to keep consistency
        var sum = 0;
        foreach (var v in bestStarsPerLevel.Values)
            sum += v;
        totalScore = sum;
    }

    // clear stars button, mainly for testing, but will reset player profile data
    public void ClearBestStars()
    {
        bestStarsPerLevel.Clear();
        totalScore = 0;
    }

}
