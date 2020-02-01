﻿using System;
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
			NextTurnStage();
		}
	}

	private void NextTurnStage() {
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
