using UnityEngine;

public class DoNotDestroyObject : MonoBehaviour
{
    public static DoNotDestroyObject Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return; 
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
