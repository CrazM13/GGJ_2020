using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	private const float ACTION_TIME = 0.5f;
	private const float BASE_FIX_CHANCE = 0.33f;

	public int unitNumber = 0;

	private List<UnitAction> actions = new List<UnitAction>();

	private int remainingActions = 4;

	private bool performingActions = false;

	private float actionTimer = 0;

	private Vector2 startPosition = Vector2.zero;

	void Start() {
		startPosition = transform.position;
	}

	void Update() {

		if (actions.Count > 0) {

			if (actions[0].GetActionType() == UnitAction.ActionType.MOVE) {
				actionTimer += Time.deltaTime;
				transform.position = Vector2.Lerp(startPosition, actions[0].GetActionTarget(), actionTimer / ACTION_TIME);
			}

			if (actionTimer >= ACTION_TIME) {
				startPosition = transform.position;
				TileManager.Instance.OnUnitMovedToTile(transform.position, unitNumber);
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

	public void ExecuteActions() {
		performingActions = true;
	}

	public void SetActionCount(int maxActions) {
		this.remainingActions = maxActions;
	}

	public float GetFixChance(Vector2 target) {
		DisasterTypes type = TileManager.Instance.GetTileDisasterType(target);

		int level = SkillStorage.GetLevel(unitNumber, SkillStorage.StatFromDisaster(type));
		return BASE_FIX_CHANCE + ((float)level * 2 / 100f) + ((float)remainingActions * 2 / 100f);
	}

	public int GetRemainingActions() {
		return remainingActions;
	}

	public bool AreActionsComplete() {
		return actions.Count == 0;
	}

	public void Kill() {
		// TMP
		gameObject.SetActive(false);
	}

	public void RunFixAction() {

		Vector2? fixLocation = null;

		if (actions.Count > 0) {
			if (actions[0].GetActionType() == UnitAction.ActionType.FIX) {
				fixLocation = actions[0].GetActionTarget();
			}
		}

		if (fixLocation.HasValue) {
			if (Random.value < GetFixChance(fixLocation.Value)) {
				SkillStorage.AddUpgradePoint();
				TileManager.Instance.ClearDisaster(actions[0].GetActionTarget());
			}
		}
	}

	public void SetPosition(Vector2 position) {
		transform.position = position;
		startPosition = position;
	}

}
