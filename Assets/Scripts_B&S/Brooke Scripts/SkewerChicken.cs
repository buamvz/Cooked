using UnityEngine;

public class SkewerChicken : MonoBehaviour
{
    [Header("Chicken Sprites")]
    [SerializeField] private Sprite chickenLessSprite;
    [SerializeField] private Sprite chickenLessLessSprite;

    private SpriteRenderer spriteRenderer;

    //keeps track of how many times this chicken has been picked up.
    private int pickupCount = 0;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void PickUpChicken()
    {
        pickupCount++;

        Debug.Log(gameObject.name + " has been picked up " + pickupCount + " times");

        ChangeSprite();
    }

    private void ChangeSprite()
    {
        if (spriteRenderer == null)
        {
            Debug.LogWarning(gameObject.name +" does not have a spriteRenderer");

            return;
        }

        //first
        if (pickupCount == 1)
        {
            if (chickenLessSprite != null)
            {
                spriteRenderer.sprite = chickenLessSprite;

                Debug.Log(gameObject.name +" changed to LESS chicken");
            }
            else
            {
                Debug.Log(gameObject.name +" is missing the Chicken Less sprite");
            }
        }

        //second
        else if (pickupCount == 2)
        {
            if (chickenLessLessSprite != null)
            {
                spriteRenderer.sprite = chickenLessLessSprite;

                Debug.Log(gameObject.name +" changed to LESS LESS chicken");
            }
            else
            {
                Debug.Log(gameObject.name +" is missing the Less Less sprite");
            }
        }

        //third
        else if (pickupCount >= 3)
        {
            Debug.Log(gameObject.name +" has been fully taken and is now setActive false");

            gameObject.SetActive(false);
        }
    }
}
