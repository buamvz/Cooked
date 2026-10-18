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
    //[SerializeField] private Sprite chickenLessSprite;
    //[SerializeField] private Sprite chickenLessLessSprite;

    private bool hasPickedUpChicken = false;
    private bool hasBeenPlacedInBowl = false;

    //moved counting each hit on chicken to skewerchicken script
    private void Start()
    {
        if (skewerSpriteRenderer == null)
        {
            skewerSpriteRenderer = GetComponent<SpriteRenderer>();
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

        Debug.Log("skewer picked up chicken from " + chicken.name);

        //changes skewer sprite
        if (skewerSpriteRenderer != null && chickenSkewerSprite != null)
        {
            skewerSpriteRenderer.sprite = chickenSkewerSprite;

            Debug.Log("skewer changed to chicken skewer");
        }

        //tells  chicken that it has been picked up
        SkewerChicken skewerChicken = chicken.GetComponent<SkewerChicken>();

        if (skewerChicken != null)
        {
            skewerChicken.PickUpChicken();
        }
        else
        {
            Debug.LogWarning(chicken.name + " missing skewerChicken component");
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
