using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatPanel : MonoBehaviour
{
    public int characterID;
    public Text statOutput;
    public Text nameLabel;
    public Image portraitHolder;

    public Sprite hero1Sprite;
    public Sprite hero2Sprite;
    public Sprite hero3Sprite;
    public Sprite hero4Sprite;
    public Sprite deadSprite;

    // Start is called before the first frame update
    void Start()
    {
        BuildStatOutput();

        nameLabel.text = (SkillStorage.GetName(characterID));

        switch (characterID)
        {
            case 1:
                portraitHolder.sprite = hero1Sprite;
                break;
            case 2:
                portraitHolder.sprite = hero2Sprite;
                break;
            case 3:
                portraitHolder.sprite = hero3Sprite;
                break;
            case 4:
                portraitHolder.sprite = hero4Sprite;
                break;
        }

        if(!GameManager.Instance.IsUnitAvailable(characterID))
        {
            portraitHolder.sprite = deadSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void BuildStatOutput()
    {
        string output = "";


        // TODO: Stats!!!!!


        statOutput.text = output;
    }
}
