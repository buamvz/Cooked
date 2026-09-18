using UnityEngine;

public class BoiledPotOn : MonoBehaviour
{
    [Header("Stove Stuff")]
    [SerializeField] private Transform stovePosition;

    [SerializeField] private Collider2D potCollider;
    [SerializeField] private Collider2D stoveCollider;

    [Header("Heat Stuff")]
    [SerializeField] private FlameOn flameOnScript;
    private bool hasSnapped = false;

    private bool isPotOnFlame = false;

    private void Start()
    {
        isPotOnFlame = false;
        hasSnapped = false;
    }

    private void Update()
    {
        CheckPotPosition();
    }

    private void CheckPotPosition()
    {
        if (potCollider.IsTouching(stoveCollider))
        {
            isPotOnFlame = true;

            //only snap pot once
            if(!hasSnapped)
            {
                hasSnapped = true;
                //snap pot onto stove position
                transform.position = stovePosition.position;


                Debug.Log("Boiling pot snapped onto stove");
            }

            
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
        if (!isPotOnFlame)
            return false;

        if (flameOnScript == null)
            return false;

        return flameOnScript.isFlameOn;
    }
}
