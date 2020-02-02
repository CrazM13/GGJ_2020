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
    }

    // Update is called once per frame
    void Update()
    {
		progress.value = (float)SkillStorage.GetLevel(characterID, stat) / 50f;

		if (Input.GetKeyDown(KeyCode.Space)) {
			SkillStorage.AddUpgradePoint();
		}

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
