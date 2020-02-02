using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemsSpreadState : ITurnState {

	public bool DidWin() {
		return false;
	}

	public bool IsDone() {
		return !TileManager.Instance.AreDisastersSpreading;
	}

	public void Start() {
		if (TurnManager.instance.TurnNumber % 4 == 0) {
			TileManager.Instance.StartDisasterSpread();
		}

        GameUI ui = GameObject.FindObjectOfType<GameUI>();
        if(ui)
        {
            ui.stageDisplay.text = "Stage - Problems Spread";
        }
	}

	public void Update() {/*MT*/}

	public void End() {
		if (UnitManager.instance.GetRemainingUnitsCount() <= 0) {
			GameManager.Instance.EndGame();
		}
	}
}
