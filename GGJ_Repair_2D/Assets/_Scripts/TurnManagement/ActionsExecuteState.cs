using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsExecuteState : ITurnState {

	public bool DidWin() {
		// CHECK TILES
		return false;
	}

	public bool IsDone() {
		return UnitManager.instance.AreActionsComplete();
	}

	public void Start() {
		UnitManager.instance.AttemptRepairs();
	}

	public void Update() {/*MT*/}

}
