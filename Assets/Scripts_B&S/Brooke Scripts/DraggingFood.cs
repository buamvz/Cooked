using UnityEngine;
using UnityEngine.InputSystem;

public class DraggingFood : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;

    private Vector3 offset;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        //TOUCH
        if (Touchscreen.current != null)
        {
            var touch = Touchscreen.current.primaryTouch;

            if (touch.press.wasPressedThisFrame)
            {
                Vector2 touchPosition = touch.position.ReadValue();
                TryStartDragging(touchPosition);
            }

            if (touch.press.isPressed && isDragging)
            {
                Vector2 touchPosition = touch.position.ReadValue();
                Drag(touchPosition);
            }

            if (touch.press.wasReleasedThisFrame)
            {
                isDragging = false;
            }
        }

        //MOUSE
        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                Vector2 mousePosition = Mouse.current.position.ReadValue();
                TryStartDragging(mousePosition);
            }

            if (Mouse.current.leftButton.isPressed && isDragging)
            {
                Vector2 mousePosition = Mouse.current.position.ReadValue();
                Drag(mousePosition);
            }

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                isDragging = false;
            }
        }
    }

    private void TryStartDragging(Vector2 screenPosition)
    {
        Vector3 worldPosition = ScreenToWorld(screenPosition);

        RaycastHit2D hit = Physics2D.Raycast(
            worldPosition,
            Vector2.zero
        );

        if (hit.collider != null)
        {
            Debug.Log("Dragging raycast hit: " + hit.collider.gameObject.name);

            if (hit.collider.gameObject == gameObject)
            {
                isDragging = true;

                offset = transform.position - worldPosition;

                Debug.Log("Started dragging " + gameObject.name);
            }
        }
    }

    private void Drag(Vector2 screenPosition)
    {
        Vector3 worldPosition = ScreenToWorld(screenPosition);

        transform.position = worldPosition + offset;
    }

    private Vector3 ScreenToWorld(Vector2 screenPosition)
    {
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(
            new Vector3(screenPosition.x, screenPosition.y, -mainCamera.transform.position.z));

        worldPosition.z = transform.position.z;

        return worldPosition;
    }
}