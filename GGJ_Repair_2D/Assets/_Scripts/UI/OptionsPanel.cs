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


    }

    public void OnVolumeSliderChanged(float newVal)
    {
        AudioListener.volume = newVal;
    }

    public void HandleCloseButton()
    {
        Panel.SetActive(false);
    }
}
