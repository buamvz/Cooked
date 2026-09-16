using UnityEngine;
using UnityEngine.InputSystem;

public class FlameOn : MonoBehaviour
{
    [SerializeField] private Collider2D dialCollider;
    [SerializeField] public bool isFlameOn = false;
    [SerializeField] private GameObject flameObject;

    private SpriteRenderer dialSpriteRenderer;
    [SerializeField] private Sprite dialOnSprite;
    [SerializeField] private Sprite dialOffSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dialCollider = GetComponent<Collider2D>();
        dialSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        FlameToggle();

        if (isFlameOn)
        {
            flameObject.SetActive(true);
            dialSpriteRenderer.sprite = dialOnSprite;
        }
        else
        {
            flameObject.SetActive(false);
            dialSpriteRenderer.sprite = dialOffSprite;
        }
    }

    // Sienna - taking the mouse position reading from my drag object script
    // Here, it checks if the collider on the dial is clicked which toggles the isFlameOn bool on/off
    // private Vector3 mousePositionOffset;
    public void FlameToggle()
    {
        if (Pointer.current.press.wasPressedThisFrame)
        {
            Vector3 mousePos = GetMouseWorldPosition();
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider == dialCollider)
            {
                isFlameOn = !isFlameOn;
                // mousePositionOffset = transform.position - GetMouseWorldPosition();
            }
        }
    }
    private Vector3 GetMouseWorldPosition()
    {
        Vector2 mousePixelPoint = Pointer.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePixelPoint);
        worldPos.z = transform.position.z;
        return worldPos;
    }

}
