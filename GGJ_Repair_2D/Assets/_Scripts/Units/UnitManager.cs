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
			u.ExecuteActions();
		}
	}

	public void ResetRemainingActions() {
		foreach (Unit u in units) {
			int remainingActions = u.GetRemainingActions();
			u.SetActionCount(remainingActions == BASE_UNIT_ACTIONS ? BASE_UNIT_ACTIONS + 1 : BASE_UNIT_ACTIONS);
		}
	}

	public bool AreActionsComplete() {
		bool allComplete = true;
		foreach (Unit u in units) {
			if (!u.AreActionsComplete()) allComplete = false;
		}
		return allComplete;
	}

}
