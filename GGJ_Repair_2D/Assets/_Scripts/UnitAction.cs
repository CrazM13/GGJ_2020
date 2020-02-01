using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAction {
	
	public enum ActionDirection { UP, DOWN, LEFT, RIGHT, NONE }
	private ActionDirection direction;

	private Vector2 target;
	public enum ActionType { MOVE, FIX }
	private ActionType type;

	public UnitAction(ActionType type, Vector2 target, ActionDirection direction) {
		this.direction = direction;
		this.target = target;
		this.type = type;
	}

	public ActionType GetActionType() {
		return type;
	}

	public ActionDirection GetActionDirection() {
		return direction;
	}

	public Vector2 GetActionTarget() {
		return target;
	}

}

