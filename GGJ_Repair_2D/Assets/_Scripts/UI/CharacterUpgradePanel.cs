using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterUpgradePanel : MonoBehaviour
{
    public int characterID;
	public Stats stat;

	private Slider progress;

    // Start is called before the first frame update
    void Start()
    {
		progress = GetComponentInChildren<Slider>();
		progress.maxValue = 50;
		progress.minValue = 0;
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
