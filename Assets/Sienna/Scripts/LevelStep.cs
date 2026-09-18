using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Step
{
    Cutting,
    Frying,
    Boiling,
    Plating
}

// what a step needs
[System.Serializable]
public class LevelStep
{
    public Step stepType;
    public Scene scene;
    public bool isCompleted;
}

//state machine to determine step info
