using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButtons : MonoBehaviour
{
    void Start()
    {
        bool auxMusic = AudioManager.Instance.isMusicOn;
        bool auxSFX = AudioManager.Instance.isSfxOn;

        Debug.Log(AudioManager.Instance.isMusicOn);
        switch (gameObject.name)
        {
            case "Music On Button":
                gameObject.SetActive(auxMusic);
                break;
            case "Music Off Button":
                gameObject.SetActive(!auxMusic);
                break;
            default:
            case "SFX On Button":
                gameObject.SetActive(auxSFX);
                break;
            case "SFX Off Button":
                gameObject.SetActive(!auxSFX);
                break;
        }
    }
    public void SwitchMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }
    public void SwitchSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }
}