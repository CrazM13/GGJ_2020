using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleNewGameButton()
    {
       
        CreateNewCampaign();
        SoundSystem.Instance.PlaySound(SoundEvents.Select);
    }

    public void HandleOptionsButton()
    {
        OptionsPanel options = FindObjectOfType<OptionsPanel>();
        if(options)
        {
            options.Panel.SetActive(true);
        }

        SoundSystem.Instance.PlaySound(SoundEvents.Select);
    }

    public void HandleQuitButton()
    {
        Debug.Log("Quit button pressed.");
        Application.Quit();
    }

    void CreateNewCampaign()
    {
        string name1 = RandomNameGenerator.GetRandomName();
        string name2 = RandomNameGenerator.GetRandomName();
        string name3 = RandomNameGenerator.GetRandomName();
        string name4 = RandomNameGenerator.GetRandomName();

        Debug.Log("Player 1 is " + name1);
        Debug.Log("Player 2 is " + name2);
        Debug.Log("Player 3 is " + name3);
        Debug.Log("Player 4 is " + name4);


        // todo: skill point resets?

        SkillStorage.SetName(1, name1);
        SkillStorage.SetName(2, name2);
        SkillStorage.SetName(3, name3);
        SkillStorage.SetName(4, name4);

		GameManager.Instance.ResetDeath();
		GameManager.Instance.ShowUpgradeScreen();
    }

    public void HandleCreditsScreen()
    {
		GameManager.Instance.ShowCreditScreen();
    }
}
