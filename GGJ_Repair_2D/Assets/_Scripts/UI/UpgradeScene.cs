using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeScene : MonoBehaviour
{
    public string mainLevelScene = "TestScene";
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
        SceneManager.LoadScene("MainMenu");
    }

    public void HandleContinueButton()
    {
        SceneManager.LoadScene(mainLevelScene);
    }
}
