using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {

	#region Const
	private const int BASE_UNIT_ACTIONS = 4;
	#endregion

	public static UnitManager instance = null;

	public Unit[] units;

	private void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(this);
		}
	}

	public void AttemptRepairs() {
		foreach(Unit u in units) {
			u.RunFixAction();
		}
	}

	public void ResetRemainingActions() {
		foreach (Unit u in units) {
			int remainingActions = u.GetRemainingActions();
			u.SetActionCount(remainingActions == BASE_UNIT_ACTIONS || remainingActions == BASE_UNIT_ACTIONS + 1 ? BASE_UNIT_ACTIONS + 1 : BASE_UNIT_ACTIONS);
		}
	}

	public bool AreActionsComplete() {
		bool allComplete = true;
		foreach (Unit u in units) {
			if (!u.AreActionsComplete()) allComplete = false;
		}
		return allComplete;
	}

	public void Kill(int unitNumber) {
		units[unitNumber - 1].Kill();
	}

	public void SetUnitPosition(int unitNumber, Vector2 position) {
		units[unitNumber - 1].SetPosition(position);
	}

	private void OnDestroy() {
		instance = null;
	}

	public void AddAction(int unitNumber, UnitAction action) {
		units[unitNumber - 1]?.AddAction(action);
	}

	public bool IsLastActionFixing(int unitNumber) {
		return units[unitNumber - 1].IsLastActionFixing();
	}

	public int GetRemainingActions(int unitNumber) {
		return units[unitNumber - 1].GetRemainingActions();
	}

	public int GetRemainingUnitsCount() {
		int count = 0;
		foreach (Unit u in units) {
			if (u.gameObject.activeInHierarchy) count++;
		}
		return count;
	}

}
