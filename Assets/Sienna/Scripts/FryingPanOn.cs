using UnityEngine;

public class FryingPanOn : MonoBehaviour
{
    [SerializeField] private FryingStepManager gameManager;
    [SerializeField] private bool isPanOnFlame;
    [SerializeField] private Transform stovePosition;

    [SerializeField] private Collider2D panCollider;
    [SerializeField] private Collider2D stoveCollider;

    [SerializeField] private DragObject panDragObjectScript;

    void Start()
    {
        gameManager.HideMeat();
        isPanOnFlame = false;
    }

    
    void Update()
    {
        if (isPanOnFlame)
        {
            panDragObjectScript.enabled = false;
            gameManager.ShowMeat();
        }

        PanOnFlame();
    }

    public void PanOnFlame()
    {
        if (panCollider.IsTouching(stoveCollider))
        {
            isPanOnFlame = true;
            gameObject.transform.position = stovePosition.position;
        }
    }
}
