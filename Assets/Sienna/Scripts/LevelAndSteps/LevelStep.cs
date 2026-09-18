using System;

public enum Step
{
    Cutting,
    Frying,
    Boiling,
    Plating
}

// what a step needs
[System.Serializable] // inteface instead?
public class LevelStep
{
    public Step stepType;
    public bool isCompleted;
}

public interface IStepCompleter
{
    static event Action OnStepComplete;
}

//public interface ILevelStep
//{
//    bool isCompleted { get; set; }
//}


//state machine to determine step info
