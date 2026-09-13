using UnityEngine;
using UnityEngine.InputSystem;

public class CuttingFood : MonoBehaviour
{
    [Header("Cutting")]
    [SerializeField] private Sprite[] cutSprites;
    [SerializeField] private int requiredCuts = 3;

    private DraggingFood draggingFood;
    private SpriteRenderer spriteRenderer;

    private int currentCuts = 0;

    private Camera mainCamera;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        draggingFood = GetComponent<DraggingFood>();
        mainCamera = Camera.main;

        // Make sure dragging is disabled until the food is completely cut
        if (draggingFood != null)
        {
            draggingFood.enabled = false;
        }
    }

    private void Update()
    {
        //Mobile: real game play
        if (Touchscreen.current != null)
        {
            var touch = Touchscreen.current.primaryTouch;

            if (touch.press.wasPressedThisFrame)
            {
                Vector2 touchPosition = touch.position.ReadValue();

                TryCut(touchPosition);
            }
        }

        //PC: testing on computer
        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Vector2 mousePosition = Mouse.current.position.ReadValue();

                TryCut(mousePosition);
            }
        }
    }

    private void TryCut(Vector2 screenPosition)
    {
        // Convert screen position to world position
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(
            new Vector3(
                screenPosition.x,
                screenPosition.y,
                -mainCamera.transform.position.z
            )
        );

        // Check what was clicked/tapped
        Collider2D hit = Physics2D.OverlapPoint(worldPosition);

        if (hit != null && hit.gameObject == gameObject)
        {
            Debug.Log("Food clicked/tapped!");

            CutFood();
        }
    }

    private void CutFood()
    {
        if (currentCuts >= requiredCuts)
            return;

        currentCuts++;

        Debug.Log("Current cuts: " + currentCuts);

        if (cutSprites.Length > 0)
        {
            int spriteIndex = Mathf.Clamp(
                currentCuts - 1,
                0,
                cutSprites.Length - 1
            );

            Debug.Log(
                "Changing to sprite: " +
                cutSprites[spriteIndex].name
            );

            spriteRenderer.sprite = cutSprites[spriteIndex];
        }

        if (currentCuts >= requiredCuts)
        {
            FinishedCutting();
        }
    }

    private void FinishedCutting()
    {
        Debug.Log(gameObject.name + " is fully cut!");

        if (draggingFood != null)
        {
            draggingFood.enabled = true;
        }
    }
}