using UnityEngine;

public class BoiledPotOn : MonoBehaviour
{
    [Header("Stove Stuff")]
    [SerializeField] private Transform stovePosition;

    [SerializeField] private Collider2D potCollider;
    [SerializeField] private Collider2D stoveCollider;

    [Header("Heat Stuff")]
    [SerializeField] private FlameOn flameOnScript;

    private bool isPotOnFlame = false;

    private void Start()
    {
        isPotOnFlame = false;
    }

    private void Update()
    {
        CheckPotPosition();
    }

    private void CheckPotPosition()
    {
        //checking wether the pot is touching the stove
        if (potCollider.IsTouching(stoveCollider))
        {
            isPotOnFlame = true;

            //snap pot to the stove
            transform.position = stovePosition.position;
        }
        else
        {
            isPotOnFlame = false;
        }
    }

    public bool IsPotOnFlame()
    {
        return isPotOnFlame;
    }

    public bool IsHeatOn()
    {
        return isPotOnFlame && flameOnScript.isFlameOn;
    }
}
