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
        switch( action.GetActionType())
        {
            case UnitAction.ActionType.FIX:
                FindObjectOfType<GameUI>()?.AddHeroHUDRepair(unitNumber);
                SoundSystem.Instance.PlayHeroConfirmSound(unitNumber);
                break;
            case UnitAction.ActionType.MOVE:
                FindObjectOfType<GameUI>()?.AddHeroHUDMove(unitNumber);
                SoundSystem.Instance.PlayHeroMoveSound(unitNumber);
                break;
        }

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
		if (Input.GetKey(KeyCode.F)) return 1000f;
		return BASE_FIX_CHANCE + ((float)level * 2 / 100f) + ((float)remainingActions * 2 / 100f);
	}

	public int GetRemainingActions() {
		return remainingActions;
	}

	public bool AreActionsComplete() {
		return actions.Count == 0;
	}

	public void Kill() {

        // Sounds
        switch(unitNumber)
        {
            case 1:
                SoundSystem.Instance.PlaySound(SoundEvents.H1Death);
                break;
            case 2:
                SoundSystem.Instance.PlaySound(SoundEvents.H2Death);
                break;
            case 3:
                SoundSystem.Instance.PlaySound(SoundEvents.H3Death);
                break;
            case 4:
                SoundSystem.Instance.PlaySound(SoundEvents.H4Death);
                break;
        }

		// TMP
		gameObject.SetActive(false);
	}

	public void RunFixAction() {

		Vector2 fixLocation;

		if (actions.Count > 0) {
			if (actions[0].GetActionType() == UnitAction.ActionType.FIX) {
				fixLocation = actions[0].GetActionTarget();
				if (Random.value < GetFixChance(fixLocation))
                {
					SkillStorage.AddUpgradePoint();
					TileManager.Instance.ClearDisaster(actions[0].GetActionTarget());
				}
                else
                {
                    SoundSystem.Instance.PlaySound(SoundEvents.DisasterRepairFailure);
                }
				actions.RemoveAt(0);
			}
		}
	}

	public void SetPosition(Vector2 position) {
		transform.position = position;
		startPosition = position;
	}

}
