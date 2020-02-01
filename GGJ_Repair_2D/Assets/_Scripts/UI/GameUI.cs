using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public Transform peopleContainer;
    public GameObject peopleImageTemplate;

    public Transform problemContainer;
    public GameObject problemImageTemplate;

    public Button endTurnButton;

    // Start is called before the first frame update
    void Start()
    {
        // hide the template
        peopleImageTemplate.SetActive(false);
        problemImageTemplate.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateNumPeople(int newNumber)
    {
        for (int i = 0; i < peopleContainer.childCount; i++)
        {
            Transform child = peopleContainer.GetChild(i);
            if(child.gameObject != peopleImageTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        for(int i = 0; i < newNumber; i++)
        {
            GameObject goNew = GameObject.Instantiate<GameObject>(peopleImageTemplate, peopleContainer, worldPositionStays: false);
            goNew.SetActive(true);
        }
    }

    public void UpdateNumProblems(int newNumber)
    {
        for (int i = 0; i < problemContainer.childCount; i++)
        {
            Transform child = problemContainer.GetChild(i);
            if (child.gameObject != problemImageTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        for (int i = 0; i < newNumber; i++)
        {
            GameObject goNew = GameObject.Instantiate<GameObject>(problemImageTemplate, problemContainer, worldPositionStays: false);
            goNew.SetActive(true);
        }
    }


    [ContextMenu("Test set number 1")]
    public void TestSetNumbers1()
    {
        UpdateNumPeople(3);
        UpdateNumProblems(4);
    }


    public void HandleEndTurnButton()
    {
        FindObjectOfType<TurnManager>()?.HandleEndTurnButton();
    }

    public void HandleMainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
