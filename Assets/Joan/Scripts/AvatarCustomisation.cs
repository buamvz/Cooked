using UnityEngine;
using UnityEngine.UI;

public class AvatarCustomisation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("UI Reference")]
    public GameObject avatarSelectionPanel;
    public Image currentAvatar;

    [Header("Avatar Options")]
    public Button[] avatarOptionButtons;
    public Sprite[] avatarSprites;
    public Button saveAvatarButton;

    private int selectedAvatarIndex = -1;

    private void Start()
    {
        avatarSelectionPanel.SetActive(false);

        for (int i = 0; i < avatarOptionButtons.Length; i++)
        {
            int index = i;

            avatarOptionButtons[i].onClick.AddListener(
                () => SelectAvatar(index)
            );
        }
        saveAvatarButton.onClick.AddListener(SaveAvatar);
    }
 
    // Update is called once per frame
    public void OpenAvatarSelection()
    {
        avatarSelectionPanel.SetActive(true);
    }
    public void SelectAvatar(int index)
    {
        if (index >= 0 && index < avatarSprites.Length)
        {
            selectedAvatarIndex = index;
            currentAvatar.sprite = avatarSprites[index];
            currentAvatar.enabled = true;
        }
    }
    public void SaveAvatar()
    {
        if (selectedAvatarIndex >= 0 && 
            selectedAvatarIndex < avatarSprites.Length)
            {
                currentAvatar.sprite = avatarSprites[selectedAvatarIndex];
                currentAvatar.enabled = true;
                avatarSelectionPanel.SetActive(false);
            }
    }
}
