using UnityEngine;
using System.Collections.Generic;

public class SkewerFood : MonoBehaviour
{
    [Header("Skewer Sprites")]
    [SerializeField] private SpriteRenderer skewerSpriteRenderer;

    //[SerializeField] private Sprite emptySkewerSprite;
    [SerializeField] private Sprite chickenSkewerSprite;
    [SerializeField] private Sprite finalSkewerSprite;

    [Header("Chickens")]
    [SerializeField] private List<GameObject> chickens;
    [SerializeField] private Sprite chickenLessSprite;
    [SerializeField] private Sprite chickenLessLessSprite;

    private bool hasPickedUpChicken = false;
    private bool hasBeenPlacedInBowl = false;

    //keeps track of how many times each chicken has been picked up so for different sprite changes
    private Dictionary<GameObject, int> chickenPickupCounts =
        new Dictionary<GameObject, int>();
    private void Start()
    {
        if (skewerSpriteRenderer == null)
        {
            skewerSpriteRenderer = GetComponent<SpriteRenderer>();
        }

        //starts every chicken at 0 pickups from skewer
        foreach (GameObject chicken in chickens)
        {
            if (chicken != null)
            {
                chickenPickupCounts[chicken] = 0;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if the skewer already has chicken and has not reached the bowl then cant pick up anymore chicken
        if (hasBeenPlacedInBowl)
            return;

        //checks if the skewer has hit one of the chicken objects.
        foreach (GameObject chicken in chickens)
        {
            if (chicken == null)
                continue;

            //check if the object we hit is this chicken.
            if (other.gameObject == chicken)
            {

                if (hasPickedUpChicken)
                {
                    Debug.Log("this skewer already has chicken");
                    return;
                }
                    

                PickUpChicken(chicken);
                return;
            }
        }

        //if the skewer has chicken and hits the bowl change it to the final sprite
        if (hasPickedUpChicken && other.CompareTag("Bowl"))
        {
            PlaceInBowl();
        }
    }

    private void PickUpChicken(GameObject chicken)
    {
        hasPickedUpChicken = true;

        Debug.Log("skewer picked up: " + chicken.name);

        //change the skewer to the version with chicken
        if (skewerSpriteRenderer != null && chickenSkewerSprite != null)
        {
            skewerSpriteRenderer.sprite = chickenSkewerSprite;
            Debug.Log("skewer sprite changed to chicken skewer");
        }

        //makeing this chicken has a pickup count.
        if (!chickenPickupCounts.ContainsKey(chicken))
        {
            chickenPickupCounts[chicken] = 0;
        }

        //increase this specific chicken's pickup count so it stays individual
        chickenPickupCounts[chicken]++;

        int pickupCount = chickenPickupCounts[chicken];

        Debug.Log(chicken.name + " has been picked up " + pickupCount +" times");
        ChangeChickenSprite(chicken, pickupCount);

    }

    private void ChangeChickenSprite(GameObject chicken, int pickupCount)
    {
        SpriteRenderer chickenRenderer =
            chicken.GetComponent<SpriteRenderer>();

        if (chickenRenderer == null)
        {
            Debug.LogWarning(chicken.name + " does not have a spriteRenderer");

            return;
        }

        //first
        if (pickupCount == 1)
        {
            if (chickenLessSprite != null)
            {
                chickenRenderer.sprite = chickenLessSprite;

                Debug.Log(chicken.name + " changed to less chicken");
            }
            else
            {
                Debug.LogWarning("chicken Less Sprite has not been assigned");
            }
        }

        //second
        else if (pickupCount == 2)
        {
            if (chickenLessLessSprite != null)
            {
                chickenRenderer.sprite = chickenLessLessSprite;

                Debug.Log(
                    chicken.name +
                    " changed to less less chicken."
                );
            }
            else
            {
                Debug.LogWarning(
                    "Chicken Less Less Sprite has not been assigned."
                );
            }
        }

        //third
        else if (pickupCount >= 3)
        {
            chicken.SetActive(false);

            Debug.Log(chicken.name + " has been fully taken and disappeared");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void HandleCollision(GameObject other)
    {
        if (hasBeenPlacedInBowl)
            return;

        //check chickens
        foreach (GameObject chicken in chickens)
        {
            if (chicken == null)
                continue;

            if (other == chicken)
            {
                if (hasPickedUpChicken)
                {
                    Debug.Log("this skewer already has chicken");
                    return;
                }

                PickUpChicken(chicken);
                return;
            }
        }

        //check bowl
        if (hasPickedUpChicken && other.CompareTag("Bowl"))
        {
            PlaceInBowl();
        }
    }

    private void PlaceInBowl()
    {
        hasBeenPlacedInBowl = true;

        Debug.Log("skewer placed in bowl");

        if (skewerSpriteRenderer != null && finalSkewerSprite != null)
        {
            skewerSpriteRenderer.sprite = finalSkewerSprite;

            Debug.Log("skewer changed to final sprite");
        }
    }
}
