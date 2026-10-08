using TMPro;
using UnityEngine;

public class CookMeat : MonoBehaviour
{
    public MeatType meatType;

    public enum MeatType
    {
        Beef,
        Pork,
        Chicken
    }

    [SerializeField] private FlameOn flameOnScript;

    // changing from colours to meat sprites
    [SerializeField] private Sprite rawSprite;
    [SerializeField] private Sprite cookingSprite;
    [SerializeField] private Sprite cookedSprite;
    [SerializeField] private Sprite burntSprite;

    private GameObject meat;
    private SpriteRenderer spriteRenderer;
    //[SerializeField] private Collider2D fryingPanCollider;

    
    private bool inFryingPan;
    public bool isCooked;

    private float rawTime;
    private float cookingTime;
    private float burntTime;

    public float cookTimeRemaining;
    public float currentCookTime;



    void Start()
    {
        warningPanel.SetActive(false);
        warningIcon.SetActive(false);

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
        else
        {
            burning = false;
        }
        BurnWarning();
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
            case MeatType.Pork:
                return 7f;
            default:
                return 0;
        }
    }

    public void SetInitialCookTime()
    {
        cookTimeRemaining = GetInitialCookTime();
    }

    public void GetTimesBasedOnMeat()
    {
        switch (meatType)
        {
            case MeatType.Beef:
                rawTime = 3f;
                cookingTime = 5f;
                burntTime = 8f;
                break;
            case MeatType.Chicken:
                rawTime = 5f;
                cookingTime = 10f;
                burntTime = 15f;
                break;
            case MeatType.Pork:
                rawTime = 5f;
                cookingTime = 7f;
                burntTime = 10f;
                break;
            default:
                rawTime = 0f;
                cookingTime = 0f;
                burntTime = 0f;
                break;
        }
    }

    public void Cook()
    {
        GetTimesBasedOnMeat();

        if (currentCookTime > burntTime)
        {
            burning = false;
            spriteRenderer.sprite = burntSprite;
            isCooked = false;
        }
        else if (currentCookTime > cookingTime)
        {
            burning = true;
            spriteRenderer.sprite = cookedSprite;
            currentCookTime += Time.deltaTime;
            isCooked = true;
        }
        else if (currentCookTime > rawTime)
        {
            burning = false;
            spriteRenderer.sprite = cookingSprite;
            currentCookTime += Time.deltaTime;
        }
        else
        {
            burning = false;
            currentCookTime += Time.deltaTime;
        }

        //if (cookTimeRemaining > 0)
        //{
        //    cookTimeRemaining -= Time.deltaTime;
        //    spriteRenderer.sprite = rawSprite;
        //}
        //else
        //{
        //    spriteRenderer.sprite = cookedSprite;
        //    isCooked = true;
        //}
    }

    // burn warning from carleen
    public GameObject warningPanel;
    public GameObject warningIcon;
    public TMP_Text statusText;

    public bool burning;

    public float flashSpeed = 0.5f;
    private float flashTimer = 0f;
    public void BurnWarning()
    {
        if (burning)
        {
            warningPanel.SetActive(true);
            statusText.text = "WARNING! Food is almost burnt!";

            flashTimer += Time.deltaTime;

            if (flashTimer >= flashSpeed)
            {
                warningIcon.SetActive(!warningIcon.activeSelf);
                flashTimer = 0f;
            }
        }
        else
        {
            warningIcon.SetActive(false);
            warningPanel.SetActive(false);
        }
    }
}
