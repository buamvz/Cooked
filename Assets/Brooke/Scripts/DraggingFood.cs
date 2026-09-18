using UnityEngine;
using UnityEngine.InputSystem;

public class DraggingFood : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;

    private float zDistance;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.isPressed)
        {
            Vector2 touchPosition =
                Touchscreen.current.primaryTouch.position.ReadValue();

            HandleDrag(touchPosition);
        }
        else if (Mouse.current != null &&
                 Mouse.current.leftButton.isPressed)
        {
            Vector2 mousePosition =
                Mouse.current.position.ReadValue();

            HandleDrag(mousePosition);
        }
        else
        {
            isDragging = false;
        }
    }

    private void HandleDrag(Vector2 screenPosition)
    {
        Vector3 worldPosition =
            mainCamera.ScreenToWorldPoint(
                new Vector3(screenPosition.x, screenPosition.y, zDistance)
            );

        worldPosition.z = transform.position.z;

        if (!isDragging)
        {
            RaycastHit2D hit =
                Physics2D.Raycast(worldPosition, Vector2.zero);

            if (hit.collider != null &&
                hit.collider.gameObject == gameObject)
            {
                isDragging = true;
            }
        }

        if (isDragging)
        {
            transform.position = worldPosition;
        }
    }
}