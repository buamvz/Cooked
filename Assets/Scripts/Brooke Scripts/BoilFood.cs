using UnityEngine;

public class BoilFood : MonoBehaviour
{
    [Header("Boiling")]
    [SerializeField] private float boilTime = 15f;

    [Header("Boiling Sprites")]
    [SerializeField] private Sprite boiledSprite;

    [Header("References")]
    [SerializeField] private DraggingFood draggingFood;
    [SerializeField] private BoiledPotOn boilingPot;

    private SpriteRenderer spriteRenderer;

    private float boilTimeRemaining;

    private bool inBoilingPot = false;
    private bool isBoiled = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        boilTimeRemaining = boilTime;

        //noodles cany be dragged until it is boiled/15 seconds happnes
        if (draggingFood != null)
        {
            draggingFood.enabled = false;
        }
    }

    private void Update()
    {
        if (inBoilingPot && !isBoiled)
        {
            if (boilingPot != null && boilingPot.IsHeatOn())
            {
                Boil();
            }
        }
    }

    private void Boil()
    {
        if (boilTimeRemaining > 0)
        {
            boilTimeRemaining -= Time.deltaTime;

            Debug.Log("Boiling... " + boilTimeRemaining.ToString("F1"));
        }
        else
        {
            FinishBoiling();
        }
    }

    private void FinishBoiling()
    {
        isBoiled = true;

        Debug.Log(gameObject.name + " is fully boiled");

        //change to the boiled noodle sprite to the loosened noodles spirte
        if (boiledSprite != null)
        {
            spriteRenderer.sprite = boiledSprite;
        }

        //stops the pot positiotn from cathcing the noodles as the player moves them
        transform.SetParent(null, true);

        Debug.Log(gameObject.name + " released from boiling pot");
        //lets the player to drag the boiled noodles

        if (draggingFood != null)
        {
            draggingFood.enabled = true;

            Debug.Log(gameObject.name + " dragging enabled!");
        }

    }

    //noodles placed the boiling pot
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BoilingPot"))
        {
            inBoilingPot = true;

            Debug.Log(gameObject.name +" entered the boiling pot."
            );
        }
    }

    //noodles leave the boiling pot go then go to bowl
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BoilingPot"))
        {
            inBoilingPot = false;

            Debug.Log(gameObject.name +" left the boiling pot."
            );
        }
    }

    public bool IsBoiled()
    {
        return isBoiled;
    }
}
