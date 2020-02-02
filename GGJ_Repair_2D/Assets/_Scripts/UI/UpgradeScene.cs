using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeScene : MonoBehaviour
{

	public Text remainingPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		remainingPoints.text = SkillStorage.GetRemainingPoints().ToString();

		if (Input.GetKeyDown(KeyCode.Space)) {
			SkillStorage.AddUpgradePoint();
		}

	}

    public void HandleBackButton()
    {
        SoundSystem.Instance.PlaySound(SoundEvents.Select);
        SkillStorage.Cancel();
		GameManager.Instance.QuitToMenu();
    }

    public void HandleContinueButton()
    {
        SoundSystem.Instance.PlaySound(SoundEvents.Select);
        SkillStorage.Confirm();
		GameManager.Instance.StartGame();
    }
}
