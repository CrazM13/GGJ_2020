using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHUDPanel : MonoBehaviour
{
    public int characterID = 1;

    public Text nameLabel;
    public Image portrait;

    public Image action1;
    public Image action2;
    public Image action3;
    public Image action4;

    public Sprite moveSprite;
    public Sprite repairSprite;

    public Sprite hero1Sprite;
    public Sprite hero2Sprite;
    public Sprite hero3Sprite;
    public Sprite hero4Sprite;
    

    // Start is called before the first frame update
    void Start()
    {
        switch(characterID)
        {
            case 1:
                portrait.sprite = hero1Sprite;
                break;
            case 2:
                portrait.sprite = hero2Sprite;
                break;
            case 3:
                portrait.sprite = hero3Sprite;
                break;
            case 4:
                portrait.sprite = hero4Sprite;
                break;
        }

        SetName(SkillStorage.GetName(characterID));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Image FindNextAvailableActionSlot()
    {
        if (action1.sprite == null)
        {
            return action1;
        }

        if(action2.sprite == null)
        {
            return action2;
        }

        if(action3.sprite == null)
        {
            return action3;
        }

        if(action4.sprite == null)
        {
            return action4;
        }

        return null;//no more room!
    }

    public void AddMove()
    {
        Image slot = FindNextAvailableActionSlot();
        if(slot)
        {
            slot.sprite = moveSprite;
            slot.enabled = true;
        }
    }

    public void AddRepair()
    {
        Image slot = FindNextAvailableActionSlot();
        if (slot)
        {
            slot.sprite = repairSprite;
            slot.enabled = true;
        }
    }

    public void ClearActions()
    {
        action1.sprite = null;
        action2.sprite = null;
        action3.sprite = null;
        action4.sprite = null;

        action1.enabled = false;
        action2.enabled = false;
        action3.enabled = false;
        action4.enabled = false;
    }

    public void SetName(string i_name)
    {
        nameLabel.text = i_name;
    }
}
