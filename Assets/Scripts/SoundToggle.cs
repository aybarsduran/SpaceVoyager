using UnityEngine;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour
{
    public AudioSource audioSource;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public Image soundButtonImage;

    private bool isSoundOn = true;

    private void Start()
    {
        isSoundOn = true;
        soundButtonImage.sprite = soundOnSprite;
    }

    public void OnClick()
    {
        isSoundOn = !isSoundOn;

        if (isSoundOn)
        {
            audioSource.volume = 1f;
            soundButtonImage.sprite = soundOnSprite;
        }
        else
        {
            audioSource.volume = 0f;
            soundButtonImage.sprite = soundOffSprite;
        }
    }
}
