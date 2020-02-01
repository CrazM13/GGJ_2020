using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

	public static TurnManager instance = null;
	private List<ITurnState> turnStates;
	private int turnStage = 0;

	public int TurnNumber { get; private set; } = 0;

	private void Awake() {
		if (!instance) {
			instance = this;
		} else {
			Destroy(this);
		}
	}

	private void Start() {
		turnStates = new List<ITurnState> {
			new IssueCommandState(),
			new ActionsExecuteState(),
			new ProblemsSpreadState()
		};

	}

	private void Update() {
		turnStates[turnStage].Update();
		bool isDone = turnStates[turnStage].IsDone();

		if (isDone) {
			if (turnStates[turnStage].DidWin()) {
				GameManager.Instance.ShowUpgradeScreen();
			} else {
				NextTurnStage();
			}
		}
	}

	private void NextTurnStage() {
		Debug.Log($"CHANGE FROM STAGE {turnStage} TO STAGE {(turnStage + 1) % turnStates.Count}");
		turnStage++;

		if (turnStage >= turnStates.Count) {
			TurnNumber++;
			turnStage = 0;
		}

		turnStates[turnStage].Start();
	}

	public void HandleEndTurnButton() {
		NextTurnStage();
	}

}
