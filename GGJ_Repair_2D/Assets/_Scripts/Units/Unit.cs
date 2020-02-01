using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	private const float ACTION_TIME = 0.5f;
	private const float BASE_FIX_CHANCE = 0.33f;

	public int unitNumber = 0;

	private List<UnitAction> actions = new List<UnitAction>();

	private int remainingActions = 3;

	private bool performingActions = false;

	private float actionTimer = 0;

	private Vector2 startPosition = Vector2.zero;

	void Start() {
		startPosition = transform.position;
	}

	void Update() {

		if (Input.GetMouseButtonDown(0)) {
			TileManager.Instance.ClearDisaster(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			AddAction(new UnitAction(UnitAction.ActionType.MOVE, Camera.main.ScreenToWorldPoint(Input.mousePosition), UnitAction.ActionDirection.NONE));
		}

		if (Input.GetMouseButtonDown(1)) {
			AddAction(new UnitAction(UnitAction.ActionType.FIX, Camera.main.ScreenToWorldPoint(Input.mousePosition), UnitAction.ActionDirection.NONE));
		}

		if (actions.Count > 0) {
				
			actionTimer += Time.deltaTime;

			switch(actions[0].GetActionType()) {
				case UnitAction.ActionType.FIX:
					if (actionTimer >= ACTION_TIME) {
						TileManager.Instance.ClearDisaster(actions[0].GetActionTarget());
					}
					break;
				case UnitAction.ActionType.MOVE:
					transform.position = Vector2.Lerp(startPosition, actions[0].GetActionTarget(), actionTimer / ACTION_TIME);
					break;
			}

			if (actionTimer >= ACTION_TIME) {
				startPosition = transform.position;
				actionTimer = 0;
				actions.RemoveAt(0);
			}

		}
	}

	public void AddAction(UnitAction action) {
		if (remainingActions <= 0) return;
		remainingActions--;
		// SET UI

		actions.Add(action);
	}

	public void RemoveAction(int actionIndex) {
		do {
			actions.RemoveAt(actions.Count - 1);
			actionIndex++;
		} while (actionIndex < remainingActions);
	}

	public void ExecuteActions() {
		performingActions = true;
	}

	public void SetActionCount(int maxActions) {
		this.remainingActions = maxActions;
	}

	public float GetFixChance(Vector2 target) {
		// CHECK TYPE
		int level = 0;// GET CHANCE FROM STATIC STATS
		return BASE_FIX_CHANCE + ((float)level * 2 / 100f) + ((float)remainingActions * 2 / 100f);
	}

}
