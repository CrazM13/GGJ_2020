using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IssueCommandState : ITurnState {

	

	public bool DidWin() {
		return false;
	}

	public bool IsDone() {
		return false;
	}

	public void Start() {
		UnitManager.instance.ResetRemainingActions();
	}

	public void Update() {
		
	}

}
