using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	private const float ACTION_TIME = 0.5f;
	private const float BASE_FIX_CHANCE = 0.33f;

	public int unitNumber = 0;

	private List<UnitAction> actions;

	private int maxActions = 3;

	private bool performingActions = false;

	private float actionTimer = 0;

	private Vector2 startPosition = Vector2.zero;

	void Start() {
		startPosition = transform.position;
	}

	void Update() {
		if (performingActions) {
			if (actions.Count > 0) {
				
				actionTimer += Time.deltaTime;

				switch(actions[0].GetActionType()) {
					case UnitAction.ActionType.FIX:

						break;
					case UnitAction.ActionType.MOVE:
						transform.position = Vector2.Lerp(startPosition, actions[0].GetActionTarget(), actionTimer / ACTION_TIME);
						break;
				}

				if (actionTimer >= ACTION_TIME) {
					actionTimer = 0;
					actions.RemoveAt(0);
				}

			} else {
				performingActions = false;
			}
		}
	}

	public void AddAction(UnitAction action) {
		if (actions.Count >= maxActions) return;
		// SET UI

		actions.Add(action);
	}

	public void RemoveAction(int actionIndex) {
		do {
			actions.RemoveAt(actions.Count - 1);
			actionIndex++;
		} while (actionIndex < maxActions);
	}

	public void ExecuteActions() {
		performingActions = true;
	}

	public void SetActionCount(int maxActions) {
		this.maxActions = maxActions;
	}

	public float GetFixChance(Vector2 target) {
		// CHECK TYPE
		int level = 0;// GET CHANCE FROM STATIC STATS
		return BASE_FIX_CHANCE + ((float)level * 2 / 100f);
	}

}
