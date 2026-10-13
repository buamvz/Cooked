using UnityEngine;

public class CookingSizzle : MonoBehaviour
{
    [SerializeField] private AudioSource sizzleAudio;

    [SerializeField] private Collider2D panCollider;
    [SerializeField] private Collider2D stoveCollider;

    [SerializeField] private FlameOn flameOn;

    void Update()
    {
        // Is the pan touching the stove?
        bool panOnStove = panCollider.IsTouching(stoveCollider);

        // Is the stove flame on?
        bool stoveOn = flameOn.isFlameOn;

        // Is any CookMeat food touching the pan?
        bool foodInPan = false;

        Collider2D[] objectsTouchingPan = new Collider2D[20];

        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;

        int count = panCollider.Overlap(filter, objectsTouchingPan);

        for (int i = 0; i < count; i++)
        {
            if (objectsTouchingPan[i].GetComponentInParent<CookMeat>() != null)
            {
                foodInPan = true;
                break;
            }
        }

        // Sizzle only when ALL three conditions are true
        if (panOnStove && stoveOn && foodInPan)
        {
            if (!sizzleAudio.isPlaying)
            {
                sizzleAudio.Play();
            }
        }
        else
        {
            if (sizzleAudio.isPlaying)
            {
                sizzleAudio.Stop();
            }
        }
    }
}