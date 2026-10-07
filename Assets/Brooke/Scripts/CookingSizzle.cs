using UnityEngine;

public class CookingSizzle : MonoBehaviour
{
    public AudioSource sizzleAudio;

    public bool foodInPan = false;
    public bool stoveOn = false;

    void Update()
    {
        if (foodInPan && stoveOn)
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

    public void FoodPlacedInPan()
    {
        foodInPan = true;
    }

    public void FoodRemovedFromPan()
    {
        foodInPan = false;
    }

    public void TurnStoveOn()
    {
        stoveOn = true;
    }

    public void TurnStoveOff()
    {
        stoveOn = false;
    }
}