using UnityEngine;
using UnityEngine.InputSystem;

public class DragObject : MonoBehaviour
{
    private Vector3 mousePositionOffset;
    private bool isDragging;

    private Vector3 GetMouseWorldPosition()
    {
        Vector2 mousePixelPoint = Pointer.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePixelPoint);
        worldPos.z = transform.position.z;
        return worldPos;
    }

    // edit for new unity input system
    private void Update()
    {
       if (Pointer.current.press.wasPressedThisFrame)
        {
            Vector3 mousePos = GetMouseWorldPosition();
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null && hit.transform == transform)
            {
                isDragging = true;
                mousePositionOffset = transform.position - GetMouseWorldPosition();
            }
        }

       if (Pointer.current.press.wasReleasedThisFrame)
            isDragging = false;
        

       if (isDragging)
            transform.position = GetMouseWorldPosition() + mousePositionOffset;
    }
}
