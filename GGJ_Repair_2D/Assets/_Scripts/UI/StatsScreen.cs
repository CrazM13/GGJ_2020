using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatsScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleMainMenuButton()
    {
		GameManager.Instance.QuitToMenu();
    }

    public void HandleContinueButton()
    {
		GameManager.Instance.ShowUpgradeScreen();
    }
}
