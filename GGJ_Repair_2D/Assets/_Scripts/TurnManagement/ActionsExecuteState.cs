using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsExecuteState : ITurnState {

	public bool DidWin() {
		// CHECK TILES
		return TileManager.Instance.GetDisasterCount() <= 0;
	}

	public void End() {
		
	}

	public bool IsDone() {
		return UnitManager.instance.AreActionsComplete();
	}

	public void Start()
    {
        GameUI ui = GameObject.FindObjectOfType<GameUI>();
        if(ui)
        {
            ui.stageDisplay.text = "Stage - Actions Execute";
        }
        
	}

	public void Update() {
		UnitManager.instance.AttemptRepairs();
	}

}
