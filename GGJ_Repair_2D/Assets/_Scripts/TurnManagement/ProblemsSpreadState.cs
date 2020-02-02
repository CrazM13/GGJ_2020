using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemsSpreadState : ITurnState {

	public bool DidWin() {
		return false;
	}

	public bool IsDone() {
		return true;
	}

	public void Start() {
		if (TurnManager.instance.TurnNumber % 3 == 0) {
			TileManager.Instance.StartDisasterSpread();
		}
	}

	public void Update() {/*MT*/}
}
