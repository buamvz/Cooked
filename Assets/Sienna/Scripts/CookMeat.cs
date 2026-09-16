using UnityEngine;

public class CookMeat : MonoBehaviour
{
    public MeatType meatType;

    public enum MeatType
    {
        Beef,
        Chicken
    }

    [SerializeField] private Color rawColour;
    [SerializeField] private Color cookedColour;

    private GameObject meat;
    private SpriteRenderer spriteRenderer;
    //[SerializeField] private Collider2D fryingPanCollider;

    public float cookTimeRemaining;
    private bool inFryingPan;
    public bool isCooked;

    [SerializeField] private FlameOn flameOnScript;

    void Start()
    {
        SetInitialCookTime();

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the meat is in the frying pan and the flame is turned on -> the meat can cook
        if (inFryingPan && flameOnScript.isFlameOn)
        {
            Cook();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FryingPan"))
        {
            inFryingPan = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FryingPan"))
        {
            inFryingPan = false;
        }
    }

    public float GetInitialCookTime()
    {
        isCooked = false;

        switch (meatType)
        {
            case MeatType.Beef:
                return 5f;
            case MeatType.Chicken:
                return 10f;
            default:
                return 0;
        }
    }

    public void SetInitialCookTime()
    {
        cookTimeRemaining = GetInitialCookTime();
    }

    public void Cook()
    {
        if (cookTimeRemaining > 0)
        {
            cookTimeRemaining -= Time.deltaTime;
            spriteRenderer.color = rawColour;
        }
        else
        {
            spriteRenderer.color = cookedColour;
            isCooked = true;
        }
    }

}
