
using System.Collections.Generic;

[System.Serializable]
public class Level
{
    public string levelName;
    public int levelNumber;
    public List<LevelStep> steps;

    public bool levelIsCompleted;
}
