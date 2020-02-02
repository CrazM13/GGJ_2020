using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameUI : MonoBehaviour
{

    public Button endTurnButton;

    public CharacterHUDPanel hero1HUD;
    public CharacterHUDPanel hero2HUD;
    public CharacterHUDPanel hero3HUD;
    public CharacterHUDPanel hero4HUD;

    public Text stageDisplay;
    public Text turnDisplay;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void HandleEndTurnButton()
    {
        SoundSystem.Instance.PlaySound(SoundEvents.Select);
        FindObjectOfType<TurnManager>()?.HandleEndTurnButton();
		SetEndTurnInteractable(false);

	}

    public void HandleMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public CharacterHUDPanel GetHeroHUD(int hero)
    {
        switch(hero)
        {
            case 1: return hero1HUD;
            case 2: return hero2HUD;
            case 3: return hero3HUD;
            case 4: return hero4HUD;
        }
        return null;
    }

    public void SetHeroHUDName(int hero, string i_name)
    {
        GetHeroHUD(hero).SetName(i_name);
    }

    public void AddHeroHUDMove(int hero)
    {
        GetHeroHUD(hero).AddMove();
    }

    public void AddHeroHUDRepair(int hero)
    {
        GetHeroHUD(hero).AddRepair();
    }

    public void ClearHeroHUDActions(int hero)
    {
        GetHeroHUD(hero).ClearActions();
    }

    public void ClearAllHeroHUDActions()
    {
        ClearHeroHUDActions(1);
        ClearHeroHUDActions(2);
        ClearHeroHUDActions(3);
        ClearHeroHUDActions(4);
    }

	public void SetEndTurnInteractable(bool interactable) {
		endTurnButton.interactable = interactable;
		if (!interactable) EventSystem.current.SetSelectedGameObject(null);
	}

}
