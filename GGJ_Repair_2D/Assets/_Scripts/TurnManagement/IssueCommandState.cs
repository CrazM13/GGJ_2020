using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IssueCommandState : ITurnState {

	private int selectedUnit = 0;

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

	private void OnClick() {
		if (selectedUnit <= 0 || selectedUnit > 4) {
			SelectUnit();
		} else {
			AttemptAction();
		}
	}

	private void SelectUnit() {
		//selectedUnit = TileManager.
	}

	private void AttemptAction() {

	}


}
