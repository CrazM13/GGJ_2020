using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    public GameObject Panel;

    // Start is called before the first frame update
    void Start()
    {
        Panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnFullscreenToggled(bool isOn)
    {
        Screen.fullScreen = isOn;

        SoundSystem.Instance.PlaySound(SoundEvents.Select);
    }

    public void OnVolumeSliderChanged(float newVal)
    {
        AudioListener.volume = newVal;
        SoundSystem.Instance.audioSource.volume = newVal;
        SoundSystem.Instance.PlaySound(SoundEvents.Select);
    }

    public void OnMusicVolumeSliderChanged(float newVal)
    {
        MusicPlayer.Current.audioSource.volume = newVal;
    }

    public void HandleCloseButton()
    {
        SoundSystem.Instance.PlaySound(SoundEvents.Select);
        Panel.SetActive(false);
    }
}
