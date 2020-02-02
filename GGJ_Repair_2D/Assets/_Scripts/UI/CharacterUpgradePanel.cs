using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterUpgradePanel : MonoBehaviour
{
    public int characterID;
	public Stats stat;
    public Text nameLabel;

	private Slider progress;

    // Start is called before the first frame update
    void Start()
    {
		progress = GetComponentInChildren<Slider>();
		progress.maxValue = 50;
		progress.minValue = 0;

        if(nameLabel)
        {
            nameLabel.text = SkillStorage.GetName(characterID);
        }
	}

    // Update is called once per frame
    void Update()
    {
		int level = SkillStorage.GetLevel(characterID, stat);
		progress.value = level;
		Debug.Log(level);
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
    }
}
