using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterUpgradePanel : MonoBehaviour
{
    public int characterID;
	public Stats stat;
    public Text nameLabel;
    public Image portrait;

	private Slider progress;

    public Sprite hero1sprite;
    public Sprite hero2sprite;
    public Sprite hero3sprite;
    public Sprite hero4sprite;

    public Sprite deadSprite;

    // Start is called before the first frame update
    void Start()
    {
		progress = GetComponentInChildren<Slider>();
		progress.maxValue = 50;
		progress.minValue = 0;

        if(hero1sprite && nameLabel)
        {
            nameLabel.text = SkillStorage.GetName(characterID);
        }

        if (portrait)
        {
            switch (characterID)
            {
                case 1:
                    portrait.sprite = hero1sprite;
                    break;
                case 2:
                    portrait.sprite = hero2sprite;
                    break;
                case 3:
                    portrait.sprite = hero3sprite;
                    break;
                case 4:
                    portrait.sprite = hero4sprite;
                    break;
            }
        }

        if(!GameManager.Instance.IsUnitAvailable(characterID))
        {
            portrait.sprite = deadSprite;
        }
	}

    // Update is called once per frame
    void Update()
    {
		int level = SkillStorage.GetLevel(characterID, stat);
		progress.value = level;
    }

    public void HandleStatChange(bool increase)
    {
        // attempt increase
        if(increase)
        {
			SkillStorage.AddTmpPoint(characterID, stat);
        }
        else
        {
			SkillStorage.RemoveTmpPoint(characterID, stat);
		}

        SoundSystem.Instance.PlaySound(SoundEvents.Select);
    }
}
