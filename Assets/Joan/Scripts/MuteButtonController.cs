using UnityEngine;
using UnityEngine.UI;

public class MuteButtonController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button muteButton;
    [SerializeField] private Image buttonImage;

    [Header("Icons")]
    [SerializeField] private Sprite muteIcon;
    [SerializeField] private Sprite soundOnIcon;

    private bool isMuted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
      if (muteButton == null || buttonImage == null ||
          muteIcon == null || soundOnIcon == null)
          {
            Debug.LogError("Assign all MuteButtonController references.", this);
            enabled = false;
            return;
          }  
          isMuted = AudioListener.pause;
          UpdateIcon();

          muteButton.onClick.AddListener(ToggleMute);
    }

    private void ToggleMute()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        buttonImage.sprite = isMuted ? muteIcon : soundOnIcon;
    }
    private void OnDestroy()
    {
        if (muteButton != null)
            muteButton.onClick.RemoveListener(ToggleMute);
    }
}
