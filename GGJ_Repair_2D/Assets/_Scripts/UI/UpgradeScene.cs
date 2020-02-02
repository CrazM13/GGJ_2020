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

	}

    public void HandleBackButton()
    {
		SkillStorage.Cancel();
		GameManager.Instance.QuitToMenu();
    }

    public void HandleContinueButton()
    {
		SkillStorage.Confirm();
		GameManager.Instance.StartGame();
    }
}
