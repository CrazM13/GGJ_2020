using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleBackButton()
    {
		GameManager.Instance.QuitToMenu();
    }

    public void HandleContinueButton()
    {
		GameManager.Instance.StartGame();
    }
}
