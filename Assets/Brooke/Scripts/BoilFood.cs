using UnityEngine;

public class BoilFood : MonoBehaviour
{
    [Header("Boiling")]
    [SerializeField] private float boilTime = 15f;

    [Header("Boiling Sprites")]
    [SerializeField] private Sprite[] boilSprites;

    [Header("References")]
    [SerializeField] private DraggingFood draggingFood;

    private SpriteRenderer spriteRenderer;

    private float boilTimeRemaining;

    private bool inBoilingPot = false;
    private bool isBoiling = false;
    private bool isBoiled = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        boilTimeRemaining = boilTime;

        if (draggingFood != null)
        {
            draggingFood.enabled = false;
        }
    }

    private void Update()
    {
        //tells tge noodles that heat is turned on
        if (inBoilingPot && isBoiling && !isBoiled)
        {
            Boil();
        }
    }

    private void Boil()
    {
        if (boilTimeRemaining > 0)
        {
            boilTimeRemaining -= Time.deltaTime;

            Debug.Log("Boiling... " + boilTimeRemaining.ToString("F1") + " seconds remaining");

            UpdateBoilSprite();
        }
        else
        {
            FinishBoiling();
        }
    }

    private void UpdateBoilSprite()
    {
        if (boilSprites.Length == 0)
            return;

        //timing how much the food is boiled
        float progress = 1f - (boilTimeRemaining / boilTime);

        int spriteIndex = Mathf.FloorToInt(
            progress * boilSprites.Length
        );

        spriteIndex = Mathf.Clamp(
            spriteIndex,
            0,
            boilSprites.Length - 1
        );

        spriteRenderer.sprite = boilSprites[spriteIndex];
    }

    private void FinishBoiling()
    {
        isBoiled = true;

        Debug.Log(gameObject.name + " is fully boiled!");

        //make sure the final sprite is shown/changed to
        if (boilSprites.Length > 0)
        {
            spriteRenderer.sprite =
                boilSprites[boilSprites.Length - 1];
        }

        //now the player to drag the boiled food to the bowl
        if (draggingFood != null)
        {
            draggingFood.enabled = true;
        }
    }

    //when food enters the boiling pot
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BoilingPot"))
        {
            inBoilingPot = true;

            Debug.Log(gameObject.name + " entered the boiling pot.");
        }
    }

    // when food leaves the boiling pot
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BoilingPot"))
        {
            inBoilingPot = false;

            Debug.Log(gameObject.name + " left the boiling pot.");
        }
    }

    //the boiling pot when the heat is turned on/off
    public void SetBoiling(bool boiling)
    {
        isBoiling = boiling;

        if (boiling)
        {
            Debug.Log(gameObject.name + " has started boiling.");
        }
        else
        {
            Debug.Log(gameObject.name + " has stopped boiling.");
        }
    }

    public bool IsBoiled()
    {
        return isBoiled;
    }
}
