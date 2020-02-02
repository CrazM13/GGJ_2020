﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IssueCommandState : ITurnState {

	private int selectedUnit = -1;

	private Vector3 mousePosition;
	private Vector3 selectedTile;

	public bool DidWin() {
		return false;
	}

	public bool IsDone() {
		return false;
	}

	public void Start() {
		UnitManager.instance.ResetRemainingActions();
	}

	public void Update() {
		if (selectedUnit <= 0 || selectedUnit > 4) {
			HighlightOptions();
		}

		if (Input.GetMouseButtonDown(0)) {
			OnClick();
		}

	}

	private void HighlightOptions() {
		TileManager.Instance.ToggleAdjacentHighlight(selectedTile, true);
	}

	private void OnClick() {
		mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		if (selectedUnit <= 0) {
			SelectUnit();
		} else {
			AttemptAction();
		}
	}

	private void SelectUnit() {
		selectedUnit = TileManager.Instance.GetUnitOccupyingCell(mousePosition);
		Debug.Log(selectedUnit);
		selectedTile = mousePosition;
	}

	private void AttemptAction() {
		WorldTile tile = TileManager.Instance.GetWorldTileAtPosition(selectedTile);

		WorldTile selected = TileManager.Instance.GetWorldTileAtPosition(mousePosition);

		foreach (WorldTile.TileDirections d in tile.adjacentTiles.Keys) {
			WorldTile t = tile.adjacentTiles[d];
			if (selected == t) {
				
				if (selected.occupiedByUnit == -1) {
					SendAction(selected, d);
				}
			}
		}

	}

	private void SendAction(WorldTile tile, WorldTile.TileDirections direction) {
		UnitAction action;
		
		if (tile.type == DisasterTypes.Count) {
			action = new UnitAction(UnitAction.ActionType.MOVE, TileManager.Instance.GetWorldCoordsFromCellPosition(tile.cellLocation), WorldTileDirectionToActionDirection(direction));
		} else {
			action = new UnitAction(UnitAction.ActionType.FIX, TileManager.Instance.GetWorldCoordsFromCellPosition(tile.cellLocation), UnitAction.ActionDirection.NONE);
		}

		UnitManager.instance.AddAction(selectedUnit, action);
	}

	private UnitAction.ActionDirection WorldTileDirectionToActionDirection(WorldTile.TileDirections directions) {
		switch (directions) {
			case WorldTile.TileDirections.Above:
				return UnitAction.ActionDirection.UP;
			case WorldTile.TileDirections.Below:
				return UnitAction.ActionDirection.DOWN;
			case WorldTile.TileDirections.Left:
				return UnitAction.ActionDirection.LEFT;
			case WorldTile.TileDirections.Right:
				return UnitAction.ActionDirection.RIGHT;
			default:
				return UnitAction.ActionDirection.NONE;
		}
	}

}
